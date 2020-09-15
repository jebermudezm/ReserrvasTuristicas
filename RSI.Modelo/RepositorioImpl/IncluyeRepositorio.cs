using RSI.Modelo.Entidades.Maestros;
using RSI.Modelo.RepositorioCont;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RSI.Modelo.RepositorioImpl
{
    public class IncluyeRepositorio : RepositorioBase, IIncluyeRepositorio
    {
        public IncluyeRepositorio(RSIModelContext modelContext) : base(modelContext)
        {
        }

        public void Actualizar(Incluye entidad)
        {
            var Incluye = modelContext.Includes.FirstOrDefault(x => x.Id == entidad.Id);
            Incluye.Codigo = entidad.Codigo;
            Incluye.Descripcion = entidad.Descripcion;
            Incluye.PlanId = entidad.PlanId;
            Incluye.ModificadoPor = entidad.ModificadoPor;
            Incluye.FechaModificacion = entidad.FechaModificacion;
            modelContext.SaveChanges();
        }

        public int Agregar(Incluye entidad)
        {
            modelContext.Includes.Add(entidad);
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

        public bool Any(Func<Incluye, bool> predicado)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(Incluye entidad)
        {
            modelContext.Includes.Remove(entidad);
            modelContext.SaveChanges();
        }

        public Incluye Obtener(int id)
        {
            return modelContext.Includes.FirstOrDefault(x => x.Id == id);
        }

        public List<Incluye> ObtenerLista()
        {
            return modelContext.Includes.ToList();
        }

        public IQueryable<Incluye> ObtenerQueryable()
        {
            return modelContext.Includes.AsQueryable();
        }

        public void ValidarEntidad(Incluye entidad)
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
                var Incluye = ObtenerQueryable().FirstOrDefault(x => x.Descripcion == entidad.Descripcion);
                if (Incluye != null)
                {
                    mensajes.Add("Ya existe registrado un Incluye con la misma descripción.");
                    hayEerror = true;
                }
            }
            if (hayEerror)
            {
                throw new InvalidOperationException($"Validación Incluye: {string.Join(Environment.NewLine, mensajes)}");
            }
        }

    }
}
