using RSI.Modelo.Entidades.Maestros;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSI.Modelo.Entidades.Movimientos
{
    [Table("ReservaDetalle")]
    public class ReservaDetalle
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Reserva")]
        public int ReservaId { get; set; }
        public int Item { get; set; }
        public double Cantidad { get; set; }
        public string Descripcion { get; set; }
        public double ValorUnitario { get; set; }
        public double ValorTotalBruto { get; set; }
        public double PorcentajeDescuento { get; set; }
        public double ValorDescuento { get; set; }
        public double ValorBase { get; set; }
        [ForeignKey("Impuesto")]
        public int ImpuestoId { get; set; }
        public double PorcentajeImpuesto { get; set; }
        public double ValorImpuesto { get; set; }
        public double ValorTotal { get; set; }

        [Required]
        public string CreadoPor { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; }

        [StringLength(50)]
        public string ModificadoPor { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public virtual Reserva Reserva { get; set; }
        public virtual Lista Impuesto { get; set; }
    }
}
