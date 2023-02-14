using Api.Core.Domain.Common;
using Api.Sync.Core.Application.ContpaqiComercialApi.Interfaces;
using MediatR;

namespace Api.Sync.Core.Application.ContpaqiComercialApi.Queries.GetPendingApiRequests;

public sealed record GetPendingApiRequestsQuery : IRequest<IEnumerable<ApiRequestBase>>;

public sealed class GetPendingApiRequestsQueryHandler : IRequestHandler<GetPendingApiRequestsQuery, IEnumerable<ApiRequestBase>>
{
    private readonly IContpaqiComercialApiService _contpaqiComercialApiService;

    public GetPendingApiRequestsQueryHandler(IContpaqiComercialApiService contpaqiComercialApiService)
    {
        _contpaqiComercialApiService = contpaqiComercialApiService;
    }

    public async Task<IEnumerable<ApiRequestBase>> Handle(GetPendingApiRequestsQuery request, CancellationToken cancellationToken)
    {
        return await _contpaqiComercialApiService.GetPendingRequestsAsync(cancellationToken);
    }
}
