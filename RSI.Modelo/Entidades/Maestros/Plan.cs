using RSI.Modelo.Entidades.Movimientos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSI.Modelo.Entidades.Maestros
{
    [Table("Plan")]
    public class Plan:Maestro
    {
        [Key]
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; } 
        public string Hotel { get; set; }
        [ForeignKey("Destino")]
        public int DestinoId { get; set; }
        public virtual Destino Destino { get; set; }
        public virtual ICollection<Incluye> Incluye { get; set; }
    }
}
