namespace InvoiceService.Model;

public class Invoice
{
    public Guid InvoiceId { get; set; }
    public Guid AuctionId { get; set; }
    public string WinnerId { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string? DocumentUrl { get; set; }
    public DateTime CreatedAt { get; set; }
}