using RSI.Modelo.Entidades.Movimientos;
using System.Linq;

namespace RSI.Modelo.RepositorioCont
{
    public interface IFacturaRepositorio : IRSIRepositorio<Factura>
    {
        IQueryable<NumeroValorable> ObtenerNumeroValorableQueryable(string prefijo);
    }
}
