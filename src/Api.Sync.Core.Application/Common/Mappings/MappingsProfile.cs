using Api.Core.Domain.Models;
using ARSoftware.Contpaqi.Comercial.Sdk.DatosAbstractos;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Extensions;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Helpers;
using AutoMapper;

namespace Api.Sync.Core.Application.Common.Mappings;

public sealed class MappingsProfile : Profile
{
    public MappingsProfile()
    {
        CreateMap<Documento, tDocumento>()
            .ForMember(dest => dest.aCodConcepto, opt => opt.MapFrom(src => src.Concepto.Codigo))
            .ForMember(dest => dest.aSerie, opt => opt.MapFrom(src => src.Serie))
            .ForMember(dest => dest.aFolio, opt => opt.MapFrom(src => src.Folio))
            .ForMember(dest => dest.aFecha, opt => opt.MapFrom(src => src.Fecha.ToSdkFecha()))
            .ForMember(dest => dest.aCodigoCteProv, opt => opt.MapFrom(src => src.Cliente.Codigo))
            .ForMember(dest => dest.aNumMoneda, opt => opt.MapFrom(src => src.Moneda.Id))
            .ForMember(dest => dest.aTipoCambio, opt => opt.MapFrom(src => src.TipoCambio))
            .ForMember(dest => dest.aImporte, opt => opt.MapFrom(src => src.Total))
            .ForMember(dest => dest.aCodigoAgente, opt => opt.MapFrom(src => src.Agente.Codigo))
            .ForMember(dest => dest.aReferencia, opt => opt.MapFrom(src => src.Referencia));

        CreateMap<Movimiento, tMovimiento>()
            .ForMember(dest => dest.aCodProdSer, opt => opt.MapFrom(src => src.Producto.Codigo))
            .ForMember(dest => dest.aUnidades, opt => opt.MapFrom(src => src.Unidades))
            .ForMember(dest => dest.aPrecio, opt => opt.MapFrom(src => src.Precio))
            .ForMember(dest => dest.aCodAlmacen, opt => opt.MapFrom(src => src.Almacen.Codigo))
            .ForMember(dest => dest.aReferencia, opt => opt.MapFrom(src => src.Referencia))
            .ForMember(dest => dest.aReferencia, opt => opt.MapFrom(src => src.Referencia));

        CreateMap<SeriesCapas, tSeriesCapas>()
            .ForMember(dest => dest.aUnidades, opt => opt.MapFrom(src => src.Unidades))
            .ForMember(dest => dest.aTipoCambio, opt => opt.MapFrom(src => src.TipoCambio))
            .ForMember(dest => dest.aSeries, opt => opt.MapFrom(src => src.Series))
            .ForMember(dest => dest.aPedimento, opt => opt.MapFrom(src => src.Pedimento))
            .ForMember(dest => dest.aAgencia, opt => opt.MapFrom(src => src.Agencia))
            .ForMember(dest => dest.aFechaPedimento,
                opt => opt.MapFrom(src => src.FechaPedimento != DateTime.MinValue ? src.FechaPedimento.ToSdkFecha() : string.Empty))
            .ForMember(dest => dest.aNumeroLote, opt => opt.MapFrom(src => src.NumeroLote))
            .ForMember(dest => dest.aFechaFabricacion,
                opt => opt.MapFrom(src => src.FechaFabricacion != DateTime.MinValue ? src.FechaFabricacion.ToSdkFecha() : string.Empty))
            .ForMember(dest => dest.aFechaCaducidad,
                opt => opt.MapFrom(src => src.FechaCaducidad != DateTime.MinValue ? src.FechaCaducidad.ToSdkFecha() : string.Empty));

        CreateMap<Cliente, tCteProv>()
            .ForMember(dest => dest.cTipoCliente, opt => opt.MapFrom(src => TipoClienteHelper.ConvertToSdkValue(src.Tipo)))
            .ForMember(dest => dest.cCodigoCliente, opt => opt.MapFrom(src => src.Codigo))
            .ForMember(dest => dest.cRazonSocial, opt => opt.MapFrom(src => src.RazonSocial))
            .ForMember(dest => dest.cRFC, opt => opt.MapFrom(src => src.Rfc))
            .ForMember(dest => dest.cFechaAlta, opt => opt.MapFrom(src => DateTime.Today.ToSdkFecha()))
            .ForMember(dest => dest.cNombreMoneda, opt => opt.MapFrom(src => "Peso Mexicano"))
            .ForMember(dest => dest.cBanVentaCredito, opt => opt.MapFrom(src => 1))
            .ForMember(dest => dest.cEstatus, opt => opt.MapFrom(src => 1));

        CreateMap<Direccion, tDireccion>()
            .ForMember(dest => dest.cTipoDireccion, opt => opt.MapFrom(src => 1))
            .ForMember(dest => dest.cCiudad, opt => opt.MapFrom(src => src.Ciudad))
            .ForMember(dest => dest.cCodigoPostal, opt => opt.MapFrom(src => src.CodigoPostal))
            .ForMember(dest => dest.cColonia, opt => opt.MapFrom(src => src.Colonia))
            .ForMember(dest => dest.cEstado, opt => opt.MapFrom(src => src.Estado))
            .ForMember(dest => dest.cNombreCalle, opt => opt.MapFrom(src => src.Calle))
            .ForMember(dest => dest.cNumeroExterior, opt => opt.MapFrom(src => src.NumeroExterior))
            .ForMember(dest => dest.cNumeroInterior, opt => opt.MapFrom(src => src.NumeroInterior))
            .ForMember(dest => dest.cPais, opt => opt.MapFrom(src => src.Pais));

        CreateMap<Documento, LlaveDocumento>()
            .ForMember(dest => dest.ConceptoCodigo, opt => opt.MapFrom(src => src.Concepto.Codigo))
            .ForMember(dest => dest.Serie, opt => opt.MapFrom(src => src.Serie))
            .ForMember(dest => dest.Folio, opt => opt.MapFrom(src => src.Folio));

        CreateMap<LlaveDocumento, tLlaveDoc>()
            .ForMember(dest => dest.aCodConcepto, opt => opt.MapFrom(src => src.ConceptoCodigo))
            .ForMember(dest => dest.aSerie, opt => opt.MapFrom(src => src.Serie))
            .ForMember(dest => dest.aFolio, opt => opt.MapFrom(src => src.Folio))
            .ReverseMap();
    }
}
