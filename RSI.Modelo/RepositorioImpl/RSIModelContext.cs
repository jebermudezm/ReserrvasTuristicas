using RSI.Modelo.Entidades.Maestros;
using RSI.Modelo.Entidades.Movimientos;
using RSI.Modelo.Entidades.Seguridad;
using System.Data.Entity;

namespace RSI.Modelo.RepositorioImpl
{
    public interface RSIModelContext
    {
        #region seguridad
        
        DbSet<Usuario> Usuarios { get; set; }
        DbSet<Rol> Roles { get; set; }

        #endregion
        #region Maestros
        DbSet<AppParametro> AppParametros { get; set; }
        DbSet<Cliente> Clientes { get; set; }
        DbSet<Proveedor> Proveedores { get; set; }
        DbSet<Destino> Destinos { get; set; }
        DbSet<Convenio> Convenios { get; set; }
        DbSet<Plan> Planes { get; set; }
        DbSet<Incluye> Includes { get; set; }
        DbSet<DetalleIncluye> DetallesInclude { get; set; }
        DbSet<PlanTuristico> PlanesTuristicos { get; set; }
        DbSet<DetallePlanTuristico> DetallesPlanTuristico { get; set; }
        DbSet<ItemDetallePlanTuristico> ItemsDetallePlanTuristico { get; set; }
        DbSet<Concepto> Conceptos { get; set; }
        DbSet<ConceptoValor> ConceptosValor { get; set; }
        DbSet<PlanFecha> PlanesFecha { get; set; }
        DbSet<CuposAcomodacion> CuposAcomodaciones { get; set; }
        DbSet<TipoLista> TipoListas { get; set; }
        DbSet<Lista> Listas { get; set; }
        DbSet<Producto> Productos { get; set; }

        #endregion

        #region Movimientos
        DbSet<Reserva> Reservas { get; set; }
        DbSet<ReservaDetalle> ReservasDetalle { get; set; }
        DbSet<ReservaIntegrante> ReservaIntegrantes { get; set; }

        DbSet<Factura> Facturas { get; set; }
        DbSet<FacturaDetalle> FacturasDetalle { get; set; }

        DbSet<Pago> Pagos { get; set; }
       
        DbSet<Cartera> Carteras { get; set; }
        DbSet<NumeroValorable> NumeroValorables { get; set; }
        DbSet<ReservaImpuesto> ReservaImpuestos { get; set; }

        #endregion
        int SaveChanges();
    }
}