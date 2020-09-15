using RSI.Modelo.Entidades.Seguridad;
using RSI.Modelo.RepositorioCont;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RSI.Modelo.RepositorioImpl
{
    public class UsuarioRepositorio : RepositorioBase, IUsuarioRepositorio
    {
        public UsuarioRepositorio(RSIModelContext modelContext) : base(modelContext)
        {
        }

        public void Actualizar(Usuario entidad)
        {
            var usuario = modelContext.Usuarios.FirstOrDefault(x => x.Id == entidad.Id);
            usuario.Nombre = entidad.Nombre;
            usuario.Cedula = entidad.Cedula;
            usuario.Telefono = entidad.Telefono;
            usuario.Contrasena = entidad.Contrasena;
            usuario.ModificadoPor = entidad.ModificadoPor;
            usuario.FechaModificacion = entidad.FechaModificacion;
            modelContext.SaveChanges();
        }

        public int Agregar(Usuario entidad)
        {
            modelContext.Usuarios.Add(entidad);
            modelContext.SaveChanges();
            return entidad.Id;
        }

        public bool Any(Func<Usuario, bool> predicado)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(Usuario entidad)
        {
            modelContext.Usuarios.Remove(entidad);
        }

        public Usuario Obtener(int id)
        {
            return modelContext.Usuarios.FirstOrDefault(x => x.Id == id);
        }

        public List<Usuario> ObtenerLista()
        {
            return modelContext.Usuarios.ToList();
        }

        public IQueryable<Usuario> ObtenerQueryable()
        {
            return modelContext.Usuarios.AsQueryable();
        }

        public void ValidarEntidad(Usuario entidad)
        {
            throw new NotImplementedException();
        }
    }
}
