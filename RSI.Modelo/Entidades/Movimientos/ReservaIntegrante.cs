using RSI.Modelo.Entidades.Maestros;
using RSI.Modelo.Entidades.Seguridad;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSI.Modelo.Entidades.Movimientos
{
    [Table("ReservaIntegrante")]
    public class ReservaIntegrante
    {
        [Key]
        public int Id { get; set; }
        //[ForeignKey("ClienteId")]
        public int ClienteId { get; set; }
        public string Nombre { get; set; }
        public string NumeroDocumento { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Edad { get; set; }

        [ForeignKey("Reserva")]
        public int ReservaId { get; set; }
        [Required]
        public string CreadoPor { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; }

        [StringLength(50)]
        public string ModificadoPor { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public virtual Reserva Reserva { get; set; }
        //public virtual Cliente Cliente { get; set; }
    }
}
