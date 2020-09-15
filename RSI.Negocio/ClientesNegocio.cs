using RSI.Modelo.Entidades.Maestros;
using RSI.Modelo.Entidades.Seguridad;
using RSI.Modelo.RepositorioCont;
using RSI.Modelo.RepositorioImpl;
using System;
using System.Collections.Generic;

namespace RSI.Negocio
{
    public class ClienteNegocio : ContextBase
    {
        private readonly IClienteRepositorio _cliente;
        private readonly IListaRepositorio _documentoIdentidad;

        public ClienteNegocio()
        {
            _cliente = new ClienteRepositorio(_context);
            _documentoIdentidad = new ListaRepositorio(_context);
        }
        public List<Cliente> ObtenerTodos()
        {
            return _cliente.ObtenerLista();
        }
        public List<Lista> ObtenerDocumentos()
        {
            return _documentoIdentidad.ObtenerLista();
        }

        public void Guardar(int id, int IdTipoDoc, string numeroDoc, string nombre, string apodo, DateTime fechaNacimiento, string direccion, string telefono, string correo, string observacion, Usuario usuarioLogueado)
        {
            var cliente = new Cliente
            {
                Id = id,
                DocumentoIdentidadId = IdTipoDoc,
                NumeroDocumentoIdentidad = numeroDoc,
                NombreORazonSocial = nombre,
                Apodo = apodo,
                FechaNacimiento = fechaNacimiento,
                Direccion = direccion,
                Telefono = telefono,
                Correo = correo,
                Observacion = observacion
            };
            if (id == -1)
            {
                cliente.CreadoPor = usuarioLogueado.UserName;
                cliente.FechaCreacion = DateTime.Now;
                var newId = _cliente.Agregar(cliente);
            }
            else
            {

                cliente.ModificadoPor = usuarioLogueado.UserName;
                cliente.FechaModificacion = DateTime.Now;
                _cliente.Actualizar(cliente);
            }
        }
    }
}
