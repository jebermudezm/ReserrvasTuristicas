using RSI.Modelo.Entidades.Movimientos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSI.Modelo.Entidades.Maestros
{
    [Table("Cliente")]
    public class Cliente : Maestro
    {
        [Key]
        public int Id { get; set; }

        [StringLength(20), Required]
        public string NumeroDocumentoIdentidad { get; set; }

        [StringLength(150), Required]
        public string NombreORazonSocial { get; set; }

        [StringLength(50)]
        public string Apodo { get; set; }
        [Required]
        public DateTime FechaNacimiento { get; set; }

        [StringLength(150)]
        public string Direccion { get; set; }

        [StringLength(50)]
        public string Telefono { get; set; }
        public int Edad { get => DateTime.Today.AddTicks(-FechaNacimiento.Ticks).Year - 1; }

        [StringLength(150)]
        public string Correo { get; set; }

        //[ForeignKey("DocumentoIdentidad")]
        public int DocumentoIdentidadId { get; set; }
        public int TipoPersonaId { get; set; }
        //public virtual Lista DocumentoIdentidad { get; set; }
        public virtual ICollection<Reserva> Clientes { get; set; }

    }
}
