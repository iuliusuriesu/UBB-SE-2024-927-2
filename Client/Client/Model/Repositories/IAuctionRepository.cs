using System.Threading.Tasks;
using Client.Model.Entities;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Client.Model.Repositories
{
    public interface IAuctionRepository
    {
        List<Auction> ListOfAuctions { get; set; }
        Task<List<Auction>> GetAllAuctions();

        Task AddAuctionToRepo(Auction auction);
        Task AddToDB(string name, string description, DateTime date, float currentMaxSum);
        Task RemoveAuctionFromRepo(Auction auction);
        Task UpdateAuctionIntoRepo(Auction oldauction, Auction newauction);
        Task RemoveFromDB(int auctionID);
        Task UpdateIntoDB(int oldAuctionID, Auction newAuction);
        float GetBidMaxSum(int index);
    }
}
