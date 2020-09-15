using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSI.Mvc.Web.ViewModel
{
    public class PagoReservaViewModel
    {
        [Display(Name = "Reserva")]
        public int Id { get; set; }
        [Display(Name = "Descripción Reserva")]
        public string Reserva { get; set; }
        [Display(Name = "Cliente")]
        public int ClienteId { get; set; }
        [Display(Name = "Número Documento")]
        public string NumeroDocumento { get; set; }
        [Display(Name = "Nombre Cliente")]
        public string Nombre { get; set; }
        [Display(Name = "Fecha"), DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }
        [Display(Name = "Valor Reserva")]
        public double Valor { get; set; }
        [Display(Name = "Valor Pagado")]
        public double ValorPagado { get; set; }
        [Display(Name = "Saldo")]
        public double Saldo { get => Valor - ValorPagado;}
       
        public ICollection<PagosReservaViewModel> Pagos { get; set; }
    }
    

    public class PagosReservaViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Reserva")]
        public int ReservaId { get; set; }
        [Display(Name = "Fecha"), DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }
        [Display(Name = "Valor")]
        [Range(0, double.MaxValue, ErrorMessage = "Por Favor Ingrese un número.")]
        public double Valor { get; set; }
        [Display(Name = "Saldo")]
        public double Saldo { get; set; }
        [Display(Name = "Observación")]
        public string Observacion { get; set; }
    }
}