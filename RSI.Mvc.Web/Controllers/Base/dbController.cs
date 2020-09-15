using Kendo.Mvc.UI;
using Newtonsoft.Json;
using RSI.Modelo.RepositorioImpl;
using RSI.Mvc.Web.Controllers.Helper;
using RSI.Mvc.Web.ViewModel;
using System;
using System.Data.SqlClient;
using System.Text;
using System.Web.Mvc;

namespace RSI.Mvc.Web.Controllers
{

    public partial class dbController : Controller
    {

        public RSIModelContextDB _context;
        public EntitiesHelper _helperMap;
        public EntitiesHelper _helperCache;

        public dbController()
        {
            _context = new RSIModelContextDB(System.Configuration.ConfigurationManager.ConnectionStrings["MS_TableConnectionString"].ConnectionString);
            _helperMap = new EntitiesHelper();
            //db = new ConsolaOperacionesContextDB(GetConnection("ConsolaOperaciones"));
        }


        public string ModuleName {
			set { TempData["ModuleName"] = value; }
		}


		private SqlConnection GetConnection(string Name)
        {
            var sqlConection = new SqlConnection(Name);
			return sqlConection;
		}

		protected override void OnException(ExceptionContext filterContext) {
			if (filterContext.ExceptionHandled) return;

		//	string[] DataUser = MvcApplication.GetUsuario().usuario_ad_name.Split(new Char[] { '\\' });

		//	//ErrorConfig DataErrorRSI = new ErrorConfig() {
		//	//	ID = 500,
		//	//	ApplicationName = MvcApplication.Aplicacion.Nombre,
		//	//	Environment = MvcApplication.Ambiente.Tipo,
		//	//	//Domain = DataUser[0],
		//	//	UserAccount = DataUser[1]
		//	//};

		//	DataErrorRSI.Message = filterContext.Exception.Message + "<br/><br/><h5>" + filterContext.Exception.StackTrace.Replace("\r\n", "<br/>") + "</h5>";
		//	filterContext.HttpContext.Session["DataErrorRSI"] = DataErrorRSI;
		//	filterContext.Result = RedirectToAction("Error", "Templates", new { CodeError = DataErrorRSI.ID });

			base.OnException(filterContext);
		}

        protected ContentResult ConstruirResultado(DataSourceResult resultado)
        {
            return Content(JsonConvert.SerializeObject(resultado, Formatting.None, new JsonSerializerSettings { DateFormatString = "yyyy-MM-ddTHH:mm:ss" }), "application/json");
        }

        public ActionResult MyJsonResult(string mensaje)
        {
            return new JsonResult
            {
                Data = new { Success = false, ErrorMessage = mensaje },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        protected string DesEncriptar(string textoEncriptado)
        {
            if (textoEncriptado == null)
                return string.Empty;

            if (textoEncriptado.Trim() == String.Empty)
                return string.Empty;

            byte[] vector = Convert.FromBase64String(textoEncriptado);
            UTF8Encoding auxEncodig = new UTF8Encoding();
            return auxEncodig.GetString(vector);
        }

        protected string Encriptar(string textoAEncriptar)
        {
            if (textoAEncriptar == null)
                return string.Empty;

            if (textoAEncriptar.Trim() == String.Empty)
                return string.Empty;

            UTF8Encoding auxEncodig = new UTF8Encoding();
            byte[] vector = auxEncodig.GetBytes(textoAEncriptar);
            return Convert.ToBase64String(vector);
        }

        public string GetAllExeption(Exception ex)
        {
            if (ex == null) return "";
            return ex.Message + "<br/>" + GetAllExeption(ex.InnerException);
        }
        public UsuarioViewModel ObtenerUsuarioLogueado()
        {
            var user = Session["USER"] as UsuarioViewModel;
            return user;
        }

        public DateTime ValidarFecha(string fecha)
        {
            try
            {
                var fechaRetorno = DateTime.Today;
                if (!string.IsNullOrEmpty(fecha))
                {
                    fechaRetorno = DateTime.Parse(fecha);
                }
                return fechaRetorno;
            }
            catch (Exception)
            {

                return DateTime.Today;
            }
            
        }
    }
}
