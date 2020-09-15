using System;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Web.Helpers;
using System.Linq;

namespace RSI.Mvc.Web.Controllers {

	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public class ValidateJsonAntiForgeryTokenAttribute : FilterAttribute, IAuthorizationFilter {
		public void OnAuthorization(AuthorizationContext filterContext) {
			if (filterContext == null) { throw new ArgumentNullException("filterContext"); }

			var httpContext = filterContext.HttpContext;
			var cookie = httpContext.Request.Cookies[AntiForgeryConfig.CookieName];
			AntiForgery.Validate(cookie != null ? cookie.Value : null, httpContext.Request.Headers["__RequestVerificationToken"]);
		}
	}

	public class AuthorizeRsi : AuthorizeAttribute {
		public int[] groups { get; set; }
		public string Groups {
			set {
				groups = Array.ConvertAll(value.Trim().Replace(" ", "").Split(','), s => int.Parse(s));
			}
		}

		public int[] roles { get; set; }
		public new string Roles {
			get { return ""; }
			set {
				roles = Array.ConvertAll(value.Trim().Replace(" ", "").Split(','), s => int.Parse(s));
			}
		}

		//private ErrorConfig error;

		protected override bool AuthorizeCore(HttpContextBase httpContext) {
			//var user = MvcApplication.GetUsuario();

			//if (user.Responsabilidades.Any(a => roles.Contains(a.Resp_Code))) return true;
			//if (user.Grupos.Any(a => groups.Contains(a.Group_Code))) return true;
			return false;
		}

		protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext) {
			//if (error == null) error = new ErrorConfig() { ID = 6, Message = "El usuario no tiene permisos para acceder en este módulo" };
			//filterContext.HttpContext.Session["DataErrorRSI"] = error;
			//filterContext.Result = new RedirectToRouteResult(
			//	new System.Web.Routing.RouteValueDictionary(new { controller = "Templates", action = "Error", CodeError = error.ID })
			//);
		}
	}
}
