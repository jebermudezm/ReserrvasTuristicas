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
    public class DestinoController : dbController
    {
        #region Variables
        private readonly IDestinoRepositorio _Destino;
        private readonly IListaRepositorio _documentoIdentidad;

        #endregion
        #region Constructor
        public DestinoController()
        {
            _Destino = new DestinoRepositorio(_context);
            _documentoIdentidad = new ListaRepositorio(_context);
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
                var listaDestinos = _Destino.ObtenerLista();
                var listaDestinoViewModel = ObtenerDestinos();
                return ConstruirResultado(listaDestinoViewModel.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                return MyJsonResult($"se presentó el siguiente error: {ex.Message}");
            }
        }

        public List<DestinoViewModel> ObtenerDestinos()
        {
            var listaDestinos = _Destino.ObtenerLista();
            var listaDestinoViewModel = new List<DestinoViewModel>();
            foreach (var item in listaDestinos)
            {
                listaDestinoViewModel.Add(_helperMap.MapDestinoViewModel(item));
            }
            return listaDestinoViewModel;
        }

        public ActionResult _Create()
        {
            try
            {
                var documentos = _documentoIdentidad.ObtenerLista();
                ViewBag.DocumentoIdentidadId = new SelectList(documentos, "Id", "Descripcion");
                return PartialView();
            }
            catch (Exception ex)
            {
                return MyJsonResult(GetAllExeption(ex));
            }
        }

        [HttpPost]
        public ActionResult _Create(DestinoViewModel Destino)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var modelState = ModelState.Values.Where(a => a.Errors.Count > 0).First();
                    var mensaje = modelState.Errors.FirstOrDefault().ErrorMessage;

                    return MyJsonResult(mensaje);
                }
                var client = _Destino.ObtenerQueryable().FirstOrDefault(x => x.Descripcion == Destino.Descripcion);
                if (client != null)
                {
                    return MyJsonResult("Ya existe un Destino con ese nombre, por favor corregir. Gracias!");
                }
                var entidadDestino = _helperMap.MapDestinoModel(Destino);
                var usr = ObtenerUsuarioLogueado();
                entidadDestino.CreadoPor = usr.UserName;
                entidadDestino.FechaCreacion = DateTime.Now;
                var DestinoId =_Destino.Agregar(entidadDestino);
                
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
                var documentos = _documentoIdentidad.ObtenerLista();
                ViewBag.DocumentoIdentidadId = new SelectList(documentos, "Id", "Descripcion");
                var entidad = _Destino.ObtenerQueryable().FirstOrDefault(x => x.Id == id);
                var editViewModel = _helperMap.MapDestinoViewModel(entidad);

                return PartialView(editViewModel);
            }
            catch (Exception ex)
            {
                return MyJsonResult(GetAllExeption(ex));
            }
        }
        [HttpPost]
        public ActionResult _Edit(DestinoViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var modelState = ModelState.Values.Where(a => a.Errors.Count > 0).First();
                    var mensaje = modelState.Errors.FirstOrDefault().ErrorMessage;

                    return MyJsonResult(mensaje);
                }

                var client = _Destino.ObtenerQueryable().FirstOrDefault(x => x.Descripcion == model.Descripcion && x.Id != model.Id);
                if (client != null)
                {
                    return MyJsonResult("Ya existe un Destino con ese Nombre, por favor corregir. Gracias!");
                }
                var entidadDestino = _helperMap.MapDestinoModel(model);
                var usr = ObtenerUsuarioLogueado();
                entidadDestino.ModificadoPor = usr.UserName;
                entidadDestino.FechaModificacion = DateTime.Now;
                _Destino.Actualizar(entidadDestino);

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
                var entidad = _Destino.ObtenerQueryable().FirstOrDefault(x => x.Id == id);
                var editViewModel = _helperMap.MapDestinoViewModel(entidad);

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
                var motivoEvento = _Destino.ObtenerQueryable().FirstOrDefault(x => x.Id == id);

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
                var entidad = _Destino.ObtenerQueryable().FirstOrDefault(x => x.Id == id);
                _Destino.Eliminar(entidad);
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return MyJsonResult(GetAllExeption(ex));
            }
        }
    }
}