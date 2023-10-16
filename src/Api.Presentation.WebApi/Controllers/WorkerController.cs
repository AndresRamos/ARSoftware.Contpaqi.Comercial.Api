using Api.Presentation.WebApi.Authentication;
using Api.Presentation.WebApi.Filters;
using Api.Presentation.WebApi.Hubs;
using Microsoft.AspNetCore.Mvc;

namespace Api.Presentation.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
[ServiceFilter(typeof(ApiKeyAuthFilter))]
[ApiExceptionFilter]
public class WorkerController : ControllerBase
{
    [FromHeader(Name = "Ocp-Apim-Subscription-Key")]
    public string ApimSubscriptionKey { get; set; } = string.Empty;

    [FromHeader(Name = "x-Empresa-Rfc")] public string EmpresaRfc { get; set; } = string.Empty;

    [HttpGet("IsActive")]
    public ActionResult<bool> IsActive()
    {
        return ApiRequestHub.Connections.Any(c => c.SubscriptionKey == ApimSubscriptionKey && c.Empresas.Any(e => e == EmpresaRfc));
    }
}