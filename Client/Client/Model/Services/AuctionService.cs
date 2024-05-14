using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BiddingPlatform.Auction
{
    public class AuctionService : IAuctionService
    {
        private IAuctionRepository AuctionRepository { get; set; }

        public AuctionService(AuctionRepository auctionRepository)
        {
            this.AuctionRepository = auctionRepository;
        }

        public void AddAuction(int id, DateTime startingDate, string description, string name, float currentMaxSum)
        {
            IAuctionModel auction = new AuctionModel(id, startingDate, description, name, currentMaxSum);
            this.AuctionRepository.AddAuctionToRepo(auction);
        }

        public void RemoveAuction(int id, DateTime startingDate, string description, string name, float currentMaxSum)
        {
            IAuctionModel auction = new AuctionModel(id, startingDate, description, name, currentMaxSum);
            this.AuctionRepository.RemoveAuctionFromRepo(auction);
        }

        public List<IAuctionModel> GetAuctions()
        {
            return this.AuctionRepository.ListOfAuctions;
        }

        public void UpdateAuction(int id, DateTime oldstartingDate, string olddescription, string oldname, float oldcurrentMaxSum, DateTime newstartingDate, string newdescription, string newname, float newcurrentMaxSum)
        {
            IAuctionModel oldauction = new AuctionModel(id, oldstartingDate, olddescription, oldname, oldcurrentMaxSum);
            IAuctionModel newauction = new AuctionModel(id, newstartingDate, newdescription, newname, newcurrentMaxSum);
            this.AuctionRepository.UpdateAuctionIntoRepo(oldauction, newauction);
        }

        public float GetMaxBidSum(int index)
        {
            return this.AuctionRepository.GetBidMaxSum(index);
        }

        public void AddBid(string name, string description, DateTime date, float currentMaxSum)
        {
            this.AuctionRepository.AddToDB(name, description, date, currentMaxSum);
        }
    }
}
