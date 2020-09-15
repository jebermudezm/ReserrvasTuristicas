using RSI.Modelo.Entidades.Maestros;
using RSI.Modelo.RepositorioCont;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RSI.Modelo.RepositorioImpl
{
    public class PlanTuristicoRepositorio : RepositorioBase, IPlanTuristicoRepositorio
    {
        public PlanTuristicoRepositorio(RSIModelContext modelContext) : base(modelContext)
        {
        }

        public void Actualizar(PlanTuristico entidad)
        {
            var planTuristico = modelContext.PlanesTuristicos.FirstOrDefault(x => x.Id == entidad.Id);
            planTuristico.Codigo = entidad.Codigo ?? planTuristico.Codigo;
            planTuristico.Descripcion = entidad.Descripcion ?? planTuristico.Descripcion;
            planTuristico.Hotel = entidad.Hotel;
            planTuristico.CostoAdulto = entidad.CostoAdulto;
            planTuristico.CostoMenor = entidad.CostoMenor;
            planTuristico.CostoInfante = entidad.CostoInfante;
            planTuristico.ValorAdulto = entidad.ValorAdulto;
            planTuristico.ValorMenor = entidad.ValorMenor;
            planTuristico.ValorInfante = entidad.ValorInfante;
            planTuristico.Observacion = entidad.Observacion;
            planTuristico.DestinoId = entidad.DestinoId;
            planTuristico.FechaSalida = entidad.FechaSalida;
            planTuristico.Fecharegreso = entidad.Fecharegreso;
            planTuristico.ProveedorId = entidad.ProveedorId;
            planTuristico.ModificadoPor = entidad.ModificadoPor;
            planTuristico.FechaModificacion = entidad.FechaModificacion;
            modelContext.SaveChanges();
        }

        public int Agregar(PlanTuristico entidad)
        {
            modelContext.PlanesTuristicos.Add(entidad);
            modelContext.SaveChanges();
            return entidad.Id;
        }

        public int AgregarPlanTuristico(PlanTuristico entidad, Plan plan)
        {
            modelContext.PlanesTuristicos.Add(entidad);
            modelContext.SaveChanges();
            foreach (var item in plan.Incluye)
            {
                var detalleplanTuristico = new DetallePlanTuristico
                {
                    PlanTuristicoId = entidad.Id,
                    Codigo = item.Codigo,
                    Descripcion = item.Descripcion,
                    CostoAdulto = 0,
                    CostoMenor = 0,
                    CostoInfante = 0,
                    ValorAdulto = 0,
                    ValorMenor = 0,
                    ValorInfante = 0,
                    FechaCreacion = DateTime.Now,
                    CreadoPor = entidad.CreadoPor
                };
                modelContext.DetallesPlanTuristico.Add(detalleplanTuristico);
                modelContext.SaveChanges();
                foreach (var item1 in item.DetalleIncluye)
                {
                    var itemDetalleplanTuristico = new ItemDetallePlanTuristico
                    {
                        DetallePlanTuristicoId = detalleplanTuristico.Id,
                        Codigo = item1.Codigo,
                        Descripcion = item1.Descripcion,
                        CostoAdulto = 0,
                        CostoMenor = 0,
                        CostoInfante = 0,
                        ValorAdulto = 0,
                        ValorMenor = 0,
                        ValorInfante = 0,
                        FechaCreacion = DateTime.Now,
                        CreadoPor = entidad.CreadoPor
                    };
                    modelContext.ItemsDetallePlanTuristico.Add(itemDetalleplanTuristico);
                    modelContext.SaveChanges();
                }
            }
            return entidad.Id;
        }

        public bool Any(Func<PlanTuristico, bool> predicado)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(PlanTuristico entidad)
        {
            modelContext.PlanesTuristicos.Remove(entidad);
            modelContext.SaveChanges();
        }

        public PlanTuristico Obtener(int id)
        {
            return modelContext.PlanesTuristicos.FirstOrDefault(x => x.Id == id);
        }

        public List<PlanTuristico> ObtenerLista()
        {
            return modelContext.PlanesTuristicos.ToList();
        }

        public IQueryable<PlanTuristico> ObtenerQueryable()
        {
            return modelContext.PlanesTuristicos.AsQueryable();
        }

        public void ValidarEntidad(PlanTuristico entidad)
        {
            List<string> mensajes = new List<string>();
            bool hayEerror = false;
            if (string.IsNullOrEmpty(entidad.Descripcion))
            {
                mensajes.Add("La descripción es requerido.");
                hayEerror = true;
            }
            if (string.IsNullOrEmpty(entidad.Codigo))
            {
                mensajes.Add("El código es un campo requerido.");
                hayEerror = true;
            }
            if (!hayEerror)
            {
                var PlanTuristico = ObtenerQueryable().FirstOrDefault();
                if (PlanTuristico != null)
                {
                    mensajes.Add("Ya existe registrado un PlanTuristico con la misma descripción para las mismas fechas de salida y de reqreso.");
                    hayEerror = true;
                }
            }
            if (hayEerror)
            {
                throw new InvalidOperationException($"Validación PlanTuristico: {string.Join(Environment.NewLine, mensajes)}");
            }
        }

        public void ActualizarValoresPlanTuristico(int id)
        {
            var planTuristico = Obtener(id);
            if(planTuristico.DetallePlanTuristico.Sum(x => x.CostoAdulto) > 0)
                planTuristico.CostoAdulto = planTuristico.DetallePlanTuristico.Sum(x => x.CostoAdulto);
            if (planTuristico.DetallePlanTuristico.Sum(x => x.CostoMenor) > 0)
                planTuristico.CostoMenor = planTuristico.DetallePlanTuristico.Sum(x => x.CostoMenor);
            if (planTuristico.DetallePlanTuristico.Sum(x => x.CostoInfante) > 0)
                planTuristico.CostoInfante = planTuristico.DetallePlanTuristico.Sum(x => x.CostoInfante);
            if (planTuristico.DetallePlanTuristico.Sum(x => x.ValorAdulto) > 0)
                planTuristico.ValorAdulto = planTuristico.DetallePlanTuristico.Sum(x => x.ValorAdulto);
            if (planTuristico.DetallePlanTuristico.Sum(x => x.ValorMenor) > 0)
                planTuristico.ValorMenor = planTuristico.DetallePlanTuristico.Sum(x => x.ValorMenor);
            if (planTuristico.DetallePlanTuristico.Sum(x => x.ValorInfante) > 0)
                planTuristico.ValorInfante = planTuristico.DetallePlanTuristico.Sum(x => x.ValorInfante);
            Actualizar(planTuristico);
        }
    }
}
