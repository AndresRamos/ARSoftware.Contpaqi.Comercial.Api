using Api.Core.Application.Common.Interfaces;
using ARSoftware.Contpaqi.Api.Common.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.Core.Application.Requests.Commands.CreateApiResponse;

public sealed record CreateApiResponseCommand(ApiResponse ApiResponse, string SubscriptionKey, Guid ApiRequestId) : IRequest;

public sealed class CreateApiResponseCommandHandler : IRequestHandler<CreateApiResponseCommand>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public CreateApiResponseCommandHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task Handle(CreateApiResponseCommand request, CancellationToken cancellationToken)
    {
        ApiRequest apiRequest =
            await _applicationDbContext.Requests.FirstAsync(
                m => m.Id == request.ApiRequestId && m.SubscriptionKey == request.SubscriptionKey, cancellationToken);

        apiRequest.SetResponse(request.ApiResponse);

        await _applicationDbContext.SaveChangesAsync(cancellationToken);
    }
}
