using Api.Core.Domain.Requests;
using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructure.Persistence.Configurations;

public static class ApiRequestConfiguration
{
    public static void ConfigureRequests(ModelBuilder builder)
    {
        builder.Configure<ActualizarAgenteRequest, ActualizarAgenteRequestModel, ActualizarAgenteRequestOptions>();
        builder.Configure<ActualizarAlmacenRequest, ActualizarAlmacenRequestModel, ActualizarAlmacenRequestOptions>();
        builder.Configure<ActualizarClienteRequest, ActualizarClienteRequestModel, ActualizarClienteRequestOptions>();
        builder.Configure<ActualizarDocumentoRequest, ActualizarDocumentoRequestModel, ActualizarDocumentoRequestOptions>();
        builder.Configure<ActualizarProductoRequest, ActualizarProductoRequestModel, ActualizarProductoRequestOptions>();
        builder.Configure<BuscarAgentesRequest, BuscarAgentesRequestModel, BuscarAgentesRequestOptions>();
        builder.Configure<BuscarAlmacenesRequest, BuscarAlmacenesRequestModel, BuscarAlmacenesRequestOptions>();
        builder.Configure<BuscarClientesRequest, BuscarClientesRequestModel, BuscarClientesRequestOptions>();
        builder.Configure<BuscarConceptosRequest, BuscarConceptosRequestModel, BuscarConceptosRequestOptions>();
        builder.Configure<BuscarEmpresasRequest, BuscarEmpresasRequestModel, BuscarEmpresasRequestOptions>();
        builder.Configure<BuscarProductosRequest, BuscarProductosRequestModel, BuscarProductosRequestOptions>();
        builder.Configure<CancelarDocumentoRequest, CancelarDocumentoRequestModel, CancelarDocumentoRequestOptions>();
        builder.Configure<CrearAgenteRequest, CrearAgenteRequestModel, CrearAgenteRequestOptions>();
        builder.Configure<CrearAlmacenRequest, CrearAlmacenRequestModel, CrearAlmacenRequestOptions>();
        builder.Configure<CrearClienteRequest, CrearClienteRequestModel, CrearClienteRequestOptions>();
        builder.Configure<CrearDocumentoRequest, CrearDocumentoRequestModel, CrearDocumentoRequestOptions>();
        builder.Configure<CrearFacturaRequest, CrearFacturaRequestModel, CrearFacturaRequestOptions>();
        builder.Configure<CrearProductoRequest, CrearProductoRequestModel, CrearProductoRequestOptions>();
        builder.Configure<EliminarClienteRequest, EliminarClienteRequestModel, EliminarClienteRequestOptions>();
        builder.Configure<EliminarDocumentoRequest, EliminarDocumentoRequestModel, EliminarDocumentoRequestOptions>();
        builder.Configure<EliminarProductoRequest, EliminarProductoRequestModel, EliminarProductoRequestOptions>();
        builder.Configure<GenerarDocumentoDigitalRequest, GenerarDocumentoDigitalRequestModel, GenerarDocumentoDigitalRequestOptions>();
        builder.Configure<SaldarDocumentoRequest, SaldarDocumentoRequestModel, SaldarDocumentoRequestOptions>();
        builder.Configure<TimbrarDocumentoRequest, TimbrarDocumentoRequestModel, TimbrarDocumentoRequestOptions>();
    }
}
