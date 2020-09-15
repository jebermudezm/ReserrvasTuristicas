using FrameWork.MenuControl;
using FrameworkNet.Rsi;
using RSI.Mvc.Web.Controllers.Helper;
using RSI.Mvc.Web.ViewModel;
using System;
using System.Linq;
using System.Web.Mvc;

namespace RSI.Mvc.Web.Controllers
{
    public class InfoTemplate
    {
        public string TitleAppWeb { get; set; }
        public string ApplicationName { get; set; }
        public string ApplicationDescription { get; set; }
        public string CdnSrcEnv { get; set; }
        public string EnvironmentName { get; set; }
        public string UserIDoc { get; set; }
        public string UserFullName { get; set; }
        public string UserLName { get; set; }
        public string UserCharge { get; set; }
    }

    public class TemplatesController : dbController
    {

        public ActionResult MainMenu()
        {
            var user = ObtenerUsuarioLogueado();

            MenuBar SideBar = new MenuBar(Request, null, null);

            SideBar.Items(m =>
            {
                m.AddItem().Text("Home").ClassIcon("fa fa-home").Action("Home", "Index");
                if (user != null)
                {
                    m.AddSeparator().Text("Zona de registro").ClassIcon("fa fa-trophy");
                    m.AddMenu().Text("Registro").ClassIcon("fa fa-pencil fa-fw")
                        .Items(i =>
                        {
                            i.AddItem().Text("Concepto").Action("Concepto", "Index").Roles("1");
                            i.AddItem().Text("Clientes").Action("Cliente", "Index").Roles("1");
                            i.AddItem().Text("Productos").Action("Producto", "Index").Roles("1");
                            i.AddItem().Text("Proveedores").Action("Proveedor", "Index").Roles("1");
                            i.AddItem().Text("Destinos").Action("Destino", "Index").Roles("1");
                            i.AddItem().Text("Planes base").Action("Plan", "Index").Roles("1");
                            i.AddItem().Text("Convenios").Action("Convenio", "Index").Roles("1");
                            i.AddItem().Text("Planes Turisticos").Action("PlanTuristico", "Index").Roles("1");
                            i.AddItem().Text("Acomodación Por Plan").Action("PlanFecha", "Index").Roles("1");
                            
                        });
                    m.AddSeparator().Text("zona transaccional").ClassIcon("fa fa-trophy");
                    m.AddMenu().Text("Movimientos").ClassIcon("fa fa-pencil fa-fw")
                        .Items(i =>
                        {
                            i.AddItem().Text("Reservas").Action("Reserva", "Index").Roles("1");
                            i.AddItem().Text("Facturas").Action("Factura", "Index").Roles("1");
                            //i.AddItem().Text("Cartera").Action("Cartera", "Index").Roles("1");
                            i.AddItem().Text("Pagos").Action("Pago", "Index").Roles("1");
                        });
                    m.AddSeparator().Text("zona reportes").ClassIcon("fa fa-trophy");
                    m.AddMenu().Text("Reportes").ClassIcon("fa fa-file")
                       .Items(i =>
                       {
                           i.AddItem().Text("Reporte plan turístico").Action("ReportePlanTuristico", "ReportePlanes").Roles("1");
                           i.AddItem().Text("Reporte Ventas").Action("x", "Index").Roles("1");
                           i.AddItem().Text("Reporte Abonos").Action("x", "Index").Roles("1");
                           //i.AddItem().Text("Reporte 4").Action("x", "Index").Roles("1");
                           //m.AddSeparator().Text("Conexiones").ClassIcon("fa fa-database");
                       });
                    //m.AddMenu().Text("Base de Datos").ClassIcon("fa fa-database").Roles("1")
                    //    .Items(i =>
                    //    {
                    //        i.AddItem().Text("Crear conexiones").Link("~/DataBases/Connections").Roles("1");
                    //    });
                    //m.AddMenu().Text("Base de Datos").ClassIcon("fa fa-database").Roles("1")
                    //    .Items(i =>
                    //    {
                    //        i.AddItem().Text("Crear conexiones").Link("~/DataBases/Connections").Roles("1");
                    //    });
                }
            });
            return PartialView(SideBar.ListItems);
        }

