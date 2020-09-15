using RSI.Modelo.Entidades.Maestros;
using RSI.Modelo.Entidades.Seguridad;
using RSI.Modelo.RepositorioCont;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RSI.Modelo.RepositorioImpl
{
    public class ListaRepositorio : RepositorioBase, IListaRepositorio
    {
        public ListaRepositorio(RSIModelContext modelContext) : base(modelContext)
        {
        }

        public void Actualizar(Lista entidad)
        {
            var tipoLista = modelContext.Listas.FirstOrDefault(x => x.Id == entidad.Id);
            tipoLista.Codigo = entidad.Codigo;
            tipoLista.Descripcion = entidad.Descripcion;
            tipoLista.FechaModificacion = entidad.FechaModificacion;
            tipoLista.ModificadoPor = entidad.ModificadoPor;
            modelContext.SaveChanges();
        }

        public int Agregar(Lista entidad)
        {
            modelContext.Listas.Add(entidad);
            modelContext.SaveChanges();
            return entidad.Id;
        }

        public bool Any(Func<Lista, bool> predicado)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(Lista entidad)
        {
            modelContext.Listas.Remove(entidad);
        }

        public Lista Obtener(int id)
        {
            return modelContext.Listas.FirstOrDefault(x => x.Id == id);
        }

        public List<Lista> ObtenerLista()
        {
            return modelContext.Listas.ToList();
        }

        public IQueryable<Lista> ObtenerQueryable()
        {
            return modelContext.Listas.AsQueryable();
        }

        public IQueryable<TipoLista> ObtenerTipoListaQueryable()
        {
            return modelContext.TipoListas.AsQueryable();
        }

        public void ValidarEntidad(Lista entidad)
        {
            throw new NotImplementedException();
        }
    }
}
