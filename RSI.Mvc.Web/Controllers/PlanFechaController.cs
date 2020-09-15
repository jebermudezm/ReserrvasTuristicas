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
    public class PlanFechaController : dbController
    {
        #region Variables
        private readonly IPlanRepositorio _plan;
        private readonly IProveedorRepositorio _proveedor;
        private readonly IPlanFechaRepositorio _planFecha;
        private readonly ICuposAcomodacionRepositorio _cuposAcomodacion;
        private readonly IConceptoRepositorio _concepto;
        private readonly IConceptoValorRepositorio _conceptoValor;

        #endregion
        #region Constructor
        public PlanFechaController()
        {
            _plan = new PlanRepositorio(_context);
            _proveedor = new ProveedorRepositorio(_context);
            _planFecha = new PlanFechaRepositorio(_context);
            _cuposAcomodacion = new CuposAcomodacionRepositorio(_context);
            _concepto = new ConceptoRepositorio(_context);
            _conceptoValor = new ConceptoValorRepositorio(_context);
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
                var listaPlanFechaViewModel = ObtenerPlanes();
                return ConstruirResultado(listaPlanFechaViewModel.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                return MyJsonResult($"se presentó el siguiente error: {ex.Message}");
            }
        }

        public List<PlanFechaViewModel> ObtenerPlanes()
        {
            var listaPlanesFecha = _planFecha.ObtenerLista();
            var resultado = new List<PlanFechaViewModel>();
            foreach (var item in listaPlanesFecha)
            {
                var listaAcomodacion = item.CuposAcomodacion.ToList();

                var acomodacionViewModel = listaAcomodacion.Select(s => new CuposAcomodacionViewModel
                    {
                        Cantidad = s.Cantidad,
                        Acomodacion = s.ConceptoValor.Valor,
                        ConceptoValorId = s.ConceptoValorId,
                        CreadoPor = s.CreadoPor,
                        FechaCreacion = s.FechaCreacion,
                        FechaModificacion = s.FechaModificacion,
                        Id = s.Id,
                        ModificadoPor = s.ModificadoPor,
                        Observacion = s.Observacion,
                        PlanFechaId = s.PlanFechaId,
                        Valor = s.Valor
                    }).ToList();
                resultado.Add(new PlanFechaViewModel
                {
                    CuposAcomodacion = acomodacionViewModel,
                    Codigo = item.Codigo,
                    CreadoPor = item.CreadoPor,
                    FechaCreacion = item.FechaCreacion,
                    FechaSalida = item.FechaSalida,
                    FechaModificacion = item.FechaModificacion,
                    FechaRegreso = item.FechaRegreso,
                    Id = item.Id,
                    ModificadoPor = item.ModificadoPor,
                    Nombre = item.Nombre,
                    Observacion = item.Observacion,
                    PlanId = item.PlanId,
                    ProveedorId = item.ProveedorId,
                    Plan = item.Plan.Descripcion,
                    Proveedor = item.Proveedor.NombreORazonSocial
                });
            }
            return resultado;
        }

        public ActionResult _Create()
        {
            try
            {
                var planes = _plan.ObtenerLista();
                var proveedores = _proveedor.ObtenerLista();
                ViewBag.ProveedorId = new SelectList(proveedores, "Id", "NombreORazonSocial");
                ViewBag.PlanId = new SelectList(planes, "Id", "Descripcion");
                return PartialView();
            }
            catch (Exception ex)
            {
                return MyJsonResult(GetAllExeption(ex));
            }
        }

        [HttpPost]
        public ActionResult _Create(PlanFechaViewModel plan)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var modelState = ModelState.Values.Where(a => a.Errors.Count > 0).First();
                    var mensaje = modelState.Errors.FirstOrDefault().ErrorMessage;

                    return MyJsonResult(mensaje);
                }
                var planBD = _planFecha.ObtenerQueryable().FirstOrDefault(x => x.Codigo == plan.Codigo && x.FechaSalida == plan.FechaSalida && x.FechaRegreso == plan.FechaRegreso);
                if (planBD != null)
                {
                    return MyJsonResult("Ya existe un Plan con la misma descripción para las mismas fechas, por favor corregir. Gracias!");
                }
                var entidadPlan = _helperMap.MapPlanFechaModel(plan);
                var usr = ObtenerUsuarioLogueado();
                entidadPlan.CreadoPor = usr.UserName;
                entidadPlan.FechaCreacion = DateTime.Now;
                var PlanId = _planFecha.Agregar(entidadPlan);

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
                var planes = _plan.ObtenerLista();
                var proveedores = _proveedor.ObtenerLista();
                ViewBag.ProveedorId = new SelectList(proveedores, "Id", "NombreORazonSocial");
                ViewBag.PlanId = new SelectList(planes, "Id", "Descripcion");

                var entidad = _planFecha.ObtenerQueryable().FirstOrDefault(x => x.Id == id);
                var editViewModel = new PlanFechaViewModel()
                {
                    Id = entidad.Id,
                    Observacion = entidad.Observacion,
                    ModificadoPor = entidad.ModificadoPor,
                    FechaModificacion = entidad.FechaModificacion,
                    FechaCreacion = entidad.FechaCreacion,
                    CreadoPor = entidad.CreadoPor,
                    Codigo = entidad.Codigo,
                    CuposAcomodacion = entidad.CuposAcomodacion.Select(s => new CuposAcomodacionViewModel()
                    {
                        Cantidad = s.Cantidad,
                        EntityConceptoValor = new ConceptoValorViewModel()
                        {
                            ConceptoId = s.ConceptoValor.ConceptoId,
                            Valor = s.ConceptoValor.Valor,
                            CreadoPor = s.ConceptoValor.CreadoPor,
                            FechaCreacion = s.ConceptoValor.FechaCreacion,
                            FechaModificacion = s.ConceptoValor.FechaModificacion,
                            ModificadoPor = s.ConceptoValor.ModificadoPor,
                            Observacion = s.ConceptoValor.Observacion,
                            Id = s.ConceptoValor.Id,
                        },
                        ConceptoValorId = s.ConceptoValorId,
                        CreadoPor = s.CreadoPor,
                        FechaCreacion = s.FechaCreacion,
                        FechaModificacion = s.FechaModificacion,
                        Id = s.Id,
                        Valor = s.Valor,
                        ModificadoPor = s.ModificadoPor,
                        Observacion = s.Observacion,
                        PlanFechaId = s.PlanFechaId,
                    }).ToList(),
                    FechaRegreso = entidad.FechaRegreso,
                    FechaSalida = entidad.FechaSalida,
                    Nombre = entidad.Nombre,
                    Plan = entidad.Plan.Descripcion,
                    PlanId = entidad.PlanId,
                    Proveedor = entidad.Proveedor.NombreORazonSocial,
                    ProveedorId = entidad.ProveedorId
                }; //_helperMap.MapPlanFechaViewModel(entidad);;

                return PartialView(editViewModel);
            }
            catch (Exception ex)
            {
                return MyJsonResult(GetAllExeption(ex));
            }
        }
        [HttpPost]
        public ActionResult _Edit(PlanFechaViewModel model)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    var modelState = ModelState.Values.Where(a => a.Errors.Count > 0).First();
                    var mensaje = modelState.Errors.FirstOrDefault().ErrorMessage;

                    return MyJsonResult(mensaje);
                }

                var planBD = _planFecha.ObtenerQueryable().FirstOrDefault(x => x.Codigo == model.Codigo && x.FechaSalida == model.FechaSalida && x.FechaRegreso == model.FechaRegreso && x.Id != model.Id);
                if (planBD != null)
                {
                    return MyJsonResult("Ya existe un Plan con la misma descripción para las mismas fechas, por favor corregir. Gracias!");
                }
                var entidadPlan = _helperMap.MapPlanFechaModel(model);
                var usr = ObtenerUsuarioLogueado();
                entidadPlan.ModificadoPor = usr.UserName;
                entidadPlan.FechaModificacion = DateTime.Now;
                _planFecha.Actualizar(entidadPlan);

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
                var entidad = _planFecha.ObtenerQueryable().FirstOrDefault(x => x.Id == id);
                var editViewModel  = new PlanFechaViewModel()
                {
                    Id = entidad.Id,
                    Observacion = entidad.Observacion,
                    ModificadoPor = entidad.ModificadoPor,
                    FechaModificacion = entidad.FechaModificacion,
                    FechaCreacion = entidad.FechaCreacion,
                    CreadoPor = entidad.CreadoPor,
                    Codigo = entidad.Codigo,
                    CuposAcomodacion = entidad.CuposAcomodacion.Select(s => new CuposAcomodacionViewModel()
                    {
                        Cantidad = s.Cantidad,
                        EntityConceptoValor = new ConceptoValorViewModel()
                        {
                            ConceptoId = s.ConceptoValor.ConceptoId,
                            Valor = s.ConceptoValor.Valor,
                            CreadoPor = s.ConceptoValor.CreadoPor,
                            FechaCreacion = s.ConceptoValor.FechaCreacion,
                            FechaModificacion = s.ConceptoValor.FechaModificacion,
                            ModificadoPor = s.ConceptoValor.ModificadoPor,
                            Observacion = s.ConceptoValor.Observacion,
                            Id = s.ConceptoValor.Id,
                        },
                        ConceptoValorId = s.ConceptoValorId,
                        CreadoPor = s.CreadoPor,
                        FechaCreacion = s.FechaCreacion,
                        FechaModificacion = s.FechaModificacion,
                        Id = s.Id,
                        Valor = s.Valor,
                        ModificadoPor = s.ModificadoPor,
                        Observacion = s.Observacion,
                        PlanFechaId = s.PlanFechaId,
                    }).ToList(),
                    FechaRegreso = entidad.FechaRegreso,
                    FechaSalida = entidad.FechaSalida,
                    Nombre = entidad.Nombre,
                    Plan = entidad.Plan.Descripcion,
                    PlanId = entidad.PlanId,
                    Proveedor = entidad.Proveedor.NombreORazonSocial,
                    ProveedorId = entidad.ProveedorId
                }; //_helperMap.MapPlanFechaViewModel(entidad);
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
                var motivoEvento = _planFecha.ObtenerQueryable().FirstOrDefault(x => x.Id == id);

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
                var entidad = _planFecha.ObtenerQueryable().FirstOrDefault(x => x.Id == id);
                _planFecha.Eliminar(entidad);
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return MyJsonResult(GetAllExeption(ex));
            }
        }

        #region Detalle (Cupos Acomodación)

        [AllowJsonGet]
        public ActionResult Read_Detalle([DataSourceRequest] DataSourceRequest request, int planFechaId)
        {
            try
            {
                var cuposAcomodacion = _cuposAcomodacion.ObtenerQueryable().Where(x => x.PlanFechaId == planFechaId).ToList();
                IEnumerable<CuposAcomodacionViewModel> lstModel = ObtenerCuposAcomodacion(cuposAcomodacion);

                return ConstruirResultado(lstModel.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                return MyJsonResult(GetAllExeption(ex));
            }
        }

        private static IEnumerable<CuposAcomodacionViewModel> ObtenerCuposAcomodacion(List<Modelo.Entidades.Maestros.CuposAcomodacion> cuposAcomodacion)
        {
            return cuposAcomodacion.Select(s => new CuposAcomodacionViewModel()
            {
                Cantidad = s.Cantidad,
                Acomodacion = s.ConceptoValor.Valor,
                EntityConceptoValor = new ConceptoValorViewModel()
                {
                    ConceptoId = s.ConceptoValor.ConceptoId,
                    Valor = s.ConceptoValor.Valor,
                    CreadoPor = s.ConceptoValor.CreadoPor,
                    FechaCreacion = s.ConceptoValor.FechaCreacion,
                    FechaModificacion = s.ConceptoValor.FechaModificacion,
                    ModificadoPor = s.ConceptoValor.ModificadoPor,
                    Observacion = s.ConceptoValor.Observacion,
                    Id = s.ConceptoValor.Id,
                },
                ConceptoValorId = s.ConceptoValorId,
                CreadoPor = s.CreadoPor,
                FechaCreacion = s.FechaCreacion,
                FechaModificacion = s.FechaModificacion,
                Id = s.Id,
                Valor = s.Valor,
                ModificadoPor = s.ModificadoPor,
                Observacion = s.Observacion,
                PlanFechaId = s.PlanFechaId,
            });
        }

        public JsonResult ObtenerAcomodacion()
        {
            var conceptoId = _concepto.ObtenerQueryable().FirstOrDefault(x => x.Codigo == "ACOMODACION")?.Id ?? 0;
            var registros = _conceptoValor.ObtenerQueryable().Where(x => x.ConceptoId == conceptoId).Select(_helperMap.MapConeptoValorViewModel).ToList();
            return Json(registros, JsonRequestBehavior.AllowGet);
        }

        // GET: DeltaOperacion/Create
        public ActionResult _CreateDetalleGrid(int? planFechaId)
        {
            try
            {
                var viewModel = new PlanFechaViewModel();

                var id = planFechaId == null ? 0 : planFechaId.Value;
                if (id != 0)
                {
                    var entidad = _planFecha.Obtener(id);

                    viewModel = new PlanFechaViewModel()
                    {
                        Id = entidad.Id,
                        Observacion = entidad.Observacion,
                        ModificadoPor = entidad.ModificadoPor,
                        FechaModificacion = entidad.FechaModificacion,
                        FechaCreacion = entidad.FechaCreacion,
                        CreadoPor = entidad.CreadoPor,
                        Codigo = entidad.Codigo,
                        CuposAcomodacion = entidad.CuposAcomodacion.Select(s => new CuposAcomodacionViewModel()
                        {
                            Cantidad = s.Cantidad,
                            EntityConceptoValor = new ConceptoValorViewModel()
                            {
                                ConceptoId = s.ConceptoValor.ConceptoId,
                                Valor = s.ConceptoValor.Valor,
                                CreadoPor = s.ConceptoValor.CreadoPor,
                                FechaCreacion = s.ConceptoValor.FechaCreacion,
                                FechaModificacion = s.ConceptoValor.FechaModificacion,
                                ModificadoPor = s.ConceptoValor.ModificadoPor,
                                Observacion = s.ConceptoValor.Observacion,
                                Id = s.ConceptoValor.Id,
                            },
                            ConceptoValorId = s.ConceptoValorId,
                            CreadoPor = s.CreadoPor,
                            FechaCreacion = s.FechaCreacion,
                            FechaModificacion = s.FechaModificacion,
                            Id = s.Id,
                            Valor = s.Valor,
                            ModificadoPor = s.ModificadoPor,
                            Observacion = s.Observacion,
                            PlanFechaId = s.PlanFechaId,
                        }).ToList(),
                        FechaRegreso = entidad.FechaRegreso,
                        FechaSalida = entidad.FechaSalida,
                        Nombre = entidad.Nombre,
                        Plan = entidad.Plan.Descripcion,
                        PlanId = entidad.PlanId,
                        Proveedor = entidad.Proveedor.NombreORazonSocial,
                        ProveedorId = entidad.ProveedorId
                    }; //_helperMap.MapPlanFechaViewModel(entidad);
                }
                else
                {
                    return MyJsonResult("Los includ deben ir asociados a un plan.");
                }
                var conceptoId = _concepto.ObtenerQueryable().FirstOrDefault(x => x.Codigo == "ACOMODACION")?.Id ?? 0;
                var acomodacion = _conceptoValor.ObtenerQueryable().Where(x => x.ConceptoId == conceptoId).Select(_helperMap.MapConeptoValorViewModel).First();
                ViewData["defaultConceptoValor"] = acomodacion;
                return PartialView(viewModel);
            }
            catch (Exception ex)
            {
                return MyJsonResult(GetAllExeption(ex));
            }
        }

        // POST: DeltaOperacion/Create
        [HttpPost]
        public ActionResult _CreateDetalleGrid(PlanFechaViewModel planFechaViewmodel)
        {
            try
            {
                var usr = ObtenerUsuarioLogueado();
                var datosBD = _cuposAcomodacion.ObtenerQueryable().Where(x => x.PlanFechaId == planFechaViewmodel.Id).ToList();

                foreach (var item in planFechaViewmodel.CuposAcomodacion)
                {
                    var registroBD = datosBD?.FirstOrDefault(x => x.Id == item.Id);
                    if (registroBD == null)
                    {
                        var entidad = _helperMap.MapCuposAcomodacionModel(item);
                        entidad.PlanFechaId = planFechaViewmodel.Id;
                        entidad.FechaCreacion = DateTime.Now;
                        entidad.CreadoPor = usr.UserName;
                        var includId = _cuposAcomodacion.Agregar(entidad);
                    }
                    else
                    {
                        registroBD.ModificadoPor = usr.UserName;
                        registroBD.FechaModificacion = DateTime.Now;
                        registroBD.Cantidad = item.Cantidad;
                        registroBD.Observacion = item.Observacion;
                        _cuposAcomodacion.Actualizar(registroBD);
                    }

                }
                var cuposAcomodacion = planFechaViewmodel.CuposAcomodacion.ToList();
                foreach (var item in datosBD)
                {
                    var existe = cuposAcomodacion.Any(x => x.ConceptoValorId == item.ConceptoValorId);
                    if (!existe)
                    {
                        _cuposAcomodacion.Eliminar(item);
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