using Client.Model.Entities;
using System.Collections.Generic;

namespace Client.Model.Repositories
{
    public interface IBidRepository
    {
        List<Bid> Bids { get; set; }
        void AddBidToRepo(Bid bid);
        void DeleteBidFromRepo(Bid bid);
        List<Bid> GetBids();
        void UpdateBidIntoRepo(Bid oldbid, Bid newbid);
    }
}