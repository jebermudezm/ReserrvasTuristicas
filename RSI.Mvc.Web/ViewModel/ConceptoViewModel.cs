namespace RSI.Mvc.Web.ViewModel
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class ConceptoViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Código"), StringLength(maximumLength: 20, MinimumLength = 1, ErrorMessage = "El campo {0} debe tener minímo {1} y maxímo {2} caracteres"), Required]
        public string Codigo { get; set; }
        [Display(Name = "Nombre"), StringLength(maximumLength: 150, MinimumLength = 1, ErrorMessage = "El campo {0} debe tener minímo {1} y maxímo {2} caracteres"), Required]
        public string Nombre { get; set; }

        public ICollection<ConceptoValorViewModel> ConceptoValor { get; set; }
    }
}