using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using RSI.Modelo.Entidades.Maestros;
using RSI.Modelo.Entidades.Movimientos;
using RSI.Modelo.RepositorioCont;
using RSI.Modelo.RepositorioImpl;
using RSI.Mvc.Web.Controllers.Helper;
using RSI.Mvc.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace RSI.Mvc.Web.Controllers
{
    public class ReservaPagoController : dbController
    {
        #region Variables
        private RSIModelContextDB db = new RSIModelContextDB();
        private readonly IPlanRepositorio _plan;
        private readonly IReservaRepositorio _reserva;
        private readonly IClienteRepositorio _cliente;
        private readonly IPagoRepositorio _pago;

        #endregion
        #region Constructor
        public ReservaPagoController()
        {
            _plan = new PlanRepositorio(_context);
            _reserva = new ReservaRepositorio(_context);
            _cliente = new ClienteRepositorio(_context);
            _pago = new PagoRepositorio(_context);

        }
        #endregion

        // GET: Plan
        public ActionResult Index()
        {
            try
            {
                var user = ObtenerUsuarioLogueado();
                if (user == null)
                    return RedirectToAction("Login", "SegUsuario");
                return View();
            }
            catch (Exception ex)
            {
                return MyJsonResult(GetAllExeption(ex));
            }
        }
        public ActionResult Index_Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                var user = ObtenerUsuarioLogueado();
                if (user == null)
                    return RedirectToAction("Login", "SegUsuario");
                var listaViewModel = ObtenerClientesReservasNoPagadasTotalMente();
                return ConstruirResultado(listaViewModel.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                return MyJsonResult($"se presentó el siguiente error: {ex.Message}");
            }
        }

        private List<PagoReservaViewModel> ObtenerClientesReservasNoPagadasTotalMente()
        {
        //    var reserva = _reserva.ObtenerQueryable().Where(x => x.ValorPagado == "No" || x.ValorPagado == null).ToList();
        //    var listaClientes = (from cli in db.Clientes
        //                    join re in reserva on cli.Id equals re.ClienteId
        //                    select new { clienteId = cli.Id, NumeroDocumento = cli.NumeroDocumentoIdentidad, Nombre = cli.NombreORazonSocial, re.Id, re.PlanTuristico.Descripcion, re.ValorTotal }).ToList();
            var lista = new List<PagoReservaViewModel>();
            //foreach (var item in listaClientes)
            //{
            //    lista.Add(new PagoReservaViewModel
            //    {
            //        //Id = item.Id,
            //        ClienteId = item.clienteId,
            //        NumeroDocumento = item.NumeroDocumento,
            //        Nombre = item.Nombre,
            //        Reserva = item.Descripcion,
            //        Valor = item.ValorTotal
            //    });
            //}
            return lista;
        }
        
        #region Pagos

        [AllowJsonGet]
        public ActionResult Read_Pagos([DataSourceRequest] DataSourceRequest request, int reservaId)
        {
            try
            {
                var pagos = _pago.ObtenerQueryable().Where(x => x.ReservaId == reservaId).ToList();
                var model = new List<PagosReservaViewModel>();
                foreach (var item in pagos)
                {
                    //model.Add(_helperMap.MapPagoViewModel(item));
                }
                return ConstruirResultado(model.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                return MyJsonResult(GetAllExeption(ex));
            }
        }

        // GET: DeltaOperacion/Create
        public ActionResult _CreatePagosGrid(int? reservaId)
        {
            try
            {
                var viewModel = new PagosReservaViewModel();

                var id = reservaId == null ? 0 : reservaId.Value;
                if (id != 0)
                {
                    var entidad = _pago.Obtener(id);
                    //viewModel = _helperMap.MapPagoViewModel(entidad);
                }
                else
                {
                    return MyJsonResult("Los pagos deben ir asociados a una reserva.");
                }

                return PartialView(viewModel);
            }
            catch (Exception ex)
            {
                return MyJsonResult(GetAllExeption(ex));
            }
        }

        // POST: DeltaOperacion/Create
        [HttpPost]
        public ActionResult _CreatePagosGrid(PagoReservaViewModel reservaViewmodel)
        {
            try
            {
                var usr = ObtenerUsuarioLogueado();
                var datosBD = _pago.ObtenerQueryable().Where(x => x.ReservaId == reservaViewmodel.Id).ToList();

                var listaPagos = (from bd in datosBD
                                                 join dp in reservaViewmodel.Pagos.ToList() on bd.Fecha equals dp.Fecha into detaJoin
                                                 from detPlan in detaJoin.DefaultIfEmpty()
                                                 select new { Detalle = bd, Registro = detPlan != null }).ToList();

                foreach (var item in reservaViewmodel.Pagos)
                {
                    var registroBD = datosBD?.FirstOrDefault(x => x.Fecha == item.Fecha);
                    if (registroBD == null)
                    {
                        var entidad = _helperMap.MapPagoModel(item);
                        entidad.FechaCreacion = DateTime.Now;
                        entidad.CreadoPor = usr.UserName;
                        var includId = _pago.Agregar(entidad);
                    }
                    else
                    {
                        registroBD.ModificadoPor = usr.UserName;
                        registroBD.FechaModificacion = DateTime.Now;
                        registroBD.Fecha = item.Fecha;
                        registroBD.ReservaId = item.ReservaId;
                        registroBD.Valor = item.Valor;
                        registroBD.Saldo = item.Saldo;

                        _pago.Actualizar(registroBD);
                    }
                }
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return MyJsonResult(GetAllExeption(ex));
            }
        }
        #endregion
    }
}