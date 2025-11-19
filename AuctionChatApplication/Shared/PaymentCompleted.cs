namespace Shared;

public record PaymentCompleted(Guid InvoiceId, string Status, string? TransactionRef, DateTime Timestamp);