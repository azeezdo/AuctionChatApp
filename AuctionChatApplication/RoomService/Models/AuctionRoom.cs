namespace AuctionChatApplication.RoomService.Models;

public class AuctionRoom
{
    public int Id { get; set; }
    public string RoomId { get; set; } = string.Empty;
    public string Item { get; set; } = string.Empty;
    public decimal StartingPrice { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Status { get; set; } = "created";
}
