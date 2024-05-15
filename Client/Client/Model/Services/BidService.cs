using Client.Model.Entities;
using Client.Model.Repositories;
using System;
using System.Collections.Generic;

namespace Client.Model.Services
{
    public class BidService : IBidService
    {
        public IBidRepository BidRepository { get; set; }
        public BidService(IBidRepository bidRepository)
        {
            BidRepository = bidRepository;
        }

        public void AddBid(int id, User user, float bidSum, DateTime biddate)
        {
            Bid toAdd = new Bid(id, user, bidSum, biddate) as Bid;

            this.BidRepository.AddBidToRepo(toAdd);
        }

        public void RemoveBid(int bidID, User user, float bidSum, DateTime biddate)
        {
            Bid toremove = new Bid(bidID, user, bidSum, biddate);
            this.BidRepository.DeleteBidFromRepo(toremove);
        }

        public void UpdateBid(int bidID, User userToBeUpdated, float bidSumToBeUpdated, DateTime bidDateToBeUpdated, User newuser, float newbidSum, DateTime newBidDate)
        {
            Bid bidToBeUpdated = new Bid(bidID, userToBeUpdated, bidSumToBeUpdated, bidDateToBeUpdated) as Bid;
            Bid newBid = new Bid(bidID, newuser, newbidSum, newBidDate) as Bid;
            this.BidRepository.UpdateBidIntoRepo(bidToBeUpdated, newBid);
        }

        public List<Bid> GetBids()
        {
            return this.BidRepository.GetBids();
        }
    }
}
