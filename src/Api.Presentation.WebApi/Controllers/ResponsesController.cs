using Api.Core.Application.Responses.Commands.CreateApiResponse;
using Api.Core.Domain.Common;
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
}
