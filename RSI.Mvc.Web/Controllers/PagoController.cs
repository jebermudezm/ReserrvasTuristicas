using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using RSI.Modelo.Entidades.Maestros;
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
    public class PagoController : dbController
    {
        #region Variables
        private readonly IClienteRepositorio _cliente;
        private readonly IReservaRepositorio _reserva;
        private readonly IPagoRepositorio _pago;

        #endregion
        #region Constructor
        public PagoController()
        {
            _cliente = new ClienteRepositorio(_context);
            _reserva = new ReservaRepositorio(_context);
            _pago = new PagoRepositorio(_context);
        }
        #endregion

        // GET: Cliente
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
                var model = _reserva.ObtenerQueryable()
                    .Where(x => x.ValorTotal > x.ValorPagado).ToList();
                var viewModel = new List<PagoReservaViewModel>();
                foreach (var item in model)
                {
                    viewModel.Add(new PagoReservaViewModel {
                        Id = item.Id,
                        Reserva = item.PlanTuristico.Descripcion,
                        ClienteId = item.ClienteId,
                        NumeroDocumento = item.Cliente.NumeroDocumentoIdentidad,
                        Nombre = item.Cliente.NombreORazonSocial,
                        Fecha = item.Fecha,
                        Valor = item.ValorTotal,
                        ValorPagado = item.ValorPagado
                    });
                }

                return ConstruirResultado(viewModel.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                return MyJsonResult($"se presentó el siguiente error: {ex.Message}");
            }
        }
        

        [AllowJsonGet]
        public ActionResult Read_Detalle([DataSourceRequest] DataSourceRequest request, int reservaId)
        {
            try
            {
                var model = obtenerPagosReserva(reservaId).Pagos.ToList();
                return ConstruirResultado(model.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                return MyJsonResult(GetAllExeption(ex));
            }
        }


        public ActionResult _Create(int reservaId)
        {
            try
            {
                var reserva = _reserva.ObtenerQueryable().Where(x => x.Id == reservaId).ToList();
                ViewBag.ReservaId = new SelectList(reserva, "Id", "Id");
                
                return PartialView();
            }
            catch (Exception ex)
            {
                return MyJsonResult(GetAllExeption(ex));
            }
        }

        [HttpPost]
        public ActionResult _Create(PagosReservaViewModel pago)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var modelState = ModelState.Values.Where(a => a.Errors.Count > 0).First();
                    var mensaje = modelState.Errors.FirstOrDefault().ErrorMessage;

                    return MyJsonResult(mensaje);
                }
                var exite = _pago.ObtenerQueryable().Any(x => x.Fecha == pago.Fecha);
                if (exite)
                {
                    return MyJsonResult("Ya existe un pago para esta reserva con la misma fecha, por favor corregir. Gracias!");
                }
                var usr = ObtenerUsuarioLogueado();
                var entidadPago = _helperMap.MapPagoModel(pago);
                entidadPago.CreadoPor = usr.UserName;
                entidadPago.FechaCreacion = DateTime.Now;
                var pagoId = _pago.Agregar(entidadPago);

                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return MyJsonResult(GetAllExeption(ex));
            }
        }

        private PagoReservaViewModel obtenerPagosReserva(int reservaId)
        {
            var pagos = _reserva.Obtener(reservaId).Pago.ToList();

            var entidad = _reserva.Obtener(reservaId);

            var viewModel = new PagoReservaViewModel()
                {
                    Id = entidad.Id,
                    ClienteId = entidad.ClienteId,
                    NumeroDocumento = entidad.Cliente.NumeroDocumentoIdentidad,
                    Fecha = entidad.Fecha,
                    Reserva = entidad.PlanTuristico.Descripcion,
                    Nombre = entidad.Cliente.NombreORazonSocial,
                    Valor = entidad.ValorTotal,
                    ValorPagado = entidad.ValorPagado,
                    Pagos = entidad.Pago.Select(s => new PagosReservaViewModel()
                    {
                        Id = s.Id,
                        ReservaId = s.ReservaId,
                        Fecha = s.Fecha,
                        Valor = s.Valor,
                        Saldo = s.Saldo,
                        Observacion = s.Observacion
                    }).ToList(),
                    
                };
            return viewModel;
        }


        public ActionResult _CreatePagoGrid(int? reservaId)
        {
            try
            {
                var viewModel = new PagoReservaViewModel();

                var id = reservaId == null ? 0 : reservaId.Value;
                if (id != 0)
                {
                    viewModel = obtenerPagosReserva(id);
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
        public ActionResult _CreatePagoGrid(PagoReservaViewModel pagosReservaViewmodel)
        {
            try
            {
                var usr = ObtenerUsuarioLogueado();
                var datosBD = _reserva.Obtener(pagosReservaViewmodel.Id);

                foreach (var item in pagosReservaViewmodel.Pagos)
                {
                    var existe = datosBD?.Pago?.Any(x => x.Fecha == item.Fecha);
                    var entidadPago = _helperMap.MapPagoModel(item);
                    if (existe.Value)
                    {
                        entidadPago.CreadoPor = usr.UserName;
                        entidadPago.FechaCreacion = DateTime.Now;
                        var pagoId = _pago.Agregar(entidadPago);
                    }
                    else
                    {
                        entidadPago.ModificadoPor = usr.UserName;
                        entidadPago.FechaModificacion = DateTime.Now;
                        _pago.Actualizar(entidadPago);
                    }

                }
                
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return MyJsonResult(GetAllExeption(ex));
            }
        }
    }
}