namespace Shared;

public record BidPlaced
{
    public System.Guid AuctionId { get; init; }
    public string RoomId { get; init; } = string.Empty;
    public System.Guid BidId { get; init; }
    public string UserId { get; init; } = string.Empty;
    public string UserDisplayName { get; init; } = string.Empty;
    public decimal Amount { get; init; }
    public System.DateTime Timestamp { get; init; }
}