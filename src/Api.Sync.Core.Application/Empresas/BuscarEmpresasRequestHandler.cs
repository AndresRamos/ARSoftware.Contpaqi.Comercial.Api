using Api.Core.Domain.Common;
using Api.Core.Domain.Factories;
using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Api.Sync.Core.Application.Empresas;

public sealed class BuscarEmpresasRequestHandler : IRequestHandler<BuscarEmpresasRequest, ApiResponseBase>
{
    private readonly IEmpresaRepository _empresaRepository;
    private readonly ILogger _logger;

    public BuscarEmpresasRequestHandler(IEmpresaRepository empresaRepository, ILogger<BuscarEmpresasRequestHandler> logger)
    {
        _empresaRepository = empresaRepository;
        _logger = logger;
    }

    public async Task<ApiResponseBase> Handle(BuscarEmpresasRequest request, CancellationToken cancellationToken)
    {
        try
        {
            List<Empresa> empresas = (await _empresaRepository.BuscarTodoAsync(cancellationToken)).ToList();

            return ApiResponseFactory.CreateSuccessfull<BuscarEmpresasResponse, BuscarEmpresasResponseModel>(request.Id,
                new BuscarEmpresasResponseModel { Empresas = empresas });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error al buscar empresas.");
            return ApiResponseFactory.CreateFailed<BuscarEmpresasResponse>(request.Id, e.Message);
        }
    }
}
