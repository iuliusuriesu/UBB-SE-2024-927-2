using System.Data.SqlClient;
using BiddingPlatform.User;

namespace BiddingPlatform.Bid
{
    public class BidRepository : IBidRepository
    {
        private string connectionString;
        public List<IBidModel> Bids { get; set; }

        public BidRepository(string connectionString)
        {
            this.connectionString = connectionString;
            this.Bids = new List<IBidModel>();
            this.LoadBidsFromDatabase();
        }

        public BidRepository(List<IBidModel> bids, string connectionString)
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

                    BasicUser user = this.GetUserFromDataBase(userId);

                    BidModel bid = new BidModel(bidId, user, bidSum, timeOfBid);
                    Bids.Add((IBidModel)bid);
                }
            }
        }

        private BasicUser GetUserFromDataBase(int userID)
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
                    BasicUser user;
                    user = new BasicUser(userID, username, nickname);
                    return user;
                }
            }
            return null;
        }

        public void AddBidToRepo(IBidModel bid)
        {
            this.Bids.Add(bid);
        }

        public List<IBidModel> GetBids()
        {
            return this.Bids;
        }

        public void DeleteBidFromRepo(IBidModel bid)
        {
            this.Bids.Remove(bid);
        }

        public void UpdateBidIntoRepo(IBidModel oldbid, IBidModel newbid)
        {
            int oldbidIndex = this.Bids.FindIndex(bid => bid == oldbid);
            if (oldbidIndex != -1)
            {
                this.Bids[oldbidIndex] = newbid;
            }
        }
    }
}
