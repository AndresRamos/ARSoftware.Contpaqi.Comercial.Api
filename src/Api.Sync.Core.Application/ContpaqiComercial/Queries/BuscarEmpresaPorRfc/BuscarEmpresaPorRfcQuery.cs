using System.Collections.Immutable;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using Api.Sync.Core.Application.ContpaqiComercial.Models;
using MediatR;

namespace Api.Sync.Core.Application.ContpaqiComercial.Queries.BuscarEmpresaPorRfc;

public sealed record BuscarEmpresaPorRfcQuery(string Rfc) : IRequest<EmpresaDto>;

public sealed class BuscarEmpresaPorRfcQueryHandler : IRequestHandler<BuscarEmpresaPorRfcQuery, EmpresaDto>
{
    private readonly IEmpresaRepository _empresaRepository;

    public BuscarEmpresaPorRfcQueryHandler(IEmpresaRepository empresaRepository)
    {
        _empresaRepository = empresaRepository;
    }

    public async Task<EmpresaDto> Handle(BuscarEmpresaPorRfcQuery request, CancellationToken cancellationToken)
    {
        ImmutableList<EmpresaDto> empresas = (await _empresaRepository.BuscarEmpresasAsync(cancellationToken)).ToImmutableList();

        return empresas.First(e => e.Rfc == request.Rfc);
    }
}
