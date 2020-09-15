using RSI.Modelo.Entidades.Maestros;
using RSI.Modelo.RepositorioCont;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RSI.Modelo.RepositorioImpl
{
    public class DetallePlanTuristicoRepositorio : RepositorioBase, IDetallePlanTuristicoRepositorio
    {
        public DetallePlanTuristicoRepositorio(RSIModelContext modelContext) : base(modelContext)
        {
        }

        public void Actualizar(DetallePlanTuristico entidad)
        {
            var detallePlanTuristico = modelContext.DetallesPlanTuristico.FirstOrDefault(x => x.Id == entidad.Id);
            detallePlanTuristico.Codigo = entidad.Codigo;
            detallePlanTuristico.Descripcion = entidad.Descripcion;
            detallePlanTuristico.CostoAdulto = entidad.CostoAdulto;
            detallePlanTuristico.CostoMenor = entidad.CostoMenor;
            detallePlanTuristico.CostoInfante = entidad.CostoInfante;
            detallePlanTuristico.ValorAdulto = entidad.ValorAdulto;
            detallePlanTuristico.ValorMenor = entidad.ValorMenor;
            detallePlanTuristico.ValorInfante = entidad.ValorInfante;
            detallePlanTuristico.PlanTuristicoId = entidad.PlanTuristicoId;
            detallePlanTuristico.ModificadoPor = entidad.ModificadoPor;
            detallePlanTuristico.FechaModificacion = entidad.FechaModificacion;
            modelContext.SaveChanges();
        }

        public int Agregar(DetallePlanTuristico entidad)
        {
            modelContext.DetallesPlanTuristico.Add(entidad);
            modelContext.SaveChanges();
            return entidad.Id;
        }

        public void Actualizar(Plan entidad)
        {
            throw new NotImplementedException();
        }
        public int Agregar(Plan entidad)
        {
            throw new NotImplementedException();
        }

        public bool Any(Func<DetallePlanTuristico, bool> predicado)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(DetallePlanTuristico entidad)
        {
            modelContext.DetallesPlanTuristico.Remove(entidad);
            modelContext.SaveChanges();
        }

        public DetallePlanTuristico Obtener(int id)
        {
            return modelContext.DetallesPlanTuristico.FirstOrDefault(x => x.Id == id);
        }

        public List<DetallePlanTuristico> ObtenerLista()
        {
            return modelContext.DetallesPlanTuristico.ToList();
        }

        public IQueryable<DetallePlanTuristico> ObtenerQueryable()
        {
            return modelContext.DetallesPlanTuristico.AsQueryable();
        }

        public void ValidarEntidad(DetallePlanTuristico entidad)
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
                var DetallePlanTuristico = ObtenerQueryable().FirstOrDefault(x => x.Descripcion == entidad.Descripcion);
                if (DetallePlanTuristico != null)
                {
                    mensajes.Add("Ya existe registrado un DetallePlanTuristico con la misma descripción.");
                    hayEerror = true;
                }
            }
            if (hayEerror)
            {
                throw new InvalidOperationException($"Validación DetallePlanTuristico: {string.Join(Environment.NewLine, mensajes)}");
            }
        }

        public void ActualizarValoresDetallePlanTuristico(int id)
        {
            var detallePlanTuristico = Obtener(id);
            if (detallePlanTuristico.ItemDetallePlanTuristico.Sum(x => x.CostoAdulto) > 0)
                detallePlanTuristico.CostoAdulto = detallePlanTuristico.ItemDetallePlanTuristico.Sum(x => x.CostoAdulto);
            if (detallePlanTuristico.ItemDetallePlanTuristico.Sum(x => x.CostoMenor) > 0)
                detallePlanTuristico.CostoMenor = detallePlanTuristico.ItemDetallePlanTuristico.Sum(x => x.CostoMenor);
            if (detallePlanTuristico.ItemDetallePlanTuristico.Sum(x => x.CostoInfante) > 0)
                detallePlanTuristico.CostoInfante = detallePlanTuristico.ItemDetallePlanTuristico.Sum(x => x.CostoInfante);
            if (detallePlanTuristico.ItemDetallePlanTuristico.Sum(x => x.ValorAdulto) > 0)
                detallePlanTuristico.ValorAdulto = detallePlanTuristico.ItemDetallePlanTuristico.Sum(x => x.ValorAdulto);
            if (detallePlanTuristico.ItemDetallePlanTuristico.Sum(x => x.ValorMenor) > 0)
                detallePlanTuristico.ValorMenor = detallePlanTuristico.ItemDetallePlanTuristico.Sum(x => x.ValorMenor);
            if (detallePlanTuristico.ItemDetallePlanTuristico.Sum(x => x.ValorInfante) > 0)
                detallePlanTuristico.ValorInfante = detallePlanTuristico.ItemDetallePlanTuristico.Sum(x => x.ValorInfante);
            Actualizar(detallePlanTuristico);
        }

    }
}
