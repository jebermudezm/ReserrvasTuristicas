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
    public class PlanTuristicoController : dbController
    {
        #region Variables
        private readonly IPlanRepositorio _plan;
        private readonly IProveedorRepositorio _proveedor;
        private readonly IDestinoRepositorio _destino;
        private readonly IIncluyeRepositorio _Incluye;
        private readonly IDetalleIncluyeRepositorio _detalleIncluye;
        private readonly IPlanTuristicoRepositorio _planTuristico;
        private readonly IDetallePlanTuristicoRepositorio _detallePlanTuristico;
        private readonly IItemDetallePlanTuristicoRepositorio _itemDetallePlanTuristico;

        #endregion
        #region Constructor
        public PlanTuristicoController()
        {
            _plan = new PlanRepositorio(_context);
            _proveedor = new ProveedorRepositorio(_context);
            _destino = new DestinoRepositorio(_context);
            _Incluye = new IncluyeRepositorio(_context);
            _detalleIncluye = new DetalleIncluyeRepositorio(_context);
            _planTuristico = new PlanTuristicoRepositorio(_context);
            _detallePlanTuristico = new DetallePlanTuristicoRepositorio(_context);
            _itemDetallePlanTuristico = new ItemDetallePlanTuristicoRepositorio(_context);

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
                var listaPlanViewModel = ObtenerPlanesTuristicos();
                return ConstruirResultado(listaPlanViewModel.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                return MyJsonResult($"se presentó el siguiente error: {ex.Message}");
            }
        }

        public List<PlanTuristicoViewModel> ObtenerPlanesTuristicos()
        {
            var listaPlanesTuristicos = _planTuristico.ObtenerLista();
            var listaPlanViewModel = new List<PlanTuristicoViewModel>();
            foreach (var item in listaPlanesTuristicos)
            {
                listaPlanViewModel.Add(_helperMap.MapPlanTuristicoViewModel(item));
            }
            return listaPlanViewModel;
        }

        public ActionResult _Create()
        {
            try
            {
                var proveedores = _proveedor.ObtenerLista();
                ViewBag.ProveedorId = new SelectList(proveedores, "Id", "NombreORazonSocial");
                var planes = _plan.ObtenerLista();
                ViewBag.PlanId = new SelectList(planes, "Id", "Descripcion");
                return PartialView();
            }
            catch (Exception ex)
            {
                return MyJsonResult(GetAllExeption(ex));
            }
        }

        [HttpPost]
        public ActionResult _Create(PlanTuristicoViewModel planTuristico)
        {
            try
            {
                var plan = _plan.Obtener(planTuristico.PlanId);
                planTuristico.Codigo = plan.Codigo;
                planTuristico.Descripcion = plan.Descripcion;
                planTuristico.DestinoId = plan.DestinoId;
                planTuristico.Hotel = plan.Hotel;
                if(planTuristico.Observacion == "")
                    planTuristico.Observacion = plan.Observacion;

                if (!ModelState.IsValid)
                {
                    var modelState = ModelState.Values.Where(a => a.Errors.Count > 0).First();
                    var mensaje = modelState.Errors.FirstOrDefault().ErrorMessage;

                    return MyJsonResult(mensaje);
                }
                var planesTuristico = _planTuristico.ObtenerQueryable()
                    .FirstOrDefault(x => x.Descripcion == planTuristico.Descripcion && x.FechaSalida == planTuristico.FechaSalida && x.Fecharegreso == planTuristico.FechaRegreso);
                if (planesTuristico != null)
                {
                    return MyJsonResult("Ya existe un Plan con la misma descripción para las mismas fechas de salida y de regreso, por favor corregir. Gracias!");
                }
                if (planTuristico.FechaSalida > planTuristico.FechaRegreso)
                {
                    return MyJsonResult("La fecha de salida no puede ser mayor que la fecha de regreso, por favor corregir. Gracias!");

                }
                var entidadPlan = _helperMap.MapPlanTuristicoModel(planTuristico);
                var usr = ObtenerUsuarioLogueado();
                entidadPlan.CreadoPor = usr.UserName;
                entidadPlan.FechaCreacion = DateTime.Now;
                var PlanId =_planTuristico.AgregarPlanTuristico(entidadPlan, plan);
                
                
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
                var proveedores = _proveedor.ObtenerLista();
                ViewBag.ProveedorId = new SelectList(proveedores, "Id", "NombreORazonSocial");
                var destinos = _destino.ObtenerLista();
                ViewBag.DestinoId = new SelectList(destinos, "Id", "Descripcion");
                var entidad = _planTuristico.ObtenerQueryable().FirstOrDefault(x => x.Id == id);
                var editViewModel = _helperMap.MapPlanTuristicoViewModel(entidad);

                return PartialView(editViewModel);
            }
            catch (Exception ex)
            {
                return MyJsonResult(GetAllExeption(ex));
            }
        }
        [HttpPost]
        public ActionResult _Edit(PlanTuristicoViewModel model)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    var modelState = ModelState.Values.Where(a => a.Errors.Count > 0).First();
                    var mensaje = modelState.Errors.FirstOrDefault().ErrorMessage;

                    return MyJsonResult(mensaje);
                }

                var planBD = _planTuristico.ObtenerQueryable().FirstOrDefault(x => x.Descripcion == model.Descripcion && x.FechaSalida == model.FechaSalida && x.Fecharegreso == model.FechaRegreso && x.Id != model.Id);
                if (planBD != null)
                {
                    return MyJsonResult("Ya existe un Plan Turistico con la misma descripción para las mismas fechas de salida y regreso, por favor corregir. Gracias!");
                }
                var planBD1 = _planTuristico.ObtenerQueryable().FirstOrDefault(x => x.Codigo == model.Codigo && x.Id != model.Id);
                if (planBD1 != null)
                {
                    return MyJsonResult("Ya existe un Plan con el mismo código, por favor corregir. Gracias!");
                }
                var entidadPlan = _helperMap.MapPlanTuristicoModel(model);
                var usr = ObtenerUsuarioLogueado();
                entidadPlan.ModificadoPor = usr.UserName;
                entidadPlan.FechaModificacion = DateTime.Now;
                _planTuristico.Actualizar(entidadPlan);
                _planTuristico.ActualizarValoresPlanTuristico(model.Id);

                return new HttpStatusCodeResult(HttpStatusCode.NoContent);

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
                var entidad = _planTuristico.ObtenerQueryable().FirstOrDefault(x => x.Id == id);
                var editViewModel = _helperMap.MapPlanTuristicoViewModel(entidad);

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
                var planTuristico = _planTuristico.ObtenerQueryable().FirstOrDefault(x => x.Id == id);

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
                var entidad = _planTuristico.ObtenerQueryable().FirstOrDefault(x => x.Id == id);
                _planTuristico.Eliminar(entidad);
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return MyJsonResult(GetAllExeption(ex));
            }
        }

        #region Incluye

        [AllowJsonGet]
        public ActionResult Read_DetallePlanTuristico([DataSourceRequest] DataSourceRequest request, int planTuristicoId)
        {
            try
            {
                var detallePlanTuristico = _detallePlanTuristico.ObtenerQueryable().Where(x => x.PlanTuristicoId == planTuristicoId).ToList();
                var model = new List<DetallePlanTuristicoViewModel>();
                foreach (var item in detallePlanTuristico)
                {
                    model.Add(_helperMap.MapDetallePlanTuristicoViewModel(item));
                }
                return ConstruirResultado(model.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                return MyJsonResult(GetAllExeption(ex));
            }
        }

        // GET: DeltaOperacion/Create
        public ActionResult _CreateDetallePlanTuristicoGrid(int? planTuristicoId)
        {
            try
            {
                var viewModel = new PlanTuristicoViewModel();

                var id = planTuristicoId == null ? 0 : planTuristicoId.Value;
                if (id != 0)
                {
                    var entidad = _planTuristico.Obtener(id);
                    viewModel = _helperMap.MapPlanTuristicoViewModel(entidad);
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
        public ActionResult _CreateDetallePlanTuristicoGrid(PlanTuristicoViewModel planTuristicoViewmodel)
        {
            try
            {
                var usr = ObtenerUsuarioLogueado();
                var datosBD = _detallePlanTuristico.ObtenerQueryable().Where(x => x.PlanTuristicoId == planTuristicoViewmodel.Id).ToList();

                var listaDetallePlanTuristico = (from bd in datosBD
                                                 join dp in planTuristicoViewmodel.DetallePlanTuristico.ToList() on bd.Id equals dp.Id into detaJoin
                                     from detPlan in detaJoin.DefaultIfEmpty()
                                     select new { Detalle = bd, Seleccionado = detPlan != null }).ToList();
                
                foreach (var item in planTuristicoViewmodel.DetallePlanTuristico)
                {
                    var registroBD = datosBD?.FirstOrDefault(x => x.Codigo == item.Codigo);
                    if (registroBD == null)
                    {
                        var entidad = _helperMap.MapDetallePlanTuristicoModel(item);
                        entidad.PlanTuristicoId = planTuristicoViewmodel.Id;
                        entidad.FechaCreacion = DateTime.Now;
                        entidad.CreadoPor = usr.UserName;
                        var includId = _detallePlanTuristico.Agregar(entidad);
                    }
                    else
                    {
                        registroBD.ModificadoPor = usr.UserName;
                        registroBD.FechaModificacion = DateTime.Now;
                        registroBD.Codigo = item.Codigo;
                        registroBD.Descripcion = item.Descripcion;
                        registroBD.CostoAdulto = item.CostoAdulto;
                        registroBD.CostoMenor = item.CostoMenor;
                        registroBD.CostoInfante = item.CostoInfante;
                        registroBD.ValorAdulto = item.ValorAdulto;
                        registroBD.ValorMenor = item.ValorMenor;
                        registroBD.ValorInfante = item.ValorInfante;

                        _detallePlanTuristico.Actualizar(registroBD);
                    }
                }
                _planTuristico.ActualizarValoresPlanTuristico(planTuristicoViewmodel.Id);
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return MyJsonResult(GetAllExeption(ex));
            }
        }
        #endregion
        #region Incluye Detalle

        [AllowJsonGet]
        public ActionResult Read_ItemDetallePlanTuristico([DataSourceRequest] DataSourceRequest request, int detallePlanTuristicoId)
        {
            try
            {
                var itemDetallePlanTuristico = _itemDetallePlanTuristico.ObtenerQueryable().Where(x => x.DetallePlanTuristicoId == detallePlanTuristicoId).ToList();
                var model = new List<ItemDetallePlanTuristicoViewModel>();
                foreach (var item in itemDetallePlanTuristico)
                {
                    model.Add(_helperMap.MapItemDetallePlanesTuristicosViewModel(item));
                }
                return ConstruirResultado(model.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                return MyJsonResult(GetAllExeption(ex));
            }
        }

        // GET: DeltaOperacion/Create
        public ActionResult _CreateItemDetallePlanTuristicoGrid(int? detallePlanTuristicoId)
        {
            try
            {
                var viewModel = new DetallePlanTuristicoViewModel();

                var id = detallePlanTuristicoId == null ? 0 : detallePlanTuristicoId.Value;
                if (id != 0)
                {
                    var entidad = _detallePlanTuristico.Obtener(id);
                    viewModel = _helperMap.MapDetallePlanTuristicoViewModel(entidad);
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
        public ActionResult _CreateItemDetallePlanTuristicoGrid(DetallePlanTuristicoViewModel detallePlanTuristicoViewModel)
        {
            try
            {
                var detallePlanTuristicoViewmodel = detallePlanTuristicoViewModel.ItemDetallePlanTuristico.ToList();
                var id = detallePlanTuristicoViewModel.Id;
                
                var usr = ObtenerUsuarioLogueado();
                var datosBD = _itemDetallePlanTuristico.ObtenerQueryable().Where(x => x.DetallePlanTuristicoId == id).ToList();
                foreach (var item in detallePlanTuristicoViewmodel)
                {
                    
                    var registroBD = datosBD?.FirstOrDefault(x => x.Codigo == item.Codigo);
                    if (registroBD == null)
                    {
                        var itemDetallePlanTuristico = new ItemDetallePlanTuristicoViewModel
                        {
                            DetallePlanTuristicoId = id,
                            Codigo = item.Codigo,
                            Descripcion = item.Descripcion,
                            CostoAdulto = item.CostoAdulto,
                            CostoMenor = item.CostoMenor,
                            CostoInfante = item.CostoInfante,
                            ValorAdulto = item.ValorAdulto,
                            ValorMenor = item.ValorMenor,
                            ValorInfante = item.ValorInfante
                        };
                        var entidad = _helperMap.MapItemDetallePlanTuristicoModel(itemDetallePlanTuristico);
                        entidad.DetallePlanTuristicoId = id;
                        entidad.FechaCreacion = DateTime.Now;
                        entidad.CreadoPor = usr.UserName;
                        var includId = _itemDetallePlanTuristico.Agregar(entidad);
                    }
                    else
                    {
                        registroBD.ModificadoPor = usr.UserName;
                        registroBD.FechaModificacion = DateTime.Now;
                        registroBD.Codigo = item.Codigo;
                        registroBD.Descripcion = item.Descripcion;
                        registroBD.CostoAdulto = item.CostoAdulto;
                        registroBD.CostoMenor = item.CostoMenor;
                        registroBD.CostoInfante = item.CostoInfante;
                        registroBD.ValorAdulto = item.ValorAdulto;
                        registroBD.ValorMenor = item.ValorMenor;
                        registroBD.ValorInfante = item.ValorInfante;
                        _itemDetallePlanTuristico.Actualizar(registroBD);
                    }
                }
                _detallePlanTuristico.ActualizarValoresDetallePlanTuristico(detallePlanTuristicoViewModel.Id);
                var planTuristicoId = _detallePlanTuristico.Obtener(detallePlanTuristicoViewModel.Id).PlanTuristicoId;
                _planTuristico.ActualizarValoresPlanTuristico(planTuristicoId);
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