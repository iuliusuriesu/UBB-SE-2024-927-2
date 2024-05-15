using System.Data.SqlClient;
using System;
using Client.Model.Entities;
using System.Collections.Generic;

namespace Client.Model.Repositories
{
    public class BidRepository : IBidRepository
    {
        private string connectionString;
        public List<Bid> Bids { get; set; }

        public BidRepository(string connectionString)
        {
            this.connectionString = connectionString;
            this.Bids = new List<Bid>();
            this.LoadBidsFromDatabase();
        }

        public BidRepository(List<Bid> bids, string connectionString)
        {
            this.connectionString = connectionString;
            Bids = bids;
        }

        private void LoadBidsFromDatabase()
        {
            string query = "SELECT * FROM Bid";

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int bidId = Convert.ToInt32(reader["BidID"]);
                    float bidSum = Convert.ToSingle(reader["BidSum"]);
                    DateTime timeOfBid = Convert.ToDateTime(reader["TimeOfBid"]);
                    int userId = Convert.ToInt32(reader["UserID"]);

                    User user = this.GetUserFromDataBase(userId);

                    Bid bid = new Bid(bidId, user, bidSum, timeOfBid);
                    Bids.Add((Bid)bid);
                }
            }
        }

        private User GetUserFromDataBase(int userID)
        {
            string query = $"SELECT * FROM Users WHERE UserID = {userID}";

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    string username = reader["Username"].ToString();
                    string nickname = reader["Nickname"].ToString();
                    string userType = reader["UserType"].ToString();
                    string password = reader["Password"].ToString();
               
                    User user;
                    user = new User(userID, username, nickname, password, userType);
                    return user;
                }
            }
            return null;
        }

        public void AddBidToRepo(Bid bid)
        {
            this.Bids.Add(bid);
        }

        public List<Bid> GetBids()
        {
            return this.Bids;
        }

        public void DeleteBidFromRepo(Bid bid)
        {
            this.Bids.Remove(bid);
        }

        public void UpdateBidIntoRepo(Bid oldbid, Bid newbid)
        {
            int oldbidIndex = this.Bids.FindIndex(bid => bid == oldbid);
            if (oldbidIndex != -1)
            {
                this.Bids[oldbidIndex] = newbid;
            }
        }
    }
}
