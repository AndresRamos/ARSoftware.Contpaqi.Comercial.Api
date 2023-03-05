using Api.Core.Application.Responses.Commands.CreateApiResponse;
using Api.Core.Domain.Common;
using Api.Core.Domain.Requests;
using Api.Presentation.WebApi.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Presentation.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[ServiceFilter(typeof(ApiKeyAuthFilter))]
public class ResponsesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ResponsesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [FromHeader(Name = "Ocp-Apim-Subscription-Key")]
    public string ApimSubscriptionKey { get; set; } = string.Empty;

    [HttpPost]
    public async Task<ActionResult> Post(ApiResponseBase apiResponse)
    {
        try
        {
            await _mediator.Send(new CreateApiResponseCommand(apiResponse, ApimSubscriptionKey));
        }
        catch (Exception e)
        {
            BadRequest(e.Message);
        }

        return Ok();
    }

    [HttpGet("JsonModel")]
    public ActionResult<ApiResponseBase> JsonModel(string responseName)
    {
        Type responseType = typeof(CrearDocumentoResponse);

        var responseFullName = $"{responseType.Namespace}.{responseName}";

        Type? type = responseType.Assembly.GetType(responseFullName);

        if (type is null)
            throw new InvalidOperationException($"Couldn't find type for response with name {responseFullName}.");

        if (Activator.CreateInstance(type) is not ApiResponseBase instance)
            throw new InvalidOperationException($"Couldn't create instance for type {type}.");

        return Ok(instance);
    }
}
