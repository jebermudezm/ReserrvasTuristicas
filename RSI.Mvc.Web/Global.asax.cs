using AutoMapper;
using FrameworkNet.Ambientes;
using FrameworkNet.Rsi;
using RSI.Web.App_Start;
using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace RSI.Mvc.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public const string MultipleParameterKey = "_multiple_";

        protected void Application_Start()
        {
            Mapper.Initialize(c => c.AddProfile<MappingProfile>());
            AreaRegistration.RegisterAllAreas();
            //GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Session_Start(object sender, EventArgs e)
        {
        }

        public static Aplicacion GetAplicacion{ get; set; }
        public static Usuario GetUsuario { get; set; }
        public static Ambiente Ambiente { get; internal set; }
    }
}
