using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Replicate.TraceManager
{
    public class PSException : Exception
    {
        /// <summary>
        /// Id del mensaje de error.
        /// </summary>
        private int messageId;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="messageId">Id del mensaje de error.</param>
        /// <param name="message">Mensaje de error.</param>
        public PSException(int messageId, string message)
            : base(message)
        {
            this.messageId = messageId;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="messageId">Id del mensaje de error.</param>
        /// <param name="message">Mensaje de error.</param>
        /// <param name="innerException">Excepcion interna.</param>
        public PSException(int messageId, string message, Exception innerException)
            : base(message, innerException)
        {
            this.messageId = messageId;
        }

        /// <summary>
        /// Identificador del error.
        /// </summary>
        public int MessageId
        {
            get { return this.messageId; }
        }

        /// <summary>
        /// Retorna el mensaje completo para una excepcion.
        /// </summary>
        /// <param name="exception">Excepcion de la que se desea obtener el mensaje completo.</param>
        /// <returns>Mensaje generado.</returns>
        public static string CompleteMessage(Exception exception)
        {
            string message = exception.Message;
            Exception innerException = exception.InnerException;
            while (innerException != null)
            {
                message += ", " + innerException.Message;
                innerException = innerException.InnerException;
            }

            return message;
        }
    }
}
