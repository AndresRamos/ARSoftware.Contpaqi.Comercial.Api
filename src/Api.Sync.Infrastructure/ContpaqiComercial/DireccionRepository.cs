using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.Abstractions.Dtos;
using ARSoftware.Contpaqi.Comercial.Sdk.Abstractions.Enums;
using ARSoftware.Contpaqi.Comercial.Sdk.Abstractions.Repositories;
using AutoMapper;

namespace Api.Sync.Infrastructure.ContpaqiComercial;

public sealed class DireccionRepository : IDireccionRepository
{
    private readonly IDireccionRepository<DireccionDto> _direccionRepository;
    private readonly IMapper _mapper;

    public DireccionRepository(IDireccionRepository<DireccionDto> direccionRepository, IMapper mapper)
    {
        _direccionRepository = direccionRepository;
        _mapper = mapper;
    }

    public Direccion? BuscarDireccionPorId(int id)
    {
        DireccionDto? direccionDto = _direccionRepository.BuscarPorId(id);

        if (direccionDto is null) return null;

        return _mapper.Map<Direccion>(direccionDto);
    }

    public Direccion? BuscarDireccionPorCliente(string codigoCliente, TipoDireccion tipo)
    {
        DireccionDto? direccionDto = _direccionRepository.BuscarPorCliente(codigoCliente, tipo);

        if (direccionDto is null) return null;

        return _mapper.Map<Direccion>(direccionDto);
    }
}
