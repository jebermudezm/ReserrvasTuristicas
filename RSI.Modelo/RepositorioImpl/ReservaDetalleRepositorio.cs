using RSI.Modelo.Entidades.Movimientos;
using RSI.Modelo.RepositorioCont;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RSI.Modelo.RepositorioImpl
{
    public class ReservaDetalleRepositorio : RepositorioBase, IReservaDetalleRepositorio
    {
        public ReservaDetalleRepositorio(RSIModelContext modelContext) : base(modelContext)
        {
        }

        public void Actualizar(ReservaDetalle entidad)
        {
            var reserva = modelContext.ReservasDetalle.FirstOrDefault(x => x.Id == entidad.Id);
            reserva.ReservaId = entidad.ReservaId;
            reserva.Item = entidad.Item;
            reserva.Cantidad = entidad.Cantidad;
            reserva.Descripcion = entidad.Descripcion;
            reserva.ValorUnitario = entidad.ValorUnitario;
            reserva.PorcentajeDescuento = entidad.PorcentajeDescuento;
            reserva.ValorDescuento = entidad.ValorDescuento;
            reserva.ValorTotal = entidad.ValorTotal;
            reserva.ModificadoPor = entidad.ModificadoPor;
            reserva.FechaModificacion = entidad.FechaModificacion;
            modelContext.SaveChanges();
        }

        public void ActualizarValoresReserva(int id)
        {
            var detalle = modelContext.ReservasDetalle.Where(x => x.ReservaId == id).ToList();
            var reserva = modelContext.Reservas.FirstOrDefault(x => x.Id == id);
            reserva.ValorBruto = detalle.Sum(x => x.ValorTotalBruto);
            reserva.ValorDescuento = detalle.Sum(x => x.ValorDescuento);
            reserva.TotalImpuesto = detalle.Sum(x => x.ValorImpuesto);
            reserva.ValorTotal = detalle.Sum(x => x.ValorTotal);
            modelContext.SaveChanges();
        }
        public void ActualizarDetalle(int id)
        {
            var reserva = modelContext.Reservas.Where(x => x.Id == id).ToList();
            var detalle = modelContext.ReservasDetalle.Where(x => x.ReservaId == id).ToList();
            
            //var reserva = modelContext.Reservas.FirstOrDefault(x => x.Id == id);
            //reserva.ValorBruto = detalle.Sum(x => x.ValorTotalBruto);
            //reserva.ValorDescuento = detalle.Sum(x => x.ValorDescuento);
            //reserva.ValorImpuesto = detalle.Sum(x => x.ValorImpuesto);
            //reserva.ValorTotal = detalle.Sum(x => x.ValorTotal);
            modelContext.SaveChanges();
        }
        public int Agregar(ReservaDetalle entidad)
        {
            modelContext.ReservasDetalle.Add(entidad);
            modelContext.SaveChanges();
            return entidad.Id;
        }

        public bool Any(Func<ReservaDetalle, bool> predicado)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(ReservaDetalle entidad)
        {
            modelContext.ReservasDetalle.Remove(entidad);
            modelContext.SaveChanges();
        }

        public ReservaDetalle Obtener(int id)
        {
            return modelContext.ReservasDetalle.FirstOrDefault(x => x.Id == id);
        }

        public List<ReservaDetalle> ObtenerLista()
        {
            return modelContext.ReservasDetalle.ToList();
        }

        public IQueryable<ReservaDetalle> ObtenerQueryable()
        {
            return modelContext.ReservasDetalle.AsQueryable();
        }

        public void ValidarEntidad(ReservaDetalle entidad)
        {
            throw new NotImplementedException();
        }
    }
}
