using BiddingPlatform.Bid;
using BiddingPlatform.User;

namespace BiddingPlatform.Auction
{
    public interface IAuctionRepository
    {
        List<IAuctionModel> ListOfAuctions { get; set; }
        void AddAuctionToRepo(IAuctionModel auction);
        void AddToDB(string name, string description, DateTime date, float currentMaxSum);
        void RemoveAuctionFromRepo(IAuctionModel auction);
        void UpdateAuctionIntoRepo(IAuctionModel oldauction, IAuctionModel newauction);
        float GetBidMaxSum(int index);
    }
}
