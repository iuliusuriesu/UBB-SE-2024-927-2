using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model.Entities
{
    public class Auction
    {
        public int auctionId { get; set; }
        public DateTime startingDate { get; set; }
        public string description { get; set; }
        public string name { get; set; }
        public float currentMaxSum { get; set; }
        public List<User> listOfUsers { get; set; }
        public List<Bid> listOfBids { get; set; }

        public Auction(int _id, DateTime startingDate, string description, string name, float currentMaxSum, List<User> listOfUsers, List<Bid> listOfBids)
        {
            this.auctionId = _id;
            this.startingDate = startingDate;
            this.description = description;
            this.name = name;
            this.currentMaxSum = currentMaxSum;
            this.listOfUsers = listOfUsers;
            this.listOfBids = listOfBids;
        }

        public Auction(int _id, DateTime startingDate, string description, string name, float currentMaxSum)
        {
            this.auctionId = _id;
            this.startingDate = startingDate;
            this.description = description;
            this.name = name;
            this.currentMaxSum = currentMaxSum;
            this.listOfUsers = new List<User>();
            this.listOfBids = new List<Bid>();
        }

        //more to be implemented
    }
}
