using Client.Model.Entities;
using Client.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Client.Model.Services
{
    public class AuctionService : IAuctionService
    {
        private IAuctionRepository AuctionRepository { get; set; }

        public AuctionService(IAuctionRepository auctionRepository)
        {
            this.AuctionRepository = auctionRepository;
        }

        public async Task AddAuction(int id, DateTime startingDate, string description, string name, float currentMaxSum)
        {
            Auction auction = new Auction(id, startingDate, description, name, currentMaxSum);
            await this.AuctionRepository.AddAuctionToRepo(auction);
        }

        public async Task RemoveAuction(int id, DateTime startingDate, string description, string name, float currentMaxSum)
        {
            Auction auction = new Auction(id, startingDate, description, name, currentMaxSum);
            await this.AuctionRepository.RemoveAuctionFromRepo(auction);
        }

        public async Task<List<Auction>> GetAuctions()
        {
            return await this.AuctionRepository.GetAllAuctions();
        }

        public async Task UpdateAuction(int id, DateTime oldstartingDate, string olddescription, string oldname, float oldcurrentMaxSum, DateTime newstartingDate, string newdescription, string newname, float newcurrentMaxSum)
        {
            Auction oldauction = new Auction(id, oldstartingDate, olddescription, oldname, oldcurrentMaxSum);
            Auction newauction = new Auction(id, newstartingDate, newdescription, newname, newcurrentMaxSum);
            await this.AuctionRepository.UpdateAuctionIntoRepo(oldauction, newauction);
        }

        public float GetMaxBidSum(int index)
        {
            return this.AuctionRepository.GetBidMaxSum(index);
        }

        public async Task AddBid(string name, string description, DateTime date, float currentMaxSum)
        {
            await this.AuctionRepository.AddAuctionToRepo(new Auction(0, date, description, name, currentMaxSum));
        }
    }
}
