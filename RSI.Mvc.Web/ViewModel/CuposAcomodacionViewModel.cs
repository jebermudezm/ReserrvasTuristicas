using System.ComponentModel.DataAnnotations;

namespace RSI.Mvc.Web.ViewModel
{
    public class CuposAcomodacionViewModel : MaestroViewModel
    {
        public int Id { get; set; }
        [Display(Name = "PlanFecha")]
        public int PlanFechaId { get; set; }
        public int ConceptoValorId { get; set; }
        [Display(Name = "Acomodación")]
        public string Acomodacion { get; set; }
        [Display(Name = "N° Cupos")]
        public int Cantidad { get; set; }
        [Display(Name = "Valor")]
        public double Valor { get; set; }
        [Display(Name = "Acomodación")]
        [UIHint("DropDownConceptoValor")]
        public ConceptoValorViewModel EntityConceptoValor
        {
            get;
            set;
        }
    }
}
