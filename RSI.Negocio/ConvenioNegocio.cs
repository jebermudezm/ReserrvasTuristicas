using RSI.Modelo.Entidades.Maestros;
using RSI.Modelo.Entidades.Seguridad;
using RSI.Modelo.RepositorioCont;
using RSI.Modelo.RepositorioImpl;
using System;
using System.Collections.Generic;

namespace RSI.Negocio
{
    public class ConvenioNegocio : ContextBase
    {
        private readonly IConvenioRepositorio _convenio;

        public ConvenioNegocio()
        {
            _convenio = new ConvenioRepositorio(_context);
        }
        public List<Convenio> ObtenerTodos()
        {
            return _convenio.ObtenerLista();
        }

        public void Guardar(int id, string nombre, double descruento, string observacion, Usuario usuarioLogueado)
        {
            var convenio = new Convenio
            {
                Id = id,
                Nombre = nombre,
                Descuento = descruento,
                Observacion = observacion
            };
            if (id == -1)
            {
                convenio.CreadoPor = usuarioLogueado.UserName;
                convenio.FechaCreacion = DateTime.Now;
                var newId = _convenio.Agregar(convenio);
            }
            else
            {
                convenio.ModificadoPor = usuarioLogueado.UserName;
                convenio.FechaModificacion = DateTime.Now;
                _convenio.Actualizar(convenio);
            }
        }
    }
}
