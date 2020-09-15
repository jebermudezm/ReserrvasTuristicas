using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using RSI.Modelo.Entidades.Maestros;
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
    public class ClienteController : dbController
    {
        #region Variables
        private readonly IClienteRepositorio _cliente;
        private readonly IListaRepositorio _lista;

        #endregion
        #region Constructor
        public ClienteController()
        {
            _cliente = new ClienteRepositorio(_context);
            _lista = new ListaRepositorio(_context);
        }
        #endregion

        // GET: Cliente
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
                var listaClienteViewModel = ObtenerClientes();
                return ConstruirResultado(listaClienteViewModel.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                return MyJsonResult($"se presentó el siguiente error: {ex.Message}");
            }
        }

        public List<ClienteViewModel> ObtenerClientes()
        {
            var listaClientes = _cliente.ObtenerLista();
            var lista = _lista.ObtenerLista();
            var resultado = new List<ClienteViewModel>();
            foreach (var item in listaClientes)
            {
                resultado.Add(_helperMap.MapClienteViewModel(item, lista));
            }
            return resultado.ToList();
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
        public ActionResult _Create(ClienteViewModel cliente)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var modelState = ModelState.Values.Where(a => a.Errors.Count > 0).First();
                    var mensaje = modelState.Errors.FirstOrDefault().ErrorMessage;

                    return MyJsonResult(mensaje);
                }
                var client = _cliente.ObtenerQueryable().FirstOrDefault(x => x.NumeroDocumentoIdentidad == cliente.NumeroDocumentoIdentidad);
                if (client != null)
                {
                    return MyJsonResult("Ya existe un cliente con ese número de documento, por favor corregir. Gracias!");
                }
                var entidadCliente = _helperMap.MapClienteModel(cliente);
                var usr = ObtenerUsuarioLogueado();
                    entidadCliente.CreadoPor = usr.UserName;
                entidadCliente.FechaCreacion = DateTime.Now;
                var clienteId =_cliente.Agregar(entidadCliente);
                
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
                var documentos = _lista.ObtenerLista();
                
                var entidad = _cliente.ObtenerQueryable().FirstOrDefault(x => x.Id == id);
                var editViewModel = _helperMap.MapClienteViewModel(entidad, documentos);
                var lista = _lista.ObtenerLista();
                ViewBag.DocumentoIdentidadId = new SelectList(lista.Where(x => x.TipoLista.Codigo == "TIPODOCID").ToList(), "Id", "Descripcion");
                ViewBag.TipoPersonaId = new SelectList(lista.Where(x => x.TipoLista.Codigo == "TIPOPERSONA").ToList(), "Id", "Descripcion");
                return PartialView(editViewModel);
            }
            catch (Exception ex)
            {
                return MyJsonResult(GetAllExeption(ex));
            }
        }
        [HttpPost]
        public ActionResult _Edit(ClienteViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var modelState = ModelState.Values.Where(a => a.Errors.Count > 0).First();
                    var mensaje = modelState.Errors.FirstOrDefault().ErrorMessage;

                    return MyJsonResult(mensaje);
                }

                var client = _cliente.ObtenerQueryable().FirstOrDefault(x => x.NumeroDocumentoIdentidad == model.NumeroDocumentoIdentidad && x.Id != model.Id);
                if (client != null)
                {
                    return MyJsonResult("Ya existe un cliente con ese número de documento, por favor corregir. Gracias!");
                }
                var entidadCliente = _helperMap.MapClienteModel(model);
                var usr = ObtenerUsuarioLogueado();
                entidadCliente.ModificadoPor = usr.UserName;
                entidadCliente.FechaModificacion = DateTime.Now;
                _cliente.Actualizar(entidadCliente);

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
                var entidad = _cliente.ObtenerQueryable().FirstOrDefault(x => x.Id == id);
                var editViewModel = _helperMap.MapClienteViewModel(entidad, lista);

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
                var cliente = _cliente.ObtenerQueryable().FirstOrDefault(x => x.Id == id);

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
                var entidad = _cliente.ObtenerQueryable().FirstOrDefault(x => x.Id == id);
                _cliente.Eliminar(entidad);
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return MyJsonResult(GetAllExeption(ex));
            }
        }
    }
}