using System.Data.SqlClient;
using BiddingPlatform.Bid;
using BiddingPlatform.User;

namespace BiddingPlatform.Auction
{
    public class AuctionRepository : IAuctionRepository
    {
        private string connectionString;
        public List<IAuctionModel> ListOfAuctions { get; set; }
        public AuctionRepository(string connectionString)
        {
            this.connectionString = connectionString;
            this.ListOfAuctions = new List<IAuctionModel>();
            this.LoadAuctionsFromDatabase();
        }

        public AuctionRepository(List<IAuctionModel> listOfAuctions, string connectionString)
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
                    List<BasicUser> users = LoadUserFromDatabase(auctionId);
                    List<IBidModel> bids = LoadBidFromDatabase(auctionId);
                    IAuctionModel auction = new AuctionModel(auctionId, dateOfStart, auctionDescription, auctionName, currentMaxSum, users, bids);
                    ListOfAuctions.Add(auction);
                }
            }
        }

        private List<BasicUser> LoadUserFromDatabase(int auctionId)
        {
            List<BasicUser> users = new List<BasicUser>();

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

                    BasicUser user = new BasicUser(userId, username, nickname);
                    users.Add(user);
                }
            }

            return users;
        }
        private List<IBidModel> LoadBidFromDatabase(int auctionID)
        {
            List<IBidModel> bids = new List<IBidModel>();

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
                    BasicUser user = this.GetUserFromDataBase(userId);
                    BidModel bid = new BidModel(bidID, user, bidSum, timeOfBid);
                    bids.Add((IBidModel)bid);
                }
            }

            return bids;
        }

        private BasicUser GetUserFromDataBase(int userID)
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
                    BasicUser user;
                    user = new BasicUser(userID, username, nickname);
                    return user;
                }
            }
            return null;
        }

        public void AddAuctionToRepo(IAuctionModel auction)
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
        public void RemoveAuctionFromRepo(IAuctionModel auction)
        {
            ListOfAuctions.Remove(auction);
        }

        public void UpdateAuctionIntoRepo(IAuctionModel oldauction, IAuctionModel newauction)
        {
            int oldauctionIndex = this.ListOfAuctions.FindIndex(auction => auction == oldauction);
            if (oldauctionIndex != -1)
            {
                this.ListOfAuctions[oldauctionIndex] = newauction;
            }
        }
        public float GetBidMaxSum(int index)
        {
            return this.ListOfAuctions[index].CurrentMaxBid;
        }
    }
}
