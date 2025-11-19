using MassTransit;
using Microsoft.AspNetCore.SignalR;
using NotificationService.Hubs;
using Shared;

namespace NotificationService.Consumers;

public class AuctionEndedConsumer : IConsumer<AuctionEnded>
{
    private readonly IHubContext<AuctionHub> _hub;
    private readonly IPublishEndpoint _publish;
    public AuctionEndedConsumer(IHubContext<AuctionHub> hub, IPublishEndpoint publish) { _hub = hub; _publish = publish; }
    public async Task Consume(ConsumeContext<AuctionEnded> context)
    {
        await _hub.Clients.Group(context.Message.RoomId).SendAsync("AuctionEnded", context.Message);


        if (context.Message.WinnerId != null && context.Message.WinningBidId.HasValue && context.Message.WinningAmount.HasValue)
        {
            var invoiceReq = new InvoiceRequested();
            await _publish.Publish(invoiceReq);
        }
    }
}