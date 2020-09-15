using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RSI.Mvc.Web.ViewModel
{
    public class ReportePlanTuristicoViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

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
        public string Destino { get; set; }
        [Display(Name = "Proveedor")]
        public string Proveedor { get; set; }
        public double ValorAdulto { get; set; }
        public double ValorMenor { get; set; }
        public double ValorInfante { get; set; }


    }
}