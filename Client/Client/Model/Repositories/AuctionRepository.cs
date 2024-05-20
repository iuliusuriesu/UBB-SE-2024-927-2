using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.Http;
using System.Threading.Tasks;
using Client.Model.Entities;

namespace Client.Model.Repositories
{
    public class AuctionRepository : IAuctionRepository
    {
        private string connectionString;
        public List<Auction> ListOfAuctions { get; set; }
        public AuctionRepository()
        {
            string connectionString = "";
            // inainte connection string ul era dat ca parametru
            // acum nu va mai fi nevoie ptc va fi inlocuit de call uri catre BackEnd
            // si oricum nu merg chestii date ca parametru din cauza dependency
            // injection ului
            this.connectionString = connectionString;
            this.ListOfAuctions = new List<Auction>();
            this.LoadAuctionsFromDatabase();    // this is async, maybe should be awaited, constructor can't be async, might need a workaround
        }

        public AuctionRepository(List<Auction> listOfAuctions, string connectionString)
        {
            this.connectionString = connectionString;
            this.ListOfAuctions = listOfAuctions;
        }

        private async Task LoadAuctionsFromDatabase()
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

        public async Task<List<Auction>> GetAllAuctions()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync("https://localhost:7100/api/Auctions");
                string apiResponse = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    List<Auction> auctions = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Auction>>(apiResponse);
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
                    var users = Newtonsoft.Json.JsonConvert.DeserializeObject<Auction>(apiResponse).listOfUsers;
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
                    bids = Newtonsoft.Json.JsonConvert.DeserializeObject<Auction>(apiResponse).listOfBids;
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

        public void AddToDB(string name, string description, DateTime date, float currentMaxSum)
        {
            using (var httpClient = new HttpClient())
            {
                Auction auction = new Auction(0, date, description, name, currentMaxSum);
                var response = httpClient.PostAsync("https://localhost:7100/api/Auctions", new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(auction), System.Text.Encoding.UTF8, "application/json")).Result;
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Auction added successfully.");
                }
                else
                {
                    throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
                }
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
