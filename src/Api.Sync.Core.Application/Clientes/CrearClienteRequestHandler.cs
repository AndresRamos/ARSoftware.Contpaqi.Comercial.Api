using Api.Core.Domain.Common;
using Api.Core.Domain.Factories;
using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.DatosAbstractos;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Helpers;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Models.Enums;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Api.Sync.Core.Application.Clientes;

public sealed class CrearClienteRequestHandler : IRequestHandler<CrearClienteRequest, ApiResponseBase>
{
    private readonly IClienteProveedorService _clienteProveedorService;
    private readonly IClienteRepository _clienteRepository;
    private readonly IDireccionService _direccionService;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;

    public CrearClienteRequestHandler(IClienteProveedorService clienteProveedorService,
                                      IClienteRepository clienteRepository,
                                      IMapper mapper,
                                      IDireccionService direccionService,
                                      ILogger<CrearClienteRequestHandler> logger)
    {
        _clienteProveedorService = clienteProveedorService;
        _clienteRepository = clienteRepository;
        _mapper = mapper;
        _direccionService = direccionService;
        _logger = logger;
    }

    public async Task<ApiResponseBase> Handle(CrearClienteRequest request, CancellationToken cancellationToken)
    {
        Cliente cliente = request.Model.Cliente;

        try
        {
            int clienteId = _clienteProveedorService.Crear(_mapper.Map<tCteProv>(cliente));

            var datosCliente = new Dictionary<string, string>(cliente.DatosExtra);

            if (cliente.UsoCfdi is not null && !datosCliente.ContainsKey(nameof(admClientes.CUSOCFDI)))
                datosCliente.Add(nameof(admClientes.CUSOCFDI), cliente.UsoCfdi.Clave);

            if (cliente.RegimenFiscal is not null && !datosCliente.ContainsKey(nameof(admClientes.CREGIMFISC)))
                datosCliente.Add(nameof(admClientes.CREGIMFISC), cliente.RegimenFiscal.Clave);

            _clienteProveedorService.Actualizar(clienteId, datosCliente);

            await ActualizarDireccionFiscalAsync(cliente.Codigo, cliente.DireccionFiscal, cancellationToken);

            return ApiResponseFactory.CreateSuccessfull<CrearClienteResponse, CrearClienteResponseModel>(request.Id,
                new CrearClienteResponseModel { Cliente = (await _clienteRepository.BuscarPorIdAsync(clienteId, cancellationToken))! });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error al crear el cliente.");
            return ApiResponseFactory.CreateFailed<CrearClienteResponse>(request.Id, e.Message);
        }
    }

    private async Task ActualizarDireccionFiscalAsync(string clienteCodigo, Direccion direccionFiscal, CancellationToken cancellationToken)
    {
        var direccionComercial = _mapper.Map<tDireccion>(direccionFiscal);
        direccionComercial.cCodCteProv = clienteCodigo;
        direccionComercial.cTipoCatalogo = TipoCatalogoDireccionHelper.ConvertToSdkValue(TipoCatalogoDireccion.Clientes);

        if (!await _clienteRepository.ExisteDireccionFiscalDelClienteAsync(clienteCodigo, cancellationToken))
            _direccionService.Crear(direccionComercial);
        else
            _direccionService.Actualizar(direccionComercial);
    }
}
