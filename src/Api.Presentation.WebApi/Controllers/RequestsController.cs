using Api.Core.Application.Requests.Commands.CreateApiRequest;
using Api.Core.Application.Requests.Queries.GetApiRequestById;
using Api.Core.Application.Requests.Queries.GetApiRequests;
using Api.Core.Application.Requests.Queries.GetPendingApiRequests;
using Api.Core.Domain.Common;
using Api.Core.Domain.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Presentation.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
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

    [HttpGet]
    public async Task<ActionResult<ApiRequestBase>> Get(DateOnly startDate, DateOnly endDate)
    {
        IEnumerable<ApiRequestBase> apiRequests = await _mediator.Send(new GetApiRequestsQuery(startDate, endDate));

        if (!apiRequests.Any())
            return NoContent();

        return Ok(apiRequests);
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

        await Task.Delay(10000);

        ApiRequestBase? request = await _mediator.Send(new GetApiRequestByIdQuery(requestId));

        string? pathByAction = _linkGenerator.GetPathByAction("Get", "Requests", new { id = requestId });

        return Created(pathByAction!, request);
    }

    [HttpGet("JsonModel")]
    public ActionResult<ApiRequestBase> JsonModel(string requestName)
    {
        Type requestType = typeof(CrearDocumentoRequest);
        var requestFullName = $"{requestType.Namespace}.{requestName}";
        Type? type = requestType.Assembly.GetType(requestFullName);

        if (type is null)
            throw new InvalidOperationException($"Couldn't find type for request with name {requestFullName}.");

        if (Activator.CreateInstance(type) is not ApiRequestBase instance)
            throw new InvalidOperationException($"Couldn't create instance for type {type}.");

        return Ok(instance);
    }
}
