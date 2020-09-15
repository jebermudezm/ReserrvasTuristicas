using RSI.Modelo.Entidades.Movimientos;
using RSI.Modelo.RepositorioCont;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RSI.Modelo.RepositorioImpl
{
    public class FacturaDetalleRepositorio : RepositorioBase, IFacturaDetalleRepositorio
    {
        public FacturaDetalleRepositorio(RSIModelContext modelContext) : base(modelContext)
        {
        }

        public void Actualizar(FacturaDetalle entidad)
        {
            var facturaDetalle = modelContext.FacturasDetalle.FirstOrDefault(x => x.Id == entidad.Id);
            facturaDetalle.FacturaId = entidad.FacturaId;
            facturaDetalle.Item = entidad.Item;
            facturaDetalle.Cantidad = entidad.Cantidad;
            facturaDetalle.Descripcion = entidad.Descripcion;
            facturaDetalle.ValorUnitario = entidad.ValorUnitario;
            facturaDetalle.ValorBruto = entidad.ValorBruto;
            facturaDetalle.PorcentajeDescuento = entidad.PorcentajeDescuento;
            facturaDetalle.ValorAntesImpuesto = entidad.ValorAntesImpuesto;
            facturaDetalle.PorcentajeIVA = entidad.PorcentajeIVA;
            facturaDetalle.ValorIVA = entidad.ValorIVA;
            facturaDetalle.ValorNeto = entidad.ValorNeto;
            facturaDetalle.CreadoPor = entidad.CreadoPor;
            facturaDetalle.FechaCreacion = entidad.FechaCreacion;
            facturaDetalle.ModificadoPor = entidad.ModificadoPor;
            facturaDetalle.FechaModificacion = entidad.FechaModificacion;
            modelContext.SaveChanges();
        }

        public int Agregar(FacturaDetalle entidad)
        {
            modelContext.FacturasDetalle.Add(entidad);
            modelContext.SaveChanges();
            return entidad.Id;
        }

        public bool Any(Func<FacturaDetalle, bool> predicado)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(FacturaDetalle entidad)
        {
            modelContext.FacturasDetalle.Remove(entidad);
            modelContext.SaveChanges();
        }

        public FacturaDetalle Obtener(int id)
        {
            return modelContext.FacturasDetalle.FirstOrDefault(x => x.Id == id);
        }

        public List<FacturaDetalle> ObtenerLista()
        {
            return modelContext.FacturasDetalle.ToList();
        }

        public IQueryable<FacturaDetalle> ObtenerQueryable()
        {
            return modelContext.FacturasDetalle.AsQueryable();
        }

        public void ValidarEntidad(FacturaDetalle entidad)
        {
            throw new NotImplementedException();
        }
    }
}
