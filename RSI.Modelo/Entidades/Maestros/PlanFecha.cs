using RSI.Modelo.Entidades.Movimientos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSI.Modelo.Entidades.Maestros
{
    [Table("PlanFecha")]
    public class PlanFecha:Maestro
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Plan")]
        public int PlanId { get; set; }
        [ForeignKey("Proveedor")]
        public int ProveedorId { get; set; }
        [StringLength(15), Required]
        public string Codigo { get; set; }
        [StringLength(150), Required]
        public string Nombre { get; set; }
        public DateTime FechaSalida { get; set; }
        public DateTime FechaRegreso { get; set; } 
        public virtual Plan Plan { get; set; }
        public virtual Proveedor Proveedor { get; set; }
        public virtual ICollection<CuposAcomodacion> CuposAcomodacion { get; set; }
    }
}
