using System.Web;
using System.Web.Optimization;

namespace RSI.Mvc.Web
{
	public class BundleConfig {
		// Para obtener más información sobre las uniones, consulte http://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles) {
			setLib(bundles);
			/* ---> JavaScript<---*/
			bundles.Add(new ScriptBundle("~/Propios").Include("~/Scripts/Inicio.js"));
         


            /* ---> Hojas de Estilo <---*/
            bundles.Add(new StyleBundle("~/Css").Include("~/Content/Sitio.css"));
			//BundleTable.EnableOptimizations = true;
		}

		private static void setLib(BundleCollection bundles) {
			/* ---> JavaScript<---*/
			bundles.Add(new ScriptBundle("~/jquery").Include("~/Scripts/Lib/jquery-1.12.3.min.js"));

			bundles.Add(new ScriptBundle("~/KendoJava").Include("~/Scripts/Lib/kendo.all.min.js", "~/Scripts/Lib/kendo.aspnetmvc.min.js"));

			bundles.Add(new ScriptBundle("~/LibJava").Include("~/Scripts/Lib/jquery.nicescroll.min.js"
															, "~/Scripts/Lib/jszip.min.js"
															, "~/Scripts/Lib/chosen.jquery.min.js"
															, "~/Scripts/Lib/ion.rangeSlider.min.js"
															));

			bundles.Add(new ScriptBundle("~/Bootstrap").Include("~/Scripts/Lib/bootstrap.min.js", "~/Scripts/Lib/bootstrap-switch.min.js"));

			bundles.Add(new ScriptBundle("~/Apps").Include("~/Scripts/Lib/apps.js"));

			/* ---> Hojas de Estilo <---*/
			bundles.Add(new StyleBundle("~/KendoCss").Include("~/Content/Lib/kendo.common.min.css"
															, "~/Content/Lib/kendo.bootstrap.min.css"
															, "~/Content/Lib/kendo.bootstrap.mobile.min.css"));

			bundles.Add(new StyleBundle("~/StylesApp").Include("~/Content/Lib/layout.css"
															, "~/Content/Lib/animate.css"
															, "~/Content/Lib/bootstrap.min.css"
															, "~/Content/Lib/components.css"
															, "~/Content/Lib/theme.css"
															, "~/Content/Lib/font-awesome.min.css"
															, "~/Content/Lib/simple-line-icons.css"
															, "~/Content/Lib/Site.css"
															, "~/Content/Lib/sign.css"
															, "~/Content/Lib/default.css"
															, "~/Content/Lib/bootstrap-switch.min.css"
															, "~/Content/Lib/chosen.min.css"));

		}
	}
}
