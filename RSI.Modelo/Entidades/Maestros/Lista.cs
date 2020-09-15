using RSI.Modelo.Entidades.Movimientos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSI.Modelo.Entidades.Maestros
{
    [Table("Lista")]
    public class Lista : Maestro
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("TipoLista")]
        public int TipoListaId { get; set; }
        [StringLength(20), Required]
        public string Codigo { get; set; }
        [StringLength(50), Required]
        public string Nombre { get; set; }
        public DateTime FechaVigencia { get; set; }
        [StringLength(250), Required]
        public string Descripcion { get; set; }
        public double Valor { get; set; }

        public virtual TipoLista TipoLista { get; set; }

    }
}
