namespace Api.Core.Domain.Factories;

public static class ApiResponseFactory
{
    public static TReponse CreateSuccessfull<TReponse, TModel>(Guid requestId, TModel model) where TReponse : IApiResponse<TModel>, new()
    {
        return new TReponse { IsSuccess = true, Id = requestId, Model = model };
    }

    public static TReponse CreateFailed<TReponse>(Guid requestId, string errorMessage) where TReponse : IApiResponse, new()
    {
        return new TReponse { IsSuccess = false, Id = requestId, ErrorMessage = errorMessage };
    }
}
