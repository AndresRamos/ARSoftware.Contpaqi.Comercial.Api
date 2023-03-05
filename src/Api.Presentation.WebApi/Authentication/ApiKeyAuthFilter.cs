using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

namespace Api.Presentation.WebApi.Authentication;

public sealed class ApiKeyAuthFilter : IAuthorizationFilter
{
    private readonly IConfiguration _configuration;
    private readonly IHostEnvironment _hostingEnvironment;

    public ApiKeyAuthFilter(IConfiguration configuration, IHostEnvironment hostingEnvironment)
    {
        _configuration = configuration;
        _hostingEnvironment = hostingEnvironment;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (_hostingEnvironment.IsDevelopment())
            return;

        if (!context.HttpContext.Request.Headers.TryGetValue(AuthenticationConstants.ApiKeyHeaderName, out StringValues extractedApiKey))
        {
            context.Result = new UnauthorizedObjectResult("API Key missing");
            return;
        }

        var apiKey = _configuration.GetValue<string>(AuthenticationConstants.ApiKeySectionName)!;

        if (!apiKey.Equals(extractedApiKey))
            context.Result = new UnauthorizedObjectResult("Invalid API Key.");
    }
}
