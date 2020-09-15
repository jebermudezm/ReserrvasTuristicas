using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using RSI.Modelo.RepositorioCont;
using RSI.Modelo.RepositorioImpl;
using RSI.Mvc.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace RSI.Mvc.Web.Controllers
{
    public class ConvenioController : dbController
    {
        #region Variables
        private readonly IConvenioRepositorio _Convenio;

        #endregion
        #region Constructor
        public ConvenioController()
        {
            _Convenio = new ConvenioRepositorio(_context);
        }
        #endregion

        // GET: Convenio
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
                var listaConvenios = _Convenio.ObtenerLista();
                var listaConvenioViewModel = ObtenerConvenioes();
                return ConstruirResultado(listaConvenioViewModel.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                return MyJsonResult($"se presentó el siguiente error: {ex.Message}");
            }
        }

        public List<ConvenioViewModel> ObtenerConvenioes()
        {
            var listaConvenios = _Convenio.ObtenerLista();
            var listaConvenioViewModel = new List<ConvenioViewModel>();
            foreach (var item in listaConvenios)
            {
                listaConvenioViewModel.Add(_helperMap.MapConvenioViewModel(item));
            }
            return listaConvenioViewModel;
        }

        public ActionResult _Create()
        {
            try
            {
                return PartialView();
            }
            catch (Exception ex)
            {
                return MyJsonResult(GetAllExeption(ex));
            }
        }

        [HttpPost]
        public ActionResult _Create(ConvenioViewModel convenio)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var modelState = ModelState.Values.Where(a => a.Errors.Count > 0).First();
                    var mensaje = modelState.Errors.FirstOrDefault().ErrorMessage;

                    return MyJsonResult(mensaje);
                }
                var client = _Convenio.ObtenerQueryable().FirstOrDefault(x => x.Nombre == convenio.Nombre);
                if (client != null)
                {
                    return MyJsonResult("Ya existe un Convenio con la misma descripción, por favor corregir. Gracias!");
                }
                var entidadConvenio = _helperMap.MapConvenioModel(convenio);
                var usr = ObtenerUsuarioLogueado();
                entidadConvenio.CreadoPor = usr.UserName;
                entidadConvenio.FechaCreacion = DateTime.Now;
                var ConvenioId =_Convenio.Agregar(entidadConvenio);
                
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
                var entidad = _Convenio.ObtenerQueryable().FirstOrDefault(x => x.Id == id);
                var editViewModel = _helperMap.MapConvenioViewModel(entidad);

                return PartialView(editViewModel);
            }
            catch (Exception ex)
            {
                return MyJsonResult(GetAllExeption(ex));
            }
        }
        [HttpPost]
        public ActionResult _Edit(ConvenioViewModel model)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    var modelState = ModelState.Values.Where(a => a.Errors.Count > 0).First();
                    var mensaje = modelState.Errors.FirstOrDefault().ErrorMessage;

                    return MyJsonResult(mensaje);
                }

                var ConvenioBD = _Convenio.ObtenerQueryable().FirstOrDefault(x => x.Nombre == model.Nombre && x.Id != model.Id);
                if (ConvenioBD != null)
                {
                    return MyJsonResult("Ya existe un Convenio con el mismo nombre, por favor corregir. Gracias!");
                }
                
                var entidadConvenio = _helperMap.MapConvenioModel(model);
                var usr = ObtenerUsuarioLogueado();
                entidadConvenio.ModificadoPor = usr.UserName;
                entidadConvenio.FechaModificacion = DateTime.Now;
                _Convenio.Actualizar(entidadConvenio);

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
                var entidad = _Convenio.ObtenerQueryable().FirstOrDefault(x => x.Id == id);
                var editViewModel = _helperMap.MapConvenioViewModel(entidad);

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
                var motivoEvento = _Convenio.ObtenerQueryable().FirstOrDefault(x => x.Id == id);

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
                var entidad = _Convenio.ObtenerQueryable().FirstOrDefault(x => x.Id == id);
                _Convenio.Eliminar(entidad);
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return MyJsonResult(GetAllExeption(ex));
            }
        }
    }
}