using RSI.Modelo.Entidades.Maestros;
using RSI.Modelo.Entidades.Seguridad;
using RSI.Modelo.RepositorioCont;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RSI.Modelo.RepositorioImpl
{
    public class ConceptoRepositorio : RepositorioBase, IConceptoRepositorio
    {
        public ConceptoRepositorio(RSIModelContext modelContext) : base(modelContext)
        {
        }

        public void Actualizar(Concepto entidad)
        {
            var Concepto = modelContext.Conceptos.FirstOrDefault(x => x.Id == entidad.Id);
            Concepto.Codigo = entidad.Codigo;
            Concepto.Nombre = entidad.Nombre;
            Concepto.Observacion = entidad.Observacion;
            Concepto.ModificadoPor = entidad.ModificadoPor;
            Concepto.FechaModificacion = entidad.FechaModificacion;
            modelContext.SaveChanges();
        }

        public int Agregar(Concepto entidad)
        {
            modelContext.Conceptos.Add(entidad);
            modelContext.SaveChanges();
            return entidad.Id;
        }

        public bool Any(Func<Concepto, bool> predicado)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(Concepto entidad)
        {
            modelContext.Conceptos.Remove(entidad);
            modelContext.SaveChanges();
        }

        public Concepto Obtener(int id)
        {
            return modelContext.Conceptos.FirstOrDefault(x => x.Id == id);
        }

        public List<Concepto> ObtenerLista()
        {
            return modelContext.Conceptos.ToList();
        }

        public IQueryable<Concepto> ObtenerQueryable()
        {
            return modelContext.Conceptos.AsQueryable();
        }

        public void ValidarEntidad(Concepto entidad)
        {
            List<string> mensajes = new List<string>();
            bool hayEerror = false;
            if (string.IsNullOrEmpty(entidad.Nombre))
            {
                mensajes.Add("El nombre es un campo requerido.");
                hayEerror = true;
            }
           
            if (!hayEerror)
            {
                var Concepto = ObtenerQueryable().FirstOrDefault(x => x.Codigo == entidad.Codigo);
                if (Concepto != null)
                {
                    mensajes.Add("Ya existe registrado un Concepto con el mismo Código.");
                    hayEerror = true;
                }
            }
            if (hayEerror)
            {
                throw new InvalidOperationException($"Validación Concepto: {string.Join(Environment.NewLine, mensajes)}");
            }
        }
    }
}
