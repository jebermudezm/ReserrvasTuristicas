using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSI.Mvc.Web.ViewModel
{
    public class ReservaViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Cliente")]
        public int ClienteId { get; set; }
        [Display(Name = "Nombre Cliente")]
        public string Cliente { get; set; }
        [Display(Name = "Plan Turistico")]
        public int PlanTuristicoId { get; set; }
        [Display(Name = "Descripción Plan")]
        public string Plan { get; set; }
        [Display(Name = "Convenio")]
        public int ConvenioId { get; set; }
        [Display(Name = "Nombre Convenio")]
        public string Convenio { get; set; }

        [Required, Display(Name = "Fecha"),  DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }
        [Display(Name = "Cantidad Personas Mayores de Edad")]
        public double Mayores { get; set; }
        [Display(Name = "Cantidad Personas Menores de Edad")]
        public double Menores { get; set; }
        [Display(Name = "Cantidad Personas Infantes")]
        public double Infantes { get; set; }
        [Display(Name = "Acomodación")]
        public string Acomodacion { get; set; }
        public int UsuarioId { get; set; }
        [Display(Name = "Acesor")]
        public string Usuario { get; set; }
        [Display(Name = "Valor Bruto")]
        public double ValorBruto { get; set; }
        [Display(Name = "Valor Descuento")]
        public double ValorDescuento { get; set; }
        [Display(Name = "Valor impuesto")]
        public double ValorImpuesto { get; set; }
        [Display(Name = "Valor Total")]
        public double ValorTotal { get; set; }

        [Display(Name = "Creado Por")]
        public string CreadoPor { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha Creación")]
        public DateTime FechaCreacion { get; set; }

        [Display(Name = "Modificado Por")]
        public string ModificadoPor { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha Modificación")]
        public DateTime? FechaModificacion { get; set; }

        public List<int> Seleccionados { get; set; }

        public ICollection<ReservaIntegranteViewModel> ReservaIntegrantes { get; set; }
        public ICollection<ReservaDetalleViewModel> ReservaDetalle { get; set; }
    }
}
