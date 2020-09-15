using System;
using System.ComponentModel.DataAnnotations;

namespace RSI.Mvc.Web.ViewModel
{
    public class ReservaDetalleViewModel
    {
        public int Id { get; set; }
        public int ReservaId { get; set; }
        public int Item { get; set; }
        public int ProductoId { get; set; }
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
        [Required]
        [Display(Name = "Cantidad")]
        public double Cantidad { get; set; }
        [Required]
        [Display(Name = "Valor Unitario")]
        public double ValorUnitario { get; set; }
        public double ValortotalBruto { get; set; }
        [Required]
        [Display(Name = "% Descuento")]
        public double PorcentajeDescuento { get; set; }
        [Required]
        [Display(Name = "Valor Descuento")]
        public double ValorDescuento { get; set; }
        public double ValorAntesImpuesto { get; set; }

        [Required]
        [Display(Name = "% Impuesto")]
        public double PorcentajeImpuesto { get; set; }
        [Required]
        [Display(Name = "Valor Impuesto")]
        public double ValorImpuesto { get; set; }
        [Required]
        [Display(Name = "Valor Total")]
        public double ValorTotal { get; set; }

        [Required]
        public string CreadoPor { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; }

        [StringLength(50)]
        public string ModificadoPor { get; set; }

        public DateTime? FechaModificacion { get; set; }
        
    }
}
