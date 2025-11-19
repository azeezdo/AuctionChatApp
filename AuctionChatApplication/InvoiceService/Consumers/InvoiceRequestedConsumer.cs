using InvoiceService.Data;
using InvoiceService.Model;
using MassTransit;
using Shared;

namespace InvoiceService.Consumers;

public class InvoiceRequestedConsumer : IConsumer<InvoiceRequested>
{
    private readonly InvoiceDbContext _db;
    private readonly IPublishEndpoint _publish;
    public InvoiceRequestedConsumer(InvoiceDbContext db, IPublishEndpoint publish) { _db = db; _publish = publish; }


    public async Task Consume(ConsumeContext<InvoiceRequested> context)
    {
        var inv = new Invoice
        {
            InvoiceId = Guid.NewGuid(),
            AuctionId = context.Message.AuctionId,
            WinnerId = context.Message.WinnerId,
            Amount = context.Message.Amount,
            CreatedAt = DateTime.UtcNow,
            DocumentUrl = null
        };
        _db.Invoices.Add(inv);
        await _db.SaveChangesAsync();


        var created = new InvoiceCreated(inv.InvoiceId, inv.AuctionId, inv.WinnerId, inv.Amount, inv.DocumentUrl, inv.CreatedAt);
        await _publish.Publish(created);
    }
}