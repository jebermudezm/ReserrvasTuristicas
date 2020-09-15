using RSI.Modelo.Entidades.Maestros;

namespace RSI.Modelo.RepositorioCont
{
    public interface IPlanTuristicoRepositorio : IRSIRepositorio<PlanTuristico>
    {
        int AgregarPlanTuristico(PlanTuristico entidad, Plan plan);
        void ActualizarValoresPlanTuristico(int id);
    }
}
