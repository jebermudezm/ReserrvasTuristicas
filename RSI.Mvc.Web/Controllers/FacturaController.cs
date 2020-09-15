using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
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
    public class FacturaController : dbController
    {
        #region Variables
        private readonly IConceptoRepositorio _concepto;
        private readonly IConceptoValorRepositorio _conceptoValor;
        private readonly IFacturaRepositorio _factura;
        private readonly IClienteRepositorio _cliente;
        private readonly IReservaRepositorio _reserva;
        private readonly IReservaDetalleRepositorio _reservaDetalle;
        private readonly IFacturaDetalleRepositorio _facturaDetalle;
        private readonly INumeroValorableRepositorio _numeroValorable;


        #endregion
        #region Constructor
        public FacturaController()
        {
            _concepto = new ConceptoRepositorio(_context);
            _conceptoValor = new ConceptoValorRepositorio(_context);
            _factura = new FacturaRepositorio(_context);
            _cliente = new ClienteRepositorio(_context);
            _reserva = new ReservaRepositorio(_context);
            _reservaDetalle = new ReservaDetalleRepositorio(_context);
            _facturaDetalle = new FacturaDetalleRepositorio(_context);
            _numeroValorable = new NumeroValorableRepositorio(_context);
        }
        #endregion

        // GET: Destino
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
                var listaDestinos = _factura.ObtenerLista();
                var listaDestinoViewModel = ObtenerDestinos();
                return ConstruirResultado(listaDestinoViewModel.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                return MyJsonResult($"se presentó el siguiente error: {ex.Message}");
            }
        }


        public List<FacturaViewModel> ObtenerDestinos()
        {
            var listaFacturas = _factura.ObtenerLista();
            var listaFacturasViewModel = new List<FacturaViewModel>();
            foreach (var item in listaFacturas)
            {
                listaFacturasViewModel.Add(_helperMap.MapFacturaViewModel(item));
            }
            return listaFacturasViewModel;
        }

        public ActionResult _Create()
        {
            try
            {
                //var clientes = _cliente.ObtenerLista();
                //ViewBag.ClienteId = new SelectList(clientes, "Id", "NombreORazonSocial");

                return PartialView();
            }
            catch (Exception ex)
            {
                return MyJsonResult(GetAllExeption(ex));
            }
        }

        public JsonResult GetCliente()
        {
            var clientes = _cliente.ObtenerLista();
            return Json(clientes.Select(c => new { ClienteId = c.Id, Nombre = c.NombreORazonSocial }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetReserva(int id)
        {
            var reservas = _reserva.ObtenerQueryable().Where(x => x.ClienteId == id).ToList();
            return Json(reservas.Select(c => new { ReservaId = c.Id, c.PlanTuristico.Descripcion}), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult _Create(FacturaViewModel factura)
        {
            try
            {
                if (factura.ClienteId == 0)
                {
                    return MyJsonResult("Debe seleccionar un Cliente. Gracias!");
                }
                if (factura.ReservaId == 0)
                {
                    return MyJsonResult("Debe seleccionar una reserva. Gracias!");
                }
                var cliente = _cliente.Obtener(factura.ClienteId);
                var reserva = _reserva.Obtener(factura.ReservaId);
                var reservaDetalle = _reservaDetalle.ObtenerQueryable().Where(x => x.ReservaId == reserva.Id);

                var conceptoFormatoDocumento = _concepto.ObtenerQueryable().FirstOrDefault(x => x.Codigo == "Factura");
                var formatoDocumento = _conceptoValor.ObtenerQueryable().Where(x => x.ConceptoId == conceptoFormatoDocumento.Id).OrderByDescending(x => x.Id).FirstOrDefault();
                var prefijo = formatoDocumento.Valor;
                var entidadNumeroFactura = _numeroValorable.ObtenerQueryable().Where(x => x.Prefijo == prefijo).OrderByDescending(o => o.Numero).FirstOrDefault();
                var numeroFactura = entidadNumeroFactura?.Numero == null ? 1 : entidadNumeroFactura.Numero+1;
                var numeroViewModel = new NumeroValorableViewModel
                {
                    ConceptoId = formatoDocumento.Id,
                    Prefijo = prefijo,
                    Numero = numeroFactura
                };
                var entidadNumero = _helperMap.MapNumeroValorableModel(numeroViewModel);
                var numeroId = _numeroValorable.Agregar(entidadNumero);
                entidadNumeroFactura = _numeroValorable.Obtener(numeroId);

                factura.Prefijo = prefijo;
                factura.Numero = numeroFactura;
                factura.ValorBruto = reserva.ValorBruto;
                factura.ValorDescuento = reserva.ValorDescuento;
                factura.ValorAntesImpuesto = 0;
                factura.ValorIVA = 0;
                factura.ValorNeto = reserva.ValorTotal;

                var entidadFactura = _helperMap.MapFacturaModel(factura);
                var usr = ObtenerUsuarioLogueado();
                entidadFactura.CreadoPor = usr.UserName;
                entidadFactura.FechaCreacion = DateTime.Now;

                var listaDetalleFactura = new List<FacturaDetalle>();
                var nroItem = 1;

                foreach (var item in reservaDetalle)
                {
                   
                    var detalleFactura = new FacturaDetalleViewModel
                    {
                        //FacturaId = facturaId,
                        Item = nroItem,
                        Cantidad = 1,
                        Descripcion = item.Descripcion ?? "",
                        ValorUnitario = item.ValorUnitario,
                        ValorBruto = item.ValorUnitario,
                        ValorAntesImpuesto = item.ValorUnitario,
                        ValorIVA = 0,
                        ValorNeto = item.ValorUnitario,
                        CreadoPor = usr.UserName,
                        FechaCreacion = DateTime.Now
                    };
                    var entidadDetalleFactura = _helperMap.MapFacturaDetalleModel(detalleFactura);
                    //entidadDetalleFactura.Descripcion = clienteItem.NombreORazonSocial;
                    listaDetalleFactura.Add(entidadDetalleFactura);
                    //var iddet = _factura.Agregar(entidadDetalleFactura);
                    nroItem++;
                }
                entidadFactura.FacturaDetalle = listaDetalleFactura;
                var facturaId = _factura.Agregar(entidadFactura);
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return MyJsonResult(GetAllExeption(ex));
            }
        }

        public ActionResult _Edit(int id)
        {
            try
            {
                var clientes = _cliente.ObtenerLista();
                ViewBag.ClienteId = new SelectList(clientes, "Id", "NombreORazonSocial");
                return PartialView();
            }
            catch (Exception ex)
            {
                return MyJsonResult(GetAllExeption(ex));
            }
        }
        [HttpPost]
        public ActionResult _Edit(FacturaViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var modelState = ModelState.Values.Where(a => a.Errors.Count > 0).First();
                    var mensaje = modelState.Errors.FirstOrDefault().ErrorMessage;

                    return MyJsonResult(mensaje);
                }

                var client = _factura.ObtenerQueryable().FirstOrDefault(x => x.Prefijo == model.Prefijo && x.Numero == model.Numero);
                if (client != null)
                {
                    return MyJsonResult("Ya existe una factura con el mismo número y prefijo, por favor corregir. Gracias!");
                }
                var entidadFactura = _helperMap.MapFacturaModel(model);
                var usr = ObtenerUsuarioLogueado();
                entidadFactura.ModificadoPor = usr.UserName;
                entidadFactura.FechaModificacion = DateTime.Now;
                _factura.Actualizar(entidadFactura);

                return new HttpStatusCodeResult(HttpStatusCode.NoContent);

            }
            catch (Exception ex)
            {
                return MyJsonResult(GetAllExeption(ex));
            }
        }

        public ActionResult _Factura(int id)
        {
            try
            {
                var entidad = _factura.ObtenerQueryable().FirstOrDefault(x => x.Id == id);
                var editViewModel = _helperMap.MapFacturaViewModel(entidad);

                return PartialView(editViewModel);

            }
            catch (Exception ex)
            {
                return MyJsonResult(GetAllExeption(ex));
            }
        }



        public ActionResult _Details(int id)
        {
            try
            {
                var entidad = _factura.ObtenerQueryable().FirstOrDefault(x => x.Id == id);
                var editViewModel = _helperMap.MapFacturaViewModel(entidad);

                return PartialView(editViewModel);

            }
            catch (Exception ex)
            {
                return MyJsonResult(GetAllExeption(ex));
            }
        }
        public ActionResult _Delete(int id)
        {
            try
            {
                var factura = _factura.ObtenerQueryable().FirstOrDefault(x => x.Id == id);

                return PartialView();
            }
            catch (Exception ex)
            {
                return MyJsonResult(GetAllExeption(ex));
            }
        }

        [HttpPost, ActionName("_Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var modelState = ModelState.Values.Where(a => a.Errors.Count > 0).First();
                    var mensaje = modelState.Errors.FirstOrDefault().ErrorMessage;
                    return MyJsonResult(mensaje);
                }
                var entidad = _factura.Obtener(id);
                _factura.Eliminar(entidad);
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return MyJsonResult(GetAllExeption(ex));
            }
        }

        #region Detalle
        [AllowJsonGet]
        public ActionResult Read_MovimientoDetalle([DataSourceRequest] DataSourceRequest request, int facturaId)
        {
            try
            {
                var facturaDetalle = _facturaDetalle.ObtenerQueryable().Where(x => x.FacturaId == facturaId).ToList();
                var model = new List<FacturaDetalleViewModel>();
                foreach (var item in facturaDetalle)
                {
                    model.Add(new FacturaDetalleViewModel
                    {
                        Id = item.Id,
                        Item = item.Item,
                        Cantidad = item.Cantidad,
                        Descripcion = item.Descripcion,
                        ValorUnitario = item.ValorUnitario,
                        PorcentajeDescuento = item.PorcentajeDescuento,
                        ValorAntesImpuesto = item.ValorAntesImpuesto,
                        PorcentajeIVA = item.PorcentajeIVA,
                        ValorIVA = item.ValorIVA,
                        ValorNeto = item.ValorNeto,
                        CreadoPor = item.CreadoPor,
                        FechaCreacion = item.FechaCreacion,
                        ModificadoPor = item.ModificadoPor,
                        FechaModificacion = item.FechaModificacion
                    });
                }
                return ConstruirResultado(model.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                return MyJsonResult(GetAllExeption(ex));
            }
        }

        //public ActionResult _AgregarItems(int? id)
        //{
        //    if (id == null)
        //        return MyJsonResult("El Id es un parametro requerido ");

        //    var reserva = db.Reservas.Find(id);
        //    if (reserva == null)
        //        return MyJsonResult("No existe reserva con el Id: " + id);


        //    var listaClientes = (from cliente in db.Clientes
        //                         join reservaDetalle in db.ReservasDetalle.Where(x => x.ReservaId == id) on cliente.Id equals reservaDetalle.ClienteId into detaJoin
        //                         from detalleCliente in detaJoin.DefaultIfEmpty()
        //                         select new { Cliente = cliente, Seleccionado = detalleCliente != null }).ToList();

        //    var reservaVm = new ReservaViewModel
        //    {
        //        Id = reserva.Id,
        //        //Cliente = reserva.Cliente.NombreORazonSocial,
        //    };
        //    ViewBag.Seleccionados = new SelectList(listaClientes.Where(gb => gb.Seleccionado).Select(x => x.Cliente), "Id", "NombreORazonSocial");
        //    ViewBag.Disponibles = new SelectList(listaClientes.Where(gb => !gb.Seleccionado).Select(x => x.Cliente), "Id", "NombreORazonSocial");
        //    return PartialView(reservaVm);
        //}

        //[HttpPost]
        ////[ValidateAntiForgeryToken]
        //public ActionResult _AgregarItems(ReservaViewModel reservaVm)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            var modelState = ModelState.Values.Where(a => a.Errors.Count > 0).First();
        //            var mensaje = modelState.Errors.FirstOrDefault().ErrorMessage;
        //            return MyJsonResult(mensaje);
        //        }

        //        var reserva = db.Reservas.FirstOrDefault(sa => sa.Id == reservaVm.Id);
        //        if (reserva == null)
        //            return MyJsonResult("No existe una reserva con el Id: " + reservaVm.Id);
        //        var usr = ObtenerUsuarioLogueado();
        //        var clientesAsignados = db.ReservasDetalle.Where(x => x.ReservaId == reservaVm.Id).ToList();
        //        var ahora = DateTime.Now;
        //        reserva.FechaModificacion = ahora;
        //        reserva.ModificadoPor = usr.UserName;

        //        if (reservaVm.Seleccionados == null)
        //            reservaVm.Seleccionados = new List<int>();

        //        var reservaBD = _reserva.Obtener(reservaVm.Id);

        //        var itemsToAdd = reservaVm.Seleccionados.Where(i => !clientesAsignados.Any(sagb => sagb.ClienteId == i)).ToList();
        //        var itemsToRemove = clientesAsignados.Where(sagb => !reservaVm.Seleccionados.Any(i => i == sagb.ClienteId)).ToList();
        //        var plan = _planTuristico.Obtener(reservaBD.PlanTuristicoId);
        //        using (DbContextTransaction dbTran = db.Database.BeginTransaction())
        //        {
        //            try
        //            {
        //                foreach (var item in itemsToRemove)
        //                {
        //                    db.ReservasDetalle.Remove(item);
        //                }

        //                foreach (var item in itemsToAdd)
        //                {
        //                    var cliente = db.Clientes.FirstOrDefault(i => i.Id == item);
        //                    if (cliente == null)
        //                        throw new Exception("No existe <Cliente> con el id: " + item);
        //                    var edad = DateTime.Today.AddTicks(-cliente.FechaNacimiento.Value.Ticks).Year - 1;
        //                    var valor = 0.0;
        //                    if (edad >= 18)
        //                        valor = plan.ValorAdulto;
        //                    else if (edad >= 5)
        //                        valor = plan.ValorMenor;
        //                    else
        //                        valor = plan.ValorInfante;
        //                    var porcentajeDescuento = reservaBD.PorcentajeDescuento / 100;
        //                    var sagb = new ReservaDetalle
        //                    {
        //                        ClienteId = cliente.Id,
        //                        ReservaId = reservaVm.Id,
        //                        ValorUnitario = valor,
        //                        ValorDescuento = (valor * porcentajeDescuento),
        //                        ValorTotal = valor - (valor * porcentajeDescuento),
        //                        FechaCreacion = ahora,
        //                        CreadoPor = usr.UserName
        //                    };
        //                    db.ReservasDetalle.Add(sagb);
        //                }
        //                db.SaveChanges();
        //                dbTran.Commit();
        //            }
        //            catch (Exception dbEx)
        //            {
        //                dbTran.Rollback();
        //                throw dbEx;
        //            }
        //        }
        //        foreach (var item in itemsToRemove)
        //        {
        //            var itemToRemove = reservaVm.ReservaDetalle?.FirstOrDefault(x => x.ClienteId == item.ClienteId);
        //            if (itemToRemove != null)
        //                reservaVm.ReservaDetalle.Remove(itemToRemove);
        //        }
        //        var deta = _reservaDetalle.ObtenerQueryable().Where(x => x.ReservaId == reservaVm.Id).ToList();
        //        var clientes = _cliente.ObtenerLista();
        //        var lista = new List<ReservaDetalleViewModel>();
        //        foreach (var item in itemsToAdd)
        //        {
        //            var cliente = clientes.FirstOrDefault(x => x.Id == item);
        //            var det = new ReservaDetalleViewModel
        //            {
        //                ClienteId = item,
        //                IdentificacionCliente = cliente.NumeroDocumentoIdentidad,
        //                FechaNacimiento = cliente.FechaNacimiento.Value
        //            };
        //            lista.Add(det);
        //        }
        //        ActualizarEncabezado(reservaBD.Id);
        //        return new HttpStatusCodeResult(HttpStatusCode.NoContent);
        //    }
        //    catch (Exception ex)
        //    {
        //        return MyJsonResult(GetAllExeption(ex));
        //    }
        //}

        private void ActualizarEncabezado(int id)
        {
            var reserva = _reserva.Obtener(id);
            var detalle = reserva.ReservasDetalle.ToList();
            reserva.ValorTotal = detalle.Sum(s => s.ValorTotal);
            reserva.ValorDescuento = detalle.Sum(s => s.ValorDescuento);
            reserva.ValorBruto = detalle.Sum(s => s.ValorUnitario);
            _reserva.Actualizar(reserva);
        }
        #endregion
    }
}