using RSI.Modelo.Entidades.Maestros;
using RSI.Modelo.RepositorioCont;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RSI.Modelo.RepositorioImpl
{
    public class ConvenioRepositorio : RepositorioBase, IConvenioRepositorio
    {
        public ConvenioRepositorio(RSIModelContext modelContext) : base(modelContext)
        {
        }

        public void Actualizar(Convenio entidad)
        {
            var Convenio = modelContext.Convenios.FirstOrDefault(x => x.Id == entidad.Id);
            Convenio.Nombre = entidad.Nombre;
            Convenio.Descuento = entidad.Descuento;
            Convenio.Observacion = entidad.Observacion;
            Convenio.ModificadoPor = entidad.ModificadoPor;
            Convenio.FechaModificacion = entidad.FechaModificacion;
            modelContext.SaveChanges();
        }

        public int Agregar(Convenio entidad)
        {
            modelContext.Convenios.Add(entidad);
            modelContext.SaveChanges();
            return entidad.Id;
        }

        public bool Any(Func<Convenio, bool> predicado)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(Convenio entidad)
        {
            modelContext.Convenios.Remove(entidad);
            modelContext.SaveChanges();
        }

        public Convenio Obtener(int id)
        {
            return modelContext.Convenios.FirstOrDefault(x => x.Id == id);
        }

        public List<Convenio> ObtenerLista()
        {
            return modelContext.Convenios.ToList();
        }

        public IQueryable<Convenio> ObtenerQueryable()
        {
            return modelContext.Convenios.AsQueryable();
        }

        public void ValidarEntidad(Convenio entidad)
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
                var Convenio = ObtenerQueryable().FirstOrDefault(x => x.Nombre == entidad.Nombre);
                if (Convenio != null)
                {
                    mensajes.Add($"Ya existe registrado con el mismo Nombre. Id: {Convenio.Id}, Nombre: {Convenio.Nombre}.");
                    hayEerror = true;
                }
            }
            if (hayEerror)
            {
                throw new InvalidOperationException($"Validación Convenio: {string.Join(Environment.NewLine, mensajes)}");
            }
        }
    }
}
