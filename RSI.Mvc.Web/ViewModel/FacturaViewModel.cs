using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RSI.Mvc.Web.ViewModel
{
    public class FacturaViewModel
    {
        public int Id { get; set; }
        public string Prefijo { get; set; }
        [Display(Name = "Número")]
        public int Numero { get; set; }
        [Display(Name = "Fecha"), DisplayFormat(DataFormatString = "{0:d hh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }
        [Display(Name = "Cliente")]
        public int ClienteId { get; set; }
        [Display(Name = "Número Identificación Cliente")]
        public string CedulaONit { get; set; }
        [Display(Name = "Nombre Cliente")]
        public string Cliente { get; set; }
        [Display(Name = "Nombre")]
        public int ReservaId { get; set; }
        public string Reserva { get; set; }
        public double ValorBruto { get; set; }
        [Display(Name = "Valor Descuento")]
        public double ValorDescuento { get; set; }
        [Display(Name = "Valor Antes de Impuesto")]
        public double ValorAntesImpuesto { get; set; }
        [Display(Name = "Valor IVA")]
        public double ValorIVA { get; set; }
        [Display(Name = "Valor Neto")]
        public double ValorNeto { get; set; }
        public string CreadoPor { get; set; }
        [Display(Name = "Fecha Creación"), DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime FechaCreacion { get; set; }
        public string ModificadoPor { get; set; }
        [Display(Name = "Fecha Modificación"), DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? FechaModificacion { get; set; }
        public ICollection<FacturaDetalleViewModel> DetalleFactura { get; set; }
    }
}
