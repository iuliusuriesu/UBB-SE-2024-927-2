﻿using Client.Model.Entities;
using Client.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Client.Model.Services
{
    public class AuctionService : IAuctionService
    {
        private IAuctionRepository AuctionRepository { get; set; }

        public AuctionService(AuctionRepository auctionRepository)
        {
            this.AuctionRepository = auctionRepository;
        }

        public void AddAuction(int id, DateTime startingDate, string description, string name, float currentMaxSum)
        {
            Auction auction = new Auction(id, startingDate, description, name, currentMaxSum);
            this.AuctionRepository.AddAuctionToRepo(auction);
        }

        public void RemoveAuction(int id, DateTime startingDate, string description, string name, float currentMaxSum)
        {
            Auction auction = new Auction(id, startingDate, description, name, currentMaxSum);
            this.AuctionRepository.RemoveAuctionFromRepo(auction);
        }

        public List<Auction> GetAuctions()
        {
            return this.AuctionRepository.ListOfAuctions;
        }

        public void UpdateAuction(int id, DateTime oldstartingDate, string olddescription, string oldname, float oldcurrentMaxSum, DateTime newstartingDate, string newdescription, string newname, float newcurrentMaxSum)
        {
            Auction oldauction = new Auction(id, oldstartingDate, olddescription, oldname, oldcurrentMaxSum);
            Auction newauction = new Auction(id, newstartingDate, newdescription, newname, newcurrentMaxSum);
            this.AuctionRepository.UpdateAuctionIntoRepo(oldauction, newauction);
        }

        public float GetMaxBidSum(int index)
        {
            return this.AuctionRepository.GetBidMaxSum(index);
        }

        public void AddBid(string name, string description, DateTime date, float currentMaxSum)
        {
            this.AuctionRepository.AddToDB(name, description, date, currentMaxSum);
        }
    }
}
