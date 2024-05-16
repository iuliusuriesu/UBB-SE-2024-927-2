using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model.Entities
{
    public class Bid : IBid
    {
        public int BidId { get; set; }
        public User BidUser { get; set; }
        public float BidSum { get; set; }
        public DateTime BidDateTime { get; set; }

        public Bid(int id, User user, float bidSum, DateTime bidDateTime)
        {
            this.BidId = id;
            this.BidUser = user;
            this.BidSum = bidSum;
            this.BidDateTime = bidDateTime;
        }

    }
}