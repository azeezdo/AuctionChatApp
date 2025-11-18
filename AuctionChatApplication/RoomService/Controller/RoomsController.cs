using AuctionChatApplication.RoomService.Data;
using AuctionChatApplication.RoomService.Models;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace AuctionChatApplication.RoomService.Controller;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    private readonly RoomDbContext _db;
    private readonly IPublishEndpoint _publish;

    public RoomsController(RoomDbContext db, IPublishEndpoint publish) { _db = db; _publish = publish; }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRoomDto dto)
    {
        var room = new AuctionRoom { RoomId = dto.RoomId ?? Guid.NewGuid().ToString(), Item = dto.Item ?? string.Empty, StartingPrice = dto.StartingPrice, StartTime = dto.StartTime, EndTime = dto.EndTime };
        _db.Rooms.Add(room);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { roomId = room.RoomId }, room);
    }

    [HttpGet("{roomId}")]
    public IActionResult Get(string roomId)
    {
        var room = _db.Rooms.FirstOrDefault(r => r.RoomId == roomId);
        if (room == null) return NotFound();
        return Ok(room);
    }

    [HttpPost("{roomId}/start")]
    public async Task<IActionResult> Start(string roomId)
    {
        var room = _db.Rooms.FirstOrDefault(r => r.RoomId == roomId);
        if (room == null) return NotFound();

        room.Status = "started";
        await _db.SaveChangesAsync();

        var evt = new AuctionStarted { AuctionId = Guid.NewGuid(), RoomId = room.RoomId, StartTime = room.StartTime, EndTime = room.EndTime, Item = room.Item, StartingPrice = room.StartingPrice };
        await _publish.Publish(evt);

        return Accepted(evt);
    }

    [HttpPost("{roomId}/end")]
    public async Task<IActionResult> End(string roomId, [FromBody] EndAuctionDto dto)
    {
        var room = _db.Rooms.FirstOrDefault(r => r.RoomId == roomId);
        if (room == null) return NotFound();

        room.Status = "ended";
        await _db.SaveChangesAsync();

        var evt = new AuctionEnded { AuctionId = dto.AuctionId, RoomId = room.RoomId, EndTime = DateTime.UtcNow, WinnerId = dto.WinnerId, WinningAmount = dto.WinningAmount, WinningBidId = dto.WinningBidId };
        await _publish.Publish(evt);

        return Accepted(evt);
    }
}

public record CreateRoomDto(string? RoomId, string? Item, decimal StartingPrice, DateTime StartTime, DateTime EndTime);
public record EndAuctionDto(Guid AuctionId, string? WinnerId, Guid? WinningBidId, decimal? WinningAmount);
