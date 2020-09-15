using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSI.Modelo.Entidades.Maestros
{
    [Table("ConceptoValor")]
    public class ConceptoValor:Maestro
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Concepto")]
        public int ConceptoId { get; set; }
        [StringLength(150)]
        public string Valor { get; set; }
        public virtual Concepto Concepto { get; set; }
        public virtual ICollection<CuposAcomodacion> CuposAcomodacion { get; set; }
    }
}
