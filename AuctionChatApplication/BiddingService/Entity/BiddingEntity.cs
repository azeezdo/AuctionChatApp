namespace BiddingService.Entity;

public class BiddingEntity
{
    public Guid Id { get; set; }
    public Guid AuctionId { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string UserDisplayName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime Timestamp { get; set; }
}
