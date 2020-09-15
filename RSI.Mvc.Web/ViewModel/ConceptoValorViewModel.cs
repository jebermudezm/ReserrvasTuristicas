namespace RSI.Mvc.Web.ViewModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class ConceptoValorViewModel : MaestroViewModel
    {
        public int Id { get; set; }
        public int ConceptoId { get; set; }
 
       // [Display(Name = "Valor"), StringLength(maximumLength: 150, MinimumLength = 1, ErrorMessage = "El campo {0} debe tener minímo {1} y maxímo {2} caracteres"), Required]
        public string Valor { get; set; }
        
    }
}