using RSI.Modelo.Entidades.Movimientos;
using RSI.Modelo.RepositorioCont;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RSI.Modelo.RepositorioImpl
{
    public class PagoRepositorio : RepositorioBase, IPagoRepositorio
    {
        public PagoRepositorio(RSIModelContext modelContext) : base(modelContext)
        {
        }

        public void Actualizar(Pago entidad)
        {
            var reserva = modelContext.Reservas.FirstOrDefault(x => x.Id == entidad.ReservaId);
            var pago = modelContext.Pagos.FirstOrDefault(x => x.Id == entidad.Id);
            reserva.ValorPagado = reserva.ValorPagado + entidad.Valor;
            entidad.Saldo = reserva.ValorTotal - reserva.ValorPagado;
            pago.ReservaId = entidad.ReservaId;
            pago.Fecha = entidad.Fecha;
            pago.Valor = entidad.Valor;
            pago.Saldo = entidad.Saldo;
            pago.CreadoPor = entidad.CreadoPor;
            pago.FechaCreacion = entidad.FechaCreacion;
            pago.ModificadoPor = entidad.ModificadoPor;
            pago.FechaModificacion = entidad.FechaModificacion;
            modelContext.SaveChanges();
        }

        public int Agregar(Pago entidad)
        {
            var reserva = modelContext.Reservas.FirstOrDefault(x => x.Id == entidad.ReservaId);
            reserva.ValorPagado = reserva.ValorPagado + entidad.Valor;
            entidad.Saldo = reserva.ValorTotal - reserva.ValorPagado;
            modelContext.Pagos.Add(entidad);
            modelContext.SaveChanges();
            return entidad.Id;
        }

        public bool Any(Func<Pago, bool> predicado)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(Pago entidad)
        {
            modelContext.Pagos.Remove(entidad);
            modelContext.SaveChanges();
        }

        public Pago Obtener(int id)
        {
            return modelContext.Pagos.FirstOrDefault(x => x.Id == id);
        }

        public List<Pago> ObtenerLista()
        {
            return modelContext.Pagos.ToList();
        }

        public IQueryable<Pago> ObtenerQueryable()
        {
            return modelContext.Pagos.AsQueryable();
        }

        public void ValidarEntidad(Pago entidad)
        {
            throw new NotImplementedException();
        }
    }
}
