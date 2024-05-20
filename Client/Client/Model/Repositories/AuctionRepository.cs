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
                    foreach (var auction in auctions)
                    {
                        auction.auctionName = auction.auctionName;
                    }
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

        public async Task AddAuctionToRepo(Auction auction)
        {
            ListOfAuctions.Add(auction);
            await this.AddToDB(auction.auctionName, auction.auctionDescription, auction.startingDate, auction.currentMaxSum);
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
        }

        public async Task RemoveAuctionFromRepo(Auction auction)
        {
            ListOfAuctions.Remove(auction);
            await this.RemoveFromDB(auction.auctionID);
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
            //this.ListOfAuctions.RemoveAll(auction => auction.auctionId == auctionID);
        }

        public async Task UpdateAuctionIntoRepo(Auction oldauction, Auction newauction)
        {
            int oldauctionIndex = this.ListOfAuctions.FindIndex(auction => auction.auctionID == oldauction.auctionID);
            if (oldauctionIndex != -1)
            {
                this.ListOfAuctions[oldauctionIndex] = newauction;
            }
            await UpdateIntoDB(oldauction.auctionID, newauction);
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
        }

        public float GetBidMaxSum(int index)
        {
            return this.ListOfAuctions[index].currentMaxSum;
        }
    }
}
