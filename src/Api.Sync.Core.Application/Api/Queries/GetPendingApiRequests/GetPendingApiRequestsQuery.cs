using Api.Sync.Core.Application.Api.Interfaces;
using ARSoftware.Contpaqi.Api.Common.Domain;
using MediatR;

namespace Api.Sync.Core.Application.Api.Queries.GetPendingApiRequests;

public sealed record GetPendingRequestsQuery : IRequest<IEnumerable<ApiRequest>>;

public sealed class GetPendingRequestsQueryHandler : IRequestHandler<GetPendingRequestsQuery, IEnumerable<ApiRequest>>
{
    private readonly IContpaqiComercialApiService _contpaqiComercialApiService;

    public GetPendingRequestsQueryHandler(IContpaqiComercialApiService contpaqiComercialApiService)
    {
        _contpaqiComercialApiService = contpaqiComercialApiService;
    }

    public async Task<IEnumerable<ApiRequest>> Handle(GetPendingRequestsQuery request, CancellationToken cancellationToken)
    {
        return (await _contpaqiComercialApiService.GetPendingRequestsAsync(cancellationToken)).ToList();
    }
}
