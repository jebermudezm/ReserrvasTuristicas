using System;
using System.ComponentModel.DataAnnotations;

namespace RSI.Mvc.Web.ViewModel
{
    public class ProveedorViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Display(Name = "Número Documento Identidad")]
        [StringLength(20), Required]
        public string NumeroDocumentoIdentidad { get; set; }
        [Display(Name = "Tipo Persona")]
        public int TipoPersonaId { get; set; }
        [Display(Name = "Nombre Persona")]
        public string TipoPersona { get; set; }

        [StringLength(150), Required]
        [Display(Name = "Nombre o Razón Social")]
        public string NombreORazonSocial { get; set; }

        [StringLength(150), Required]
        [Display(Name = "Contacto")]
        public string Contacto { get; set; }

        [StringLength(150)]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        [StringLength(15)]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        [StringLength(150)]
        [Display(Name = "Correo")]
        public string Correo { get; set; }
        [StringLength(500)]
        [Display(Name = "Observación")]
        public string Observacion { get; set; }
        [Display(Name = "Documento Id")]
        public int DocumentoIdentidadId { get; set; }
        [Display(Name = "Tipo Documento")]
        public string TipoDocumento { get; set; }
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