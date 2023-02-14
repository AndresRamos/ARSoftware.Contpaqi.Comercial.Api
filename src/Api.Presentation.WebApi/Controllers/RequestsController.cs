using Api.Core.Application.Requests.Commands.CreateApiRequest;
using Api.Core.Application.Requests.Queries.GetApiRequestById;
using Api.Core.Application.Requests.Queries.GetPendingApiRequests;
using Api.Core.Domain.Common;
using Api.Core.Domain.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Presentation.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RequestsController : ControllerBase
{
    private readonly LinkGenerator _linkGenerator;
    private readonly IMediator _mediator;

    public RequestsController(IMediator mediator, LinkGenerator linkGenerator)
    {
        _mediator = mediator;
        _linkGenerator = linkGenerator;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiRequestBase>> Get(Guid id)
    {
        ApiRequestBase? request = await _mediator.Send(new GetApiRequestByIdQuery(id));

        if (request is null)
            return NotFound();

        return Ok(request);
    }

    [HttpGet("Pending")]
    public async Task<ActionResult<IEnumerable<ApiRequestBase>>> GetPending()
    {
        IEnumerable<ApiRequestBase> apiRequests = await _mediator.Send(new GetPendingApiRequestsQuery());

        if (!apiRequests.Any())
            return NoContent();

        return Ok(apiRequests);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Post(ApiRequestBase apiRequest)
    {
        Guid requestId = await _mediator.Send(new CreateApiRequestCommand(apiRequest));

        string? pathByAction = _linkGenerator.GetPathByAction("Get", "Requests", new { id = requestId });

        return Created(pathByAction!, requestId);
    }

    [HttpGet("Test")]
    public ActionResult<ApiRequestBase> Test()
    {
        return Ok(new CreateDocumentoDigitalRequest());
    }
}
