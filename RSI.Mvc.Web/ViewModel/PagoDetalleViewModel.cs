using System;
using System.ComponentModel.DataAnnotations;

namespace RSI.Mvc.Web.ViewModel
{
    public class PagoDetalleViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Pago")]
        public int PagoId { get; set; }
        [Display(Name = "Item")]
        public int Item { get; set; }
        [Display(Name = "Fecha"), DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }
        public double Valor { get; set; }
        public string CreadoPor { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime? FechaModificacion { get; set; }
        [Display(Name = "Nombre Pago")]
        public string Pago { get; set; }
    }
}
