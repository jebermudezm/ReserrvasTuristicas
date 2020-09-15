using RSI.Modelo.Entidades.Movimientos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSI.Modelo.Entidades.Maestros
{
    [Table("Acomodacion")]
    public class CuposAcomodacion : Maestro
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("PlanFecha")]
        public int PlanFechaId { get; set; }
        [ForeignKey("ConceptoValor")]
        public int ConceptoValorId { get; set; }
        public int Cantidad { get; set; }
        public double Valor { get; set; }
        public virtual PlanFecha PlanFecha { get; set; }
        public virtual ConceptoValor ConceptoValor { get; set; }
    }
}
