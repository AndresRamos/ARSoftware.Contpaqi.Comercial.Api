using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using MediatR;

namespace Api.Sync.Core.Application.Requests.Empresas.BuscarEmpresas;

public sealed class BuscarEmpresasRequestHandler : IRequestHandler<BuscarEmpresasRequest, BuscarEmpresasResponse>
{
    private readonly IEmpresaRepository _empresaRepository;

    public BuscarEmpresasRequestHandler(IEmpresaRepository empresaRepository)
    {
        _empresaRepository = empresaRepository;
    }

    public async Task<BuscarEmpresasResponse> Handle(BuscarEmpresasRequest request, CancellationToken cancellationToken)
    {
        List<Empresa> empresas = (await _empresaRepository.BuscarTodoAsync(request.Options, cancellationToken)).ToList();

        return BuscarEmpresasResponse.CreateInstance(empresas);
    }
}
