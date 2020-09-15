using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RSI.Mvc.Web.ViewModel
{
    public class ReservaToPrintViewModel
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string Cliente { get; set; }
        public string Telefono { get; set; }
        public string Destino { get; set; }
        public int PlanId { get; set; }
        public string Plan { get; set; }
        public string Hotel { get; set; }
        public int ConvenioId { get; set; }
        public string Convenio { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaSalida { get; set; }
        public DateTime FechaRegreso { get; set; }
        public string Acomodacion { get; set; }
        public int UsuarioId { get; set; }
        public string Usuario { get; set; }
        public double ValorBruto { get; set; }
        public double ValorDescuento { get; set; }
        public double ValorImpuesto { get; set; }
        [DisplayFormat(DataFormatString = "{0:n3}", ApplyFormatInEditMode = true)]
        public double ValorTotal { get; set; }
        public string InformacionGeneral { get; set; }
        public string Cortesia { get; set; }
        

        public ICollection<IntegrantesViewModel> ReservaIntegrantes { get; set; }
        public ICollection<ListasViewModel> Lista { get; set; }
    }
    public class IntegrantesViewModel
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string Cliente { get; set; }
        public string NumeroIdentificacion { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Edad { get; set; }

    }
    public class ListasViewModel
    {
        public string tipo { get; set; }
        public string Descripcion { get; set; }
    }

}
