using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using RSI.Modelo.Entidades.Maestros;
using RSI.Modelo.Entidades.Movimientos;
using RSI.Modelo.Entidades.Seguridad;
using RSI.Mvc.Web.ViewModel;

namespace RSI.Mvc.Web.Controllers.Helper
{
    public class EntitiesHelper
    {
        public Concepto MapConceptoModel(ConceptoViewModel viewModel)
        {
            return Mapper.Map<Concepto>(viewModel);
        }
        public ConceptoViewModel MapConeptoViewModel(Concepto model)
        {
            return Mapper.Map<ConceptoViewModel>(model);
        }
        public ConceptoValor MapConceptoValorModel(ConceptoValorViewModel viewModel)
        {
            return Mapper.Map<ConceptoValor>(viewModel);
        }
        public ConceptoValorViewModel MapConeptoValorViewModel(ConceptoValor model)
        {
            return Mapper.Map<ConceptoValorViewModel>(model);
        }

        public Usuario MapUsuarioModel(UsuarioViewModel viewModel)
        {
            return Mapper.Map<Usuario>(viewModel);
        }
        public UsuarioViewModel MapUsuarioViewModel(Usuario model)
        {
            return Mapper.Map<UsuarioViewModel>(model);
        }

        public List<UsuarioViewModel> MapListUsuarioViewModel(List<Usuario> lista)
        {
            return Mapper.Map<List<UsuarioViewModel>>(lista);
        }

        public Cliente MapClienteModel(ClienteViewModel viewModel)
        {
            return Mapper.Map<Cliente>(viewModel);
        }
        public ClienteViewModel MapClienteViewModel(Cliente item, List<Lista> lista)
        {
            return new ClienteViewModel
            {
                Id = item.Id,
                DocumentoIdentidadId = item.DocumentoIdentidadId,
                TipoDocumento = lista.FirstOrDefault(x => x.Id == item.DocumentoIdentidadId)?.Descripcion ?? "",
                TipoPersonaId = item.TipoPersonaId,
                TipoPersona = lista.FirstOrDefault(x => x.Id == item.TipoPersonaId)?.Descripcion ?? "",
                NumeroDocumentoIdentidad = item.NumeroDocumentoIdentidad,
                NombreORazonSocial = item.NombreORazonSocial,
                Apodo = item.Apodo,
                FechaNacimiento = item.FechaNacimiento,
                Direccion = item.Direccion,
                Telefono = item.Telefono,
                Correo = item.Correo,
                Observacion = item.Observacion,
                CreadoPor = item.CreadoPor,
                FechaCreacion = item.FechaCreacion,
                ModificadoPor = item.ModificadoPor,
                FechaModificacion = item.FechaModificacion
            };
        }

       
        public Proveedor MapProveedorModel(ProveedorViewModel viewModel)
        {
            return Mapper.Map<Proveedor>(viewModel);
        }
        public ProveedorViewModel MapProveedorViewModel(Proveedor item, List<Lista> lista)
        {
            return new ProveedorViewModel
            {
                Id = item.Id,
                DocumentoIdentidadId = item.DocumentoIdentidadId,
                TipoDocumento = lista.FirstOrDefault(x => x.Id == item.DocumentoIdentidadId)?.Descripcion ?? "",
                TipoPersonaId = item.TipoPersonaId,
                TipoPersona = lista.FirstOrDefault(x => x.Id == item.TipoPersonaId)?.Descripcion ?? "",
                NumeroDocumentoIdentidad = item.NumeroDocumentoIdentidad,
                NombreORazonSocial = item.NombreORazonSocial,
                Contacto = item.Contacto,
                Direccion = item.Direccion,
                Telefono = item.Telefono,
                Correo = item.Correo,
                Observacion = item.Observacion,
                CreadoPor = item.CreadoPor,
                FechaCreacion = item.FechaCreacion,
                ModificadoPor = item.ModificadoPor,
                FechaModificacion = item.FechaModificacion
            };
        }

        public ProductoViewModel MapProductoViewModel(Producto item, List<Lista> lista)
        {
            return new ProductoViewModel
            {
                Id = item.Id,
                ProveedorId = item.ProveedorId,
                Proveedor = item.Proveedor.NombreORazonSocial,
                ImpuestoId = item.ImpuestoId,
                Impuesto = lista.FirstOrDefault(x => x.Id == item.ImpuestoId)?.Descripcion ?? "",
                Codigo = item.Codigo,
                Nombre = item.Nombre,
                Costo = item.Costo,
                Valor = item.Valor,
                Observacion = item.Observacion
            };
        }
        
        public Producto MapProductoModel(ProductoViewModel viewModel)
        {
            return Mapper.Map<Producto>(viewModel);
        }

        public Destino MapDestinoModel(DestinoViewModel viewModel)
        {
            return Mapper.Map<Destino>(viewModel);
        }
        public DestinoViewModel MapDestinoViewModel(Destino model)
        {
            return Mapper.Map<DestinoViewModel>(model);
        }
        
        public Plan MapPlanModel(PlanViewModel viewModel)
        {
            return Mapper.Map<Plan>(viewModel);
        }
        public PlanViewModel MapPlanViewModel(Plan model)
        {
            return Mapper.Map<PlanViewModel>(model);
        }

        public PlanFecha MapPlanFechaModel(PlanFechaViewModel viewModel)
        {
            return Mapper.Map<PlanFecha>(viewModel);
        }

        internal ICollection<CuposAcomodacionViewModel> MapCuposAcomodacionModel(object p)
        {
            throw new NotImplementedException();
        }

