using Api.Core.Application.Requests.Commands.CreateApiRequest;
using Api.Core.Application.Requests.Queries.GetApiRequestById;
using Api.Core.Application.Requests.Queries.GetApiRequests;
using Api.Core.Application.Requests.Queries.GetPendingApiRequests;
using Api.Core.Domain.Requests;
using Api.Presentation.WebApi.Authentication;
using Api.Presentation.WebApi.Filters;
using ARSoftware.Contpaqi.Api.Common.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Presentation.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[ServiceFilter(typeof(ApiKeyAuthFilter))]
[ApiExceptionFilter]
public class RequestsController : ControllerBase
{
    private readonly LinkGenerator _linkGenerator;
    private readonly IMediator _mediator;

    public RequestsController(IMediator mediator, LinkGenerator linkGenerator)
    {
        _mediator = mediator;
        _linkGenerator = linkGenerator;
    }

    [FromHeader(Name = "Ocp-Apim-Subscription-Key")]
    public string ApimSubscriptionKey { get; set; } = string.Empty;

    [FromHeader(Name = "x-Empresa-Rfc")] public string EmpresaRfc { get; set; } = string.Empty;

    [FromHeader(Name = "x-Esperar-Respuesta")]
    public bool? EsperarRespuesta { get; set; }

    /// <summary>
    ///     Busca una solicitud por id.
    /// </summary>
    /// <param name="id">Id de la solicitud a buscar.</param>
    /// <returns>Una solicitud.</returns>
    /// <response code="200">Retorna la solicitud.</response>
    /// <response code="404">No se encontro la solicitud.</response>
    /// <response code="400">Solicitud invalida.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<ApiRequest>> Get(Guid id)
    {
        ApiRequest? request = await _mediator.Send(new GetApiRequestByIdQuery(id, ApimSubscriptionKey));

        if (request is null) return NotFound();

        return Ok(request);
    }

    /// <summary>
    ///     Busca solicitudes por rango de fecha.
    /// </summary>
    /// <param name="startDate">Fecha inicio.</param>
    /// <param name="endDate">Fecha fin.</param>
    /// <returns>Coleccion de solicitudes dentro rango de fechas.</returns>
    /// <response code="200">Coleccion de solicitudes dentro rango de fechas.</response>
    /// <response code="204">No hay solicitudes dentro del rango de fecha.</response>
    /// <response code="400">Solicitud invalida.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<ApiRequest>> Get(DateOnly startDate, DateOnly endDate)
    {
        IEnumerable<ApiRequest> apiRequests = await _mediator.Send(new GetApiRequestsQuery(startDate, endDate, ApimSubscriptionKey));

        if (!apiRequests.Any()) return NoContent();

        return Ok(apiRequests);
    }

    /// <summary>
    ///     Busca las solicitudes pendientes por procesar.
    /// </summary>
    /// <returns>Coleccion de solicitudes pendientes por procesar.</returns>
    /// ///
    /// <response code="200">Retorna coleccion de solicitudes pendientes por procesar.</response>
    /// <response code="204">No hay solicitudes pendientes.</response>
    /// <response code="400">Solicitud invalida.</response>
    [HttpGet("Pending")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<IEnumerable<ApiRequest>>> GetPending()
    {
        IEnumerable<ApiRequest> apiRequests = await _mediator.Send(new GetPendingApiRequestsQuery(EmpresaRfc, ApimSubscriptionKey));

        if (!apiRequests.Any()) return NoContent();

        return Ok(apiRequests);
    }

    /// <summary>
    ///     Crear una solicitud en la base de datos para ser procesada en CONTPAQi Comercial.
    /// </summary>
    /// <param name="apiRequest">Solicitud a procesar.</param>
    /// <returns>La solicitud creada.</returns>
    /// <response code="201">Retorna la solicitud creada.</response>
    /// <response code="400">Solicitud invalida.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<Guid>> Post(ContpaqiRequest apiRequest)
    {
        Task waitTimeTask = Task.Delay(TimeSpan.FromSeconds(25));

        Guid requestId = await _mediator.Send(new CreateApiRequestCommand(apiRequest, ApimSubscriptionKey, EmpresaRfc));

        ApiRequest? request = await _mediator.Send(new GetApiRequestByIdQuery(requestId, ApimSubscriptionKey));

        if (EsperarRespuesta == true)
            while (!waitTimeTask.IsCompleted && request!.Status == RequestStatus.Pending)
            {
                await Task.Delay(1000);
                request = await _mediator.Send(new GetApiRequestByIdQuery(requestId, ApimSubscriptionKey));
            }

        string? pathByAction = _linkGenerator.GetPathByAction("Get", "Requests", new { id = requestId });

        return Created(pathByAction!, request);
    }

    /// <summary>
    ///     Regresa la estructura de una solicitud serializada en JSON.
    /// </summary>
    /// <param name="requestName">La solicitud a serializar.</param>
    /// <returns>La estructura de una solicitud serializada en JSON.</returns>
    /// <response code="200">La estructura de una solicitud serializada en JSON.</response>
    [HttpGet("JsonModel")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public ActionResult<ContpaqiRequest> JsonModel(string requestName)
    {
        Type requestType = typeof(CrearFacturaRequest);

        var requestFullName = $"{requestType.Namespace}.{requestName}";

        Type? type = requestType.Assembly.GetType(requestFullName);

        if (type is null) throw new InvalidOperationException($"Couldn't find type for request with name {requestFullName}.");

        if (Activator.CreateInstance(type) is not ContpaqiRequest instance)
            throw new InvalidOperationException($"Couldn't create instance for type {type}.");

        return Ok(instance);
    }
}
