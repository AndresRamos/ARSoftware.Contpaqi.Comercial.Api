using Api.Core.Application.Requests.Commands.CreateApiResponse;
using Api.Core.Application.Requests.Queries.GetApiRequestById;
using Api.Core.Domain.Requests;
using Api.Presentation.WebApi.Authentication;
using Api.Presentation.WebApi.Filters;
using ARSoftware.Contpaqi.Api.Common.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Presentation.WebApi.Controllers;

[ApiController]
[Route("api/requests/{id:guid}/response")]
[Produces("application/json")]
[ServiceFilter(typeof(ApiKeyAuthFilter))]
[ApiExceptionFilter]
public class ResponsesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ResponsesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [FromHeader(Name = "Ocp-Apim-Subscription-Key")]
    public string ApimSubscriptionKey { get; set; } = string.Empty;

    /// <summary>
    ///     Busca una respuesta por id.
    /// </summary>
    /// <param name="id">Id de la respuesta a buscar.</param>
    /// <returns>Una respuesta.</returns>
    /// <response code="200">Retorna la respuesta.</response>
    /// <response code="404">No se encontro la respuesta.</response>
    /// <response code="400">Solicitud invalida.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<ApiResponse>> Get(Guid id)
    {
        ApiRequest? request = await _mediator.Send(new GetApiRequestByIdQuery(id, ApimSubscriptionKey));

        if (request?.Response is null)
            return NotFound();

        return Ok(request.Response);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromRoute] Guid id, ApiResponse apiResponse)
    {
        try
        {
            await _mediator.Send(new CreateApiResponseCommand(apiResponse, ApimSubscriptionKey, id));
        }
        catch (Exception e)
        {
            BadRequest(e.Message);
        }

        return Ok();
    }

    /// <summary>
    ///     Regresa la estructura de una respuesta serializada en JSON.
    /// </summary>
    /// <param name="responseName">La respuesta a serializar.</param>
    /// <returns>La estructura de una respuesta serializada en JSON.</returns>
    /// <response code="200">La estructura de una respuesta serializada en JSON.</response>
    [HttpGet("JsonModel")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public ActionResult<ContpaqiResponse> JsonModel(string responseName)
    {
        Type responseType = typeof(CrearDocumentoResponse);

        var responseFullName = $"{responseType.Namespace}.{responseName}";

        Type? type = responseType.Assembly.GetType(responseFullName);

        if (type is null)
            throw new InvalidOperationException($"Couldn't find type for response with name {responseFullName}.");

        if (Activator.CreateInstance(type) is not ContpaqiResponse instance)
            throw new InvalidOperationException($"Couldn't create instance for type {type}.");

        return Ok(instance);
    }
}
