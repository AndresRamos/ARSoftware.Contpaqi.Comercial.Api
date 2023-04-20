using Api.Core.Domain.Common;
using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Api.Sync.Core.Application.Empresas;

public sealed class BuscarEmpresasRequestHandler : IRequestHandler<BuscarEmpresasRequest, ApiResponse>
{
    private readonly IEmpresaRepository _empresaRepository;
    private readonly ILogger _logger;

    public BuscarEmpresasRequestHandler(IEmpresaRepository empresaRepository, ILogger<BuscarEmpresasRequestHandler> logger)
    {
        _empresaRepository = empresaRepository;
        _logger = logger;
    }

    public async Task<ApiResponse> Handle(BuscarEmpresasRequest request, CancellationToken cancellationToken)
    {
        try
        {
            List<Empresa> empresas = (await _empresaRepository.BuscarTodoAsync(request.Options, cancellationToken)).ToList();

            return ApiResponse.CreateSuccessfull<BuscarEmpresasResponse, BuscarEmpresasResponseModel>(
                new BuscarEmpresasResponseModel { Empresas = empresas });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error al buscar empresas.");
            return ApiResponse.CreateFailed(e.Message);
        }
    }
}
