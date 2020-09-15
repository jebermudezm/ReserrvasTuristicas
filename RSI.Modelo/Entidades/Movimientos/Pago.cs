using RSI.Modelo.Entidades.Maestros;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSI.Modelo.Entidades.Movimientos
{
    [Table("Pagos")]
    public class Pago
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Reserva")]
        public int ReservaId { get; set; }
        public DateTime Fecha { get; set; }
        public double Valor { get; set; }
        public double Saldo { get; set; }
        public string Observacion { get; set; }
        public string CreadoPor { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public virtual Reserva Reserva { get; set; }
    }
}
