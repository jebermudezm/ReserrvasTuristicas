using RSI.Modelo.Entidades.Maestros;
using RSI.Modelo.Entidades.Seguridad;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSI.Modelo.Entidades.Movimientos
{
    [Table("Reserva")]
    public class Reserva
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Cliente")]
        public int ClienteId { get; set; }
        [ForeignKey("PlanTuristico")]
        public int PlanTuristicoId { get; set; }
        [ForeignKey("Convenio")]
        public int ConvenioId { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }
        public double ValorPagado { get; set; }
        public double ValorBruto { get; set; }
        public double PorcentajeDescuento { get; set; }
        public double ValorDescuento { get; set; }
        public double ValorBase { get; set; }
        public double TotalImpuesto { get; set; }
        public double ValorTotal { get; set; }
        public string Acomodacion { get; set; }
        public string Observaciones { get; set; }
        [Required]
        public string CreadoPor { get; set; }
        [Required]
        public DateTime FechaCreacion { get; set; }
        [StringLength(50)]
        public string ModificadoPor { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual PlanTuristico PlanTuristico { get; set; }
        public virtual Convenio Convenio { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<ReservaDetalle> ReservasDetalle { get; set; }
        public virtual ICollection<Pago> Pago { get; set; }
    }
}
