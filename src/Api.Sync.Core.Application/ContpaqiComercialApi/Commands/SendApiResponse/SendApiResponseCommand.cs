using Api.Core.Domain.Common;
using Api.Sync.Core.Application.ContpaqiComercialApi.Interfaces;
using MediatR;

namespace Api.Sync.Core.Application.ContpaqiComercialApi.Commands.SendApiResponse;

public sealed record SendApiResponseCommand(ApiResponseBase ApiResponse) : IRequest;

public sealed class SendApiResponseCommandHandler : IRequestHandler<SendApiResponseCommand>
{
    private readonly IContpaqiComercialApiService _contpaqiComercialApiService;

    public SendApiResponseCommandHandler(IContpaqiComercialApiService contpaqiComercialApiService)
    {
        _contpaqiComercialApiService = contpaqiComercialApiService;
    }

    public async Task<Unit> Handle(SendApiResponseCommand request, CancellationToken cancellationToken)
    {
        await _contpaqiComercialApiService.SendResponseAsync(request.ApiResponse, cancellationToken);

        return Unit.Value;
    }
}
