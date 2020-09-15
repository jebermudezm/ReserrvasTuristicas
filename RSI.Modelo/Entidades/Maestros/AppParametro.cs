using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSI.Modelo.Entidades.Maestros
{
    [Table("AppParametros")]
    public class AppParametro : Maestro
    {
        [Key]
        public int Id { get; set; }
        [StringLength(10), Required]
        public string Codigo { get; set; }
        [StringLength(150), Required]
        public string Descripcion { get; set; }
        [Required]
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        [StringLength(2000), Required]
        public string Valor { get; set; }

    }
}
