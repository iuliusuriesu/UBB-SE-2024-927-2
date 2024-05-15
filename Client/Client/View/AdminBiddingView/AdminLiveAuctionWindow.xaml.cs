using Client.Model.Entities;
using Client.Model.Services;
using Client.View.WindowFactory;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Client.View.AdminBiddingView
{
    /// <summary>
    /// Interaction logic for AdminLiveAuctionWindow.xaml
    /// </summary>
    public partial class AdminLiveAuctionWindow : Window
    {
        private IWindowFactory _windowFactory;

        public IAuctionService _auctionService;
        public IBidService _bidService;

        public List<Auction> auctions;

        public AdminLiveAuctionWindow(IWindowFactory windowFactory, IAuctionService auctionService, IBidService bidService)
        {
            this._windowFactory = windowFactory;
            this._auctionService = auctionService;
            this._bidService = bidService;

            auctions = _auctionService.GetAuctions();

            InitializeComponent();

            name1.Text = auctions[0].name;
            name2.Text = auctions[1].name;
            //name3.Text = auctions[3].name;
            description1.Text = auctions[0].description;
            description2.Text = auctions[1].description;
            //description3.Text = auctions[2].description;
            price1.Text = auctions[0].currentMaxSum.ToString();
            price2.Text = auctions[1].currentMaxSum.ToString();
            //price3.Text = auctions[2].currentMaxSum.ToString();

            time1.Text = (DateTime.Now - auctions[0].startingDate).Hours.ToString();
            time2.Text = (DateTime.Now - auctions[1].startingDate).Hours.ToString();
            //time3.Text = (DateTime.Now - auctions[2].startingDate).Hours.ToString();
        }

        private void EnterAuction(object sender, RoutedEventArgs e)
        {
            var auctionDetailsWindow = _windowFactory.CreateAuctionDetailsWindow(1);
            auctionDetailsWindow.Show();
            this.Close();
        }

        private void AddAuction(object sender, RoutedEventArgs e)
        {
            var addAuctionWindow = _windowFactory.CreateAddAuctionWindow();
            addAuctionWindow.Show();
            this.Close();
        }
    }
}
