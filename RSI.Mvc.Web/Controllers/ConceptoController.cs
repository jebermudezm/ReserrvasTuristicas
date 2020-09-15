namespace RSI.Mvc.Web.Controllers
{
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using RSI.Modelo.RepositorioCont;
    using RSI.Modelo.RepositorioImpl;
    using RSI.Mvc.Web.Controllers.Helper;
    using RSI.Mvc.Web.ViewModel;
    using System;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    public class ConceptoController : dbController
    {
        #region Properties
        private readonly IConceptoRepositorio _conceptoRepositorio;
        private readonly IConceptoValorRepositorio _conceptoValorRepositorio;
        #endregion

        #region Constructor
        public ConceptoController()
        {
            _conceptoRepositorio = new ConceptoRepositorio(_context);
            _conceptoValorRepositorio = new ConceptoValorRepositorio(_context);
        }
        #endregion

        #region Views

        public ActionResult Index()
        {
            var user = ObtenerUsuarioLogueado();
            if (user == null)
                return RedirectToAction("Login", "SegUsuario");
            return View();
        }

        // GET: DeltaOperacion/Create
        public ActionResult _CreateValorGrid(int? ConceptoId)
        {
            try
            {
                var viewModel = new ConceptoViewModel();

                var id = ConceptoId == null ? 0 : ConceptoId.Value;
                if (id != 0)
                {
                    var entidad = _conceptoRepositorio.Obtener(id);
                    viewModel = _helperMap.MapConeptoViewModel(entidad);
                }
                else
                {
                    return MyJsonResult("Los includ deben ir asociados a un concepto.");
                }

                return PartialView(viewModel);
            }
            catch (Exception ex)
            {
                return MyJsonResult(GetAllExeption(ex));
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Metodo para consultar desde el grid de kendo ui
        /// </summary>
        /// <param name="request">request</param>
        /// <param name="id_entidad">id_entidad</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        public ActionResult Consultar([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                var resultado = _conceptoRepositorio.ObtenerLista().Select(_helperMap.MapConeptoViewModel);
                return Json(resultado.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Metodo para crear un nuevo registro
        /// </summary>
        /// <param name="request">Parametros kendo grid</param>
        /// <param name="modelo">Modelo de datos a insertar</param>
        /// <returns>Modelo insertado</returns>
        [HttpPost]
        public ActionResult Crear([DataSourceRequest] DataSourceRequest request, ConceptoViewModel modelo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entidad = _helperMap.MapConceptoModel(modelo);
                    var usr = ObtenerUsuarioLogueado();
                    entidad.CreadoPor = usr.UserName;
                    entidad.FechaCreacion = DateTime.Now;
                    var resultado = _conceptoRepositorio.Agregar(entidad);
                    modelo.Id = resultado;
                }
                return Json(new[] { modelo }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        ///  Metodo para actualizar un registro especifico
        /// </summary>
        /// <param name="request">request</param>
        /// <param name="modelo">modelo</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Actualizar([DataSourceRequest] DataSourceRequest request, ConceptoViewModel modelo)
        {
            try
            {
                var entidad = _helperMap.MapConceptoModel(modelo);
                var usr = ObtenerUsuarioLogueado();
                    entidad.ModificadoPor = usr.UserName;
                entidad.FechaModificacion = DateTime.Now;
                _conceptoRepositorio.Actualizar(entidad);
                return Json(new[] { modelo }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Metodo para eliminar            
        /// </summary>
        /// <param name="request">Parametros kendo grid</param>
        /// <param name="model">Modelo de datos a eliminar</param>
        /// <returns>Modelo eliminado</returns>
        [HttpPost]
        public ActionResult Eliminar([DataSourceRequest] DataSourceRequest request, ConceptoViewModel model)
        {
            try
            {
                var entidad = _conceptoRepositorio.ObtenerQueryable().FirstOrDefault(x => x.Id == model.Id);
                _conceptoRepositorio.Eliminar(entidad);
                return Json(new[] { model }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Metodo para exportar a excel o pdf
        /// </summary>
        /// <param name="contentType"></param>
        /// <param name="base64"></param>
        /// <param name="fileName">Nombre de archivo</param>
        /// <returns>Archivo</returns>
        [HttpPost]
        public ActionResult Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);
            return File(fileContents, contentType, fileName);
        }

        #endregion

        #region CRUD

        // POST: DeltaOperacion/Create
        [HttpPost]
        public ActionResult _CreateValorGrid(ConceptoViewModel conceptoViewModel)
        {
            try
            {
                var usr = ObtenerUsuarioLogueado();
                var datosBD = _conceptoValorRepositorio.ObtenerQueryable().Where(x => x.ConceptoId == conceptoViewModel.Id).ToList();

                foreach (var item in conceptoViewModel.ConceptoValor)
                {
                    var registroBD = datosBD?.FirstOrDefault(x => x.Valor == item.Valor);
                    if (registroBD == null)
                    {
                        var entidad = _helperMap.MapConceptoValorModel(item);
                        entidad.ConceptoId = conceptoViewModel.Id;
                        entidad.FechaCreacion = DateTime.Now;
                        entidad.CreadoPor = usr.UserName;
                        var ConceptoValorId = _conceptoValorRepositorio.Agregar(entidad);
                    }
                    else
                    {
                        registroBD.ModificadoPor = usr.UserName;
                        registroBD.FechaModificacion = DateTime.Now;
                        registroBD.Valor = item.Valor;
                        registroBD.Observacion = item.Observacion;
                        _conceptoValorRepositorio.Actualizar(registroBD);
                    }
                }
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return MyJsonResult(GetAllExeption(ex));
            }
        }

        /// <summary>
        /// Metodo para consultar desde el grid de kendo ui
        /// </summary>
        /// <param name="request">request</param>
        /// <param name="id_configuracion">id_configuracion</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ConsultarDetalle([DataSourceRequest] DataSourceRequest request, int ConceptoId)
        {
            try
            {
                var conceptoDetalle = _conceptoValorRepositorio.ObtenerQueryable().Where(x => x.ConceptoId == ConceptoId).Select(_helperMap.MapConeptoValorViewModel);
                return Json(conceptoDetalle.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Metodo para crear un nuevo registro
        /// </summary>
        /// <param name="request">Parametros kendo grid</param>
        /// <param name="modelo">Modelo de datos a insertar</param>
        /// <returns>Modelo insertado</returns>
        [HttpPost, ErroresApi]
        public ActionResult CrearDetalle([DataSourceRequest] DataSourceRequest request, ConceptoValorViewModel modelo, int Concepto_Id)
        {
            try
            {
             
                modelo.ConceptoId  = Concepto_Id;
                var entidad = _helperMap.MapConceptoValorModel(modelo);
                var usr = ObtenerUsuarioLogueado();
                entidad.CreadoPor = usr.UserName;
                entidad.FechaCreacion = DateTime.Now;
                var resultado = _conceptoValorRepositorio.Agregar(entidad);
                modelo.Id = resultado;

                return Json(new[] { modelo }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Metodo para eliminar            
        /// </summary>
        /// <param name="request">Parametros kendo grid</param>
        /// <param name="model">Modelo de datos a eliminar</param>
        /// <returns>Modelo eliminado</returns>
        [HttpPost]
        public ActionResult EliminarDetalle([DataSourceRequest] DataSourceRequest request, ConceptoValorViewModel model)
        {
            try
            {
                _conceptoValorRepositorio.Eliminar(_helperMap.MapConceptoValorModel(model));
                return Json(new[] { model }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
