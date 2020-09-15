using RSI.Modelo.Entidades.Movimientos;
using RSI.Modelo.RepositorioCont;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RSI.Modelo.RepositorioImpl
{
    public class NumeroValorableRepositorio : RepositorioBase, INumeroValorableRepositorio
    {
        public NumeroValorableRepositorio(RSIModelContext modelContext) : base(modelContext)
        {
        }

        public void Actualizar(NumeroValorable entidad)
        {
            var factura = modelContext.NumeroValorables.FirstOrDefault(x => x.Id == entidad.Id);
            factura.ConceptoId = entidad.ConceptoId;
            factura.Prefijo = entidad.Prefijo;
            factura.Numero = entidad.Numero;
            modelContext.SaveChanges();
        }

        public int Agregar(NumeroValorable entidad)
        {
            modelContext.NumeroValorables.Add(entidad);
            modelContext.SaveChanges();
            return entidad.Id;
        }

        public bool Any(Func<NumeroValorable, bool> predicado)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(NumeroValorable entidad)
        {
            modelContext.NumeroValorables.Remove(entidad);
            modelContext.SaveChanges();
        }

        public NumeroValorable Obtener(int id)
        {
            return modelContext.NumeroValorables.FirstOrDefault(x => x.Id == id);
        }

        public List<NumeroValorable> ObtenerLista()
        {
            return modelContext.NumeroValorables.ToList();
        }

        public IQueryable<NumeroValorable> ObtenerQueryable()
        {
            return modelContext.NumeroValorables.AsQueryable();
        }
        public IQueryable<NumeroValorable> ObtenerNumeroValorableQueryable(string prefijo)
        {
            return modelContext.NumeroValorables.Where(x => x.Prefijo == prefijo);
        }

        public void ValidarEntidad(NumeroValorable entidad)
        {
            throw new NotImplementedException();
        }
    }
}
