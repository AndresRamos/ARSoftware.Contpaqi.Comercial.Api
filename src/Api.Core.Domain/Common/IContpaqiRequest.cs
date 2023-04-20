using MediatR;

namespace Api.Core.Domain.Common;

public interface IContpaqiRequest : IRequest<ApiResponse>
{
}

public interface IContpaqiRequest<TModel, TOptions> : IContpaqiRequest
{
    /// <summary>
    ///     Request model.
    /// </summary>
    public TModel Model { get; set; }

    /// <summary>
    ///     Request options.
    /// </summary>
    public TOptions Options { get; set; }
}