        public ActionResult Initialize()
        {
            var app = new Aplicacion
            {
                Codigo = 1,
                Nombre = "Sistema de reserva turisticas",
                Descripcion = "Sistema para el manejo de reservas turisticas",
                Direccion = "",
                Habilitada = true,
                Icono = "",
                Priori = 1,
                Version = "1.1.1"
            };
            var user = MvcApplication.GetUsuario;
            MvcApplication.Ambiente = new FrameworkNet.Ambientes.Ambiente("1", "Dev", "Colombia", "CO");
            var env = MvcApplication.Ambiente;

            var Info = new InfoTemplate
            {
                TitleAppWeb = app.Nombre + " v. " + app.Version
                ,
                ApplicationName = app.Nombre
                ,
                ApplicationDescription = app.Descripcion
                ,
                CdnSrcEnv = "RSI.Mvc.Web"
                ,
                EnvironmentName = "Producción"
                ,
                UserIDoc = user != null ? user.leg_numdoc : ""
                ,
                UserFullName = user != null ? user.usuario_nombre : ""
                ,
                UserLName = user != null ? user.usuario_nombre.Substring(0, user.usuario_nombre.IndexOf(" ")).Replace(".", "") : ""
                ,
                UserCharge = user != null ? user.usuario_cargo : ""
            };

            if (env.SufijoAmbiente == "_DS") { TempData["CdnSrcEnv"] = "desa-cdn.rsi"; TempData["EnvironmentName"] = "Desarrollo"; }
            if (env.SufijoAmbiente == "_TS") { TempData["CdnSrcEnv"] = "test-cdn.andes.aes"; TempData["EnvironmentName"] = "Prueba"; }
            Session["InfoTemplate"] = Info;
            return Content("");
        }

        public ActionResult Head()
        {
            return PartialView(Session["InfoTemplate"] as InfoTemplate);
        }

        public ActionResult Header()
        {
            return PartialView(Session["InfoTemplate"] as InfoTemplate);
        }

        public ActionResult Profiles()
        {
            ModuleName = "Perfil de Usuario";
            return PartialView(Session["InfoTemplate"] as InfoTemplate);
        }

        public ActionResult Error(int CodeError, string DataError)
        {
            //ErrorConfig DataErrorRSI = (Session["DataErrorRSI"] as ErrorConfig);

            ViewBag.Amb = "success"; ViewBag.App = "success"; ViewBag.UserAD = "success";
            ViewBag.TitleError = "ERROR DE ACCESO";
            if (TempData["CdnSrcEnv"] == null) { TempData["CdnSrcEnv"] = "desa-cdn.andes.aes"; }
            //if (DataErrorRSI == null) { DataErrorRSI = new ErrorConfig(); }
            //if (DataErrorRSI.UserAccount == null) {
            //	string[] DataUser = User.Identity.Name.Split(new Char[] { '\\' });
            //	//DataErrorRSI.Domain = DataUser[0];
            //	DataErrorRSI.UserAccount = DataUser[1];
            //}
            switch (CodeError)
            {
                case 0: //No existe la Aplicación
                    ViewBag.App = "danger";
                    break;
                case 1:
                    ViewBag.App = "danger"; ViewBag.UserAD = "danger";
                    break;
                case 4: // Usuario esta deshabilitado
                    ViewBag.Amb = "danger"; ViewBag.App = "danger"; ViewBag.UserAD = "danger";
                    break;
                case 5: // Usuario no tiene permisos
                    ViewBag.App = "danger"; ViewBag.UserAD = "danger";
                    break;
                case 6: // Usuario inexistente
                    ViewBag.UserAD = "danger";
                    break;
                case 404: // Página inexistente
                    ViewBag.Amb = "danger"; ViewBag.App = "danger"; ViewBag.UserAD = "danger";
                    ViewBag.TitleError = "Ubicación Errónea";
                    //DataErrorRSI.Message = "Esta intentando ingresar a una <b>Ubicación</b> que NO existe en la Aplicación Web.";
                    //DataErrorRSI.Message += "<br/>Revice la URL si es que la escribió mal o el módulo puede estar deshabilito.";
                    break;
                case 500: // Excepción
                    ViewBag.Amb = "danger"; ViewBag.App = "danger"; ViewBag.UserAD = "danger";
                    ViewBag.TitleError = "Error interno";
                    break;
            }
            //return View(DataErrorRSI);
            return null;
        }
    }
}
