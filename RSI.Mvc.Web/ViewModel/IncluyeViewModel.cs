using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RSI.Mvc.Web.ViewModel
{
    public class IncluyeViewModel
    {
        public int Id { get; set; }
        [Required, Display(Name = "Código")]
        public string Codigo { get; set; }
        [Required, Display(Name = "Nombre")]
        public string Descripcion { get; set; }
        public int PlanId { get; set; }
        public string Plan { get; set; }
        public string CreadoPor { get; set; }
        [Display(Name = "Fecha Creación"), DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime FechaCreacion { get; set; }
        public string ModificadoPor { get; set; }
        [Display(Name = "Fecha Modificación"), DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? FechaModificacion { get; set; }
        public ICollection<DetalleIncluyeViewModel> DetalleIncluye { get; set; }
    }
}
