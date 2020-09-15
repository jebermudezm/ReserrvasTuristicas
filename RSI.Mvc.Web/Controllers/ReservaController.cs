using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using RSI.Modelo.Entidades.Movimientos;
using RSI.Modelo.RepositorioCont;
using RSI.Modelo.RepositorioImpl;
using RSI.Mvc.Web.Controllers.Helper;
using RSI.Mvc.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RSI.Mvc.Web.Controllers
{
    public class ReservaController : dbController
    {
        #region Variables
        private RSIModelContextDB db = new RSIModelContextDB();
        private readonly IReservaRepositorio _reserva;
        private readonly IReservaDetalleRepositorio _reservaDetalle;
        private readonly IReservaIntegranteRepositorio _reservaIntegrante;
        private readonly IClienteRepositorio _cliente;
        private readonly IPlanTuristicoRepositorio _planTuristico;
        private readonly IConvenioRepositorio _convenio;
        private readonly IListaRepositorio _lista;

        #endregion
        #region Constructor
        public ReservaController()
        {
            _reserva = new ReservaRepositorio(_context);
            _reservaDetalle = new ReservaDetalleRepositorio(_context);
            _reservaIntegrante = new ReservaIntegrantesRepositorio(_context);
            _cliente = new ClienteRepositorio(_context);
            _planTuristico = new PlanTuristicoRepositorio(_context);
            _convenio = new ConvenioRepositorio(_context);
            _lista = new ListaRepositorio(_context);
        }
        #endregion

        #region Metodos
        // GET: Reserva
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
                var listaReservas = _reserva.ObtenerLista();
                var listaReservaViewModel = ObtenerReservas();
                return ConstruirResultado(listaReservaViewModel.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                return MyJsonResult($"se presentó el siguiente error: {ex.Message}");
            }
        }

        public List<ReservaViewModel> ObtenerReservas()
        {
            var listaReservas = _reserva.ObtenerLista();
            var listaIntegrantes = _reservaIntegrante.ObtenerLista();
            var listaReservaViewModel = new List<ReservaViewModel>();
            foreach (var item in listaReservas)
            {
                var viewModel = _helperMap.MapReservaViewModel(item);
                var detalle = new List<ReservaDetalleViewModel>();
                foreach (var item1 in item.ReservasDetalle)
                {
                    detalle.Add(new ReservaDetalleViewModel {
                        
                        Id = item1.Id,
                        ReservaId = item1.Item,
                        Item = item1.Item,
                        Descripcion = item1.Descripcion,
                        Cantidad = item1.Cantidad,
                        ValorUnitario = item1.ValorUnitario,
                        ValortotalBruto = item1.ValorTotalBruto,
                        PorcentajeDescuento = item1.PorcentajeDescuento,
                        ValorDescuento = item1.ValorDescuento,
                        ValorAntesImpuesto = item1.ValorBase,
                        PorcentajeImpuesto = item1.PorcentajeImpuesto,
                        ValorImpuesto = item1.ValorImpuesto,
                        ValorTotal =item1.ValorTotal,
                        CreadoPor = item1.CreadoPor,
                        FechaCreacion = item1.FechaCreacion,
                        ModificadoPor = item1.ModificadoPor,
                        FechaModificacion = item1.FechaModificacion
                    });
                }
                var integrantes = new List<ReservaIntegranteViewModel>();
                var integrantesReserva = listaIntegrantes.Where(x => x.ReservaId == item.Id).ToList();
                foreach (var item1 in integrantesReserva)
                {
                    integrantes.Add(new ReservaIntegranteViewModel
                    {
                        Id = item1.Id,
                        ClienteId = item1.ClienteId,
                        NombreORazonSocial = item1.Nombre,
                        NumeroIdentificacion = item1.NumeroDocumento,
                        FechaNacimiento = item1.FechaNacimiento,
                        Edad = item1.Edad,
                        ReservaId = item1.ReservaId,
                        CreadoPor = item1.CreadoPor,
                        FechaCreacion = item1.FechaCreacion,
                        ModificadoPor = item1.ModificadoPor,
                        FechaModificacion = item1.FechaModificacion
                    });
                }
                viewModel.ReservaDetalle = detalle;
                viewModel.ReservaIntegrantes = integrantes;
                listaReservaViewModel.Add(viewModel);
            }
            return listaReservaViewModel;
        }

        public ActionResult _Create()
        {
            try
            {
                var fechaIni = DateTime.Now.AddMonths(-2);
                var clientes = _cliente.ObtenerLista();
                var listaClientes = clientes.Where(x => x.Edad >= 18).ToList();
                ViewBag.ClienteId = new SelectList(listaClientes, "Id", "NombreORazonSocial");
                var fecha = DateTime.Now.Date;
                var listaPlanes = _planTuristico.ObtenerQueryable().Where(x => x.Fecharegreso >= fecha).ToList();
                ViewBag.PlanTuristicoId = new SelectList(listaPlanes, "Id", "Descripcion");

                var listaConvenio = _convenio.ObtenerLista();
                ViewBag.ConvenioId = new SelectList(listaConvenio, "Id", "Nombre");

                return PartialView();
            }
            catch (Exception ex)
            {
                return MyJsonResult(GetAllExeption(ex));
            }
        }


        [HttpPost]
        public ActionResult _Create(ReservaViewModel reserva)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var modelState = ModelState.Values.Where(a => a.Errors.Count > 0).First();
                    var mensaje = modelState.Errors.FirstOrDefault().ErrorMessage;

                    return MyJsonResult(mensaje);
                }
                var client = _reserva.ObtenerQueryable().FirstOrDefault(x => x.ClienteId == reserva.ClienteId && x.PlanTuristicoId == reserva.PlanTuristicoId);
                var lista = _lista.ObtenerQueryable().FirstOrDefault(x => x.Nombre == "SI");
                var cliente = _cliente.Obtener(reserva.ClienteId);
                var edad = DateTime.Today.AddTicks(-cliente.FechaNacimiento.Ticks).Year - 1;
                if (edad < 18)
                {
                    return MyJsonResult("El cliente cliente seleccionado como responsable es menor de edad, por favor seleccionar una persona mayor de edad. Gracias!");
                }
                var usr = ObtenerUsuarioLogueado();
                var plan = _planTuristico.Obtener(reserva.PlanTuristicoId);
                var convenio = _convenio.Obtener(reserva.ConvenioId);
                var porcentajeDescuento = convenio.Descuento;
                var valorAdulto = plan.ValorAdulto;
                var valorMenor = plan.ValorMenor;
                var valorInfante = plan.ValorInfante;
                var itemAdulto = ObtenerItem(1, lista.Id, reserva.Mayores, "Turistas Adultos", porcentajeDescuento, usr.UserName, valorAdulto, lista.Valor);
                var itemMenor = ObtenerItem(2, lista.Id, reserva.Mayores, "Turistas Menores", porcentajeDescuento, usr.UserName, valorMenor, lista.Valor);
                var itemInfante = ObtenerItem(3, lista.Id, reserva.Mayores, "Turistas Infantes", porcentajeDescuento, usr.UserName, valorInfante, lista.Valor);

                var listaDetalle = new List<ReservaDetalle>
                {
                    itemAdulto,
                    itemMenor,
                    itemInfante
                };
                reserva.ValorBruto = listaDetalle.Sum(x => x.ValorTotalBruto);
                reserva.ValorDescuento = listaDetalle.Sum(x => x.ValorDescuento);
                reserva.ValorTotal = listaDetalle.Sum(x => x.ValorTotal);
                var entidadReserva = _helperMap.MapReservaModel(reserva);
                
                entidadReserva.CreadoPor = usr.UserName;
                entidadReserva.FechaCreacion = DateTime.Now;
                entidadReserva.UsuarioId = usr.Id;
                entidadReserva.ValorPagado = 0;
                entidadReserva.ReservasDetalle = listaDetalle;
                var reservaId =_reserva.Agregar(entidadReserva);
                
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return MyJsonResult(GetAllExeption(ex));
            }
        }

        private ReservaDetalle ObtenerItem(int item, int impuestoId, double cantidad, string descripcion, double porcentajeDescuento, string userName, double valorUnitario, double porcentajeImpuesto)
        {
            var valorTotalBruto = (valorUnitario * cantidad);
            var valorDescuento = valorTotalBruto * (porcentajeDescuento / 100);
            var valorBase = valorTotalBruto - valorDescuento;
            var valorImpuesto = valorBase * (porcentajeImpuesto / 100);
            var valorTotal = valorBase + valorImpuesto;
            return new ReservaDetalle
                    {
                Item = item,
                ImpuestoId = impuestoId,
                Cantidad = cantidad,
                Descripcion = descripcion,
                PorcentajeDescuento = porcentajeDescuento,
                PorcentajeImpuesto = porcentajeImpuesto,
                CreadoPor = userName,
                FechaCreacion = DateTime.Now,
                ValorUnitario = valorUnitario,
                ValorTotalBruto = valorTotalBruto,
                ValorDescuento = valorDescuento,
                ValorBase = valorBase,
                ValorImpuesto = valorImpuesto,
                ValorTotal = valorTotal
                
            };
        }

        public ActionResult _Edit(int id)
        {
            try
            {
                var listaClientes = _cliente.ObtenerLista();
                var clientes = listaClientes.Where(x => x.Edad >= 18).ToList();
                var fecha = DateTime.Now.Date;
                var listaPlanes = _planTuristico.ObtenerQueryable().Where(x => x.Fecharegreso >= fecha).ToList();
                var convenios = _convenio.ObtenerLista();
                var entidad = _reserva.ObtenerQueryable().FirstOrDefault(x => x.Id == id);
                var editViewModel = _helperMap.MapReservaViewModel(entidad);
                editViewModel.Mayores = entidad.ReservasDetalle.FirstOrDefault(x => x.Item == 1).Cantidad;
                editViewModel.Menores = entidad.ReservasDetalle.FirstOrDefault(x => x.Item == 2).Cantidad;
                editViewModel.Infantes = entidad.ReservasDetalle.FirstOrDefault(x => x.Item == 3).Cantidad;
                ViewBag.ClienteId = new SelectList(clientes, "Id", "NombreORazonSocial", editViewModel.ClienteId);
                ViewBag.PlanTuristicoId = new SelectList(listaPlanes, "Id", "Descripcion", editViewModel.PlanTuristicoId);
                ViewBag.ConvenioId = new SelectList(convenios, "Id", "Nombre", editViewModel.ConvenioId);
                return PartialView(editViewModel);
            }
            catch (Exception ex)
            {
                return MyJsonResult(GetAllExeption(ex));
            }
        }
        [HttpPost]
        public ActionResult _Edit(ReservaViewModel model)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    var modelState = ModelState.Values.Where(a => a.Errors.Count > 0).First();
                    var mensaje = modelState.Errors.FirstOrDefault().ErrorMessage;

                    return MyJsonResult(mensaje);
                }

                var ReservaBD = _reserva.Obtener(model.Id);
                
                var usr = ObtenerUsuarioLogueado();
                ReservaBD.ModificadoPor = usr.UserName;
                ReservaBD.FechaModificacion = DateTime.Now;
                ReservaBD.UsuarioId = usr.Id;
                var plan = _planTuristico.Obtener(ReservaBD.PlanTuristicoId);
                var convenio = _convenio.Obtener(ReservaBD.ConvenioId);
                foreach (var item in ReservaBD.ReservasDetalle)
                {
                    var cantidad = item.Cantidad;
                    
                    if (item.Item == 1)
                    {
                        cantidad = model.Mayores;
                    }
                    if (item.Item == 2)
                    {
                        cantidad = model.Menores;
                    }
                    if (item.Item == 3)
                    {
                        cantidad = model.Infantes;
                    }
                    var item1 = ObtenerItem(item.Item, item.ImpuestoId, cantidad, item.Descripcion, item.PorcentajeDescuento, usr.UserName, item.ValorUnitario, item.PorcentajeImpuesto);
                    item.Cantidad = item1.Cantidad;
                    item.ValorTotalBruto = item1.ValorTotalBruto;
                    item.ValorDescuento = item1.ValorDescuento;
                    item.ValorBase = item1.ValorBase;
                    item.ValorImpuesto = item1.ValorImpuesto;
                    item.ValorTotal = item1.ValorTotal;

                }

                _reserva.Actualizar(ReservaBD);

                
               
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);

            }
            catch (Exception ex)
            {
                return MyJsonResult(GetAllExeption(ex));
            }
        }

        public ActionResult _ReservaToPrint(int id)
        {
            try
            {
                var entidad = _reserva.ObtenerQueryable().FirstOrDefault(x => x.Id == id);
                var plan = entidad.PlanTuristico;
                var cliente = entidad.Cliente;
                var convenio = entidad.Convenio;
                var usuaro = entidad.Usuario;
                var integrantesEnt = _reservaIntegrante.ObtenerQueryable().Where(x => x.ReservaId == id).ToList();
                var detalle = entidad.ReservasDetalle;
                var incluye = new List<ListasViewModel>();
                var includes = plan.DetallePlanTuristico.FirstOrDefault(x => x.Codigo == "IN")?.ItemDetallePlanTuristico.ToList();
                if (includes != null)
                {
                    foreach (var item in includes)
                    {
                        incluye.Add(new ListasViewModel
                        {
                            tipo = "Incluye",
                            Descripcion = item.Descripcion
                        });
                    }
                }
                var noInclude = plan.DetallePlanTuristico.FirstOrDefault(x => x.Codigo == "NI")?.ItemDetallePlanTuristico.ToList();
                if (noInclude != null)
                {
                    foreach (var item in noInclude)
                    {
                        incluye.Add(new ListasViewModel
                        {
                            tipo = "NoIncluye",
                            Descripcion = item.Descripcion
                        });
                    }
                }
                foreach (var item in detalle)
                {
                    incluye.Add(new ListasViewModel
                    {
                        tipo = "Detalle",
                        Descripcion = $"{item.Cantidad.ToString()} {item.Descripcion}"
                    });
                }
                var integrantes = new List<IntegrantesViewModel>();  
                foreach (var item in integrantesEnt)
                {
                    integrantes.Add(new IntegrantesViewModel {
                        Id = item.Id,
                        ClienteId = item.ClienteId,
                        Cliente = item.Nombre,
                        NumeroIdentificacion = item.NumeroDocumento,
                        FechaNacimiento = item.FechaNacimiento,
                        Edad = item.Edad
                    });
                }
                var editViewModel = new ReservaToPrintViewModel {
                    Id = entidad.Id,
                    ClienteId = entidad.ClienteId,
                    Cliente = cliente.NombreORazonSocial,
                    Destino = plan.Destino.Descripcion,
                    PlanId = plan.Id,
                    Plan = plan.Descripcion,
                    Hotel = plan.Hotel,
                    ConvenioId = convenio.Id,
                    Convenio = convenio.Nombre,
                    Acomodacion = entidad.Acomodacion,
                    Fecha = entidad.Fecha,
                    FechaSalida = plan.FechaSalida,
                    FechaRegreso = plan.Fecharegreso,
                    UsuarioId = usuaro.Id,
                    Usuario = usuaro.Nombre,
                    ValorBruto = entidad.ValorBruto,
                    ValorDescuento = entidad.ValorDescuento,
                    ValorImpuesto = entidad.TotalImpuesto,
                    ValorTotal = entidad.ValorTotal,
                    InformacionGeneral = entidad.Observaciones, 
                    Lista = incluye,
                    ReservaIntegrantes = integrantes

                };

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
                var entidad = _reserva.ObtenerQueryable().FirstOrDefault(x => x.Id == id);
                var editViewModel = _helperMap.MapReservaViewModel(entidad);

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
                var reserva = _reserva.ObtenerQueryable().FirstOrDefault(x => x.Id == id);

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
                var entidad = _reserva.ObtenerQueryable().FirstOrDefault(x => x.Id == id);
                _reserva.Eliminar(entidad);
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return MyJsonResult(GetAllExeption(ex));
            }
        }
        #endregion
        
        #region Integrantes
        [AllowJsonGet]
        public ActionResult Read_ReservaIntegrante([DataSourceRequest] DataSourceRequest request, int reservaId)
        {
            try
            {
                var model = new List<ReservaIntegranteViewModel>();
                var reserva = _reserva.Obtener(reservaId);
                var reservaIntegrantes = _reservaIntegrante.ObtenerQueryable().Where(x => x.ReservaId == reservaId).ToList();
                if (reservaIntegrantes.Count > 0)
                {
                    var clientes = _cliente.ObtenerLista();
                    var plan = _planTuristico.Obtener(reserva.PlanTuristicoId);
                    
                    foreach (var item in reservaIntegrantes)
                    {
                        var cliente = clientes.FirstOrDefault(x => x.Id == item.ClienteId);
                        model.Add(new ReservaIntegranteViewModel
                        {
                            Id = item.Id,
                            ClienteId = item.ClienteId,
                            NumeroIdentificacion = cliente.NumeroDocumentoIdentidad,
                            NombreORazonSocial = cliente.NombreORazonSocial,
                            FechaNacimiento = cliente.FechaNacimiento,
                            Edad = cliente.Edad,
                            CreadoPor = item.CreadoPor,
                            FechaCreacion = item.FechaCreacion,
                            ModificadoPor = item.ModificadoPor,
                            FechaModificacion = item.FechaModificacion
                        });
                    }
                }
                return ConstruirResultado(model.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                return MyJsonResult(GetAllExeption(ex));
            }
        }

        public ActionResult _AgregarItems(int? id)
        {
            if (id == null)
                return MyJsonResult("El Id es un parametro requerido ");

            var reserva = db.Reservas.Find(id);
            if (reserva == null)
                return MyJsonResult("No existe reserva con el Id: " + id);


            var listaClientes = (from cliente in db.Clientes
                                       join reservaIntegrantes in db.ReservaIntegrantes.Where(x => x.ReservaId == id) on cliente.Id equals reservaIntegrantes.ClienteId into detaJoin
                                              from detalleCliente in detaJoin.DefaultIfEmpty()
                                              select new { Cliente = cliente, Seleccionado = detalleCliente != null }).ToList();

            var reservaVm = new ReservaViewModel            {
                Id = reserva.Id,
                //Cliente = reserva.Cliente.NombreORazonSocial,
            };
            ViewBag.Seleccionados = new SelectList(listaClientes.Where(gb => gb.Seleccionado).Select(x => x.Cliente), "Id", "NombreORazonSocial");
            ViewBag.Disponibles = new SelectList(listaClientes.Where(gb => !gb.Seleccionado).Select(x => x.Cliente), "Id", "NombreORazonSocial");
            return PartialView(reservaVm);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult _AgregarItems(ReservaViewModel reservaVm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var modelState = ModelState.Values.Where(a => a.Errors.Count > 0).First();
                    var mensaje = modelState.Errors.FirstOrDefault().ErrorMessage;
                    return MyJsonResult(mensaje);
                }
                var reserva = db.Reservas.FirstOrDefault(sa => sa.Id == reservaVm.Id);
                if (reserva == null)
                    return MyJsonResult("No existe una reserva con el Id: " + reservaVm.Id);
                var usr = ObtenerUsuarioLogueado();
                var clientesAsignados = db.ReservaIntegrantes.Where(x => x.ReservaId == reservaVm.Id).ToList();
                var ahora = DateTime.Now;
                reserva.FechaModificacion = ahora;
                reserva.ModificadoPor = usr.UserName;
                if (reservaVm.Seleccionados == null)
                    reservaVm.Seleccionados = new List<int>();
                var reservaBD = _reserva.Obtener(reservaVm.Id);
                var itemsToAdd = reservaVm.Seleccionados.Where(i => !clientesAsignados.Any(sagb => sagb.ClienteId == i)).ToList();
                var itemsToRemove = clientesAsignados.Where(sagb => !reservaVm.Seleccionados.Any(i => i == sagb.ClienteId)).ToList();
                var plan = _planTuristico.Obtener(reservaBD.PlanTuristicoId);
                using (DbContextTransaction dbTran = db.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var item in itemsToRemove)
                        {
                            db.ReservaIntegrantes.Remove(item);
                        }

                        foreach (var item in itemsToAdd)
                        {
                            var cliente = db.Clientes.FirstOrDefault(i => i.Id == item);
                            var sagb = new ReservaIntegrante
                            {
                                ClienteId = cliente.Id,
                                Nombre = cliente.NombreORazonSocial,
                                NumeroDocumento = cliente.NumeroDocumentoIdentidad,
                                FechaNacimiento = cliente.FechaNacimiento,
                                ReservaId = reservaVm.Id,
                                Edad = DateTime.Today.AddTicks(-cliente.FechaNacimiento.Ticks).Year - 1,
                                FechaCreacion = ahora,
                                CreadoPor = usr.UserName
                            };
                            db.ReservaIntegrantes.Add(sagb);
                        }
                        db.SaveChanges();
                        dbTran.Commit();
                    }
                    catch (Exception dbEx)
                    {
                        dbTran.Rollback();
                        throw dbEx;
                    }
                }
                foreach (var item in itemsToRemove)
                {
                    var itemToRemove = reservaVm.ReservaIntegrantes?.FirstOrDefault(x => x.ClienteId == item.ClienteId);
                    if(itemToRemove != null)
                        reservaVm.ReservaIntegrantes.Remove(itemToRemove);
                }
                var deta = _reservaDetalle.ObtenerQueryable().Where(x => x.ReservaId == reservaVm.Id).ToList();
                var clientes = _cliente.ObtenerLista();
                var lista = new List<ReservaIntegranteViewModel>();
                foreach (var item in itemsToAdd)
                {
                    var cliente = clientes.FirstOrDefault(x => x.Id == item);
                    var det = new ReservaIntegranteViewModel
                    {
                        ClienteId = item,
                        NumeroIdentificacion = cliente.NumeroDocumentoIdentidad,
                        FechaNacimiento = cliente.FechaNacimiento
                    };
                    lista.Add(det);
                }
                ActualizarItemsYEncabezado(reservaBD.Id);
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return MyJsonResult(GetAllExeption(ex));
            }
        }

        private void ActualizarItemsYEncabezado(int id)
        {
            var edadAdultos = 18;
            var edadMenores = 5;
            var reserva = _reserva.Obtener(id);
            var integrantes = (from cliente in db.Clientes
                               join reservaIntegrantes in db.ReservaIntegrantes.Where(x => x.ReservaId == id) on cliente.Id equals reservaIntegrantes.ClienteId
                               select new { Cliente = cliente, Integrantes = reservaIntegrantes }).ToList();
            var clientes = integrantes.Select(x => x.Cliente).ToList();
            var detalle = reserva.ReservasDetalle.ToList();
            var adultos = clientes.Where(x => x.Edad >= edadAdultos).Count();
            var menores = clientes.Where(x => x.Edad >= edadMenores && x.Edad < edadAdultos).Count();
            var infantes = clientes.Where(x => x.Edad < edadMenores).Count();

            
            reserva.ValorTotal = detalle.Sum(s => s.ValorTotal);
            reserva.ValorDescuento = detalle.Sum(s => s.ValorDescuento);

            reserva.ValorBruto = detalle.Sum(s => s.ValorTotalBruto);
            _reserva.Actualizar(reserva);

            _reservaDetalle.ActualizarDetalle(id);

        }
        #endregion

        #region Detalle

        [AllowJsonGet]
        public ActionResult Read_ReservaDetalle([DataSourceRequest] DataSourceRequest request, int ReservaId)
        {
            try
            {
                var detalleReserva = _reservaDetalle.ObtenerQueryable().Where(x => x.ReservaId == ReservaId).ToList();
                var model = new List<ReservaDetalleViewModel>();
                foreach (var item in detalleReserva)
                {
                    model.Add(_helperMap.MapReservaDetalleViewModel(item));
                }
                return ConstruirResultado(model.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                return MyJsonResult(GetAllExeption(ex));
            }
        }

        // GET: DeltaOperacion/Create
        public ActionResult _AgregarDetalleGrid(int? id)
        {
            try
            {
                var viewModel = new ReservaViewModel();
                var detalle = new List<ReservaDetalleViewModel>();

                var reservaId = id == null ? 0 : id.Value;
                if (reservaId != 0)
                {
                    var entidad = _reserva.Obtener(reservaId);
                    viewModel = _helperMap.MapReservaViewModel(entidad);
                    foreach (var item in entidad.ReservasDetalle)
                    {
                        detalle.Add(new ReservaDetalleViewModel
                        {
                            Id = item.Id,
                            Item = item.Item,
                            Cantidad = item.Cantidad,
                            Descripcion = item.Descripcion,
                            ValorUnitario = item.ValorUnitario,
                            PorcentajeDescuento = item.PorcentajeDescuento,
                            ValorDescuento = item.ValorDescuento,
                            PorcentajeImpuesto = item.PorcentajeImpuesto,
                            ValorAntesImpuesto = item.ValorBase,
                            ValorImpuesto = item.ValorImpuesto,
                            ValortotalBruto = item.ValorTotalBruto,
                            ValorTotal = item.ValorTotal
                        });
                    }
                    viewModel.ReservaDetalle = detalle;
                }
                else
                {
                    return MyJsonResult("Los includ deben ir asociados a un plan.");
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
        public ActionResult _AgregarDetalleGrid(ReservaViewModel reservaViewModel)
        {
            try
            {
                var usr = ObtenerUsuarioLogueado();
                var datosBD = _reservaDetalle.ObtenerQueryable().Where(x => x.ReservaId == reservaViewModel.Id).ToList();

                var listaReservaDetalle = (from bd in datosBD
                                                 join dp in reservaViewModel.ReservaDetalle.ToList() on bd.Id equals dp.Id into detaJoin
                                                 from det in detaJoin.DefaultIfEmpty()
                                                 select new { Detalle = bd, Seleccionado = det != null }).ToList();

                //foreach (var item in reservaVM.ReservaDetalle)
                //{
                //    //var registroBD = datosBD?.FirstOrDefault(x => x.ProductoId == item.ProductoId);
                //    if (registroBD == null)
                //    {
                //        var entidad = _helperMap.MapReservaDetalleModel(item);
                //        entidad.ReservaId = reservaVM.Id;
                //        entidad.FechaCreacion = DateTime.Now;
                //        entidad.CreadoPor = usr.UserName;
                //        var includId = _reservaDetalle.Agregar(entidad);
                //    }
                //    else
                //    {
                //        registroBD.ModificadoPor = usr.UserName;
                //        registroBD.FechaModificacion = DateTime.Now;
                //        registroBD.Item = item.Item;
                //        registroBD.Descripcion = item.Producto;
                //        registroBD.Cantidad = item.Cantidad;
                //        registroBD.ValorUnitario = item.ValorUnitario;
                //        registroBD.ValorTotalBruto = item.ValortotalBruto;
                //        registroBD.PorcentajeDescuento = item.PorcentajeDescuento;
                //        registroBD.ValorDescuento = item.ValorDescuento;
                //        registroBD.PorcentajeImpuesto = item.PorcentajeImpuesto;
                //        registroBD.ValorImpuesto = item.ValorImpuesto;
                //        registroBD.ValorTotal = item.ValorTotal;

                //        _reservaDetalle.Actualizar(registroBD);
                //    }
                //}
                _reservaDetalle.ActualizarValoresReserva(reservaViewModel.Id);
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