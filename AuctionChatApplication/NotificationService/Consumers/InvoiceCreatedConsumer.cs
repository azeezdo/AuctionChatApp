using MassTransit;
using Microsoft.AspNetCore.SignalR;
using NotificationService.Hubs;
using Shared;

namespace NotificationService.Consumers;

public class InvoiceCreatedConsumer : IConsumer<InvoiceCreated>
{
    private readonly IHubContext<AuctionHub> _hub;
    public InvoiceCreatedConsumer(IHubContext<AuctionHub> hub) => _hub = hub;
    public async Task Consume(ConsumeContext<InvoiceCreated> context)
    {
// notify winner privately
        await _hub.Clients.User(context.Message.WinnerId).SendAsync("InvoiceCreated", context.Message);
    }
}