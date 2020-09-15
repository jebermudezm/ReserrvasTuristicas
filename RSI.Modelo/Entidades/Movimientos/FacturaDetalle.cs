using RSI.Modelo.Entidades.Maestros;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSI.Modelo.Entidades.Movimientos
{
    [Table("FacturaDetalle")]
    public class FacturaDetalle
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Factura")]
        public int FacturaId { get; set; }
        public int Item { get; set; }
        public double Cantidad { get; set; }
        public string Descripcion { get; set; }
        public double ValorUnitario { get; set; }
        public double ValorBruto { get; set; }
        public double PorcentajeDescuento { get; set; }
        public double ValorAntesImpuesto { get; set; }
        public double PorcentajeIVA { get; set; }
        public double ValorIVA { get; set; }
        public double ValorNeto { get; set; }
        public string CreadoPor { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public virtual Factura Factura { get; set; }
    }
}
