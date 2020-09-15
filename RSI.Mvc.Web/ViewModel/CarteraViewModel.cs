using RSI.Modelo.Entidades.Maestros;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSI.Modelo.Entidades.Movimientos
{
    public class CarteraViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Fecha de pago"),DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }
        [Display(Name = "Cliente")]
        public int ClienteId { get; set; }
        [Display(Name = "Valor")]
        public double Valor { get; set; }
        public string CreadoPor { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime? FechaModificacion { get; set; }
        [Display(Name = "Noimbre Cliente")]
        public string Cliente { get; set; }
    }
}
