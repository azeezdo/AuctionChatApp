using MassTransit;
using PaymentService.Data;
using PaymentService.Model;
using Shared;

namespace PaymentService.Consumers;

public class InvoiceCreatedConsumer : IConsumer<InvoiceCreated>
{
    private readonly PaymentDbContext _db;
    private readonly IPublishEndpoint _publish;
    public InvoiceCreatedConsumer(PaymentDbContext db, IPublishEndpoint publish) { _db = db; _publish = publish; }


    public async Task Consume(ConsumeContext<InvoiceCreated> context)
    {
// Simulate charging the winner's card
        var payment = new Payment
        {
            PaymentId = Guid.NewGuid(),
            InvoiceId = context.Message.InvoiceId,
            Status = "Completed",
            TransactionRef = $"tx_{Guid.NewGuid()}",
            Timestamp = DateTime.UtcNow
        };
        _db.Payments.Add(payment);
        await _db.SaveChangesAsync();


        var completed = new PaymentCompleted(payment.InvoiceId, payment.Status, payment.TransactionRef, payment.Timestamp);
        await _publish.Publish(completed);
    }
}