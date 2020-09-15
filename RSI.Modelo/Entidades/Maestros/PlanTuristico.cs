using RSI.Modelo.Entidades.Movimientos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSI.Modelo.Entidades.Maestros
{
    [Table("PlanTuristico")]
    public class PlanTuristico:Maestro
    {
        [Key]
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; } 
        public string Hotel { get; set; }
        public DateTime FechaSalida { get; set; }
        public DateTime Fecharegreso { get; set; }
        public double CostoAdulto { get; set; }
        public double CostoMenor { get; set; }
        public double CostoInfante { get; set; }
        public double ValorAdulto { get; set; }
        public double ValorMenor { get; set; }
        public double ValorInfante { get; set; }
        [ForeignKey("Destino")]
        public int DestinoId { get; set; }
        [ForeignKey("Proveedor")]
        public int ProveedorId { get; set; }
        public virtual Destino Destino { get; set; }
        public virtual Proveedor Proveedor { get; set; }
        public virtual ICollection<DetallePlanTuristico> DetallePlanTuristico { get; set; }
        public virtual ICollection<Reserva> Reserva { get; set; }
    }
}
