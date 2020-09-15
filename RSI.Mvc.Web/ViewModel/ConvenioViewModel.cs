using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSI.Mvc.Web.ViewModel
{
    public class ConvenioViewModel
    {
        public int Id { get; set; }

        [StringLength(150), Required]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        [Required, Display(Name = "Descuento")]
        public double Descuento { get; set; }
        [Display(Name = "Creado Por")]
        public string CreadoPor { get; set; }
        [Display(Name = "Fecha Creación")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime FechaCreacion { get; set; }
        [Display(Name = "Modificado Por")]
        public string ModificadoPor { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha Modificación")]
        public DateTime? FechaModificacion { get; set; }

    }
}
