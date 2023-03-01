using Api.Core.Application.Common.Interfaces;
using Api.Core.Domain.Common;
using MediatR;

namespace Api.Core.Application.Requests.Commands.CreateApiRequest;

public sealed record CreateApiRequestCommand(ApiRequestBase ApiRequest, string SubscriptionKey) : IRequest<Guid>;

public sealed class CreateApiRequestCommandHandler : IRequestHandler<CreateApiRequestCommand, Guid>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public CreateApiRequestCommandHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<Guid> Handle(CreateApiRequestCommand request, CancellationToken cancellationToken)
    {
        ApiRequestBase apiRequest = request.ApiRequest;
        apiRequest.SetCreateDefaults(request.SubscriptionKey);

        _applicationDbContext.Requests.Add(apiRequest);

        await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return apiRequest.Id;
    }
}
