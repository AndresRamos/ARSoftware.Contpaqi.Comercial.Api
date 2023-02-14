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

namespace Api.Sync.Core.Application.Clientes.Commands.CreateCliente;

public sealed record CreateClienteCommand(Cliente Cliente, CreateClienteOptions Options) : IRequest;

public sealed class CreateClienteCommandHandler : IRequestHandler<CreateClienteCommand>
{
    private readonly IClienteProveedorService _clienteProveedorService;
    private readonly IClienteRepository _clienteRepository;
    private readonly IDireccionService _direccionService;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;

    public CreateClienteCommandHandler(IClienteProveedorService clienteProveedorService,
                                       IClienteRepository clienteRepository,
                                       IMapper mapper,
                                       IDireccionService direccionService,
                                       ILogger<CreateClienteCommandHandler> logger)
    {
        _clienteProveedorService = clienteProveedorService;
        _clienteRepository = clienteRepository;
        _mapper = mapper;
        _direccionService = direccionService;
        _logger = logger;
    }

    public async Task<Unit> Handle(CreateClienteCommand request, CancellationToken cancellationToken)
    {
        var datosCliente = new Dictionary<string, string>(request.Cliente.DatosExtra)
        {
            { nameof(admClientes.CUSOCFDI), request.Cliente.UsoCfdi.Clave },
            { nameof(admClientes.CREGIMFISC), request.Cliente.RegimenFiscal.Clave }
        };

        if (!await _clienteRepository.ExistePorCodigoAsync(request.Cliente.Codigo, cancellationToken))
        {
            _logger.LogDebug("Creando cliente.");
            var clienteSdk = _mapper.Map<tCteProv>(request.Cliente);
            int clienteSdkId = _clienteProveedorService.Crear(clienteSdk);
            _clienteProveedorService.Actualizar(clienteSdkId, datosCliente);
            await ActualizarDireccionFiscalAsync(request.Cliente.Codigo, request.Cliente.DireccionFiscal, cancellationToken);
        }
        else if (request.Options.Sobrescribir)
        {
            _logger.LogDebug("Actualizando cliente.");
            datosCliente.Add(nameof(admClientes.CRAZONSOCIAL), request.Cliente.RazonSocial);
            datosCliente.Add(nameof(admClientes.CRFC), request.Cliente.Rfc);
            _clienteProveedorService.Actualizar(request.Cliente.Codigo, datosCliente);
            await ActualizarDireccionFiscalAsync(request.Cliente.Codigo, request.Cliente.DireccionFiscal, cancellationToken);
        }

        return Unit.Value;
    }

    private async Task ActualizarDireccionFiscalAsync(string clienteCodigo, Direccion direccionFiscal, CancellationToken cancellationToken)
    {
        var direccionComercial = _mapper.Map<tDireccion>(direccionFiscal);
        direccionComercial.cCodCteProv = clienteCodigo;
        direccionComercial.cTipoCatalogo = TipoCatalogoDireccionHelper.ConvertToSdkValue(TipoCatalogoDireccion.Clientes);

        if (!await _clienteRepository.ExisteDireccionFiscalDelClienteAsync(clienteCodigo, cancellationToken))
        {
            _logger.LogDebug("Creando direccion fiscal del cliente.");
            _direccionService.Crear(direccionComercial);
        }
        else
        {
            _logger.LogDebug("Actualizando direccion fiscal del cliente.");
            _direccionService.Actualizar(direccionComercial);
        }
    }
}
