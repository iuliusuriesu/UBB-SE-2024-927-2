using Client.Model.Entities;
using Client.Model.Services;
using Client.View.WindowFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    /// Interaction logic for UserLiveAuctionWindow.xaml
    /// </summary>
    public partial class UserLiveAuctionWindow : Window
    {
        private IWindowFactory _windowFactory;
        public IAuctionService _auctionService;

        public List<Auction> auctions;

        public UserLiveAuctionWindow(IWindowFactory windowFactory, IAuctionService auctionService)
        {
            this._windowFactory = windowFactory;
            this._auctionService = auctionService;
            auctions = new List<Auction>();
            _auctionService.GetAuctions().ContinueWith(auctionTask =>
            {
                Application.Current.Dispatcher.Invoke((Action)delegate
                {
                    name1.Text = auctions[0].name;
                    name2.Text = auctions[1].name;
                    //name3.Text= auctions[2].name;
                    description1.Text = auctions[0].description;
                    description2.Text = auctions[1].description;
                    //description3.Text = auctions[2].description;
                    price1.Text = auctions[0].currentMaxSum.ToString();
                    price2.Text = auctions[1].currentMaxSum.ToString();
                    //price3.Text = auctions[2].currentMaxSum.ToString();

                    time1.Text = (DateTime.Now - auctions[0].startingDate).Hours.ToString();
                    time2.Text = (DateTime.Now - auctions[1].startingDate).Hours.ToString();
                    //time3.Text = (DateTime.Now - auctions[2].startingDate).Hours.ToString();

                    InitializeComponent();
                });
            });
        }

        private void NavigateToDetailsPage(int index)
        {
            var auctionDetailsWindow = _windowFactory.CreateAuctionDetailsWindow(index);
            auctionDetailsWindow.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigateToDetailsPage(0);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigateToDetailsPage(1);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            NavigateToDetailsPage(2);
        }
    }
}
