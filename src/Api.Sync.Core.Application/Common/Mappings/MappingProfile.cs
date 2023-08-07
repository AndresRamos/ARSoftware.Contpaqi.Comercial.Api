using ARSoftware.Contpaqi.Comercial.Sdk.DatosAbstractos;
using AutoMapper;

namespace Api.Sync.Core.Application.Common.Mappings;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
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
