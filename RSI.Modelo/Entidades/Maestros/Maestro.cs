using System;
using System.ComponentModel.DataAnnotations;

namespace RSI.Modelo.Entidades.Maestros
{
    public abstract class Maestro
    {
        [Required]
        public string CreadoPor { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; }
        [StringLength(50)]
        public string ModificadoPor { get; set; }
        public DateTime? FechaModificacion { get; set; }

        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        public string Observacion { get; set; }
    }
}
