namespace Api.Core.Domain.Common;

public sealed record GetPendingRequestsMessage(string SubscriptionKey, string EmpresaRfc);
