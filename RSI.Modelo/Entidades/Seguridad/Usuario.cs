using RSI.Modelo.Entidades.Movimientos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSI.Modelo.Entidades.Seguridad
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public string Cedula { get; set; }

        [StringLength(200), Required]
        public string Nombre { get; set; }
        [StringLength(50), Required]
        public string UserName { get; set; }
        [StringLength(300), Required]
        public string Contrasena { get; set; }
        [StringLength(15), Required]
        public string Telefono { get; set; }
        [StringLength(15), Required]
        public string Estado { get; set; }
        [StringLength(2), Required]
        public string CambiarContrasena { get; set; }
        public DateTime UltimoIngreso { get; set; }
        [StringLength(50), Required]
        public string CreadoPor { get; set; }
        [Required]
        public DateTime FechaCreacion { get; set; }
        [StringLength(50)]
        public string ModificadoPor { get; set; }
        public DateTime? FechaModificacion { get; set; }

        [ForeignKey("Rol")]
        public int RolId { get; set; }

        public virtual Rol Rol { get; set; }
        //public virtual ICollection<ReservaIntegrante> ReservasDetalle { get; set; }
    }
}
