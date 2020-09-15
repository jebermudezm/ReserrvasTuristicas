using RSI.Modelo.Entidades.Maestros;

namespace RSI.Modelo.RepositorioCont
{
    public interface IDetallePlanTuristicoRepositorio : IRSIRepositorio<DetallePlanTuristico>
    {
        void ActualizarValoresDetallePlanTuristico(int id);
    }
}
