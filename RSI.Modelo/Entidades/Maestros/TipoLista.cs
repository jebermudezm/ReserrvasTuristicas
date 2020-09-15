using RSI.Modelo.Entidades.Maestros;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSI.Modelo.Entidades.Maestros
{
    [Table("TipoLista")]
    public class TipoLista : Maestro
    {
        [Key]
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public ICollection<Lista> Lista { get; set; }
    }
}
