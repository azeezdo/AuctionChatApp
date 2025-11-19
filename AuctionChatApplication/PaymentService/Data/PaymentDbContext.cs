using Microsoft.EntityFrameworkCore;
using PaymentService.Model;

namespace PaymentService.Data;

public class PaymentDbContext : DbContext
{
    public PaymentDbContext(DbContextOptions<PaymentDbContext> options) : base(options) { }
    public DbSet<Payment> Payments {get; set;}
}