using RSI.Modelo.Entidades.Maestros;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSI.Modelo.Entidades.Movimientos
{
    [Table("Factura")]
    public class Factura
    {
        [Key]
        public int Id { get; set; }
        public string Prefijo { get; set; }
        public int Numero { get; set; }
        public DateTime Fecha { get; set; }
        [ForeignKey("Cliente")]
        public int ClienteId { get; set; }
        //[ForeignKey("Reserva")]
        public int ReservaId { get; set; }
        public double ValorBruto { get; set; }
        public double ValorDescuento { get; set; }
        public double ValorAntesImpuesto { get; set; }
        public double ValorIVA { get; set; }
        public double ValorNeto { get; set; }
        public string CreadoPor { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public ICollection<FacturaDetalle> FacturaDetalle { get; set; }
        public virtual Cliente Cliente { get; set; }
        //public virtual Reserva Reserva { get; set; }
    }
}
