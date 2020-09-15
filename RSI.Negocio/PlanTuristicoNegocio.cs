using RSI.Modelo.Entidades.Maestros;
using RSI.Modelo.Entidades.Seguridad;
using RSI.Modelo.RepositorioCont;
using RSI.Modelo.RepositorioImpl;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RSI.Negocio
{
    public class PlanTuristicoNegocio : ContextBase
    {
        private readonly IPlanRepositorio _planBase;
        private readonly IPlanTuristicoRepositorio _planTuristico;
        private readonly IDestinoRepositorio _destino;
        private readonly IProveedorRepositorio _proveedor;

        public PlanTuristicoNegocio()
        {
            _planBase = new PlanRepositorio(_context);
            _planTuristico = new PlanTuristicoRepositorio(_context);
            _destino = new DestinoRepositorio(_context);
            _proveedor = new ProveedorRepositorio(_context);
        }
        public List<PlanTuristico> ObtenerTodos()
        {
            return _planTuristico.ObtenerLista();
        }
        public List<Destino> ObtenerDestinos()
        {
            return _destino.ObtenerLista();
        }
        public List<Plan> ObtenerPlanBase()
        {
            return _planBase.ObtenerLista();
        }

        public List<Proveedor> ObtenerProveedores()
        {
            return _proveedor.ObtenerLista();
        }

        public void Guardar(int id, int destinoId, string codigo, string descripcion, string hotel, DateTime fechaSalida, 
            DateTime fechaRegreso, double costoAdulto, double costoMenor, double costoInfante, double valorAdulto, 
            double valorMenor, double valorInfante, int proveedorId, string observacion, Usuario usuarioLogueado)
        {
            var plan = new PlanTuristico
            {
                Id = id
                ,Codigo = codigo
                ,Descripcion = descripcion
                ,Hotel = hotel
                ,FechaSalida = fechaSalida
                ,Fecharegreso = fechaRegreso
                ,CostoAdulto = costoAdulto
                ,CostoMenor = costoMenor
                ,CostoInfante = costoInfante
                ,ValorAdulto = valorAdulto
                ,ValorMenor = valorMenor
                ,ValorInfante = valorInfante
                ,DestinoId = destinoId
                ,ProveedorId = proveedorId
                ,Observacion = observacion
            };
            if (id == -1)
            {
                plan.CreadoPor = usuarioLogueado.UserName;
                plan.FechaCreacion = DateTime.Now;
                var newId = _planTuristico.Agregar(plan);
            }
            else
            {

                plan.ModificadoPor = usuarioLogueado.UserName;
                plan.FechaModificacion = DateTime.Now;
                _planTuristico.Actualizar(plan);
            }
        }
    }
}
