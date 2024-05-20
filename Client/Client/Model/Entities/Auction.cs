using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model.Entities
{
    public class Auction
    {
        public int auctionID { get; set; }
        public DateTime startingDate { get; set; }
        public string auctionDescription { get; set; }
        public string auctionName { get; set; }
        public float currentMaxSum { get; set; }
        public List<User> users { get; set; }
        public List<Bid> bids { get; set; }

        public Auction(int _id, DateTime startingDate, string description, string name, float currentMaxSum)
        {
            this.auctionID = _id;
            this.startingDate = startingDate;
            this.auctionDescription = description;
            this.auctionName = name;
            this.currentMaxSum = currentMaxSum;
            this.users = new List<User>();
            this.bids = new List<Bid>();
        }
        public Auction() { }
     //   public Auction(int auctionID, string auctionDescription, string auctionName, float currentMaxSum, List<String> bids)
     //   {
     //       this.auctionID = auctionID;
     //       this.startingDate = DateTime.Now;
     //       //this.startingDate = startingDate;
     //       this.auctionDescription = auctionDescription;
     //       this.auctionName = auctionName;
     //       this.currentMaxSum = currentMaxSum;
     //       this.users = new List<User>();
     //       this.bids = new List<Bid>();
     //   }

        public void AddUserToAuction(User user)
        {
            this.users.Add(user);
        }

        public void AddBidToAuction(IBid bid)
        {
            this.bids.Add((Bid)bid);
        }

    }
}
