using RSI.Modelo.Entidades.Maestros;
using RSI.Modelo.Entidades.Seguridad;
using RSI.Modelo.RepositorioCont;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RSI.Modelo.RepositorioImpl
{
    public class CuposAcomodacionRepositorio : RepositorioBase, ICuposAcomodacionRepositorio
    {
        public CuposAcomodacionRepositorio(RSIModelContext modelContext) : base(modelContext)
        {
        }

        public void Actualizar(CuposAcomodacion entidad)
        {
            var cuposAcomodacion = modelContext.CuposAcomodaciones.FirstOrDefault(x => x.Id == entidad.Id);
            cuposAcomodacion.PlanFechaId = entidad.PlanFechaId;
            cuposAcomodacion.ConceptoValorId = entidad.ConceptoValorId;
            cuposAcomodacion.Cantidad = entidad.Cantidad;
            cuposAcomodacion.Observacion = entidad.Observacion;
            cuposAcomodacion.ModificadoPor = entidad.ModificadoPor;
            cuposAcomodacion.FechaModificacion = entidad.FechaModificacion;
            modelContext.SaveChanges();
        }

        public int Agregar(CuposAcomodacion entidad)
        {
            modelContext.CuposAcomodaciones.Add(entidad);
            modelContext.SaveChanges();
            return entidad.Id;
        }

        public bool Any(Func<CuposAcomodacion, bool> predicado)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(CuposAcomodacion entidad)
        {
            modelContext.CuposAcomodaciones.Remove(entidad);
            modelContext.SaveChanges();
        }

        public CuposAcomodacion Obtener(int id)
        {
            return modelContext.CuposAcomodaciones.FirstOrDefault(x => x.Id == id);
        }

        public List<CuposAcomodacion> ObtenerLista()
        {
            return modelContext.CuposAcomodaciones.ToList();
        }

        public IQueryable<CuposAcomodacion> ObtenerQueryable()
        {
            return modelContext.CuposAcomodaciones.AsQueryable();
        }

        public void ValidarEntidad(CuposAcomodacion entidad)
        {
            throw new NotImplementedException();
        }
    }
}
