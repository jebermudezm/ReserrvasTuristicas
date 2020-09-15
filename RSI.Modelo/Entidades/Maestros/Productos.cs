using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSI.Modelo.Entidades.Maestros
{
    [Table("Producto")]
    public class Producto : Maestro
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Proveedor")]
        public int ProveedorId { get; set; }
        [ForeignKey("Lista")]
        public int ImpuestoId { get; set; }
        [StringLength(20), Required]
        public string Codigo { get; set; }
        [StringLength(150), Required]
        public string Nombre { get; set; }
        public double Costo { get; set; }
        public double Valor { get; set; }
        public virtual Proveedor Proveedor { get; set; }
        public virtual Lista Lista { get; set; }
    }
}
