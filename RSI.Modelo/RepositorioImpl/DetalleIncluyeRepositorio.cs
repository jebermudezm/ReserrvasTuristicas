using RSI.Modelo.Entidades.Maestros;
using RSI.Modelo.RepositorioCont;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RSI.Modelo.RepositorioImpl
{
    public class DetalleIncluyeRepositorio : RepositorioBase, IDetalleIncluyeRepositorio
    {
        public DetalleIncluyeRepositorio(RSIModelContext modelContext) : base(modelContext)
        {
        }

        public void Actualizar(DetalleIncluye entidad)
        {
            var DetalleIncluye = modelContext.DetallesInclude.FirstOrDefault(x => x.Id == entidad.Id);
            DetalleIncluye.Codigo = entidad.Codigo;
            DetalleIncluye.Descripcion = entidad.Descripcion;
            DetalleIncluye.ModificadoPor = entidad.ModificadoPor;
            DetalleIncluye.FechaModificacion = entidad.FechaModificacion;
            modelContext.SaveChanges();
        }

        public int Agregar(DetalleIncluye entidad)
        {
            modelContext.DetallesInclude.Add(entidad);
            modelContext.SaveChanges();
            return entidad.Id;
        }

        public bool Any(Func<DetalleIncluye, bool> predicado)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(DetalleIncluye entidad)
        {
            modelContext.DetallesInclude.Remove(entidad);
            modelContext.SaveChanges();
        }

        public DetalleIncluye Obtener(int id)
        {
            return modelContext.DetallesInclude.FirstOrDefault(x => x.Id == id);
        }

        public List<DetalleIncluye> ObtenerLista()
        {
            return modelContext.DetallesInclude.ToList();
        }

        public IQueryable<DetalleIncluye> ObtenerQueryable()
        {
            return modelContext.DetallesInclude.AsQueryable();
        }

        public void ValidarEntidad(DetalleIncluye entidad)
        {
            List<string> mensajes = new List<string>();
            bool hayEerror = false;
            if (string.IsNullOrEmpty(entidad.Codigo))
            {
                mensajes.Add("El código es un campo requerido.");
                hayEerror = true;
            }
            if (string.IsNullOrEmpty(entidad.Descripcion))
            {
                mensajes.Add("La descripción es un campo requerido.");
                hayEerror = true;
            }
            if (!hayEerror)
            {
                var DetalleIncluye = ObtenerQueryable().FirstOrDefault(x => x.Codigo == entidad.Codigo);
                if (DetalleIncluye != null)
                {
                    mensajes.Add($"Ya existe registrado código.");
                    hayEerror = true;
                }
            }
            if (hayEerror)
            {
                throw new InvalidOperationException($"Validación DetalleIncluye: {string.Join(Environment.NewLine, mensajes)}");
            }
        }
    }
}
