using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Client.Model.Entities;

namespace Client.Model.Repositories
{
    public class AuctionRepository : IAuctionRepository
    {
        private string connectionString;
        public List<Auction> ListOfAuctions { get; set; }
        public AuctionRepository(string connectionString)
        {
            this.connectionString = connectionString;
            this.ListOfAuctions = new List<Auction>();
            this.LoadAuctionsFromDatabase();
        }

        public AuctionRepository(List<Auction> listOfAuctions, string connectionString)
        {
            this.connectionString = connectionString;
            this.ListOfAuctions = listOfAuctions;
        }

        private void LoadAuctionsFromDatabase()
        {
            string query = "SELECT * FROM Auction";

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int auctionId = Convert.ToInt32(reader["AuctionID"]);
                    string auctionDescription = reader["AuctionDescription"].ToString();
                    string auctionName = reader["AuctionName"].ToString();
                    float currentMaxSum = Convert.ToSingle(reader["CurrentMaxSum"]);
                    DateTime dateOfStart = Convert.ToDateTime(reader["DateOfStart"]);
                    List<User> users = LoadUserFromDatabase(auctionId);
                    List<Bid> bids = LoadBidFromDatabase(auctionId);
                    Auction auction = new Auction(auctionId, dateOfStart, auctionDescription, auctionName, currentMaxSum, users, bids);
                    ListOfAuctions.Add(auction);
                }
            }
        }

        private List<User> LoadUserFromDatabase(int auctionId)
        {
            List<User> users = new List<User>();

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                string query = @"EXEC GetUniqueUsersFromAuctionBids @AuctionID = @auctionId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@auctionId", auctionId);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int userId = Convert.ToInt32(reader["UserID"]);
                    string username = reader["Username"].ToString();
                    string nickname = reader["Nickname"].ToString();
                    string userType = reader["UserType"].ToString();
                    string password = reader["Password"].ToString();

                    User user = new User(userId, username, nickname, password, userType);
                    users.Add(user);
                }
            }

            return users;
        }
        private List<Bid> LoadBidFromDatabase(int auctionID)
        {
            List<Bid> bids = new List<Bid>();

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                string query = @"EXEC GetBidsFromAuction @AuctionID = @auctionId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("AuctionID", auctionID);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int bidID = Convert.ToInt32(reader["BidID"]);
                    float bidSum = Convert.ToSingle(reader["BidSum"]);
                    DateTime timeOfBid = Convert.ToDateTime(reader["TimeOfBid"]);
                    int userId = Convert.ToInt32(reader["UserID"]);
                    User user = this.GetUserFromDataBase(userId);
                    Bid bid = new Bid(bidID, user, bidSum, timeOfBid);
                    bids.Add((Bid)bid);
                }
            }

            return bids;
        }

        private User GetUserFromDataBase(int userID)
        {
            string query = $"SELECT * FROM Users WHERE UserID = {userID}";

            using (SqlConnection connection = new SqlConnection(connectionString))
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

        public void AddAuctionToRepo(Auction auction)
        {
            ListOfAuctions.Add(auction);
        }

        public void AddToDB(string name, string description, DateTime date, float currentMaxSum)
        {
            string query = @"INSERT INTO Auction (DateOfStart, AuctionDescription, AuctionName, CurrentMaxSum) 
                     VALUES (@DateOfStart, @AuctionDescription, @AuctionName, @CurrentMaxSum)";

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@DateOfStart", date);
                command.Parameters.AddWithValue("@AuctionDescription", description);
                command.Parameters.AddWithValue("@AuctionName", name);
                command.Parameters.AddWithValue("@CurrentMaxSum", currentMaxSum);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public void RemoveAuctionFromRepo(Auction auction)
        {
            ListOfAuctions.Remove(auction);
        }

        public void UpdateAuctionIntoRepo(Auction oldauction, Auction newauction)
        {
            int oldauctionIndex = this.ListOfAuctions.FindIndex(auction => auction == oldauction);
            if (oldauctionIndex != -1)
            {
                this.ListOfAuctions[oldauctionIndex] = newauction;
            }
        }

        public float GetBidMaxSum(int index)
        {
            return this.ListOfAuctions[index].currentMaxSum;
        }
    }
}
