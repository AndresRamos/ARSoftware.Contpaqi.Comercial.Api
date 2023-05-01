using Api.Core.Application.Common.Interfaces;
using ARSoftware.Contpaqi.Api.Common.Domain;
using MediatR;

namespace Api.Core.Application.Requests.Commands.CreateApiRequest;

public sealed record CreateApiRequestCommand(ContpaqiRequest ContpaqiRequest, string SubscriptionKey, string EmpresaRfc) : IRequest<Guid>;

public sealed class CreateApiRequestCommandHandler : IRequestHandler<CreateApiRequestCommand, Guid>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public CreateApiRequestCommandHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<Guid> Handle(CreateApiRequestCommand request, CancellationToken cancellationToken)
    {
        var apiRequest = new ApiRequest(request.SubscriptionKey, request.EmpresaRfc, request.ContpaqiRequest);

        _applicationDbContext.Requests.Add(apiRequest);

        await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return apiRequest.Id;
    }
}