        public PlanFechaViewModel MapPlanFechaViewModel(PlanFecha model)
        {
            return Mapper.Map<PlanFechaViewModel>(model);
        }

        public Incluye MapIncluyeModel(IncluyeViewModel viewModel)
        {
            return Mapper.Map<Incluye>(viewModel);
        }

        public IncluyeViewModel MapIncluyeViewModel(Incluye model)
        {
            return Mapper.Map<IncluyeViewModel>(model);
        }
        public DetalleIncluye MapDetalleIncluyeModel(DetalleIncluyeViewModel viewModel)
        {
            return Mapper.Map<DetalleIncluye>(viewModel);
        }

        public DetalleIncluyeViewModel MapDetalleIncluyeViewModel(DetalleIncluye model)
        {
            return Mapper.Map<DetalleIncluyeViewModel>(model);
        }

        public Convenio MapConvenioModel(ConvenioViewModel viewModel)
        {
            return Mapper.Map<Convenio>(viewModel);
        }

        public ConvenioViewModel MapConvenioViewModel(Convenio model)
        {
            return Mapper.Map<ConvenioViewModel>(model);
        }

        public PlanTuristico MapPlanTuristicoModel(PlanTuristicoViewModel viewModel)
        {
            return Mapper.Map<PlanTuristico>(viewModel);
        }
        public PlanTuristicoViewModel MapPlanTuristicoViewModel(PlanTuristico model)
        {
            return Mapper.Map<PlanTuristicoViewModel>(model);
        }

        public DetallePlanTuristico MapDetallePlanTuristicoModel(DetallePlanTuristicoViewModel viewModel)
        {
            return Mapper.Map<DetallePlanTuristico>(viewModel);
        }
        public DetallePlanTuristicoViewModel MapDetallePlanTuristicoViewModel(DetallePlanTuristico model)
        {
            return Mapper.Map<DetallePlanTuristicoViewModel>(model);
        }

        public ItemDetallePlanTuristico MapItemDetallePlanTuristicoModel(ItemDetallePlanTuristicoViewModel viewModel)
        {
            return Mapper.Map<ItemDetallePlanTuristico>(viewModel);
        }
        public ItemDetallePlanTuristicoViewModel MapItemDetallePlanesTuristicosViewModel(ItemDetallePlanTuristico model)
        {
            return Mapper.Map<ItemDetallePlanTuristicoViewModel>(model);
        }

        public Reserva MapReservaModel(ReservaViewModel viewModel)
        {
            return Mapper.Map<Reserva>(viewModel);
        }

        public ReservaViewModel MapReservaViewModel(Reserva model)
        {
            return Mapper.Map<ReservaViewModel>(model);
        }

        public ReservaIntegrante MapReservaIntegranteModel(ReservaIntegranteViewModel viewModel)
        {
            return Mapper.Map<ReservaIntegrante>(viewModel);
        }

        public ReservaIntegranteViewModel MapReservaIntegranteViewModel(ReservaIntegrante viewModel)
        {
            return Mapper.Map<ReservaIntegranteViewModel>(viewModel);
        }

        public ReservaDetalleViewModel MapReservaDetalleViewModel(ReservaDetalle model)
        {
            return Mapper.Map<ReservaDetalleViewModel>(model);
        }
        public ReservaDetalle MapReservaDetalleModel(ReservaDetalleViewModel model)
        {
            return Mapper.Map<ReservaDetalle>(model);
        }

        public CuposAcomodacion MapCuposAcomodacionModel(CuposAcomodacionViewModel viewModel)
        {
            return Mapper.Map<CuposAcomodacion>(viewModel);
        }

        public CuposAcomodacionViewModel MapCuposAcomodacionModel(CuposAcomodacion model)
        {
            return Mapper.Map<CuposAcomodacionViewModel>(model);
        }

        public Factura MapFacturaModel(FacturaViewModel viewModel)
        {
            return Mapper.Map<Factura>(viewModel);
        }
        public FacturaViewModel MapFacturaViewModel(Factura viewModel)
        {
            return Mapper.Map<FacturaViewModel>(viewModel);
        }

        public FacturaDetalleViewModel MapCuposAcomodacionModel(FacturaDetalle model)
        {
            return Mapper.Map<FacturaDetalleViewModel>(model);
        }
        public FacturaDetalleViewModel MapFacturaDetalleViewModel(FacturaDetalle model)
        {
            return Mapper.Map<FacturaDetalleViewModel>(model);
        }
        public FacturaDetalle MapFacturaDetalleModel(FacturaDetalleViewModel viewModel)
        {
            return Mapper.Map<FacturaDetalle>(viewModel);
        }
        public Pago MapPagoModel(PagosReservaViewModel viewModel)
        {
            return new Pago {
                ReservaId = viewModel.ReservaId,
                Fecha = viewModel.Fecha,
                Valor = viewModel.Valor,
                Saldo = viewModel.Saldo,
                Observacion =viewModel.Observacion
            };
        }
        public PagosReservaViewModel MapPagoViewModel(Pago model)
        {
            var viewModel = new PagosReservaViewModel
            {
                Id = model.Id,
                ReservaId = model.ReservaId,
                Fecha = model.Fecha,
                Valor = model.Valor,
                Saldo = model.Saldo,
                Observacion = model.Observacion
            };
            return viewModel;
        }
        

        public NumeroValorableViewModel MapPagoNumeroValorableViewModel(NumeroValorable model)
        {
            return Mapper.Map<NumeroValorableViewModel>(model);
        }
        public NumeroValorable MapNumeroValorableModel(NumeroValorableViewModel viewModel)
        {
            return Mapper.Map<NumeroValorable>(viewModel);
        }
    }
}