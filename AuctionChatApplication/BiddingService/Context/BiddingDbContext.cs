using BiddingService.Entity;
using Microsoft.EntityFrameworkCore;

namespace BiddingService.Context;

public class BiddingDbContext : DbContext
{
    public BiddingDbContext(DbContextOptions<BiddingDbContext> options) : base(options) { }
    public DbSet<BiddingEntity> Bids { get; set; }
}