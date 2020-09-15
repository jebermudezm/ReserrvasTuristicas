using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RSI.Mvc.Web.ViewModel
{
    public class PlanViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required,Display(Name = "Código")]
        public string Codigo { get; set; }
        [Display(Name = "Nombre"), Required]
        public string Descripcion { get; set; }
        [Display(Name = "Hotel")]
        [Required]
        public string Hotel { get; set; }
        [Display(Name = "Destino Id")]
        [Required]
        public int DestinoId { get; set; }
        [Display(Name = "Destino")]
        public string Destino { get; set; }
        public int ProveedorId { get; set; }
        [Display(Name = "Observación")]
        public string Observacion { get; set; }
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
        public ICollection<IncluyeViewModel> Incluye { get; set; }
    }
}