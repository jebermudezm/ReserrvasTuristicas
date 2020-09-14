using Renci.SshNet;
using Replicate.TraceManager;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Linq;

namespace Replicate.Business
{
    public class FileHandler
    {
        #region Variables
        protected Log4NetTracer trace;
        private int daysToConsult;
        private int port;
        private string userName;
        private string password;
        private string host;
        private string destinationPath;


        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public FileHandler()
        {
            try
            {
                this.trace = new Log4NetTracer();
                this.userName = ConfigurationManager.AppSettings["UserNameFTP"];
                this.password = ConfigurationManager.AppSettings["PasswordFTP"].Replace("{1}","&");
                this.daysToConsult = int.Parse(ConfigurationManager.AppSettings["DaysToConsult"]);
                this.port = int.Parse(ConfigurationManager.AppSettings["Port"]);
                this.host = ConfigurationManager.AppSettings["Host"];
                this.destinationPath = ConfigurationManager.AppSettings["DestinationPath"];
            }
            catch (Exception e)
            {
                PSException ps = new PSException(1, "Error en FileHandlerConstructor", e);
                this.trace.TraceError(ps);
            }
        }
#endregion

#region Metodos
        /// <summary>
        /// Metodo que ejecuta la tarea programada
        /// </summary>
        /// <param name="parametros">Parametros de configuración</param>
        public void Excecute()
        {
            try
            {
                this.trace.TraceInfo($"Begin FileHandler.Excecute.");
                var file = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PathsFiles.xml");
                var xmlPaths = XDocument.Load(file);
                var paths = xmlPaths.Descendants("Item") as IEnumerable<XElement>;
                foreach (var path in paths)
                {
                    var originPath = path.Attribute("Path").Value;
                    var dateWork = DateTime.Now.Date.AddDays(daysToConsult *(-1));
                    var today = DateTime.Now.Date;
                    var lastYear = 0;
                    var lastMont = 0;
                    while (dateWork <= today)
                    {
                        var year = dateWork.Year;
                        var month = dateWork.Month;
                        originPath = path.Attribute("Path").Value;
                        var validateTime = path.Attribute("ValidateTime").Value;
                        originPath = originPath.Replace("{YYYY}", year.ToString());
                        originPath = originPath.Replace("{MM}", string.Format("{0:00}", month));
                        if (year != lastYear || month != lastMont)
                        {
                            ExcecuteLoadFile(validateTime, originPath, year);
                            //LoadFilesWithWinSCP(validateTime, originPath, year);
                        }
                        lastYear = year;
                        lastMont = month;
                        dateWork = dateWork.AddDays(1);
                    }
                }
                this.trace.TraceInfo($"End FileHandler.Excecute.");
            }
            catch (Exception e)
            {
                PSException ps = new PSException(1, "Error en FileHandler.Excecute", e);
                this.trace.TraceError(ps);
            }
        }

        private void ExcecuteLoadFile(string validateTime, string originPath, int year)
        {
            var existsFiles = string.Empty;
            var filesLoad = string.Empty;
            try
            {
                this.trace.TraceInfo($"Begin FileHandler.ExcecuteLoadFile.");
                string[] dirs = System.IO.Directory.GetDirectories(originPath);
            
                DirectoryInfo info = new DirectoryInfo(originPath);

                DateTime fecha = DateTime.Now.Date.AddDays(-daysToConsult);
                var destinationPathFull = $"{destinationPath}/{year.ToString()}";
                var files = info.GetFiles().OrderByDescending(p => p.CreationTime).ToList();
                if (validateTime.ToLower() == "true")
                {
                    files = info.GetFiles().Where(x => x.CreationTime >= fecha || x.LastAccessTime >= fecha).OrderByDescending(p => p.CreationTime).ToList();
                }
                if (files.Count > 0)
                {
                    using (SftpClient client = new SftpClient(host, port, userName, password))
                    {
                        client.Connect();
                        if (!client.Exists(destinationPathFull))
                        {
                            client.CreateDirectory(destinationPathFull);
                        }
                        client.ChangeDirectory(destinationPathFull);
                        var fileList = client.ListDirectory(destinationPathFull).Select(s => s.FullName).ToList();
                        foreach (var item in files)
                        {
                            try
                            {
                                var exist = fileList.Any(x => x.Equals($"{destinationPathFull}/{item.Name}"));
                                if (!exist)
                                {
                                    using (FileStream fs = new FileStream(item.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                                    {
                                        client.BufferSize = 4 * 1024;
                                        client.UploadFile(fs, Path.GetFileName(item.FullName));
                                        filesLoad += $"{destinationPathFull}/{item.Name}, ";
                                    }
                                }
                                else
                                {
                                    existsFiles += $"{destinationPathFull}/{item.Name}, ";
                                }
                            }
                            catch (Exception e)
                            {
                                PSException ps = new PSException(1, "Error loading file", e);
                                this.trace.TraceError(ps);
                            }
                        }
                        if(existsFiles != string.Empty)
                            this.trace.TraceInfo($"The following files {existsFiles} already exists in the FSTP");
                        if (filesLoad != string.Empty)
                            this.trace.TraceInfo($"The following files {filesLoad} were load in the FSTP");
                    }
                }
                this.trace.TraceInfo($"End FileHandler.ExcecuteLoadFile.");
            }
            catch (Exception e)
            {
                PSException ps = new PSException(1, "Error ExcecuteLoadFile", e);
                this.trace.TraceError(ps);
            }
        }
#endregion
    }
}
