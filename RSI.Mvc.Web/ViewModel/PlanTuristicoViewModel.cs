using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RSI.Mvc.Web.ViewModel
{
    public class PlanTuristicoViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        public int PlanId { get; set; }

        [Display(Name = "Código")]
        public string Codigo { get; set; }
        [Display(Name = "Nombre")]
        public string Descripcion { get; set; }
        [Display(Name = "Fecha Salida"),DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public  DateTime FechaSalida { get; set; }
        [Display(Name = "Fecha Rgreso"),DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime FechaRegreso { get; set; }
        [Display(Name = "Hotel")]
        public string Hotel { get; set; }
        [Display(Name = "Destino")]
        public int DestinoId { get; set; }
        [Display(Name = "Destino")]
        public string Destino { get; set; }
        [Display(Name = "Proveedor")]
        public int ProveedorId { get; set; }
        [Display(Name = "Proveedor")]
        public string Proveedor { get; set; }
        [Display(Name = "Costo Adulto")]
        public double CostoAdulto { get; set; }
        [Display(Name = "Costo Menor")]
        public double CostoMenor { get; set; }
        [Display(Name = "Costo Infante")]
        public double CostoInfante { get; set; }
        [Display(Name = "Valor Adulto")]
        public double ValorAdulto { get; set; }
        [Display(Name = "Valor Menor")]
        public double ValorMenor { get; set; }
        [Display(Name = "Valor Infante")]
        public double ValorInfante { get; set; }
        [Display(Name = "Observación")]
        public string Observacion { get; set; }
        [Display(Name = "Creado Por")]
        public string CreadoPor { get; set; }
        [Display(Name = "Fecha Creación")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime FechaCreacion { get; set; }
        [Display(Name = "Modificado Por")]
        public string ModificadoPor { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha Modificación")]
        public DateTime? FechaModificacion { get; set; }
        public ICollection<DetallePlanTuristicoViewModel> DetallePlanTuristico { get; set; }
    }
}