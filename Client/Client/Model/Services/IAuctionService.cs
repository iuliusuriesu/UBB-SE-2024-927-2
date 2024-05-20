using Client.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model.Services
{
    public interface IAuctionService
    {
        Task AddAuction(int id, DateTime startingDate, string description, string name, float currentMaxSum);
        Task RemoveAuction(int id, DateTime startingDate, string description, string name, float currentMaxSum);
        Task<List<Auction>> GetAuctions();
        Task UpdateAuction(int id, DateTime oldstartingDate, string olddescription, string oldname, float oldcurrentMaxSum, DateTime newstartingDate, string newdescription, string newname, float newcurrentMaxSum);
        Task<float> GetMaxBidSum(int index);
        void AddBid(string name, string description, DateTime date, float currentMaxSum);
    }
}
