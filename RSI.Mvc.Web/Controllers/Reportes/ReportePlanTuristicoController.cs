using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using RSI.Modelo.RepositorioCont;
using RSI.Modelo.RepositorioImpl;
using RSI.Mvc.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RSI.Mvc.Web.Controllers.Reportes
{
    public class ReportePlanTuristicoController : dbController
    {
        private readonly IPlanTuristicoRepositorio _planTuristicoRepositorio;
        #region Constructor

        public ReportePlanTuristicoController()
        {
            _planTuristicoRepositorio = new PlanTuristicoRepositorio(_context);
        }

        #endregion

        #region Vistas

        public ActionResult Index()
        {
            return View();
        }
        // GET: ReportePlanTuristico
        public ActionResult ReportePlanes(string fecha)
        {
            var model = ValidarFecha(fecha);
            return View(model);
        }
        public ActionResult Index_Read([DataSourceRequest] DataSourceRequest request, DateTime fecha)
        {
            //var fechaConsulta = ValidarFecha(fecha);
            var planes = _planTuristicoRepositorio.ObtenerQueryable().Where(x => x.FechaSalida >= fecha).ToList();
            var list = planes.Select(s => new ReportePlanTuristicoViewModel()
            {
                Codigo = s.Codigo,
                Descripcion = s.Descripcion,
                Destino = s.Destino.Descripcion,
                FechaRegreso = s.Fecharegreso,
                FechaSalida = s.FechaSalida,
                Hotel = s.Hotel,
                Id = s.Id,
                Proveedor = s.Proveedor.NombreORazonSocial,

            });

            return ConstruirResultado(list.ToDataSourceResult(request)); //return View(resultadoPlanesTuristicos);
        }

        #endregion
    }
}
