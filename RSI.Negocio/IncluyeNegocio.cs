using RSI.Modelo.Entidades.Maestros;
using RSI.Modelo.Entidades.Seguridad;
using RSI.Modelo.RepositorioCont;
using RSI.Modelo.RepositorioImpl;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RSI.Negocio
{
    public class IncluyeNegocio : ContextBase
    {
        private readonly IIncluyeRepositorio _incluye;

        public IncluyeNegocio()
        {
            _incluye = new IncluyeRepositorio(_context);
        }
        public List<Incluye> ObtenerTodos()
        {
            return _incluye.ObtenerLista();
        }

        public List<Incluye> ObtenerPorPlan(int planId)
        {
            return _incluye.ObtenerQueryable().Where(x => x.PlanId == planId).ToList();
        }

        public int Guardar(int id, string codigo, string descripcion, string observacion, int idPlan, Usuario usuarioLogueado)
        {
            var existe = _incluye.ObtenerQueryable().Any(x => x.Id == id);
            var incluye = new Incluye
            {
                Id = id,
                Codigo = codigo,
                Descripcion = descripcion,
                PlanId = idPlan,
                Observacion = observacion
            };
            if (!existe)
            {
                incluye.CreadoPor = usuarioLogueado.UserName;
                incluye.FechaCreacion = DateTime.Now;
                var newId = _incluye.Agregar(incluye);
                return newId;
            }
            else
            {

                incluye.ModificadoPor = usuarioLogueado.UserName;
                incluye.FechaModificacion = DateTime.Now;
                _incluye.Actualizar(incluye);
                return id;

            }
            
        }
    }
}
