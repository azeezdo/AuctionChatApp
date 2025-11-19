using MassTransit;
using Microsoft.AspNetCore.SignalR;
using NotificationService.Hubs;
using Shared;

namespace NotificationService.Consumers;

public class AuctionStartedConsumer : IConsumer<AuctionStarted>
{
    private readonly IHubContext<AuctionHub> _hub;
    public AuctionStartedConsumer(IHubContext<AuctionHub> hub) => _hub = hub;
    public async Task Consume(ConsumeContext<AuctionStarted> context)
    {
        await _hub.Clients.Group(context.Message.RoomId).SendAsync("AuctionStarted", context.Message);
    }
}