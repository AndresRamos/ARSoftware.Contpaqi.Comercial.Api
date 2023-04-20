// ReSharper disable UnusedAutoPropertyAccessor.Local

using System.Text.Json.Serialization;

namespace Api.Core.Domain.Common;

public sealed class ApiResponse
{
    public ApiResponse(IContpaqiResponse contpaqiResponse, bool isSuccess, string errorMessage = "")
    {
        ContpaqiResponseType = contpaqiResponse.GetType().Name;
        ContpaqiResponse = contpaqiResponse;
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
    }

    /// <summary>
    ///     Id de la respuesta.
    /// </summary>
    [JsonInclude]
    public Guid Id { get; private set; }

    /// <summary>
    ///     Fecha de creacion de la respuesta.
    /// </summary>
    [JsonInclude]
    public DateTimeOffset DateCreated { get; private set; } = DateTimeOffset.Now;

    /// <summary>
    ///     Resultado de si fue exitosa la solicitud.
    /// </summary>
    [JsonInclude]
    public bool IsSuccess { get; private set; }

    /// <summary>
    ///     Tipo de respuesta CONTPAQi
    /// </summary>
    [JsonInclude]
    public string ContpaqiResponseType { get; private set; }

    /// <summary>
    ///     Respuesta CONTPAQi.
    /// </summary>
    [JsonInclude]
    public IContpaqiResponse ContpaqiResponse { get; private set; }

    /// <summary>
    ///     Mensaje de error.
    /// </summary>
    [JsonInclude]
    public string ErrorMessage { get; private set; }

    /// <summary>
    ///     Tiempo de ejecucion en milisegundos.
    /// </summary>
    [JsonInclude]
    public long ExecutionTime { get; set; }

    public static ApiResponse CreateSuccessfull(IContpaqiResponse contpaqiResponse)
    {
        return new ApiResponse(contpaqiResponse, true);
    }

    public static ApiResponse CreateFailed(string errorMessage)
    {
        return new ApiResponse(new EmptyContpaqiResponse(), false, errorMessage);
    }

    public static ApiResponse CreateSuccessfull<TReponse, TModel>(TModel model) where TReponse : IContpaqiResponse<TModel>, new()
    {
        return new ApiResponse(new TReponse { Model = model }, true);
    }
}
