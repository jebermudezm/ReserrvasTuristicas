using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSI.Modelo.Entidades.Maestros
{
    [Table("Proveedor")]
    public class Proveedor: Maestro
    {
        [Key]
        public int Id { get; set; }

        [StringLength(20), Required]
        public string NumeroDocumentoIdentidad { get; set; }

        [StringLength(150), Required]
        public string NombreORazonSocial { get; set; }

        [StringLength(50), Required]
        public string Contacto { get; set; }

        [StringLength(150), Required]
        public string Direccion { get; set; }

        [StringLength(50), Required]
        public string Telefono { get; set; }
   
        [StringLength(150), Required]
        public string Correo { get; set; }
        //[ForeignKey("TipoDocumento")]
        public int DocumentoIdentidadId { get; set; }
        //[ForeignKey("TipoPersona")]
        public int TipoPersonaId { get; set; }
        //public virtual Lista TipoDocumento { get; set; }
        //public virtual Lista TipoPersona { get; set; }
        public virtual ICollection<PlanTuristico> PlanTuristico { get; set; }
    }
}
