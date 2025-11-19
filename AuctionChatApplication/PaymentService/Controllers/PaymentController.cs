using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentService.Data;
using PaymentService.Model;

namespace PaymentService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly PaymentDbContext _db;
    private readonly IPublishEndpoint _publish;
    public PaymentController(PaymentDbContext db, IPublishEndpoint publish) { _db = db; _publish = publish; }


    [HttpPost("charge/{invoiceId}")]
    public async Task<IActionResult> Charge(Guid invoiceId)
    {
// In a real system, we'd contact a payment gateway. Here we simulate it.
        var payment = new Payment { PaymentId = Guid.NewGuid(), InvoiceId = invoiceId, Status = "Completed", TransactionRef = $"tx_{Guid.NewGuid()}", Timestamp = DateTime.UtcNow };
        _db.Payments.Add(payment);
        await _db.SaveChangesAsync();


        var completed = new Shared.PaymentCompleted(payment.InvoiceId, payment.Status, payment.TransactionRef, payment.Timestamp);
        await _publish.Publish(completed);


        return Accepted(new { payment.PaymentId });
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var p = await _db.Payments.FirstOrDefaultAsync(p => p.PaymentId == id);
        if (p == null) return NotFound();
        return Ok(p);
    }
}