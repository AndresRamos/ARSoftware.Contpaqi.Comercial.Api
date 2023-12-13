using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.Abstractions.Dtos;
using ARSoftware.Contpaqi.Comercial.Sdk.Abstractions.Repositories;
using AutoMapper;

namespace Api.Sync.Infrastructure.ContpaqiComercial;

public class UnidadMedidaRepository : IUnidadMedidaRepository
{
    private readonly IMapper _mapper;
    private readonly IUnidadMedidaRepository<UnidadMedidaDto> _unidadMedidaRepository;

    public UnidadMedidaRepository(IUnidadMedidaRepository<UnidadMedidaDto> unidadMedidaRepository, IMapper mapper)
    {
        _unidadMedidaRepository = unidadMedidaRepository;
        _mapper = mapper;
    }

    public UnidadMedida? BuscarPorId(int id)
    {
        UnidadMedidaDto? unidadMedidaDto = _unidadMedidaRepository.BuscarPorId(id);

        if (unidadMedidaDto is null) return null;

        var unidadMedida = _mapper.Map<UnidadMedida>(unidadMedidaDto);

        return unidadMedida;
    }

    public UnidadMedida? BuscarPorNombre(string nombre)
    {
        UnidadMedidaDto? unidadMedidaDto = _unidadMedidaRepository.BuscarPorNombre(nombre);

        if (unidadMedidaDto is null) return null;

        var unidadMedida = _mapper.Map<UnidadMedida>(unidadMedidaDto);

        return unidadMedida;
    }

    public List<UnidadMedida> BuscarTodo()
    {
        List<UnidadMedidaDto> unidadesMedidaDto = _unidadMedidaRepository.TraerTodo();

        var unidadesMedida = _mapper.Map<List<UnidadMedida>>(unidadesMedidaDto);

        return unidadesMedida;
    }
}
