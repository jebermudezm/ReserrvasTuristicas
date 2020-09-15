using RSI.Modelo.Entidades.Maestros;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSI.Modelo.Entidades.Movimientos
{
    [Table("NumeroValorable")]
    public class NumeroValorable
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("ConceptoValor")]
        public int ConceptoId { get; set; }
        public string Prefijo { get; set; }
        public int Numero { get; set; }
        public ConceptoValor ConceptoValor { get; set; }

    }
}
