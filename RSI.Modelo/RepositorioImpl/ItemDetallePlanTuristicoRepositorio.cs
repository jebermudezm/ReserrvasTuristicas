using RSI.Modelo.Entidades.Maestros;
using RSI.Modelo.RepositorioCont;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RSI.Modelo.RepositorioImpl
{
    public class ItemDetallePlanTuristicoRepositorio : RepositorioBase, IItemDetallePlanTuristicoRepositorio
    {
        public ItemDetallePlanTuristicoRepositorio(RSIModelContext modelContext) : base(modelContext)
        {
        }

        public void Actualizar(ItemDetallePlanTuristico entidad)
        {
            var itemDetallePlanTuristico = modelContext.ItemsDetallePlanTuristico.FirstOrDefault(x => x.Id == entidad.Id);
            itemDetallePlanTuristico.Codigo = entidad.Codigo;
            itemDetallePlanTuristico.Descripcion = entidad.Descripcion;
            itemDetallePlanTuristico.CostoAdulto = entidad.CostoAdulto;
            itemDetallePlanTuristico.CostoMenor = entidad.CostoMenor;
            itemDetallePlanTuristico.CostoInfante = entidad.CostoInfante;
            itemDetallePlanTuristico.ValorAdulto = entidad.ValorAdulto;
            itemDetallePlanTuristico.ValorMenor = entidad.ValorMenor;
            itemDetallePlanTuristico.ValorInfante = entidad.ValorInfante;
            itemDetallePlanTuristico.ModificadoPor = entidad.ModificadoPor;
            itemDetallePlanTuristico.FechaModificacion = entidad.FechaModificacion;
            modelContext.SaveChanges();
        }

        public int Agregar(ItemDetallePlanTuristico entidad)
        {
            modelContext.ItemsDetallePlanTuristico.Add(entidad);
            modelContext.SaveChanges();
            return entidad.Id;
        }

        public bool Any(Func<ItemDetallePlanTuristico, bool> predicado)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(ItemDetallePlanTuristico entidad)
        {
            modelContext.ItemsDetallePlanTuristico.Remove(entidad);
            modelContext.SaveChanges();
        }

        public ItemDetallePlanTuristico Obtener(int id)
        {
            return modelContext.ItemsDetallePlanTuristico.FirstOrDefault(x => x.Id == id);
        }

        public List<ItemDetallePlanTuristico> ObtenerLista()
        {
            return modelContext.ItemsDetallePlanTuristico.ToList();
        }

        public IQueryable<ItemDetallePlanTuristico> ObtenerQueryable()
        {
            return modelContext.ItemsDetallePlanTuristico.AsQueryable();
        }

        public void ValidarEntidad(ItemDetallePlanTuristico entidad)
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
                var ItemDetallePlanTuristico = ObtenerQueryable().FirstOrDefault(x => x.Codigo == entidad.Codigo);
                if (ItemDetallePlanTuristico != null)
                {
                    mensajes.Add($"Ya existe registrado código.");
                    hayEerror = true;
                }
            }
            if (hayEerror)
            {
                throw new InvalidOperationException($"Validación ItemDetallePlanTuristico: {string.Join(Environment.NewLine, mensajes)}");
            }
        }
    }
}
