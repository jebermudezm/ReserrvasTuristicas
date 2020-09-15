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
    public class ProductoController : dbController
    {
        #region Variables
        private readonly IProductoRepositorio _producto;
        private readonly IListaRepositorio _lista;
        private readonly IProveedorRepositorio _proveedor;

        #endregion
        #region Constructor
        public ProductoController()
        {
            _producto = new ProductoRepositorio(_context);
            _lista = new ListaRepositorio(_context);
            _proveedor = new ProveedorRepositorio(_context);
        }
        #endregion

        // GET: Producto
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
                var listaProductoViewModel = ObtenerProductos();
                return ConstruirResultado(listaProductoViewModel.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                return MyJsonResult($"se presentó el siguiente error: {ex.Message}");
            }
        }

        public List<ProductoViewModel> ObtenerProductos()
        {
            var listaProductos = _producto.ObtenerLista();
            var lista = _lista.ObtenerLista();
            var resultado = new List<ProductoViewModel>();
            foreach (var item in listaProductos)
            {
                resultado.Add(_helperMap.MapProductoViewModel(item, lista));
            }
            return resultado.ToList();
        }

        

        public ActionResult _Create()
        {
            try
            {
                var lista = _lista.ObtenerLista();
                var proveedor = _proveedor.ObtenerLista();
                ViewBag.ImpuestoId = new SelectList(lista.Where(x => x.TipoLista.Codigo == "IMPUESTOS").ToList(), "Id", "Descripcion");
                ViewBag.ProveedorId = new SelectList(proveedor.ToList(), "Id", "NombreORazonSocial");
                return PartialView();
            }
            catch (Exception ex)
            {
                return MyJsonResult(GetAllExeption(ex));
            }
        }

        [HttpPost]
        public ActionResult _Create(ProductoViewModel producto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var modelState = ModelState.Values.Where(a => a.Errors.Count > 0).First();
                    var mensaje = modelState.Errors.FirstOrDefault().ErrorMessage;

                    return MyJsonResult(mensaje);
                }
                var product = _producto.ObtenerQueryable().FirstOrDefault(x => x.Codigo == producto.Codigo);
                if (product != null)
                {
                    return MyJsonResult("Ya existe un Producto con ese código, por favor corregir. Gracias!");
                }
                var entidadProducto = _helperMap.MapProductoModel(producto);
                var usr = ObtenerUsuarioLogueado();
                    entidadProducto.CreadoPor = usr.UserName;
                entidadProducto.FechaCreacion = DateTime.Now;
                var ProductoId =_producto.Agregar(entidadProducto);
                
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
                var proveedor = _proveedor.ObtenerLista();
                ViewBag.ImpuestoId = new SelectList(lista.Where(x => x.TipoLista.Codigo == "IMPUESTOS").ToList(), "Id", "Descripcion");
                ViewBag.ProveedorId = new SelectList(proveedor.ToList(), "Id", "NombreORazonSocial");

                var entidad = _producto.ObtenerQueryable().FirstOrDefault(x => x.Id == id);
                var editViewModel = _helperMap.MapProductoViewModel(entidad, lista);
                return PartialView(editViewModel);
            }
            catch (Exception ex)
            {
                return MyJsonResult(GetAllExeption(ex));
            }
        }
        [HttpPost]
        public ActionResult _Edit(ProductoViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var modelState = ModelState.Values.Where(a => a.Errors.Count > 0).First();
                    var mensaje = modelState.Errors.FirstOrDefault().ErrorMessage;

                    return MyJsonResult(mensaje);
                }

                var client = _producto.ObtenerQueryable().FirstOrDefault(x => x.Codigo == model.Codigo && x.Id != model.Id);
                if (client != null)
                {
                    return MyJsonResult("Ya existe un Producto con ese código, por favor corregir. Gracias!");
                }
                var entidadProducto = _helperMap.MapProductoModel(model);
                var usr = ObtenerUsuarioLogueado();
                entidadProducto.ModificadoPor = usr.UserName;
                entidadProducto.FechaModificacion = DateTime.Now;
                _producto.Actualizar(entidadProducto);

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
                var entidad = _producto.Obtener(id);
                var editViewModel = _helperMap.MapProductoViewModel(entidad, lista);

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
                var entidad = _producto.Obtener(id);

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
                var entidad = _producto.Obtener(id);
                _producto.Eliminar(entidad);
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return MyJsonResult(GetAllExeption(ex));
            }
        }
    }
}