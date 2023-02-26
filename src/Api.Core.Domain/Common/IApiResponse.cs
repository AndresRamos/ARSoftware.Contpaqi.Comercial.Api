namespace Api.Core.Domain.Common;

public interface IApiResponse
{
    public Guid Id { get; set; }
    public DateTime DateCreated { get; set; }
    public string ErrorMessage { get; set; }
    public bool IsSuccess { get; set; }
}

public interface IApiResponse<TModel> : IApiResponse
{
    /// <summary>
    ///     Response model.
    /// </summary>
    public TModel Model { get; set; }
}
