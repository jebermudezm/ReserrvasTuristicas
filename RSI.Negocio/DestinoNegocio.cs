using RSI.Modelo.Entidades.Maestros;
using RSI.Modelo.Entidades.Seguridad;
using RSI.Modelo.RepositorioCont;
using RSI.Modelo.RepositorioImpl;
using System;
using System.Collections.Generic;

namespace RSI.Negocio
{
    public class DestinoNegocio : ContextBase
    {
        private readonly IDestinoRepositorio _destino;

        public DestinoNegocio()
        {
            _destino = new DestinoRepositorio(_context);
        }
        public List<Destino> ObtenerTodos()
        {
            return _destino.ObtenerLista();
        }

        public void Guardar(int id, string codigo, string descripcion, string observacion, Usuario usuarioLogueado)
        {
            var destino = new Destino
            {
                Id = id,
                Codigo = codigo,
                Descripcion = descripcion,
                Observacion = observacion
            };
            if (id == -1)
            {
                destino.CreadoPor = usuarioLogueado.UserName;
                destino.FechaCreacion = DateTime.Now;
                var newId = _destino.Agregar(destino);
            }
            else
            {
                destino.ModificadoPor = usuarioLogueado.UserName;
                destino.FechaModificacion = DateTime.Now;
                _destino.Actualizar(destino);
            }
        }
    }
}
