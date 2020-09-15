using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSI.Modelo.Entidades.Maestros
{
    [Table("Destino")]
    public class Destino:Maestro
    {
        [Key]
        public int Id { get; set; }
        [StringLength(4), Required]
        public string Codigo { get; set; }
        [StringLength(150), Required]
        public string Descripcion { get; set; }
        public virtual ICollection<PlanTuristico> PlanTuristico { get; set; }
        public virtual ICollection<Plan> Plan { get; set; }
    }
}
