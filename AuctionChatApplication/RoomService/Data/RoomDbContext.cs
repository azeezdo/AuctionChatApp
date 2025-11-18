using AuctionChatApplication.RoomService.Models;
using Microsoft.EntityFrameworkCore;

namespace AuctionChatApplication.RoomService.Data;

public class RoomDbContext : DbContext
{
    public RoomDbContext(DbContextOptions<RoomDbContext> options) : base(options) { }
    public DbSet<AuctionRoom> Rooms { get; set; }
}