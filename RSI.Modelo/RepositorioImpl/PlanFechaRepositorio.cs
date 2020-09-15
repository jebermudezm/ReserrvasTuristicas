using RSI.Modelo.Entidades.Maestros;
using RSI.Modelo.RepositorioCont;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RSI.Modelo.RepositorioImpl
{
    public class PlanFechaRepositorio : RepositorioBase, IPlanFechaRepositorio
    {
        public PlanFechaRepositorio(RSIModelContext modelContext) : base(modelContext)
        {
        }

        public void Actualizar(PlanFecha entidad)
        {
            var planFecha = modelContext.PlanesFecha.FirstOrDefault(x => x.Id == entidad.Id);
            planFecha.Codigo = entidad.Codigo;
            planFecha.Nombre = entidad.Nombre;
            planFecha.PlanId = entidad.PlanId;
            planFecha.ProveedorId = entidad.ProveedorId;
            planFecha.FechaSalida = entidad.FechaSalida;
            planFecha.FechaRegreso = entidad.FechaRegreso;
            planFecha.Observacion = entidad.Observacion;
            planFecha.ModificadoPor = entidad.ModificadoPor;
            planFecha.FechaModificacion = entidad.FechaModificacion;
            modelContext.SaveChanges();
        }

        public int Agregar(PlanFecha entidad)
        {
            modelContext.PlanesFecha.Add(entidad);
            modelContext.SaveChanges();
            return entidad.Id;
        }

        public bool Any(Func<PlanFecha, bool> predicado)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(PlanFecha entidad)
        {
            modelContext.PlanesFecha.Remove(entidad);
            modelContext.SaveChanges();
        }

        public PlanFecha Obtener(int id)
        {
            return modelContext.PlanesFecha.FirstOrDefault(x => x.Id == id);
        }

        public List<PlanFecha> ObtenerLista()
        {
            return modelContext.PlanesFecha.ToList();
        }

        public IQueryable<PlanFecha> ObtenerQueryable()
        {
            return modelContext.PlanesFecha.AsQueryable();
        }

        public void ValidarEntidad(PlanFecha entidad)
        {
            throw new NotImplementedException();
        }
    }
}
