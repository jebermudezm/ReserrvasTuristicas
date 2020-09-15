using RSI.Modelo.Entidades.Maestros;
using RSI.Modelo.RepositorioCont;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RSI.Modelo.RepositorioImpl
{
    public class PlanRepositorio : RepositorioBase, IPlanRepositorio
    {
        public PlanRepositorio(RSIModelContext modelContext) : base(modelContext)
        {
        }

        public void Actualizar(Plan entidad)
        {
            var Plan = modelContext.Planes.FirstOrDefault(x => x.Id == entidad.Id);
            Plan.Codigo = entidad.Codigo ?? Plan.Codigo;
            Plan.Descripcion = entidad.Descripcion ?? Plan.Descripcion;
            Plan.Hotel = entidad.Hotel;
            Plan.Observacion = entidad.Observacion;
            Plan.DestinoId = entidad.DestinoId;
            Plan.ModificadoPor = entidad.ModificadoPor;
            Plan.FechaModificacion = entidad.FechaModificacion;
            modelContext.SaveChanges();
        }

        public int Agregar(Plan entidad)
        {
            entidad.Incluye.Add(new Incluye { Codigo = "IN", Descripcion = "Incluye", CreadoPor = entidad.CreadoPor, FechaCreacion = entidad.FechaCreacion});
            entidad.Incluye.Add(new Incluye { Codigo = "NI", Descripcion = "No Incluye", CreadoPor = entidad.CreadoPor, FechaCreacion = entidad.FechaCreacion });
            entidad.Incluye.Add(new Incluye { Codigo = "CO", Descripcion = "Cortesía", CreadoPor = entidad.CreadoPor, FechaCreacion = entidad.FechaCreacion });
            entidad.Incluye.Add(new Incluye { Codigo = "GN", Descripcion = "Gastos No Especificados", CreadoPor = entidad.CreadoPor, FechaCreacion = entidad.FechaCreacion });
            entidad.Incluye.Add(new Incluye { Codigo = "IG", Descripcion = "Información General para Reservar el Plan", CreadoPor = entidad.CreadoPor, FechaCreacion = entidad.FechaCreacion });
            modelContext.Planes.Add(entidad);
            modelContext.SaveChanges();
            return entidad.Id;
        }

        public bool Any(Func<Plan, bool> predicado)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(Plan entidad)
        {
            modelContext.Planes.Remove(entidad);
            modelContext.SaveChanges();
        }

        public Plan Obtener(int id)
        {
            return modelContext.Planes.FirstOrDefault(x => x.Id == id);
        }

        public List<Plan> ObtenerLista()
        {
            return modelContext.Planes.ToList();
        }

        public IQueryable<Plan> ObtenerQueryable()
        {
            return modelContext.Planes.AsQueryable();
        }

        public void ValidarEntidad(Plan entidad)
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
                var Plan = ObtenerQueryable().FirstOrDefault();
                if (Plan != null)
                {
                    mensajes.Add("Ya existe registrado un Plan con la misma descripción para las mismas fechas de salida y de reqreso.");
                    hayEerror = true;
                }
            }
            if (hayEerror)
            {
                throw new InvalidOperationException($"Validación Plan: {string.Join(Environment.NewLine, mensajes)}");
            }
        }
    }
}
