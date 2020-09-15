using RSI.Modelo.Entidades.Maestros;
using RSI.Modelo.Entidades.Seguridad;
using RSI.Modelo.RepositorioCont;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RSI.Modelo.RepositorioImpl
{
    public class ProductoRepositorio : RepositorioBase, IProductoRepositorio
    {
        public ProductoRepositorio(RSIModelContext modelContext) : base(modelContext)
        {
        }

        public void Actualizar(Producto entidad)
        {
            var producto = modelContext.Productos.FirstOrDefault(x => x.Id == entidad.Id);
            producto.Codigo = entidad.Codigo;
            producto.ProveedorId = entidad.ProveedorId;
            producto.ImpuestoId = entidad.ImpuestoId;
            producto.Nombre = entidad.Nombre;
            producto.Observacion = entidad.Observacion;
            producto.ModificadoPor = entidad.ModificadoPor;
            producto.FechaModificacion = entidad.FechaModificacion;
            modelContext.SaveChanges();
        }

        public int Agregar(Producto entidad)
        {
            modelContext.Productos.Add(entidad);
            modelContext.SaveChanges();
            return entidad.Id;
        }

        public bool Any(Func<Producto, bool> predicado)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(Producto entidad)
        {
            modelContext.Productos.Remove(entidad);
            modelContext.SaveChanges();
        }

        public Producto Obtener(int id)
        {
            return modelContext.Productos.FirstOrDefault(x => x.Id == id);
        }

        public List<Producto> ObtenerLista()
        {
            return modelContext.Productos.ToList();
        }

        public IQueryable<Producto> ObtenerQueryable()
        {
            return modelContext.Productos.AsQueryable();
        }

        public void ValidarEntidad(Producto entidad)
        {
            List<string> mensajes = new List<string>();
            bool hayEerror = false;
            if (string.IsNullOrEmpty(entidad.Nombre))
            {
                mensajes.Add("El nombre es un campo requerido.");
                hayEerror = true;
            }
           
            if (!hayEerror)
            {
                var Producto = ObtenerQueryable().FirstOrDefault(x => x.Codigo == entidad.Codigo);
                if (Producto != null)
                {
                    mensajes.Add("Ya existe registrado un Producto con el mismo Código.");
                    hayEerror = true;
                }
            }
            if (hayEerror)
            {
                throw new InvalidOperationException($"Validación Producto: {string.Join(Environment.NewLine, mensajes)}");
            }
        }
    }
}
