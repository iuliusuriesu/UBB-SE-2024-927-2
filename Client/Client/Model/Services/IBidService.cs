using BiddingPlatform.User;

namespace BiddingPlatform.Bid
{
    public interface IBidService
    {
        IBidRepository BidRepository { get; set; }

        void AddBid(int id, BasicUser user, float bidSum, DateTime biddate);
        List<IBidModel> GetBids();
        void RemoveBid(int id, BasicUser user, float bidSum, DateTime biddate);
        void UpdateBid(int id, BasicUser olduser, float oldbidSum, DateTime oldbiddate, BasicUser newuser, float newbidSum, DateTime newbiddate);
    }
}