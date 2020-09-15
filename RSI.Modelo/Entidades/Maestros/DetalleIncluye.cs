using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSI.Modelo.Entidades.Maestros
{
    [Table("DetalleIncluye")]
    public class DetalleIncluye:Maestro
    {
        [Key]
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        [ForeignKey("Incluye")]
        public int IncluyeId { get; set; }
        public virtual Incluye Incluye { get; set; }
    }
}
