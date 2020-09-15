using System.Threading.Tasks;
using RSI.Modelo.Entidades.Movimientos;

namespace RSI.Modelo.RepositorioCont
{
    public interface IReservaDetalleRepositorio : IRSIRepositorio<ReservaDetalle>
    {
        void ActualizarValoresReserva(int id);
        void ActualizarDetalle(int id);
    }
}
