using Api.Core.Domain.Requests;
using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructure.Persistence.Configurations;

public static class ApiResponseConfiguration
{
    public static void ConfigureResponses(ModelBuilder builder)
    {
        builder.Configure<ActualizarAgenteResponse, ActualizarAgenteResponseModel>();
        builder.Configure<ActualizarAlmacenResponse, ActualizarAlmacenResponseModel>();
        builder.Configure<ActualizarClienteResponse, ActualizarClienteResponseModel>();
        builder.Configure<ActualizarDocumentoResponse, ActualizarDocumentoResponseModel>();
        builder.Configure<ActualizarProductoResponse, ActualizarProductoResponseModel>();
        builder.Configure<BuscarAgentesResponse, BuscarAgentesResponseModel>();
        builder.Configure<BuscarAlmacenesResponse, BuscarAlmacenesResponseModel>();
        builder.Configure<BuscarClientesResponse, BuscarClientesResponseModel>();
        builder.Configure<BuscarConceptosResponse, BuscarConceptosResponseModel>();
        builder.Configure<BuscarEmpresasResponse, BuscarEmpresasResponseModel>();
        builder.Configure<BuscarProductosResponse, BuscarProductosResponseModel>();
        builder.Configure<CancelarDocumentoResponse, CancelarDocumentoResponseModel>();
        builder.Configure<CrearAgenteResponse, CrearAgenteResponseModel>();
        builder.Configure<CrearAlmacenResponse, CrearAlmacenResponseModel>();
        builder.Configure<CrearClienteResponse, CrearClienteResponseModel>();
        builder.Configure<CrearDocumentoResponse, CrearDocumentoResponseModel>();
        builder.Configure<CrearFacturaResponse, CrearFacturaResponseModel>();
        builder.Configure<CrearProductoResponse, CrearProductoResponseModel>();
        builder.Configure<EliminarClienteResponse, EliminarClienteResponseModel>();
        builder.Configure<EliminarDocumentoResponse, EliminarDocumentoResponseModel>();
        builder.Configure<EliminarProductoResponse, EliminarProductoResponseModel>();
        builder.Configure<GenerarDocumentoDigitalResponse, GenerarDocumentoDigitalResponseModel>();
        builder.Configure<SaldarDocumentoResponse, SaldarDocumentoResponseModel>();
        builder.Configure<TimbrarDocumentoResponse, TimbrarDocumentoResponseModel>();
    }
}
