using InvoiceService.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvoiceService.Controller;

[ApiController]
[Route("api/[controller]")]
public class InvoiceController : ControllerBase
{
    private readonly InvoiceDbContext _db;
    public InvoiceController(InvoiceDbContext db) => _db = db;


    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var inv = await _db.Invoices.FirstOrDefaultAsync(i => i.InvoiceId == id);
        if (inv == null) return NotFound();
        return Ok(inv);
    }
}