using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace RSI.Mvc.Web.Controllers.Helper
{
    /// <summary>
    /// Extension del api controller para el manejo de errores en la api
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class ErroresApiAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// Metodo para el control de errores en la visionamos api
        /// Construye un ExceptionErrorApi para retornar la excepción
        /// </summary>
        public override void OnException(HttpActionExecutedContext context)
        {

            var ultimoError = context.Exception.GetBaseException();
            string codigoError = string.Empty;

          

            //asigna como respuesta la respuesta formada por el errorApi
            context.Response = context.Request.CreateResponse(HttpStatusCode.InternalServerError);
        }
    }

}