using Client.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.Model.Repositories
{
    public interface IBidRepository
    {
        List<Bid> Bids { get; set; }
        Task AddBidToRepo(Bid bid);
        Task DeleteBidFromRepo(Bid bid);
        List<Bid> GetBids();
        Task UpdateBidIntoRepo(Bid oldbid, Bid newbid);
    }
}