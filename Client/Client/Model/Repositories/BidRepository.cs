using Client.Model.Entities;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Client.Model.Repositories
{
    public class BidRepository : IBidRepository
    {
        private readonly HttpClient httpClient;
        public List<Bid> Bids { get; set; }

        public BidRepository(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.Bids = new List<Bid>();
            this.LoadBidsFromApi().Wait();
        }

        public BidRepository(List<Bid> bids, HttpClient httpClient)
        {
            this.httpClient = httpClient;
            Bids = bids;
        }

        private async Task LoadBidsFromApi()
        {
            var bids = await httpClient.GetFromJsonAsync<List<Bid>>("http://localhost:7100/api/bids");
            if (bids != null)
            {
                foreach (var bid in bids)
                {
                    Bids.Add(bid);
                }
            }
        }

        public async Task AddBidToRepo(Bid bid)
        {
            var response = await httpClient.PostAsJsonAsync("http://localhost:7100/api/bids", bid);
            response.EnsureSuccessStatusCode();
            Bids.Add(bid);
        }

        public List<Bid> GetBids()
        {
            return this.Bids;
        }

        public async Task DeleteBidFromRepo(Bid bid)
        {
            var response = await httpClient.DeleteAsync($"http://localhost:7100/api/bids/{bid.BidId}");
            response.EnsureSuccessStatusCode();
            Bids.Remove(bid);
        }

        public async Task UpdateBidIntoRepo(Bid oldBid, Bid newBid)
        {
            var response = await httpClient.PutAsJsonAsync($"http://localhost:7100/api/bids/{oldBid.BidId}", newBid);
            response.EnsureSuccessStatusCode();
            int oldBidIndex = this.Bids.FindIndex(bid => bid == oldBid);
            if (oldBidIndex != -1)
            {
                this.Bids[oldBidIndex] = newBid;
            }
        }
    }
}
