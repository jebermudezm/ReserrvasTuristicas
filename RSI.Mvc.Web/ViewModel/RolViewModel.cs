using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RSI.Mvc.Web.ViewModel
{
    public class RolViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public int Resp_Code { get; set; }

        public string CreadoPor { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; }

        public int? ModificadoPor { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public virtual ICollection<UsuarioViewModel> Usuarios { get; set; }
    }
}