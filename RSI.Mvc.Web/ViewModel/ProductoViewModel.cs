using RSI.Modelo.Entidades.Maestros;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSI.Mvc.Web.ViewModel
{
    public class ProductoViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Proveedor")]
        public int ProveedorId { get; set; }
        [Display(Name = "Nombre Proveedor")]
        public string Proveedor { get; set; }
        [Display(Name = "Impuesto")]
        public int ImpuestoId { get; set; }
        [Display(Name = "Nombre Impuesto")]
        public string Impuesto { get; set; }
        [Display(Name = "Código")]
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public double Costo { get; set; }
        public double Valor { get; set; }
        public string Observacion { get; set; }
    }
}
