using System.Security.Cryptography.Pkcs;

namespace ServerAPI.Domain
{
    public class Auction
    {
        public int AuctionID {  get; set; }
        public string AuctionDescription {  get; set; }
        public string AuctionName { get; set; }
        public float CurrentMaxSum { get; set; }
        DateTime DateOfStart { get; set; }
        public List<User> Users {  get; set; }
        public List<Bid> Bids {  get; set; }
    }
}
