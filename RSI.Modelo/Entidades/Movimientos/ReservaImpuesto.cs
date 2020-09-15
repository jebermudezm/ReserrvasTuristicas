using RSI.Modelo.Entidades.Maestros;
using RSI.Modelo.Entidades.Seguridad;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSI.Modelo.Entidades.Movimientos
{
    [Table("ReservaImpuesto")]
    public class ReservaImpuesto
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Reserva")]
        public int ReservaId { get; set; }
        [ForeignKey("Impuesto")]
        public int ImpuestoId { get; set; }
        public double ValorBase { get; set; }
        public string ValorImpuesto { get; set; }
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
