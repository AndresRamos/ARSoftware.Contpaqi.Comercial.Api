using System.Collections.Immutable;
using Api.Core.Domain.Models;
using Api.Sync.Core.Application.Common.Models;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using MediatR;

namespace Api.Sync.Core.Application.ContpaqiComercial.Queries.BuscarEmpresaPorRfc;

public sealed record BuscarEmpresaPorRfcQuery(string Rfc) : IRequest<Empresa?>;

public sealed class BuscarEmpresaPorRfcQueryHandler : IRequestHandler<BuscarEmpresaPorRfcQuery, Empresa?>
{
    private readonly IEmpresaRepository _empresaRepository;

    public BuscarEmpresaPorRfcQueryHandler(IEmpresaRepository empresaRepository)
    {
        _empresaRepository = empresaRepository;
    }

    public async Task<Empresa?> Handle(BuscarEmpresaPorRfcQuery request, CancellationToken cancellationToken)
    {
        ImmutableList<Empresa> empresas = (await _empresaRepository.BuscarTodoAsync(LoadRelatedDataOptions.Default, cancellationToken))
            .ToImmutableList();

        return empresas.FirstOrDefault(e => e.Rfc == request.Rfc);
    }
}
