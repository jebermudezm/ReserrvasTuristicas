using System.ServiceProcess;

namespace Replicate.Service
{
    static class Program
    {
        //Log4NetTracer trace;
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        static void Main()
        {

#if DEBUG
            Process p = new Process();
            p.ProcesoEjecucion();
#else

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Service1()
            };
            ServiceBase.Run(ServicesToRun);
#endif 
        }
    }
}
