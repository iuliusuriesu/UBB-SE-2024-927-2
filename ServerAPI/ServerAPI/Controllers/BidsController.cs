using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerAPI.Domain;

namespace ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidsController : ControllerBase
    {
        private readonly DiversityMarketplaceContext _context;

        public BidsController(DiversityMarketplaceContext context)
        {
            _context = context;
        }

        // GET: api/Bids
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bid>>> GetBids()
        {
            return await _context.Bids.ToListAsync();
        }

        // GET: api/Bids/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bid>> GetBid(int id)
        {
            var bid = await _context.Bids.FindAsync(id);

            if (bid == null)
            {
                return NotFound();
            }

            return bid;
        }

        // PUT: api/Bids/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBid(int id, Bid bid)
        {
            if (id != bid.BidId)
            {
                return BadRequest();
            }

            _context.Entry(bid).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BidExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Bids
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Bid>> PostBid(int userId, int auctionId, float bidSum)
        {
            /*
            _context.Bids.Add(bid);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBid", new { id = bid.BidId }, bid);

             */
            var bids = _context.Bids.Where(bid=>bid.AuctionId == auctionId);

            var auction = _context.Auctions.Where(auction => auction.AuctionID == auctionId).First();
            var user = _context.Users.Where(user => user.UserID == userId).First();
            Bid bid = new Bid();
            bid.BidSum = bidSum;
            bid.TimeOfBid = DateTime.Now;
            bid.Auction = auction;
            bid.User = user; 
            auction.Bids.Add(bid);
            user.Bids.Add(bid);
            _context.Bids.Add(bid);
            _context.Entry(auction).State = EntityState.Modified;
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetBid", new { id = bid.BidId }, bid);
        }

        // DELETE: api/Bids/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBid(int id)
        {
            var bid = await _context.Bids.FindAsync(id);
            if (bid == null)
            {
                return NotFound();
            }

            _context.Bids.Remove(bid);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BidExists(int id)
        {
            return _context.Bids.Any(e => e.BidId == id);
        }
    }
}
