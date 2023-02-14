﻿using Api.Core.Domain.Models;
using Api.Sync.Core.Application.ContpaqiComercial.Models;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Helpers;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Models;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Models.Enums;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Generales;
using AutoMapper;
using Agente = Api.Core.Domain.Models.Agente;
using Almacen = Api.Core.Domain.Models.Almacen;
using Direccion = Api.Core.Domain.Models.Direccion;
using Documento = Api.Core.Domain.Models.Documento;
using Movimiento = Api.Core.Domain.Models.Movimiento;
using Producto = Api.Core.Domain.Models.Producto;

namespace Api.Sync.Infrastructure.Common.Mappings;

public sealed class MappingsProfile : Profile
{
    public MappingsProfile()
    {
        CreateMap<string, string>().ConvertUsing(s => s ?? string.Empty);

        CreateMap<admDocumentos, Documento>()
            .ForMember(dest => dest.Fecha, opt => opt.MapFrom(src => src.CFECHA))
            .ForMember(dest => dest.Serie, opt => opt.MapFrom(src => src.CSERIEDOCUMENTO))
            .ForMember(dest => dest.Folio, opt => opt.MapFrom(src => src.CFOLIO))
            .ForMember(dest => dest.Referencia, opt => opt.MapFrom(src => src.CREFERENCIA))
            .ForMember(dest => dest.TipoCambio, opt => opt.MapFrom(src => src.CTIPOCAMBIO))
            .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.CTOTAL))
            .ForMember(dest => dest.Observaciones, opt => opt.MapFrom(src => src.COBSERVACIONES))
            .ForMember(dest => dest.MetodoPago, opt => opt.MapFrom(src => MetodoPagoHelper.ConvertFromSdkValue(src.CCANTPARCI)))
            .ForMember(dest => dest.FormaPago, opt => opt.MapFrom(src => FormaPago.FromClave(src.CMETODOPAG) ?? FormaPago._99))
            .ForMember(dest => dest.Moneda, opt => opt.MapFrom(src => Moneda.FromId(src.CIDMONEDA)));

        CreateMap<admMovimientos, Movimiento>()
            .ForMember(dest => dest.Unidades, opt => opt.MapFrom(src => src.CUNIDADES))
            .ForMember(dest => dest.Precio, opt => opt.MapFrom(src => src.CPRECIO))
            .ForMember(dest => dest.Referencia, opt => opt.MapFrom(src => src.CREFERENCIA))
            .ForMember(dest => dest.Observaciones, opt => opt.MapFrom(src => src.COBSERVAMOV));

        CreateMap<admConceptos, Concepto>()
            .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.CCODIGOCONCEPTO))
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.CNOMBRECONCEPTO));

        CreateMap<admAgentes, Agente>()
            .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.CCODIGOAGENTE))
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.CNOMBREAGENTE));

        CreateMap<admClientes, Cliente>()
            .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.CCODIGOCLIENTE))
            .ForMember(dest => dest.RazonSocial, opt => opt.MapFrom(src => src.CRAZONSOCIAL))
            .ForMember(dest => dest.Rfc, opt => opt.MapFrom(src => src.CRFC));

        CreateMap<admDomicilios, Direccion>()
            .ForMember(dest => dest.Calle, opt => opt.MapFrom(src => src.CNOMBRECALLE))
            .ForMember(dest => dest.NumeroExterior, opt => opt.MapFrom(src => src.CNUMEROEXTERIOR))
            .ForMember(dest => dest.NumeroInterior, opt => opt.MapFrom(src => src.CNUMEROINTERIOR))
            .ForMember(dest => dest.Colonia, opt => opt.MapFrom(src => src.CCOLONIA))
            .ForMember(dest => dest.Ciudad, opt => opt.MapFrom(src => src.CCIUDAD))
            .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.CESTADO))
            .ForMember(dest => dest.CodigoPostal, opt => opt.MapFrom(src => src.CCODIGOPOSTAL))
            .ForMember(dest => dest.Pais, opt => opt.MapFrom(src => src.CPAIS));

        CreateMap<admProductos, Producto>()
            .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.CCODIGOPRODUCTO))
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.CNOMBREPRODUCTO));

        CreateMap<admAlmacenes, Almacen>()
            .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.CCODIGOALMACEN))
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.CNOMBREALMACEN));

        CreateMap<Empresas, EmpresaDto>()
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.CNOMBREEMPRESA))
            .ForMember(dest => dest.Ruta, opt => opt.MapFrom(src => src.CRUTADATOS))
            .ForMember(dest => dest.BaseDatos, opt => opt.MapFrom(src => new DirectoryInfo(src.CRUTADATOS).Name));
    }
}
