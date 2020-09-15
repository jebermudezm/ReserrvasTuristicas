using RSI.Modelo.Entidades.Movimientos;
using RSI.Modelo.RepositorioCont;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RSI.Modelo.RepositorioImpl
{
    public class ReservaIntegrantesRepositorio : RepositorioBase, IReservaIntegranteRepositorio
    {
        public ReservaIntegrantesRepositorio(RSIModelContext modelContext) : base(modelContext)
        {
        }

        public void Actualizar(ReservaIntegrante entidad)
        {
            var reserva = modelContext.ReservaIntegrantes.FirstOrDefault(x => x.Id == entidad.Id);
            reserva.ClienteId = entidad.ClienteId;
            reserva.ModificadoPor = entidad.ModificadoPor;
            reserva.FechaModificacion = entidad.FechaModificacion;
            modelContext.SaveChanges();
        }

        public int Agregar(ReservaIntegrante entidad)
        {
            modelContext.ReservaIntegrantes.Add(entidad);
            modelContext.SaveChanges();
            return entidad.Id;
        }

        public bool Any(Func<ReservaIntegrante, bool> predicado)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(ReservaIntegrante entidad)
        {
            modelContext.ReservaIntegrantes.Remove(entidad);
            modelContext.SaveChanges();
        }

        public ReservaIntegrante Obtener(int id)
        {
            return modelContext.ReservaIntegrantes.FirstOrDefault(x => x.Id == id);
        }

        public List<ReservaIntegrante> ObtenerLista()
        {
            return modelContext.ReservaIntegrantes.ToList();
        }

        public IQueryable<ReservaIntegrante> ObtenerQueryable()
        {
            return modelContext.ReservaIntegrantes.AsQueryable();
        }

        public void ValidarEntidad(ReservaIntegrante entidad)
        {
            throw new NotImplementedException();
        }
    }
}
