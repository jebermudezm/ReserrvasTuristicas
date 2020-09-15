using System;
using System.ComponentModel.DataAnnotations;

namespace RSI.Mvc.Web.ViewModel
{
    public class ReservaIntegranteViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Cliente")]
        public int ClienteId { get; set; }
        [Display(Name = "Nombre Cliente")]
        public string NombreORazonSocial { get; set; }
        [Display(Name = "Identificación Cliente")]
        public string NumeroIdentificacion { get; set; }
        [Required, Display(Name = "Fecha de Nacimiento"), DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime FechaNacimiento { get; set; }
        public int Edad { get; set; }
        [Display(Name = "Acomodación")]
        public string Acomodacion { get; set; }

        [Display(Name = "Reserva")]
        public int ReservaId { get; set; }

        public string CreadoPor { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; }

        [StringLength(50)]
        public string ModificadoPor { get; set; }

        public DateTime? FechaModificacion { get; set; }
        
    }
}
