using RSI.Modelo.Entidades.Maestros;
using System.Linq;

namespace RSI.Modelo.RepositorioCont
{
    public interface IListaRepositorio : IRSIRepositorio<Lista>
    {
        IQueryable<TipoLista> ObtenerTipoListaQueryable();
    }
}
