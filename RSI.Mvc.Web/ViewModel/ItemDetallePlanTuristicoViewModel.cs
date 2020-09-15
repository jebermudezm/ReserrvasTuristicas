using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSI.Mvc.Web.ViewModel
{
    public class ItemDetallePlanTuristicoViewModel
    {
        public int Id { get; set; }
        [Required, Display(Name = "Código")]
        public string Codigo { get; set; }
        [Required, Display(Name = "Nombre")]
        public string Descripcion { get; set; }
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
        public int DetallePlanTuristicoId { get; set; }
        public string DetallePlanTuristico { get; set; }
        public string CreadoPor { get; set; }
        [Display(Name = "Fecha Creación"), DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime FechaCreacion { get; set; }
        public string ModificadoPor { get; set; }
        [Display(Name = "Fecha Modificación"), DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? FechaModificacion { get; set; }
    }
}
