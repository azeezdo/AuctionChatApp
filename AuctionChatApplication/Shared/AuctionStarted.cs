namespace Shared;

public record AuctionStarted
{
    public System.Guid AuctionId { get; init; }
    public string RoomId { get; init; } = string.Empty;
    public System.DateTime StartTime { get; init; }
    public System.DateTime EndTime { get; init; }
    public string Item { get; init; } = string.Empty;
    public decimal StartingPrice { get; init; }
}