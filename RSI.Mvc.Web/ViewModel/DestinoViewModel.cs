using System;
using System.ComponentModel.DataAnnotations;

namespace RSI.Mvc.Web.ViewModel
{
    public class DestinoViewModel
    {
        public int Id { get; set; }
        [StringLength(4), Required]
        [Display(Name = "Código")]
        public string Codigo { get; set; }
        [Display(Name = "Nombre")]
        [StringLength(150), Required]
        public string Descripcion { get; set; }
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
