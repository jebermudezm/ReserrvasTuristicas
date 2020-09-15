using RSI.Modelo.Entidades.Maestros;
using RSI.Modelo.Entidades.Seguridad;
using RSI.Modelo.RepositorioCont;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RSI.Modelo.RepositorioImpl
{
    public class ConceptoValorRepositorio : RepositorioBase, IConceptoValorRepositorio
    {
        public ConceptoValorRepositorio(RSIModelContext modelContext) : base(modelContext)
        {
        }

        public void Actualizar(ConceptoValor entidad)
        {
            var ConceptoValor = modelContext.ConceptosValor.FirstOrDefault(x => x.Id == entidad.Id);
            ConceptoValor.Valor = entidad.Valor;
            ConceptoValor.Observacion = entidad.Observacion;
            ConceptoValor.ModificadoPor = entidad.ModificadoPor;
            ConceptoValor.FechaModificacion = entidad.FechaModificacion;
            modelContext.SaveChanges();
        }

        public int Agregar(ConceptoValor entidad)
        {
            modelContext.ConceptosValor.Add(entidad);
            modelContext.SaveChanges();
            return entidad.Id;
        }

        public bool Any(Func<ConceptoValor, bool> predicado)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(ConceptoValor entidad)
        {
            modelContext.ConceptosValor.Remove(entidad);
            modelContext.SaveChanges();
        }

        public ConceptoValor Obtener(int id)
        {
            return modelContext.ConceptosValor.FirstOrDefault(x => x.Id == id);
        }

        public List<ConceptoValor> ObtenerLista()
        {
            return modelContext.ConceptosValor.ToList();
        }

        public IQueryable<ConceptoValor> ObtenerQueryable()
        {
            return modelContext.ConceptosValor.AsQueryable();
        }

        public void ValidarEntidad(ConceptoValor entidad)
        {
            throw new NotImplementedException();
        }
    }
}
