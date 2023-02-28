using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using Api.Core.Domain.Requests;

namespace Api.Core.Domain.Common;

public class PolymorphicTypeResolver : DefaultJsonTypeInfoResolver
{
    public override JsonTypeInfo GetTypeInfo(Type type, JsonSerializerOptions options)
    {
        JsonTypeInfo jsonTypeInfo = base.GetTypeInfo(type, options);

        Type apiRequestBaseType = typeof(ApiRequestBase);
        if (jsonTypeInfo.Type == apiRequestBaseType)
            jsonTypeInfo.PolymorphismOptions = new JsonPolymorphismOptions
            {
                TypeDiscriminatorPropertyName = "$type",
                IgnoreUnrecognizedTypeDiscriminators = true,
                UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization,
                DerivedTypes =
                {
                    new JsonDerivedType(typeof(ActualizarAgenteRequest), nameof(ActualizarAgenteRequest)),
                    new JsonDerivedType(typeof(ActualizarAlmacenRequest), nameof(ActualizarAlmacenRequest)),
                    new JsonDerivedType(typeof(ActualizarClienteRequest), nameof(ActualizarClienteRequest)),
                    new JsonDerivedType(typeof(ActualizarDocumentoRequest), nameof(ActualizarDocumentoRequest)),
                    new JsonDerivedType(typeof(ActualizarProductoRequest), nameof(ActualizarProductoRequest)),
                    new JsonDerivedType(typeof(BuscarAgentesRequest), nameof(BuscarAgentesRequest)),
                    new JsonDerivedType(typeof(BuscarAlmacenesRequest), nameof(BuscarAlmacenesRequest)),
                    new JsonDerivedType(typeof(BuscarClientesRequest), nameof(BuscarClientesRequest)),
                    new JsonDerivedType(typeof(BuscarConceptosRequest), nameof(BuscarConceptosRequest)),
                    new JsonDerivedType(typeof(BuscarEmpresasRequest), nameof(BuscarEmpresasRequest)),
                    new JsonDerivedType(typeof(BuscarProductosRequest), nameof(BuscarProductosRequest)),
                    new JsonDerivedType(typeof(CancelarDocumentoRequest), nameof(CancelarDocumentoRequest)),
                    new JsonDerivedType(typeof(CrearAgenteRequest), nameof(CrearAgenteRequest)),
                    new JsonDerivedType(typeof(CrearAlmacenRequest), nameof(CrearAlmacenRequest)),
                    new JsonDerivedType(typeof(CrearClienteRequest), nameof(CrearClienteRequest)),
                    new JsonDerivedType(typeof(CrearDocumentoRequest), nameof(CrearDocumentoRequest)),
                    new JsonDerivedType(typeof(CrearFacturaRequest), nameof(CrearFacturaRequest)),
                    new JsonDerivedType(typeof(CrearProductoRequest), nameof(CrearProductoRequest)),
                    new JsonDerivedType(typeof(EliminarClienteRequest), nameof(EliminarClienteRequest)),
                    new JsonDerivedType(typeof(EliminarDocumentoRequest), nameof(EliminarDocumentoRequest)),
                    new JsonDerivedType(typeof(EliminarProductoRequest), nameof(EliminarProductoRequest)),
                    new JsonDerivedType(typeof(GenerarDocumentoDigitalRequest), nameof(GenerarDocumentoDigitalRequest)),
                    new JsonDerivedType(typeof(SaldarDocumentoRequest), nameof(SaldarDocumentoRequest)),
                    new JsonDerivedType(typeof(TimbrarDocumentoRequest), nameof(TimbrarDocumentoRequest))
                }
            };

        Type apiResponseBaseType = typeof(ApiResponseBase);
        if (jsonTypeInfo.Type == apiResponseBaseType)
            jsonTypeInfo.PolymorphismOptions = new JsonPolymorphismOptions
            {
                TypeDiscriminatorPropertyName = "$type",
                IgnoreUnrecognizedTypeDiscriminators = true,
                UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization,
                DerivedTypes =
                {
                    new JsonDerivedType(typeof(ActualizarAgenteResponse), nameof(ActualizarAgenteResponse)),
                    new JsonDerivedType(typeof(ActualizarAlmacenResponse), nameof(ActualizarAlmacenResponse)),
                    new JsonDerivedType(typeof(ActualizarClienteResponse), nameof(ActualizarClienteResponse)),
                    new JsonDerivedType(typeof(ActualizarDocumentoResponse), nameof(ActualizarDocumentoResponse)),
                    new JsonDerivedType(typeof(ActualizarProductoResponse), nameof(ActualizarProductoResponse)),
                    new JsonDerivedType(typeof(BuscarAgentesResponse), nameof(BuscarAgentesResponse)),
                    new JsonDerivedType(typeof(BuscarAlmacenesResponse), nameof(BuscarAlmacenesResponse)),
                    new JsonDerivedType(typeof(BuscarClientesResponse), nameof(BuscarClientesResponse)),
                    new JsonDerivedType(typeof(BuscarConceptosResponse), nameof(BuscarConceptosResponse)),
                    new JsonDerivedType(typeof(BuscarEmpresasResponse), nameof(BuscarEmpresasResponse)),
                    new JsonDerivedType(typeof(BuscarProductosResponse), nameof(BuscarProductosResponse)),
                    new JsonDerivedType(typeof(CancelarDocumentoResponse), nameof(CancelarDocumentoResponse)),
                    new JsonDerivedType(typeof(CrearAgenteResponse), nameof(CrearAgenteResponse)),
                    new JsonDerivedType(typeof(CrearAlmacenResponse), nameof(CrearAlmacenResponse)),
                    new JsonDerivedType(typeof(CrearClienteResponse), nameof(CrearClienteResponse)),
                    new JsonDerivedType(typeof(CrearDocumentoResponse), nameof(CrearDocumentoResponse)),
                    new JsonDerivedType(typeof(CrearFacturaResponse), nameof(CrearFacturaResponse)),
                    new JsonDerivedType(typeof(CrearProductoResponse), nameof(CrearProductoResponse)),
                    new JsonDerivedType(typeof(EliminarClienteResponse), nameof(EliminarClienteResponse)),
                    new JsonDerivedType(typeof(EliminarDocumentoResponse), nameof(EliminarDocumentoResponse)),
                    new JsonDerivedType(typeof(EliminarProductoResponse), nameof(EliminarProductoResponse)),
                    new JsonDerivedType(typeof(GenerarDocumentoDigitalResponse), nameof(GenerarDocumentoDigitalResponse)),
                    new JsonDerivedType(typeof(SaldarDocumentoResponse), nameof(SaldarDocumentoResponse)),
                    new JsonDerivedType(typeof(TimbrarDocumentoResponse), nameof(TimbrarDocumentoResponse))
                }
            };

        return jsonTypeInfo;
    }
}
