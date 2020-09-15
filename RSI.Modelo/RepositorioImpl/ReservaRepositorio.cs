using RSI.Modelo.Entidades.Movimientos;
using RSI.Modelo.RepositorioCont;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RSI.Modelo.RepositorioImpl
{
    public class ReservaRepositorio : RepositorioBase, IReservaRepositorio
    {
        public ReservaRepositorio(RSIModelContext modelContext) : base(modelContext)
        {
        }

        public void Actualizar(Reserva entidad)
        {
            var reserva = modelContext.Reservas.FirstOrDefault(x => x.Id == entidad.Id);
            reserva.ClienteId = entidad.ClienteId;
            reserva.PlanTuristicoId = entidad.PlanTuristicoId;
            reserva.ConvenioId = entidad.ConvenioId;
            reserva.Fecha = entidad.Fecha;
            reserva.UsuarioId = entidad.UsuarioId;
            reserva.ValorBruto = entidad.ValorBruto;
            reserva.ValorPagado = entidad.ValorPagado;
            reserva.PorcentajeDescuento = entidad.PorcentajeDescuento;
            reserva.ValorDescuento = entidad.ValorDescuento;
            reserva.ValorBase = entidad.ValorBase;
            reserva.TotalImpuesto = entidad.TotalImpuesto;
            reserva.ValorTotal = entidad.ValorTotal;
            reserva.Observaciones = entidad.Observaciones;
            reserva.ModificadoPor = entidad.ModificadoPor;
            reserva.FechaModificacion = entidad.FechaModificacion;
            modelContext.SaveChanges();
        }

        public int Agregar(Reserva entidad)
        {
            modelContext.Reservas.Add(entidad);
            modelContext.SaveChanges();
            return entidad.Id;
        }

        public bool Any(Func<Reserva, bool> predicado)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(Reserva entidad)
        {
            modelContext.Reservas.Remove(entidad);
            modelContext.SaveChanges();
        }

        public Reserva Obtener(int id)
        {
            return modelContext.Reservas.FirstOrDefault(x => x.Id == id);
        }

        public List<Reserva> ObtenerLista()
        {
            return modelContext.Reservas.ToList();
        }

        public IQueryable<Reserva> ObtenerQueryable()
        {
            return modelContext.Reservas.AsQueryable();
        }

        public void ValidarEntidad(Reserva entidad)
        {
            throw new NotImplementedException();
        }
    }
}
