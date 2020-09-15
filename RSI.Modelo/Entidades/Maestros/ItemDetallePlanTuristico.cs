using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSI.Modelo.Entidades.Maestros
{
    [Table("ItemDetallePlanTuristico")]
    public class ItemDetallePlanTuristico:Maestro
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
        [ForeignKey("DetallePlanTuristico")]
        public int DetallePlanTuristicoId { get; set; }
        public virtual DetallePlanTuristico DetallePlanTuristico { get; set; }
    }
}
