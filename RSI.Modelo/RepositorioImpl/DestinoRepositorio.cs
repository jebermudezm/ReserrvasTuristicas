using RSI.Modelo.Entidades.Maestros;
using RSI.Modelo.Entidades.Seguridad;
using RSI.Modelo.RepositorioCont;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RSI.Modelo.RepositorioImpl
{
    public class DestinoRepositorio : RepositorioBase, IDestinoRepositorio
    {
        public DestinoRepositorio(RSIModelContext modelContext) : base(modelContext)
        {
        }

        public void Actualizar(Destino entidad)
        {
            var tipoDestino = modelContext.Destinos.FirstOrDefault(x => x.Id == entidad.Id);
            tipoDestino.Codigo = entidad.Codigo;
            tipoDestino.Descripcion = entidad.Descripcion;
            tipoDestino.Observacion = entidad.Observacion;
            tipoDestino.FechaModificacion = entidad.FechaModificacion;
            tipoDestino.ModificadoPor = entidad.ModificadoPor;
            modelContext.SaveChanges();
        }

        public int Agregar(Destino entidad)
        {
            modelContext.Destinos.Add(entidad);
            modelContext.SaveChanges();
            return entidad.Id;
        }

        public bool Any(Func<Destino, bool> predicado)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(Destino entidad)
        {
            modelContext.Destinos.Remove(entidad);
            modelContext.SaveChanges();
        }

        public Destino Obtener(int id)
        {
            return modelContext.Destinos.FirstOrDefault(x => x.Id == id);
        }

        public List<Destino> ObtenerLista()
        {
            return modelContext.Destinos.ToList();
        }

        public IQueryable<Destino> ObtenerQueryable()
        {
            return modelContext.Destinos.AsQueryable();
        }

        public void ValidarEntidad(Destino entidad)
        {
            throw new NotImplementedException();
        }
    }
}
