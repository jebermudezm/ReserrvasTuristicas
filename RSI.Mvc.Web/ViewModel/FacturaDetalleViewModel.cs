using System;
using System.ComponentModel.DataAnnotations;

namespace RSI.Mvc.Web.ViewModel
{
    public class FacturaDetalleViewModel
    {
        public int Id { get; set; }
        public int FacturaId { get; set; }
        public string Factura { get; set; }
        [Display(Name = "Item")]
        public int Item { get; set; }
        [Display(Name = "Cantidad")]
        public double Cantidad { get; set; }
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
        [Display(Name = "Valor Unitario")]
        public double ValorUnitario { get; set; }
        [Display(Name = "Valor Bruto")]
        public double ValorBruto { get; set; }
        [Display(Name = "Porcentaje Descuento")]
        public double PorcentajeDescuento { get; set; }
        [Display(Name = "Valor Antes de Impuesto")]
        public double ValorAntesImpuesto { get; set; }
        [Display(Name = "Porcentaje IVA")]
        public double PorcentajeIVA { get; set; }
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
    }
}
