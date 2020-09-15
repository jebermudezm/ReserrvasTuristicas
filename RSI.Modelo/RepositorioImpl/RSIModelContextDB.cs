using RSI.Modelo.Entidades.Maestros;
using RSI.Modelo.Entidades.Movimientos;
using RSI.Modelo.Entidades.Seguridad;
using System.Configuration;
using System.Data.Entity;

namespace RSI.Modelo.RepositorioImpl
{
    public class RSIModelContextDB : DbContext, RSIModelContext
    {
        public RSIModelContextDB()
            : base(ConfigurationManager.ConnectionStrings["MS_TableConnectionString"].ConnectionString)
        {
            //throw new System.Exception("Use el GetConnection");
        }

        public RSIModelContextDB(string connectionString) : base(connectionString)

        //: base(ConfigurationManager.ConnectionStrings["MS_TableConnectionString"].ConnectionString)
        //: base("Data Source=tcp:azur00sql03dv.database.windows.net,1433;Initial Catalog=RSI.Modelo_DS;User ID=ConsolaOp-AppsDev@azur00sql03dv;Password=$soOp2017Andes")
        {
            /*this.Database.Connection.ConnectionString =
                "Data Source=DESKTOP-R8E69IS;Initial Catalog=consola;Integrated Security=True";*/
            //MS_TableConnectionString
        }
        #region Aeguridad
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        #endregion
        #region Maestros
        public DbSet<AppParametro> AppParametros { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Destino> Destinos { get; set; }
        public DbSet<Convenio> Convenios { get; set; }
        public DbSet<Plan> Planes { get; set; }
        public DbSet<Incluye> Includes { get; set; }
        public DbSet<DetalleIncluye> DetallesInclude { get; set; }
        public DbSet<PlanTuristico> PlanesTuristicos { get; set; }
        public DbSet<DetallePlanTuristico> DetallesPlanTuristico { get; set; }
        public DbSet<ItemDetallePlanTuristico> ItemsDetallePlanTuristico { get; set; }
        public DbSet<Concepto> Conceptos { get; set; }
        public DbSet<ConceptoValor> ConceptosValor { get; set; }
        public DbSet<PlanFecha> PlanesFecha { get; set; }
        public DbSet<CuposAcomodacion> CuposAcomodaciones { get; set; }
        public DbSet<TipoLista> TipoListas { get; set; }
        public DbSet<Lista> Listas { get; set; }
        public DbSet<Producto> Productos { get; set; }
        #endregion
        #region Movimientos
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<ReservaDetalle> ReservasDetalle { get; set; }
        public DbSet<ReservaIntegrante> ReservaIntegrantes { get; set; }

        public DbSet<Factura> Facturas { get; set; }
        public DbSet<FacturaDetalle> FacturasDetalle { get; set; }

        public DbSet<Pago> Pagos { get; set; }
       
        public DbSet<Cartera> Carteras { get; set; }
        public DbSet<NumeroValorable> NumeroValorables { get; set; }
        public DbSet<ReservaImpuesto> ReservaImpuestos { get; set; }
        #endregion

    }
}