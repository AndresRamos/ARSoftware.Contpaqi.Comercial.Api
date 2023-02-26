using Api.Core.Application.Responses.Commands.CreateApiResponse;
using Api.Core.Domain.Common;
using Api.Core.Domain.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Presentation.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ResponsesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ResponsesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult> Post(ApiResponseBase apiResponse)
    {
        await _mediator.Send(new CreateApiResponseCommand(apiResponse));

        return Ok();
    }

    [HttpGet("JsonModel")]
    public ActionResult<ApiResponseBase> JsonModel(string responseName)
    {
        Type requestType = typeof(CrearDocumentoResponse);
        var responseFullName = $"{requestType.Namespace}.{responseName}";
        Type? type = requestType.Assembly.GetType(responseFullName);

        if (type is null)
            throw new InvalidOperationException($"Couldn't find type for response with name {responseFullName}.");

        if (Activator.CreateInstance(type) is not ApiResponseBase instance)
            throw new InvalidOperationException($"Couldn't create instance for type {type}.");

        return Ok(instance);
    }
}
