using Replicate.TraceManager;
using System;
using System.Configuration;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;
using System.Timers;

namespace Replicate.Service
{
    public partial class Service1 : ServiceBase
    {
        System.Timers.Timer timer = new System.Timers.Timer();
        Log4NetTracer trace;
        Process p;
        ThreadStart ts = null;
        Thread h = null;
        string frecuecy;
        public Service1()
        {
            this.trace = new Log4NetTracer();
            this.trace.TraceInfo("Begin Initialice Components.");
            InitializeComponent();
            this.trace.TraceInfo("Begin Instance process.");
            p = new Process();
            this.trace.TraceInfo("End Instance process.");
            frecuecy = ConfigurationManager.AppSettings["FrequencyInMinutes"].ToString();
        }
        
        
        protected override void OnStart(string[] args)
        {
            try
            {
                this.trace.TraceInfo("Start Service.");
                p.RestartService("false");
                timer.Elapsed += new System.Timers.ElapsedEventHandler(OnElapsedTime);
                timer.Interval = int.Parse(frecuecy) * 1000 * 60;
                timer.Enabled = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("Replicate.Service", "Error: " + ex.Message + "\n" + ex.StackTrace, EventLogEntryType.Error);
            }
        }
        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            this.trace.TraceInfo("Start Service.");
            ts = new ThreadStart(p.ProcesoEjecucion);
            h = new Thread(ts);
            h.Start();
        }

        protected override void OnStop()
        {
            try
            {
                this.trace.TraceInfo("Stop Service.");
                h.Abort();
                p.DetenerEjecucion();
                EventLog.WriteEntry("Replicate.Servicio", "The execution of the replicate service to the operating console was stopped.", EventLogEntryType.Error);
            }
            catch (Exception e)
            {
                p.DetenerEjecucion();
                PSException ps = new PSException(1, "Error Service is stop.", e);
                this.trace.TraceError(ps);
                EventLog.WriteEntry("Replicate.Servicio", "Error: " + e.Message + "\n" + e.StackTrace, EventLogEntryType.Error);
            }
        }
    }
}
