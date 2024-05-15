using Microsoft.EntityFrameworkCore;

namespace ServerAPI.Domain
{
    public class DiversityMarketplaceContext : DbContext
    {
        public DiversityMarketplaceContext(DbContextOptions<DiversityMarketplaceContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
