using System.Threading.Tasks;
using Client.Model.Entities;
using System;
using System.Collections.Generic;

namespace Client.Model.Repositories
{
    public interface IAuctionRepository
    {
        List<Auction> ListOfAuctions { get; set; }
        Task<List<Auction>> GetAllAuctions();

        void AddAuctionToRepo(Auction auction);
        Task AddToDB(string name, string description, DateTime date, float currentMaxSum);
        void RemoveAuctionFromRepo(Auction auction);
        void UpdateAuctionIntoRepo(Auction oldauction, Auction newauction);
        Task RemoveFromDB(int auctionID);
        Task UpdateIntoDB(int oldAuctionID, Auction newAuction);

        float GetBidMaxSum(int index);
    }
}
