using RSI.Modelo.Entidades.Maestros;
using RSI.Modelo.Entidades.Seguridad;
using RSI.Modelo.RepositorioCont;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RSI.Modelo.RepositorioImpl
{
    public class ClienteRepositorio : RepositorioBase, RepositorioCont.IClienteRepositorio
    {
        public ClienteRepositorio(RSIModelContext modelContext) : base(modelContext)
        {
        }

        public void Actualizar(Cliente entidad)
        {
            var cliente = modelContext.Clientes.FirstOrDefault(x => x.Id == entidad.Id);
            cliente.NumeroDocumentoIdentidad = entidad.NumeroDocumentoIdentidad;
            cliente.TipoPersonaId = entidad.TipoPersonaId;
            cliente.NombreORazonSocial = entidad.NombreORazonSocial;
            cliente.Direccion = entidad.Direccion;
            cliente.Telefono = entidad.Telefono;
            cliente.FechaNacimiento = entidad.FechaNacimiento;
            cliente.Apodo = entidad.Apodo;
            cliente.Correo = entidad.Correo;
            cliente.Observacion = entidad.Observacion;
            cliente.DocumentoIdentidadId = entidad.DocumentoIdentidadId;
            cliente.ModificadoPor = entidad.ModificadoPor;
            cliente.FechaModificacion = entidad.FechaModificacion;
            modelContext.SaveChanges();
        }

        public int Agregar(Cliente entidad)
        {
            modelContext.Clientes.Add(entidad);
            modelContext.SaveChanges();
            return entidad.Id;
        }

        public bool Any(Func<Cliente, bool> predicado)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(Cliente entidad)
        {
            modelContext.Clientes.Remove(entidad);
            modelContext.SaveChanges();
        }

        public Cliente Obtener(int id)
        {
            return modelContext.Clientes.FirstOrDefault(x => x.Id == id);
        }

        public List<Cliente> ObtenerLista()
        {
            return modelContext.Clientes.ToList();
        }

        public IQueryable<Cliente> ObtenerQueryable()
        {
            return modelContext.Clientes.AsQueryable();
        }

        public void ValidarEntidad(Cliente entidad)
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
                var cliente = ObtenerQueryable().FirstOrDefault(x => x.NumeroDocumentoIdentidad == entidad.NumeroDocumentoIdentidad);
                if (cliente != null)
                {
                    mensajes.Add($"Ya existe registrado un cliente con el mismo numero de documento. Id: {cliente.Id}, Nombre: {cliente.NombreORazonSocial}.");
                    hayEerror = true;
                }
            }
            if (hayEerror)
            {
                throw new InvalidOperationException($"Validación Cliente: {string.Join(Environment.NewLine, mensajes)}");
            }
        }
    }
}
