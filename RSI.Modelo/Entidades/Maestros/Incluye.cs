using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSI.Modelo.Entidades.Maestros
{
    [Table("Incluye")]
    public class Incluye:Maestro
    {
        [Key]
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        [ForeignKey("Plan")]
        public int PlanId { get; set; }
        public virtual Plan Plan { get; set; }
        public virtual ICollection<DetalleIncluye> DetalleIncluye { get; set; }
    }
}
