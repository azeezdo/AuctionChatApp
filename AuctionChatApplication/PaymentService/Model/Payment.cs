namespace PaymentService.Model;

public class Payment
{
    public Guid PaymentId { get; set; }
    public Guid InvoiceId { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? TransactionRef { get; set; }
    public DateTime Timestamp { get; set; }
}