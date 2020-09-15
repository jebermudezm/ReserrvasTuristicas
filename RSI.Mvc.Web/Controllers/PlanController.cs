using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
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
    public class PlanController : dbController
    {
        #region Variables
        private readonly IPlanRepositorio _Plan;
        private readonly IProveedorRepositorio _proveedor;
        private readonly IDestinoRepositorio _destino;
        private readonly IIncluyeRepositorio _Incluye;
        private readonly IDetalleIncluyeRepositorio _detalleIncluye;

        #endregion
        #region Constructor
        public PlanController()
        {
            _Plan = new PlanRepositorio(_context);
            _proveedor = new ProveedorRepositorio(_context);
            _destino = new DestinoRepositorio(_context);
            _Incluye = new IncluyeRepositorio(_context);
            _detalleIncluye = new DetalleIncluyeRepositorio(_context);
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
                var listaPlans = _Plan.ObtenerLista();
                var listaPlanViewModel = ObtenerPlanes();
                return ConstruirResultado(listaPlanViewModel.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                return MyJsonResult($"se presentó el siguiente error: {ex.Message}");
            }
        }

        public List<PlanViewModel> ObtenerPlanes()
        {
            var listaPlans = _Plan.ObtenerLista();
            var listaPlanViewModel = new List<PlanViewModel>();
            foreach (var item in listaPlans)
            {
                listaPlanViewModel.Add(_helperMap.MapPlanViewModel(item));
            }
            return listaPlanViewModel;
        }

        public ActionResult _Create()
        {
            try
            {
                var proveedores = _proveedor.ObtenerLista();
                ViewBag.ProveedorId = new SelectList(proveedores, "Id", "NombreORazonSocial");
                var destinos = _destino.ObtenerLista();
                ViewBag.DestinoId = new SelectList(destinos, "Id", "Descripcion");
                return PartialView();
            }
            catch (Exception ex)
            {
                return MyJsonResult(GetAllExeption(ex));
            }
        }

        [HttpPost]
        public ActionResult _Create(PlanViewModel plan)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var modelState = ModelState.Values.Where(a => a.Errors.Count > 0).First();
                    var mensaje = modelState.Errors.FirstOrDefault().ErrorMessage;

                    return MyJsonResult(mensaje);
                }
                var exite = _Plan.ObtenerQueryable().Any(x => x.Descripcion == plan.Descripcion);
                if (exite)
                {
                    return MyJsonResult("Ya existe un Plan con la misma descripción, por favor corregir. Gracias!");
                }
                var entidadPlan = _helperMap.MapPlanModel(plan);
                var usr = ObtenerUsuarioLogueado();
                entidadPlan.CreadoPor = usr.UserName;
                entidadPlan.FechaCreacion = DateTime.Now;
                var PlanId =_Plan.Agregar(entidadPlan);
                
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
                var destinos = _destino.ObtenerLista();
                ViewBag.DestinoId = new SelectList(destinos, "Id", "Descripcion");
                var entidad = _Plan.ObtenerQueryable().FirstOrDefault(x => x.Id == id);
                var editViewModel = _helperMap.MapPlanViewModel(entidad);

                return PartialView(editViewModel);
            }
            catch (Exception ex)
            {
                return MyJsonResult(GetAllExeption(ex));
            }
        }
        [HttpPost]
        public ActionResult _Edit(PlanViewModel model)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    var modelState = ModelState.Values.Where(a => a.Errors.Count > 0).First();
                    var mensaje = modelState.Errors.FirstOrDefault().ErrorMessage;

                    return MyJsonResult(mensaje);
                }

                var planBD = _Plan.ObtenerQueryable().FirstOrDefault(x => x.Descripcion == model.Descripcion && x.Id != model.Id);
                if (planBD != null)
                {
                    return MyJsonResult("Ya existe un Plan con la misma descripción. para las mismas fechas, por favor corregir. Gracias!");
                }
                var planBD1 = _Plan.ObtenerQueryable().FirstOrDefault(x => x.Codigo == model.Codigo && x.Id != model.Id);
                if (planBD1 != null)
                {
                    return MyJsonResult("Ya existe un Plan con el mismo código, por favor corregir. Gracias!");
                }
                var entidadPlan = _helperMap.MapPlanModel(model);
                var usr = ObtenerUsuarioLogueado();
                entidadPlan.ModificadoPor = usr.UserName;
                entidadPlan.FechaModificacion = DateTime.Now;
                _Plan.Actualizar(entidadPlan);

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
                var entidad = _Plan.ObtenerQueryable().FirstOrDefault(x => x.Id == id);
                var editViewModel = _helperMap.MapPlanViewModel(entidad);

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
                var motivoEvento = _Plan.ObtenerQueryable().FirstOrDefault(x => x.Id == id);

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
                var entidad = _Plan.ObtenerQueryable().FirstOrDefault(x => x.Id == id);
                _Plan.Eliminar(entidad);
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return MyJsonResult(GetAllExeption(ex));
            }
        }

        #region Incluye

        [AllowJsonGet]
        public ActionResult Read_Includes([DataSourceRequest] DataSourceRequest request, int planId)
        {
            try
            {
                var includs = _Incluye.ObtenerQueryable().Where(x => x.PlanId == planId).ToList();
                var model = new List<IncluyeViewModel>();
                foreach (var item in includs)
                {
                    model.Add(_helperMap.MapIncluyeViewModel(item));
                }
                return ConstruirResultado(model.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                return MyJsonResult(GetAllExeption(ex));
            }
        }

        // GET: DeltaOperacion/Create
        public ActionResult _CreateIncludeGrid(int? planId)
        {
            try
            {
                var viewModel = new PlanViewModel();

                var id = planId == null ? 0 : planId.Value;
                if (id != 0)
                {
                    var entidad = _Plan.Obtener(id);
                    viewModel = _helperMap.MapPlanViewModel(entidad);
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
        public ActionResult _CreateIncludeGrid(PlanViewModel planViewmodel)
        {
            try
            {
                var usr = ObtenerUsuarioLogueado();
                var datosBD = _Incluye.ObtenerQueryable().Where(x => x.PlanId == planViewmodel.Id).ToList();
 
                foreach (var item in planViewmodel.Incluye)
                {
                    var registroBD = datosBD?.FirstOrDefault(x => x.Codigo == item.Codigo);
                    if (registroBD == null)
                    {
                        var entidad = _helperMap.MapIncluyeModel(item);
                        entidad.PlanId = planViewmodel.Id;
                        entidad.FechaCreacion = DateTime.Now;
                        entidad.CreadoPor = usr.UserName;
                        var includId = _Incluye.Agregar(entidad);
                    }
                    else
                    {
                        registroBD.ModificadoPor = usr.UserName;
                        registroBD.FechaModificacion = DateTime.Now;
                        registroBD.Codigo = item.Codigo;
                        registroBD.Descripcion = item.Descripcion;
                        _Incluye.Actualizar(registroBD);
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
        #region Incluye Detalle

        [AllowJsonGet]
        public ActionResult Read_DetalleIncluye([DataSourceRequest] DataSourceRequest request, int incluyeId)
        {
            try
            {
                var detalleInclud = _detalleIncluye.ObtenerQueryable().Where(x => x.IncluyeId == incluyeId).ToList();
                var model = new List<DetalleIncluyeViewModel>();
                foreach (var item in detalleInclud)
                {
                    model.Add(_helperMap.MapDetalleIncluyeViewModel(item));
                }
                return ConstruirResultado(model.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                return MyJsonResult(GetAllExeption(ex));
            }
        }

        // GET: DeltaOperacion/Create
        public ActionResult _CreateDetalleIncludeGrid(int? incluyeId)
        {
            try
            {
                var viewModel = new IncluyeViewModel();

                var id = incluyeId == null ? 0 : incluyeId.Value;
                if (id != 0)
                {
                    var entidad = _Incluye.Obtener(id);
                    viewModel = _helperMap.MapIncluyeViewModel(entidad);
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
        public ActionResult _CreateDetalleIncludeGrid(PlanViewModel planViewModel)
        {
            try
            {
                var includViewmodel = planViewModel.Incluye.ToList();
                var id = planViewModel.Id;
                
                var usr = ObtenerUsuarioLogueado();
                var datosBD = _detalleIncluye.ObtenerQueryable().Where(x => x.IncluyeId == id).ToList();
                foreach (var item in includViewmodel)
                {
                    var detalleIncluye = new DetalleIncluyeViewModel
                    {
                        IncluyeId = id,
                        Codigo = item.Codigo,
                        Descripcion = item.Descripcion
                    };
                    var registroBD = datosBD?.FirstOrDefault(x => x.Codigo == item.Codigo);
                    if (registroBD == null)
                    {
                        var entidad = _helperMap.MapDetalleIncluyeModel(detalleIncluye);
                        entidad.IncluyeId = id;
                        entidad.FechaCreacion = DateTime.Now;
                        entidad.CreadoPor = usr.UserName;
                        var includId = _detalleIncluye.Agregar(entidad);
                    }
                    else
                    {
                        registroBD.ModificadoPor = usr.UserName;
                        registroBD.FechaModificacion = DateTime.Now;
                        registroBD.Codigo = item.Codigo;
                        registroBD.Descripcion = item.Descripcion;
                        _detalleIncluye.Actualizar(registroBD);
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