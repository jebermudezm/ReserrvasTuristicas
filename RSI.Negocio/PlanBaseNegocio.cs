using RSI.Modelo.Entidades.Maestros;
using RSI.Modelo.Entidades.Seguridad;
using RSI.Modelo.RepositorioCont;
using RSI.Modelo.RepositorioImpl;
using System;
using System.Collections.Generic;

namespace RSI.Negocio
{
    public class PlanBaseNegocio : ContextBase
    {
        private readonly IPlanRepositorio _planBase;
        private readonly IDestinoRepositorio _destino;

        public PlanBaseNegocio()
        {
            _planBase = new PlanRepositorio(_context);
            _destino = new DestinoRepositorio(_context);
        }
        public List<Plan> ObtenerTodos()
        {
            return _planBase.ObtenerLista();
        }
        public List<Destino> ObtenerDestinos()
        {
            return _destino.ObtenerLista();
        }

        public int Guardar(int id, int destinoId, string codigo, string descripcion, string hotel, string observacion, Usuario usuarioLogueado)
        {
            var plan = new Plan
            {
                Id = id,
                DestinoId = destinoId,
                Codigo = codigo,
                Descripcion = descripcion,
                Hotel = hotel,
                Observacion = observacion
            };
            if (id == -1)
            {
                plan.CreadoPor = usuarioLogueado.UserName;
                plan.FechaCreacion = DateTime.Now;
                var newId = _planBase.Agregar(plan);
                return newId;
            }
            else
            {

                plan.ModificadoPor = usuarioLogueado.UserName;
                plan.FechaModificacion = DateTime.Now;
                _planBase.Actualizar(plan);
                return id;
            }
        }
    }
}
