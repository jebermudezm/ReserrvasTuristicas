using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Replicate.TraceManager
{
    public class Log4NetTracer : IDisposable
    {
        /// <summary>
        /// The log
        /// </summary>
        private static ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string CurrentSessionTrail => throw new NotImplementedException();

        /// <summary>
        /// Inicia el trace de una operacion logica.
        /// </summary>
        public void TraceStartLogicalOperation(string operationName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Detiene el trace de una operacion logica.
        /// </summary>
        public void TraceStopLogicalOperation()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Innicia un trace.
        /// </summary>
        public void TraceStart()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Detiene un trace.
        /// </summary>
        public void TraceStop()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Realiza un trace de tipo inforamacion.
        /// </summary>
        /// <param name="message">Mensaje a guardar.</param>
        /// <param name="subCategory">Subcategoria del mensaje.</param>
        public void TraceInfo(string message, string subCategory = "")
        {
            Log.Info(message);
        }

        /// <summary>
        /// Realiza el trace de un warning.
        /// </summary>
        /// <param name="message">Mensaje a guardar.</param>
        /// <param name="subCategory">Subcategoria del mensaje.</param>
        public void TraceWarning(string message, string subCategory = "")
        {
            Log.Warn(message);
        }

        /// <summary>
        /// Realiza el trace de un error.
        /// </summary>
        /// <param name="ex">Excepcion a guardar.</param>
        /// <param name="subCategory">Subcategoria de el mensaje.</param>
        public void TraceError(PSException ex, string subCategory = "")
        {
            Log.Error(ex.Message, ex);
        }

        /// <summary>
        /// Realiza el trace de u error critico.
        /// </summary>
        /// <param name="ex">Excepcion a guardar.</param>
        /// <param name="subCategory">Subcategoria del mensaje.</param>
        public void TraceCritical(PSException ex, string subCategory = "")
        {
            Log.Fatal(ex.Message, ex);
        }

        /// <summary>
        /// Limpia la memoria de ste objeto.
        /// </summary>
        public void Dispose()
        {
            throw new NotImplementedException();
        }


        public bool IsDebugEnabled()
        {
            return Log.IsDebugEnabled;
        }

        /// <summary>
        /// Traces the debug.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="subCategory">The sub category.</param>
        public void TraceDebug(string message, string subCategory = "")
        {
            Log.Debug(message);
        }

        /// <summary>
        /// Envia un trace de error al repositorio.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="ex">Exception caught.</param>
        public void TraceError(string message, Exception ex)
        {
            Log.Error(message, ex);
        }

        /// <summary>
        /// Envia un trace de error al repositorio.
        /// </summary>
        /// <param name="ex">Exception caught.</param>
        public void TraceError(Exception ex)
        {
            Log.Error(ex);
        }

        public void WriteLine(string message)
        {
            Log.Info(message);
        }

        public void WriteIncommingFrame(string frame)
        {
            Log.Debug(frame);
        }

        public void WriteOutgoingFrameFrame(string frame)
        {
            Log.Debug(frame);
        }

        public void WriteError(string message)
        {
            Log.Error(message);
        }

        public void CommitMessages()
        {
            throw new NotImplementedException();
        }

        public void BeginMessageToMemory()
        {
            throw new NotImplementedException();
        }
    }
}
