using BiddingService.Context;
using BiddingService.Entity;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace BiddingService.Controller;

[ApiController]
[Route("api/[controller]")]
public class BidsController : ControllerBase
{
    private readonly BiddingDbContext _db;
    private readonly IPublishEndpoint _publish;

    public BidsController(BiddingDbContext db, IPublishEndpoint publish) { _db = db; _publish = publish; }

    [HttpPost("{auctionId}")]
    public async Task<IActionResult> PlaceBid(System.Guid auctionId, [FromBody] PlaceBidDto dto)
    {
        if (dto.Amount <= 0) return BadRequest("Amount must be > 0");

        var bid = new BiddingEntity { Id = System.Guid.NewGuid(), AuctionId = auctionId, UserId = dto.UserId, UserDisplayName = dto.DisplayName, Amount = dto.Amount, Timestamp = System.DateTime.UtcNow };
        _db.Bids.Add(bid);
        await _db.SaveChangesAsync();

        var message = new BidPlaced { AuctionId = auctionId, RoomId = dto.RoomId, BidId = bid.Id, UserId = bid.UserId, UserDisplayName = bid.UserDisplayName, Amount = bid.Amount, Timestamp = bid.Timestamp };
        await _publish.Publish(message);

        return Accepted(new { bid.Id });
    }

    [HttpGet("{auctionId}/highest")]
    public IActionResult GetHighest(System.Guid auctionId)
    {
        var highest = _db.Bids.Where(b => b.AuctionId == auctionId).OrderByDescending(b => b.Amount).FirstOrDefault();
        if (highest == null) return NotFound();
        return Ok(highest);
    }
}

public record PlaceBidDto(string UserId, string DisplayName, string RoomId, decimal Amount);