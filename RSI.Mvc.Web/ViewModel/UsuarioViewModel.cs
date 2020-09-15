using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RSI.Mvc.Web.ViewModel
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }
        public string Cedula { get; set; }

        [StringLength(200), Required]
        public string Nombre { get; set; }
        public string UserName { get; set; }
        public string Contrasena { get; set; }
        public string Telefono { get; set; }
        public string Estado { get; set; }
        public string CambiarContrasena { get; set; }
        public DateTime UltimoIngreso { get; set; }
        public string CreadoPor { get; set; }
        [Required]
        public DateTime FechaCreacion { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public int RolId { get; set; }

        //public virtual RolViewModel Rol { get; set; }
    }
}