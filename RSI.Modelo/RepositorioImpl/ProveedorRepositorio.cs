using RSI.Modelo.Entidades.Maestros;
using RSI.Modelo.Entidades.Seguridad;
using RSI.Modelo.RepositorioCont;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RSI.Modelo.RepositorioImpl
{
    public class ProveedorRepositorio : RepositorioBase, RepositorioCont.IProveedorRepositorio
    {
        public ProveedorRepositorio(RSIModelContext modelContext) : base(modelContext)
        {
        }

        public void Actualizar(Proveedor entidad)
        {
            var proveedor = modelContext.Proveedores.FirstOrDefault(x => x.Id == entidad.Id);
            proveedor.NumeroDocumentoIdentidad = entidad.NumeroDocumentoIdentidad;
            proveedor.NombreORazonSocial = entidad.NombreORazonSocial;
            proveedor.Direccion = entidad.Direccion;
            proveedor.Telefono = entidad.Telefono;
            proveedor.Contacto = entidad.Contacto;
            proveedor.Correo = entidad.Correo;
            proveedor.Observacion = entidad.Observacion;
            proveedor.DocumentoIdentidadId = entidad.DocumentoIdentidadId;
            proveedor.TipoPersonaId = entidad.TipoPersonaId;
            proveedor.ModificadoPor = entidad.ModificadoPor;
            proveedor.FechaModificacion = entidad.FechaModificacion;
            modelContext.SaveChanges();
        }

        public int Agregar(Proveedor entidad)
        {
            modelContext.Proveedores.Add(entidad);
            modelContext.SaveChanges();
            return entidad.Id;
        }

        public bool Any(Func<Proveedor, bool> predicado)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(Proveedor entidad)
        {
            modelContext.Proveedores.Remove(entidad);
            modelContext.SaveChanges();
        }

        public Proveedor Obtener(int id)
        {
            return modelContext.Proveedores.FirstOrDefault(x => x.Id == id);
        }

        public List<Proveedor> ObtenerLista()
        {
            return modelContext.Proveedores.ToList();
        }

        public IQueryable<Proveedor> ObtenerQueryable()
        {
            return modelContext.Proveedores.AsQueryable();
        }

        public void ValidarEntidad(Proveedor entidad)
        {
            List<string> mensajes = new List<string>();
            bool hayEerror = false;
            if (string.IsNullOrEmpty(entidad.NombreORazonSocial))
            {
                mensajes.Add("El nombre o razon social es un campo requerido.");
                hayEerror = true;
            }
            if (string.IsNullOrEmpty(entidad.NumeroDocumentoIdentidad))
            {
                mensajes.Add("El número de documento de identidad es un campo requerido.");
                hayEerror = true;
            }
            if (string.IsNullOrEmpty(entidad.DocumentoIdentidadId.ToString()))
            {
                mensajes.Add("El tipo de documento de Identidad es un campo requerido.");
                hayEerror = true;
            }
            if (!hayEerror)
            {
                var Proveedor = ObtenerQueryable().FirstOrDefault(x => x.NumeroDocumentoIdentidad == entidad.NumeroDocumentoIdentidad);
                if (Proveedor != null)
                {
                    mensajes.Add($"Ya existe registrado un Proveedor con el mismo numero de documento. Id: {Proveedor.Id}, Nombre: {Proveedor.NombreORazonSocial}.");
                    hayEerror = true;
                }
            }
            if (hayEerror)
            {
                throw new InvalidOperationException($"Validación Proveedor: {string.Join(Environment.NewLine, mensajes)}");
            }
        }
    }
}
