namespace ServerAPI.Domain
{
    public class Bid
    {
        public int BidId {  get; set; }
        public float BidSum { get; set;}
        public DateTime TimeOfBid { get; set; }
        public User User { get; set; } = null!;
        public int UserId {  get; set; }
        public Auction Auction { get; set; } = null!;
        public int AuctionId { get; set;}
    }
}
