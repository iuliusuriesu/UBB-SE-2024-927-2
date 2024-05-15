using Client.Model.Entities;
using Client.Model.Repositories;
using System;
using System.Collections.Generic;

namespace Client.Model.Services
{
    public interface IBidService
    {
        IBidRepository BidRepository { get; set; }

        void AddBid(int id, User user, float bidSum, DateTime biddate);
        List<Bid> GetBids();
        void RemoveBid(int id, User user, float bidSum, DateTime biddate);
        void UpdateBid(int id, User olduser, float oldbidSum, DateTime oldbiddate, User newuser, float newbidSum, DateTime newbiddate);
    }
}