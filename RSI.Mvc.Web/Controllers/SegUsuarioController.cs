using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using RSI.Modelo.RepositorioCont;
using RSI.Modelo.RepositorioImpl;
using RSI.Mvc.Web.Controllers.Helper;
using RSI.Mvc.Web.ViewModel;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace RSI.Mvc.Web.Controllers
{
    [Authorize]
    public class SegUsuarioController : dbController
    {
        #region Variables
        private readonly IUsuarioRepositorio _usuario;

        #endregion
        #region Constructor
        public SegUsuarioController()
        {
            _usuario = new UsuarioRepositorio(_context);
        }
        #endregion

        #region Vistas

        public PartialViewResult _AdminUser()
        {
            return PartialView();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            ViewData["CambioComtrasena"] = "false";
            return View();
        }

        #endregion

        #region Metodos de persistencia

        [HttpPost]
        public ActionResult CreateUser([DataSourceRequest] DataSourceRequest request, UsuarioViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var modelState = ModelState.Values.Where(a => a.Errors.Count > 0).First();
                    var mensaje = modelState.Errors.FirstOrDefault().ErrorMessage;
                    return MyJsonResult(mensaje);
                }
                if (!UserValidate(model))
                {
                    var modelState = ModelState.Values.Where(a => a.Errors.Count > 0).First();
                    var mensaje = modelState.Errors.FirstOrDefault().ErrorMessage;
                    return MyJsonResult(mensaje);
                }

                var entidad = _helperMap.MapUsuarioModel(model);
                var id = _usuario.Agregar(entidad);

                if (id != 0)
                {
                    ModelState.Clear();
                    return MyJsonResult("Error Insertando el usuario, pongase en contacto con soporte.");
                }

                return MyJsonResult("Usuario registrado con exito");
            }
            catch (Exception ex)
            {
                return MyJsonResult($"se presentó el siguiente error: {ex.Message}");
            }
        }

        [HttpPost]
        public ActionResult DeleteUser([DataSourceRequest] DataSourceRequest request, UsuarioViewModel model)
        {
            try
            {
                var entidad = _helperMap.MapUsuarioModel(model);
                _usuario.Eliminar(entidad);
                return MyJsonResult("Usuario Eliminado con exito.");
            }
            catch (Exception ex)
            {
                return MyJsonResult($"se presentó el siguiente error: {ex.Message}");
            }
        }

        public ActionResult GetAllUser([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                var listUsers = _usuario.ObtenerLista();
                var list = _helperMap.MapListUsuarioViewModel(listUsers);
                return ConstruirResultado(list.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                return this.Json(new DataSourceResult { Errors = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult UpdateUser([DataSourceRequest] DataSourceRequest request, UsuarioViewModel model)
        {
            try
            {
                if (!ModelState.IsValid || UserValidate(model))
                {
                    var modelState1 = ModelState.Values.Where(a => a.Errors.Count > 0).First();
                    var mensajeError = modelState1.Errors.FirstOrDefault().ErrorMessage;
                    return MyJsonResult(mensajeError);
                }
                var entidad = _helperMap.MapUsuarioModel(model);
                _usuario.Actualizar(entidad);
                var modelState = ModelState.Values.Where(a => a.Errors.Count > 0).First();
                var mensaje = modelState.Errors.FirstOrDefault().ErrorMessage;
                return MyJsonResult(mensaje);
            }
            catch (Exception ex)
            {
                return MyJsonResult($"Error: {ex.Message}");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UsuarioViewModel model)
        {
            //if (!ModelState.IsValid)
            //    return View(model);
            var contrasena = Encriptar(model.Contrasena);
            var userModel = _usuario.ObtenerQueryable().FirstOrDefault(x => x.UserName == model.UserName && x.Contrasena == contrasena);
            if (userModel == null)
            {
                ModelState.AddModelError("Autenticación", "Nombre de usuario con contraseña no válidos.");
                return View(model);
            }
            if (userModel.Estado == "INACTIVO")
            {
                ModelState.AddModelError("Autenticación", "El usuario se encuentra inactivo.");
                return View(model);
            }
            var userViewModel = _helperMap.MapUsuarioViewModel(userModel);
            Session["USER"] = userViewModel;
            Session["UserRol"] = userModel.Rol.Nombre;
            ////Session.Timeout = 5000;
            FormsAuthentication.SetAuthCookie(model.UserName, false);
            FormsAuthentication.RedirectFromLoginPage(model.UserName, false);
            return RedirectToAction("Index", "Home");

        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult LogOut()
        {
            Session.Remove("USER");
            Session.Remove("UserRol");
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "SegUsuario");
        }

        #endregion

        #region Metódos de Manejo



        private Boolean UserValidate(UsuarioViewModel model)
        {
            var isOk = true;

            if (model.UserName.Contains(" "))
            {
                ModelState.AddModelError("Usuario", "El usuario no puede contener espacios.");
                isOk = false;
            }

            if (model.Contrasena.Contains(" "))
            {
                ModelState.AddModelError("Contraseña", "La contraseña no puede contener espacios.");
                isOk = false;
            }

            if (!isOk) return isOk;

            //si el usuario y la contraseña están bien escritos, valida la existencia en la base de datos.

            var usercc = _usuario.ObtenerQueryable().FirstOrDefault(x => x.Cedula == model.Cedula);
            if (usercc != null)
            {
                ModelState.AddModelError("Cedula", "La cédula ya está registrada. Por favor verifique los datos.");
                isOk = false;
            }

            var user = _usuario.ObtenerQueryable().FirstOrDefault(x => x.UserName == model.UserName);
            if (user != null)
            {
                ModelState.AddModelError("Usuario", "El nombre de usuario ya se encuentra registrado. Favor Verificar.");
                isOk = false;
            }

            return isOk;
        }
        #endregion
    }
}