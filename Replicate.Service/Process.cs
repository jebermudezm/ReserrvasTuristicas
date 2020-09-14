
using Replicate.Business;
using Replicate.TraceManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Replicate.Service
{
    public class Process
    {
        string pathFile;
        FileHandler fileHandler;
        protected Log4NetTracer trace;
        public Process()
        {
            this.pathFile = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config.txt");
            this.trace = new Log4NetTracer();
            this.trace.TraceInfo("Start Instance to fileHandler.");
            fileHandler = new FileHandler();
            this.trace.TraceInfo("End Instance fileHandler.");
        }
        public void ProcesoEjecucion()
        {
            try
            {
                this.trace.TraceInfo("Begin Task.");
                if (ValidateExecuting())
                {
                    this.trace.TraceInfo("Begin Excecute.");
                    fileHandler.Excecute();
                    this.trace.TraceInfo("End Excecute.");
                    ModifyFile("false");
                }
                else
                {
                    this.trace.TraceInfo("Execution attempt failed because the process is running.");
                }
                this.trace.TraceInfo("End Task.");
            }
            catch (Exception e)
            {
                PSException ps = new PSException(1, "Error in ProcesoEjecucion", e);
                this.trace.TraceError(ps);
                ModifyFile("false");
            }
        }

        private bool ValidateExecuting()
        {
            var validation = false;
            this.trace.TraceInfo($"path file config.txt {pathFile}.");
            if (File.Exists(pathFile))
            {
                // Read all the content in one string 
                // and display the string 
                string str = File.ReadAllText(pathFile);
                if (str == "false")
                {
                    ModifyFile("true");
                    validation = true;
                }
            }
            else
            {
                this.trace.TraceInfo($"path file {pathFile} not exist.");
            }
            return validation;
        }

        public void RestartService(string value)
        {
            try
            {
                this.trace.TraceInfo("Begin Restart.");
                ModifyFile("false");
                this.trace.TraceInfo("End Restart.");
            }
            catch (Exception e)
            {
                PSException ps = new PSException(1, "Error in RestartService.", e);
                this.trace.TraceError(ps);
                ModifyFile("false");
            }
        }

        private void ModifyFile(string value)
        {
            TextWriter txt = new StreamWriter(pathFile);
            txt.Write(value);
            txt.Close();
            this.trace.TraceInfo($"File Config Modified in {value}.");
        }

        internal void DetenerEjecucion()
        {
            ModifyFile("false");
        }
    }
}
