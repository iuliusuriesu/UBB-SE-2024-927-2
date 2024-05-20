using System;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.Http;
using System.Threading.Tasks;
using Client.Model.Entities;
using Newtonsoft.Json;

namespace Client.Model.Repositories
{
    public class AuctionRepository : IAuctionRepository
    {
        public List<Auction> ListOfAuctions { get; set; }
        public AuctionRepository()
        {
            this.ListOfAuctions = new List<Auction>();
            this.LoadAuctionsFromDatabase();    // this is async, maybe should be awaited, constructor can't be async, might need a workaround
        }

        public AuctionRepository(List<Auction> listOfAuctions, string connectionString)
        {
            this.ListOfAuctions = listOfAuctions;
        }

        private async Task LoadAuctionsFromDatabase()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync("https://localhost:7100/api/Auctions");
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        List<Auction> auctions = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Auction>>(apiResponse);
                        foreach (var auction in auctions)
                        {
                            ListOfAuctions.Add(auction);
                        }
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        Console.WriteLine("No auctions found");
                    }
                    else
                    {
                        throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
                    }
                }
            }
            catch (HttpRequestException httpRequestException)
            {
                throw new Exception($"Request error: {httpRequestException.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}");
            }
        }

        public async Task<List<Auction>> GetAllAuctions()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync("https://localhost:7100/api/Auctions");
                string apiResponse = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    List<Auction> auctions = JsonConvert.DeserializeObject<List<Auction>>(apiResponse);
                    return auctions;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine("No auctions found");
                    return null;
                }
                else
                {
                    throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
                }
            }
        }

        private async Task<List<User>> LoadUserFromDatabase(int auctionId)
        { // this needs to be changed since the auction doesnt have the users list anymore
          // the user list can be obtained from the Bids that have Bid.AuctionId == auctionId

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"https://localhost:7100/api/Auctions/{auctionId}");
                string apiResponse = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var users = JsonConvert.DeserializeObject<Auction>(apiResponse).listOfUsers;
                    return users;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine("No auction with given id found");
                    return null;
                }
                else
                {
                    throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
                }
            }
        }

        private List<Bid> LoadBidFromDatabase(int auctionID)
        {
            List<Bid> bids = new List<Bid>();

            using (var httpClient = new HttpClient())
            {
                var response = httpClient.GetAsync($"https://localhost:7100/api/Auctions/{auctionID}").Result;
                string apiResponse = response.Content.ReadAsStringAsync().Result;
                if (response.IsSuccessStatusCode)
                {
                    bids = JsonConvert.DeserializeObject<Auction>(apiResponse).listOfBids;
                    return bids;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine("No auction with given id found.");
                    return null;
                }
                else
                {
                    throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
                }
            }
        }

        private User GetUserFromDataBase(int userID)
        {
            using (var httpClient = new HttpClient())
            {
                var response = httpClient.GetAsync($"https://localhost:7100/api/Users/{userID}").Result;
                string apiResponse = response.Content.ReadAsStringAsync().Result;
                if (response.IsSuccessStatusCode)
                {
                    User user = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(apiResponse);
                    return user;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine("No user with given id found.");
                    return null;
                }
                else
                {
                    throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
                }
            }
        }

        public void AddAuctionToRepo(Auction auction)
        {
            ListOfAuctions.Add(auction);
        }

        public async Task AddToDB(string name, string description, DateTime date, float currentMaxSum)
        {
            Auction auction = new Auction(0, date, description, name, currentMaxSum);
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsync("https://localhost:7100/api/Auctions", new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(auction), System.Text.Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Auction added successfully.");
                }
                else
                {
                    throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
                }
            }

            this.AddAuctionToRepo(auction);
        }

        public void RemoveAuctionFromRepo(Auction auction)
        {
            ListOfAuctions.Remove(auction);
        }

        public async Task RemoveFromDB(int auctionID)
        {
            using (var httpClient = new HttpClient())
            {
                string endpoint = $"https://localhost:7100/api/Auctions/{auctionID}";
                var response = await httpClient.DeleteAsync(endpoint);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Auction deleted successfully.");
                }
                else
                {
                    throw new Exception($"Error when trying to delete an auction. Status code: {response.StatusCode}, Reason: {response.ReasonPhrase}");
                }
            }

            this.ListOfAuctions.RemoveAll(auction => auction.auctionId == auctionID);
        }

        public void UpdateAuctionIntoRepo(Auction oldauction, Auction newauction)
        {
            int oldauctionIndex = this.ListOfAuctions.FindIndex(auction => auction.auctionId == oldauction.auctionId);
            if (oldauctionIndex != -1)
            {
                this.ListOfAuctions[oldauctionIndex] = newauction;
            }
        }

        public async Task UpdateIntoDB(int oldAuctionID, Auction newAuction)
        {
            using (var httpClient = new HttpClient())
            {
                string jsonSerialized = JsonConvert.SerializeObject(newAuction);
                var content = new StringContent(jsonSerialized, Encoding.UTF8, "application/json");
                string endpoint = $"https://localhost:7100/api/Auctions/{oldAuctionID}";

                var response = await httpClient.PutAsync(endpoint, content);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Auction updated successfully.");
                }
                else
                {
                    throw new Exception($"Error when trying to update an auction. Status code: {response.StatusCode}, Reason: {response.ReasonPhrase}");
                }
            }

            this.UpdateAuctionIntoRepo(new Auction(oldAuctionID, new System.DateTime(2024, 21, 05, 01, 08, 02), "description", "name", 0), newAuction);
        }

        public float GetBidMaxSum(int index)
        {
            return this.ListOfAuctions[index].currentMaxSum;
        }
    }
}
