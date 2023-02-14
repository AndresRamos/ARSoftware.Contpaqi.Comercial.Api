using Api.Core.Domain.Common;
using Api.Core.Domain.Models;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para crear un cliente.
/// </summary>
public sealed class CreateClienteRequest : ApiRequestBase
{
    public Cliente Model { get; set; } = new();
    public CreateClienteOptions Options { get; set; } = new();
}

public sealed class CreateClienteOptions
{
    public bool Sobrescribir { get; set; }
}

public sealed class CreateClienteResponse : ApiResponseBase
{
    public Cliente Model { get; set; } = new();

    public static CreateClienteResponse CreateSuccessfull(CreateClienteRequest apiRequest, Cliente model)
    {
        return new CreateClienteResponse { IsSuccess = true, Id = apiRequest.Id, Model = model };
    }

    public static CreateClienteResponse CreateFailed(CreateClienteRequest apiRequest, string errorMessage)
    {
        return new CreateClienteResponse { IsSuccess = false, ErrorMessage = errorMessage, Id = apiRequest.Id };
    }
}
