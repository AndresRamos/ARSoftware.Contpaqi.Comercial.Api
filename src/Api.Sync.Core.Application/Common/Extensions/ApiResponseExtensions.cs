using Api.Core.Domain.Common;

namespace Api.Sync.Core.Application.Common.Extensions;

public static class ApiResponseExtensions
{
    public static void ThrowIfError(this ApiResponseBase reponse)
    {
        if (reponse.IsSuccess == false)
            throw new Exception(reponse.ErrorMessage);
    }
}
