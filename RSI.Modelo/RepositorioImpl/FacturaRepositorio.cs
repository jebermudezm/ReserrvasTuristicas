using RSI.Modelo.Entidades.Movimientos;
using RSI.Modelo.RepositorioCont;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RSI.Modelo.RepositorioImpl
{
    public class FacturaRepositorio : RepositorioBase, IFacturaRepositorio
    {
        public FacturaRepositorio(RSIModelContext modelContext) : base(modelContext)
        {
        }

        public void Actualizar(Factura entidad)
        {
            var factura = modelContext.Facturas.FirstOrDefault(x => x.Id == entidad.Id);
            factura.Prefijo = entidad.Prefijo;
            factura.Numero = entidad.Numero;
            factura.Fecha = entidad.Fecha;
            factura.ClienteId = entidad.ClienteId;
            factura.ReservaId = entidad.ReservaId;
            factura.ValorBruto = entidad.ValorBruto;
            factura.ValorDescuento = entidad.ValorDescuento;
            factura.ValorAntesImpuesto = entidad.ValorAntesImpuesto;
            factura.ValorIVA = entidad.ValorIVA;
            factura.ValorNeto = entidad.ValorNeto;
            factura.CreadoPor = entidad.CreadoPor;
            factura.FechaCreacion = entidad.FechaCreacion;
            factura.ModificadoPor = entidad.ModificadoPor;
            factura.FechaModificacion = entidad.FechaModificacion;
            modelContext.SaveChanges();
        }

        public int Agregar(Factura entidad)
        {
            modelContext.Facturas.Add(entidad);
            modelContext.SaveChanges();
            return entidad.Id;
        }

        public bool Any(Func<Factura, bool> predicado)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(Factura entidad)
        {
            modelContext.Facturas.Remove(entidad);
            modelContext.SaveChanges();
        }

        public Factura Obtener(int id)
        {
            return modelContext.Facturas.FirstOrDefault(x => x.Id == id);
        }

        public List<Factura> ObtenerLista()
        {
            return modelContext.Facturas.ToList();
        }

        public IQueryable<Factura> ObtenerQueryable()
        {
            return modelContext.Facturas.AsQueryable();
        }
        public IQueryable<NumeroValorable> ObtenerNumeroValorableQueryable(string prefijo)
        {
            return modelContext.NumeroValorables.Where(x => x.Prefijo == prefijo);
        }

        public void ValidarEntidad(Factura entidad)
        {
            throw new NotImplementedException();
        }
    }
}
