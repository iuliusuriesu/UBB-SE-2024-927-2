using Client.Model.Entities;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Client.Model.Repositories
{
    public interface IAuctionRepository
    {
        List<Auction> ListOfAuctions { get; set; }
        void AddAuctionToRepo(Auction auction);
        void AddToDB(string name, string description, DateTime date, float currentMaxSum);
        void RemoveAuctionFromRepo(Auction auction);
        void UpdateAuctionIntoRepo(Auction oldauction, Auction newauction);
        float GetBidMaxSum(int index);
    }
}
