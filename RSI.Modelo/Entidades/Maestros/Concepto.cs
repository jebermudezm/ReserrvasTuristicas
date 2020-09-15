using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSI.Modelo.Entidades.Maestros
{
    [Table("Concepto")]
    public class Concepto : Maestro
    {
        [Key]
        public int Id { get; set; }
        [StringLength(20), Required]
        public string Codigo { get; set; }
        [StringLength(150), Required]
        public string Nombre { get; set; }
        public virtual ICollection<ConceptoValor> ConceptoValor { get; set; }
    }
}
