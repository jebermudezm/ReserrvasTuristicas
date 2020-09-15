using RSI.Modelo.Entidades.Movimientos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSI.Modelo.Entidades.Maestros
{
    [Table("Convenio")]
    public class Convenio : Maestro
    {
        [Key]
        public int Id { get; set; }

        [StringLength(150), Required]
        public string Nombre { get; set; }
        public double Descuento { get; set; }
        public virtual ICollection<Reserva> Clientes { get; set; }

    }
}
