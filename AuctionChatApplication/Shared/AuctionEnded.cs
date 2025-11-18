namespace Shared;

public record AuctionEnded
{
    public System.Guid AuctionId { get; init; }
    public string RoomId { get; init; } = string.Empty;
    public System.Guid? WinningBidId { get; init; }
    public string? WinnerId { get; init; }
    public decimal? WinningAmount { get; init; }
    public System.DateTime EndTime { get; init; }
}