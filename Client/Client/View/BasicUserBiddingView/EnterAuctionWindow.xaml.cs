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
    /// Interaction logic for EnterAuctionWindow.xaml
    /// </summary>
    public partial class EnterAuctionWindow : Window
    {
        private IWindowFactory _windowFactory;

        public int auctionIndex;

        public IAuctionService _auctionService;
        public IBidService _bidService;

        public List<Bid> bidModels;
        public List<Auction> auctions;

        public EnterAuctionWindow(IWindowFactory windowFactory, IAuctionService auctionService, IBidService bidService, int auctionIndex)
        {
            this._windowFactory = windowFactory;
            this.auctionIndex = auctionIndex;
            this._auctionService = auctionService;
            this._bidService = bidService;

            this.auctions = _auctionService.GetAuctions();

            InitializeComponent();

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
            var auctionDetailsWindow = _windowFactory.CreateAuctionDetailsWindow(auctionIndex);
            auctionDetailsWindow.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int suminput = 0;
            if (!int.TryParse(SumInput.Text, out suminput))
            {
                MessageBox.Show("Please enter a valid number");
                return;
            }
            suminput = Convert.ToInt32(SumInput.Text);
            if (suminput <= this._auctionService.GetMaxBidSum(auctionIndex))
            {
                MessageBox.Show("Invalid bid sum!\n The bid must be greater than that current maximum one.");
            }
            suminput = Convert.ToInt32(SumInput.Text);
            BidHistory.Text += suminput.ToString() + "\n";
        }
    }
}
