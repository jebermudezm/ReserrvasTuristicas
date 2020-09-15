using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSI.Modelo.Entidades.Maestros
{
    [Table("DetallePlanTuristico")]
    public class DetallePlanTuristico:Maestro
    {
        [Key]
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public double CostoAdulto { get; set; }
        public double CostoMenor { get; set; }
        public double CostoInfante { get; set; }
        public double ValorAdulto { get; set; }
        public double ValorMenor { get; set; }
        public double ValorInfante { get; set; }
        [ForeignKey("PlanTuristico")]
        public int PlanTuristicoId { get; set; }
        public virtual PlanTuristico PlanTuristico { get; set; }
        public virtual ICollection<ItemDetallePlanTuristico> ItemDetallePlanTuristico { get; set; }
    }
}
