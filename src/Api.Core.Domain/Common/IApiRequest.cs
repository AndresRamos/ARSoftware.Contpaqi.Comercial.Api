namespace Api.Core.Domain.Common;

public interface IApiRequest
{
    public Guid Id { get; set; }
    public string EmpresaRfc { get; set; }
    public DateTime DateCreated { get; set; }
    public RequestStatus Status { get; set; }
    public ApiResponseBase? Response { get; set; }
}

public interface IApiRequest<TModel, TOptions> : IApiRequest
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
