using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RSI.Mvc.Web.ViewModel
{
    public class DetallePlanViewModel
    {
        public int Id { get; set; }
        [Required, Display(Name = "Código")]
        public string Codigo { get; set; }
        [Required, Display(Name = "Nombre")]
        public string Descripcion { get; set; }
        public int PlanTuristicoId { get; set; }
        public string PlanTuristico { get; set; }
        public double Costo { get; set; }
        public double Valor { get; set; }
        public string CreadoPor { get; set; }
        [Display(Name = "Fecha Creación"), DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime FechaCreacion { get; set; }
        public string ModificadoPor { get; set; }
        [Display(Name = "Fecha Modificación"), DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? FechaModificacion { get; set; }
        public ICollection<ItemDetallePlanViewModel> DetalleIncluye { get; set; }
    }
}
