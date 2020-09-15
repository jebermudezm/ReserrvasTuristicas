using System;
using System.Collections.Generic;
using System.Linq;

namespace RSI.Modelo.RepositorioCont
{
    public interface IRSIRepositorio<T>
    {
        int GuardarCambios();
        int Agregar(T entidad);
        T Obtener(int id);
        void Actualizar(T entidad);
        void Eliminar(T entidad);
        bool Any(Func<T, bool> predicado);
        List<T> ObtenerLista();
        void ValidarEntidad(T entidad);

        IQueryable<T> ObtenerQueryable();
    }
}
