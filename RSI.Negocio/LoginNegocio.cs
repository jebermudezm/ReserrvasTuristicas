using System;
using System.Linq;
using RSI.Modelo.Entidades.Seguridad;
using RSI.Modelo.RepositorioCont;
using RSI.Modelo.RepositorioImpl;

namespace RSI.Negocio
{
    public class LoginNegocio : ContextBase
    {
        private readonly IUsuarioRepositorio _usuario;
        
        public LoginNegocio()
        {
            _usuario = new UsuarioRepositorio(_context);
        }

        public Usuario ObtenerUsuario(string usuario, string contrasena)
        {
            var contra = Encriptar(contrasena);
            return _usuario.ObtenerQueryable().FirstOrDefault(x => x.UserName == usuario && x.Contrasena == contra);
        }
    }
}
