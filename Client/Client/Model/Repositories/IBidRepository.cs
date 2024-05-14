namespace BiddingPlatform.Bid
{
    public interface IBidRepository
    {
        List<IBidModel> Bids { get; set; }
        void AddBidToRepo(IBidModel bid);
        void DeleteBidFromRepo(IBidModel bid);
        List<IBidModel> GetBids();
        void UpdateBidIntoRepo(IBidModel oldbid, IBidModel newbid);
    }
}