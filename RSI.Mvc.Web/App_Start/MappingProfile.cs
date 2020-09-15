using AutoMapper;
using RSI.Modelo.Entidades.Maestros;
using RSI.Modelo.Entidades.Movimientos;
using RSI.Modelo.Entidades.Seguridad;
using RSI.Mvc.Web.ViewModel;

namespace RSI.Web.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Concepto
            CreateMap<Concepto, ConceptoViewModel>();
            CreateMap<ConceptoViewModel, Concepto>();
            CreateMap<ConceptoValor, ConceptoValorViewModel>();
            CreateMap<ConceptoValorViewModel, ConceptoValor>();
            #endregion

            #region Usuario
            CreateMap<Usuario, UsuarioViewModel>();
            CreateMap<UsuarioViewModel, Usuario>();
            #endregion

            #region Cliente
            CreateMap<Cliente, ClienteViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                //.ForMember(dest => dest.TipoDocumento, opt => opt.ResolveUsing(src => src.DocumentoIdentidad.Descripcion))
                .ReverseMap();

            CreateMap<ClienteViewModel, Cliente>();
            #endregion

            #region Producto
            CreateMap<Producto, ProductoViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                //.ForMember(dest => dest.TipoDocumento, opt => opt.ResolveUsing(src => src.DocumentoIdentidad.Descripcion))
                .ReverseMap();

            CreateMap<ProductoViewModel, Producto>();
            #endregion

            #region Proveedor
            CreateMap<Proveedor, ProveedorViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                //.ForMember(dest => dest.TipoDocumento, opt => opt.ResolveUsing(src => src.DocumentoIdentidad.Descripcion))
                .ReverseMap();

            CreateMap<ProveedorViewModel, Proveedor>();
            #endregion

            #region Destino
            CreateMap<Destino, DestinoViewModel>();
            CreateMap<DestinoViewModel, Destino>();
            #endregion

            #region plan
            CreateMap<Plan, PlanViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Destino, opt => opt.ResolveUsing(src => src.Destino.Descripcion))
                .ForMember(dest => dest.Incluye, opt => opt.ResolveUsing(src => src.Incluye))
                .ReverseMap();
            CreateMap<PlanViewModel, Plan>();
            #endregion

            #region plan Fecha
            CreateMap<PlanFecha, PlanFechaViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Plan, opt => opt.ResolveUsing(src => src.Plan.Descripcion))
                .ForMember(dest => dest.Proveedor, opt => opt.ResolveUsing(src => src.Proveedor.NombreORazonSocial))
                .ReverseMap();
            CreateMap<PlanFechaViewModel, PlanFecha>();
            #endregion

            #region Incluye
            CreateMap<Incluye, IncluyeViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Plan, opt => opt.ResolveUsing(src => src.Plan.Descripcion))
                .ReverseMap();
            CreateMap<IncluyeViewModel, Incluye>();
            #endregion

            #region Detalle Incluye
            CreateMap<DetalleIncluye, DetalleIncluyeViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Include, opt => opt.ResolveUsing(src => src.Incluye.Descripcion))
                .ReverseMap();
            CreateMap<IncluyeViewModel, Incluye>();
            #endregion

            #region Convenio
            CreateMap<Convenio, ConvenioViewModel>();
            CreateMap<ConvenioViewModel, Convenio>();
            #endregion

            #region Reserva
            CreateMap<Reserva, ReservaViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Cliente, opt => opt.ResolveUsing(src => src.Cliente.NombreORazonSocial))
                .ForMember(dest => dest.Convenio, opt => opt.ResolveUsing(src => src.Convenio.Nombre))
                .ForMember(dest => dest.Usuario, opt => opt.ResolveUsing(src => src.Usuario.Nombre))
                .ReverseMap();
            CreateMap<ReservaViewModel, Reserva>();
            #endregion

            #region Reserva Detalle
            CreateMap<ReservaDetalle, ReservaDetalleViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();
            CreateMap<ReservaDetalleViewModel, ReservaDetalle>();
            #endregion

            #region plan Turistico
            CreateMap<PlanTuristico, PlanTuristicoViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Destino, opt => opt.ResolveUsing(src => src.Destino.Descripcion))
                .ForMember(dest => dest.Proveedor, opt => opt.ResolveUsing(src => src.Proveedor.NombreORazonSocial))
                .ReverseMap();
            CreateMap<PlanTuristicoViewModel, PlanTuristico>();
            #endregion

            #region Detalle Plan Turistico
            CreateMap<DetallePlanTuristico, DetallePlanTuristicoViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.PlanTuristico, opt => opt.ResolveUsing(src => src.PlanTuristico.Descripcion))
                .ReverseMap();
            CreateMap<DetallePlanTuristicoViewModel, DetallePlanTuristico>();
            #endregion

            #region Item Detalle Plan Turistico
            CreateMap<ItemDetallePlanTuristico, ItemDetallePlanTuristicoViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.DetallePlanTuristico, opt => opt.ResolveUsing(src => src.DetallePlanTuristico.Descripcion))
                .ReverseMap();
            CreateMap<ItemDetallePlanTuristicoViewModel, ItemDetallePlanTuristico>();
            #endregion

            #region Cupos Acomodación
            CreateMap<CuposAcomodacion, CuposAcomodacionViewModel>()
                .ForMember(dest => dest.Valor, opt => opt.ResolveUsing(src => src.ConceptoValor.Valor))
                .ReverseMap();
            CreateMap<ItemDetallePlanTuristicoViewModel, ItemDetallePlanTuristico>();
            #endregion

            #region Factura
            CreateMap<Factura, FacturaViewModel>()
                .ForMember(dest => dest.Cliente, opt => opt.ResolveUsing(src => src.Cliente.NombreORazonSocial))
                .ForMember(dest => dest.CedulaONit, opt => opt.ResolveUsing(src => src.Cliente.NumeroDocumentoIdentidad))
                //.ForMember(dest => dest.Reserva, opt => opt.ResolveUsing(src => src.ReservaId))
                .ReverseMap();
            CreateMap<FacturaViewModel, Factura>();
            #endregion

            #region Factura Detalle
            CreateMap<FacturaDetalle, FacturaDetalleViewModel>()
                .ForMember(dest => dest.Factura, opt => opt.ResolveUsing(src => src.Factura.Prefijo + src.Factura.Numero.ToString()))
                .ReverseMap();
            CreateMap<FacturaDetalleViewModel, FacturaDetalle>();
            #endregion

            #region Pago
            CreateMap<Pago, PagoViewModel>();
            CreateMap<PagoViewModel, Pago>();
            #endregion


            #region Pago Detalle
            CreateMap<NumeroValorable, NumeroValorableViewModel>();
            CreateMap<NumeroValorableViewModel, NumeroValorable>();
            #endregion

        }
    }
}