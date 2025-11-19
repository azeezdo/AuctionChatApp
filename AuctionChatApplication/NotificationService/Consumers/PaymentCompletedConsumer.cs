using MassTransit;
using Microsoft.AspNetCore.SignalR;
using NotificationService.Hubs;
using Shared;

namespace NotificationService.Consumers;

public class PaymentCompletedConsumer : IConsumer<PaymentCompleted>
{
    private readonly IHubContext<AuctionHub> _hub;
    public PaymentCompletedConsumer(IHubContext<AuctionHub> hub) => _hub = hub;
    public async Task Consume(ConsumeContext<PaymentCompleted> context)
    {
// broadcast payment status to winner
// NOTE: we don't know the winnerId here, so this is a simplified notification
        await _hub.Clients.All.SendAsync("PaymentCompleted", context.Message);
    }
}