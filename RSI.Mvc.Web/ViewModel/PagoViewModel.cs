using System;
using System.ComponentModel.DataAnnotations;

namespace RSI.Mvc.Web.ViewModel
{
    public class PagoViewModel
    {
        public int Id { get; set; }
        public int ReservaId { get; set; }
        [Display(Name = "Descipción Reserva")]
        public string Reserva { get; set; }
        public int ClienteId { get; set; }
        [Display(Name = "Nombre Cliente")]
        public string Cliente { get; set; }
        [Display(Name = "Fecha"), DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }
        [Display(Name = "Valor")]
        public double Valor { get; set; }
        [Display(Name = "Saldo")]
        public double Saldo { get; set; }
        [Display(Name = "Observación")]
        public string Observacion { get; set; }
        public string CreadoPor { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
