namespace Shared;

public record InvoiceCreated(Guid InvoiceId, Guid AuctionId, string WinnerId, decimal Amount, string? DocumentUrl, DateTime CreatedAt);