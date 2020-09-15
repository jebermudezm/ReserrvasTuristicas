using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RSI.Mvc.Web.ViewModel
{
    public class ExcursionViewModel : MaestroViewModel
    {
        [Required, Display(Name = "Id")]
        public int Id { get; set; }
        [Display(Name = "Plan")]
        public int PlanId { get; set; }
        [Display(Name = "Proveedor")]
        public int ProveedorId { get; set; }
        [Required, Display(Name = "Código")]
        public string Codigo { get; set; }
        [StringLength(150), Required, Display(Name = "Nombre")]
        public string Nombre { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha Salida")]
        public DateTime FechaSalida { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha Regreso")]
        public DateTime FechaRegreso { get; set; }
        [Display(Name = "Plan Turistico")]
        public string Plan { get; set; }
        public string Proveedor { get; set; }
        public ICollection<CuposAcomodacionViewModel> CuposAcomodacion { get; set; }
    }
}