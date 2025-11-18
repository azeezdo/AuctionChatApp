namespace Shared;

public record InvoiceRequested
{
    public System.Guid AuctionId { get; init; }
    public string RoomId { get; init; } = string.Empty;
    public string WinnerId { get; init; } = string.Empty;
    public System.Guid WinningBidId { get; init; }
    public decimal Amount { get; init; }
}