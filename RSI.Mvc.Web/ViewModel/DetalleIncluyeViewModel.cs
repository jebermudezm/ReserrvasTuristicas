using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSI.Mvc.Web.ViewModel
{
    public class DetalleIncluyeViewModel
    {
        public int Id { get; set; }
        [Required, Display(Name = "Código")]
        public string Codigo { get; set; }
        [Required, Display(Name = "Nombre")]
        public string Descripcion { get; set; }
        public int IncluyeId { get; set; }
        public string Include { get; set; }
        public string CreadoPor { get; set; }
        [Display(Name = "Fecha Creación"), DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime FechaCreacion { get; set; }
        public string ModificadoPor { get; set; }
        [Display(Name = "Fecha Modificación"), DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? FechaModificacion { get; set; }
    }
}
