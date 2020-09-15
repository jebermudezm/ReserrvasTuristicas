using RSI.Mvc.Web.Controllers.Helper;
using RSI.Mvc.Web.ViewModel;
using System.Web;
using System.Web.Mvc;

namespace RSI.Mvc.Web.Controllers
{
    public class HomeController : dbController {

		//[AuthorizeBui(Roles="1")]
		public ActionResult Index() {
            var usuarioLogueado = ObtenerUsuarioLogueado();
            if (usuarioLogueado == null)
                return RedirectToAction("Login", "SegUsuario");
            return View();
        }
		//------------------------------
		// This action handles the form POST and the upload
		[HttpPost]
		public ActionResult Index(HttpPostedFileBase file) {
			return RedirectToAction("Contact");
		}
		//------------------------------

		[AuthorizeRsi(Roles = "11")]
		public ActionResult About() {
			ModuleName = "Acerca de...";
			return View();
		}

		public ActionResult Contact() {
			ModuleName = "Contacto";
			return View();
		}

		public ActionResult Help() {
			ModuleName = "Ayuda";
			return View();
		}

		public ActionResult Handbook() {
			ModuleName = "Manuales";
			return View();
		}

	}
}