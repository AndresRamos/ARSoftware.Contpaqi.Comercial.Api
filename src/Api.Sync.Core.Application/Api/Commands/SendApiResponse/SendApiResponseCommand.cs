using Api.Sync.Core.Application.Api.Interfaces;
using ARSoftware.Contpaqi.Api.Common.Domain;
using MediatR;

namespace Api.Sync.Core.Application.Api.Commands.SendApiResponse;

public sealed record SendApiResponseCommand(Guid ApiRequestId, ApiResponse ApiResponse) : IRequest;

public sealed class SendApiResponseCommandHandler : IRequestHandler<SendApiResponseCommand>
{
    private readonly IContpaqiComercialApiService _contpaqiComercialApiService;

    public SendApiResponseCommandHandler(IContpaqiComercialApiService contpaqiComercialApiService)
    {
        _contpaqiComercialApiService = contpaqiComercialApiService;
    }

    public async Task Handle(SendApiResponseCommand request, CancellationToken cancellationToken)
    {
        await _contpaqiComercialApiService.SendResponseAsync(request.ApiRequestId, request.ApiResponse, cancellationToken);
    }
}
