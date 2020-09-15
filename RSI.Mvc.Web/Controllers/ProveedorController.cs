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
    public class ProveedorController : dbController
    {
        #region Variables
        private readonly IProveedorRepositorio _proveedor;
        private readonly IListaRepositorio _lista;

        #endregion
        #region Constructor
        public ProveedorController()
        {
            _proveedor = new ProveedorRepositorio(_context);
            _lista = new ListaRepositorio(_context);
        }
        #endregion

        // GET: Proveedor
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
                var listaProveedors = _proveedor.ObtenerLista();
                var listaProveedorViewModel = ObtenerProveedors();
                return ConstruirResultado(listaProveedorViewModel.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                return MyJsonResult($"se presentó el siguiente error: {ex.Message}");
            }
        }

        public List<ProveedorViewModel> ObtenerProveedors()
        {
            var lista = _lista.ObtenerLista();
            var listaProveedors = _proveedor.ObtenerLista();
            var listaProveedorViewModel = new List<ProveedorViewModel>();
            foreach (var item in listaProveedors)
            {
                listaProveedorViewModel.Add(_helperMap.MapProveedorViewModel(item, lista));
            }
            return listaProveedorViewModel;
        }

        

        public ActionResult _Create()
        {
            try
            {
                var lista = _lista.ObtenerLista();
                ViewBag.DocumentoIdentidadId = new SelectList(lista.Where(x => x.TipoLista.Codigo == "TIPODOCID").ToList(), "Id", "Descripcion");
                ViewBag.TipoPersonaId = new SelectList(lista.Where(x => x.TipoLista.Codigo == "TIPOPERSONA").ToList(), "Id", "Descripcion");
                return PartialView();
            }
            catch (Exception ex)
            {
                return MyJsonResult(GetAllExeption(ex));
            }
        }

        [HttpPost]
        public ActionResult _Create(ProveedorViewModel proveedor)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var modelState = ModelState.Values.Where(a => a.Errors.Count > 0).First();
                    var mensaje = modelState.Errors.FirstOrDefault().ErrorMessage;

                    return MyJsonResult(mensaje);
                }
                var entidadProveedor = _helperMap.MapProveedorModel(proveedor);
                var user = ObtenerUsuarioLogueado();
                    entidadProveedor.FechaCreacion = DateTime.Now;
                entidadProveedor.CreadoPor = user.UserName;
                var ProveedorId =_proveedor.Agregar(entidadProveedor);
                
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
                var lista = _lista.ObtenerLista();
                ViewBag.DocumentoIdentidadId = new SelectList(lista.Where(x => x.TipoLista.Codigo == "TIPODOCID").ToList(), "Id", "Descripcion");
                ViewBag.TipoPersonaId = new SelectList(lista.Where(x => x.TipoLista.Codigo == "TIPOPERSONA").ToList(), "Id", "Descripcion");

                var entidad = _proveedor.Obtener(id);
                var editViewModel = _helperMap.MapProveedorViewModel(entidad, lista);
                return PartialView(editViewModel);
            }
            catch (Exception ex)
            {
                return MyJsonResult(GetAllExeption(ex));
            }
        }
        [HttpPost]
        public ActionResult _Edit(ProveedorViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var modelState = ModelState.Values.Where(a => a.Errors.Count > 0).First();
                    var mensaje = modelState.Errors.FirstOrDefault().ErrorMessage;

                    return MyJsonResult(mensaje);
                }

                var client = _proveedor.ObtenerQueryable().FirstOrDefault(x => x.NumeroDocumentoIdentidad == model.NumeroDocumentoIdentidad && x.Id != model.Id);
                if (client != null)
                {
                    return MyJsonResult("Ya existe un Proveedor con ese número de documento, por favor corregir. Gracias!");
                }
                var entidadProveedor = _helperMap.MapProveedorModel(model);
                var usr = ObtenerUsuarioLogueado();
                entidadProveedor.ModificadoPor = usr.UserName;
                entidadProveedor.FechaModificacion = DateTime.Now;
                _proveedor.Actualizar(entidadProveedor);

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
                var lista = _lista.ObtenerLista();
                var entidad = _proveedor.ObtenerQueryable().FirstOrDefault(x => x.Id == id);
                var editViewModel = _helperMap.MapProveedorViewModel(entidad, lista);

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
                var proveedor = _proveedor.ObtenerQueryable().FirstOrDefault(x => x.Id == id);

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
                var entidad = _proveedor.ObtenerQueryable().FirstOrDefault(x => x.Id == id);
                _proveedor.Eliminar(entidad);
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return MyJsonResult(GetAllExeption(ex));
            }
        }
    }
}