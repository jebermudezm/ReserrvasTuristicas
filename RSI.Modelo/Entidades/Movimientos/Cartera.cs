using RSI.Modelo.Entidades.Maestros;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSI.Modelo.Entidades.Movimientos
{
    [Table("Cartera")]
    public class Cartera
    {
        [Key]
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        [ForeignKey("Cliente")]
        public int ClienteId { get; set; }
        public double Valor { get; set; }
        public string CreadoPor { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public Cliente Cliente { get; set; }
    }
}
