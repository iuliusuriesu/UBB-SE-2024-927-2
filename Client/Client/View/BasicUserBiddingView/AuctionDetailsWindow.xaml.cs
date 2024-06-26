﻿using Client.Model.Entities;
using Client.Model.Services;
using Client.View.WindowFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Client.View.BasicUserBiddingView
{
    /// <summary>
    /// Interaction logic for AuctionDetailsWindow.xaml.
    /// </summary>
    public partial class AuctionDetailsWindow : Window
    {
        private IWindowFactory _windowFactory;

        public int auctionIndex;

        public IAuctionService _auctionService;
        public IBidService _bidService;

        public List<Bid> bidModels;
        public List<Auction> auctions;

        public AuctionDetailsWindow(IWindowFactory windowFactory, IAuctionService auctionService, IBidService bidService, int auctionIndex)
        {
            this._windowFactory = windowFactory;
            InitializeComponent();
            this.auctionIndex = auctionIndex;
            this._auctionService = auctionService;
            this._bidService = bidService;
            List<Auction> auctions = _auctionService.GetAuctions();

            AuctionNameBid.Text = auctions[auctionIndex].name;
            CurrentBid.Text = auctions[auctionIndex].currentMaxSum.ToString();
            TimeLeft.Text = (DateTime.Now - auctions[auctionIndex].startingDate).Hours.ToString();

            int n = auctions[auctionIndex].listOfBids.Count;
            for (int i = 0; i < n; i++)
            {
                BidHistory.Text += auctions[auctionIndex].listOfBids[i].BidSum.ToString() + "\n";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var userLiveAuctionWindow = _windowFactory.CreateUserLiveAuctionWindow();
            userLiveAuctionWindow.Show();
            this.Close();
        }

        private void JoinAuction(object sender, RoutedEventArgs e)
        {
            var enterAuctionWindow = _windowFactory.CreateEnterAuctionWindow(auctionIndex);
            enterAuctionWindow.Show();
            this.Close();
        }
    }
}
