using RSI.Modelo.Entidades.Maestros;
using RSI.Modelo.Entidades.Seguridad;
using RSI.Modelo.RepositorioCont;
using RSI.Modelo.RepositorioImpl;
using System;
using System.Collections.Generic;

namespace RSI.Negocio
{
    public class ProveedorNegocio : ContextBase
    {
        private readonly IProveedorRepositorio _proveedor;
        private readonly IListaRepositorio _documentoIdentidad;

        public ProveedorNegocio()
        {
            _proveedor = new ProveedorRepositorio(_context);
            _documentoIdentidad = new ListaRepositorio(_context);
        }
        public List<Proveedor> ObtenerTodos()
        {
            return _proveedor.ObtenerLista();
        }
        public List<Lista> ObtenerDocumentos()
        {
            return _documentoIdentidad.ObtenerLista();
        }

        public void Guardar(int id, int IdTipoDoc, string numeroDoc, string nombre, string contacto, string direccion, string telefono, string correo, string observacion, Usuario usuarioLogueado)
        {
            var proveedor = new Proveedor
            {
                Id = id,
                DocumentoIdentidadId = IdTipoDoc,
                NumeroDocumentoIdentidad = numeroDoc,
                NombreORazonSocial = nombre,
                Contacto = contacto,
                Direccion = direccion,
                Telefono = telefono,
                Correo = correo,
                Observacion = observacion
            };
            if (id == -1)
            {
                proveedor.CreadoPor = usuarioLogueado.UserName;
                proveedor.FechaCreacion = DateTime.Now;
                var newId = _proveedor.Agregar(proveedor);
            }
            else
            {

                proveedor.ModificadoPor = usuarioLogueado.UserName;
                proveedor.FechaModificacion = DateTime.Now;
                _proveedor.Actualizar(proveedor);
            }
        }
    }
}
