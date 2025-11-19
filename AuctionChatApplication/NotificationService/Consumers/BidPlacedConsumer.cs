using MassTransit;
using Microsoft.AspNetCore.SignalR;
using NotificationService.Hubs;
using Shared;

namespace NotificationService.Consumers;

public class BidPlacedConsumer : IConsumer<BidPlaced>
{
    private readonly IHubContext<AuctionHub> _hub;
    public BidPlacedConsumer(IHubContext<AuctionHub> hub) => _hub = hub;
    public async Task Consume(ConsumeContext<BidPlaced> context)
    {
        await _hub.Clients.Group(context.Message.RoomId).SendAsync("BidPlaced", context.Message);
    }
}