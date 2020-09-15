
namespace RSI.Modelo.RepositorioImpl
{
    public abstract class RepositorioBase
    {
        protected readonly RSIModelContext modelContext;

        public RepositorioBase(RSIModelContext RSIModeloContext)
        {
            this.modelContext = RSIModeloContext;
        }

        public int GuardarCambios()
        {
            try
            {
                return modelContext.SaveChanges();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            
        }
    }

}