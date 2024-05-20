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
    /// Interaction logic for AddAuctionWindow.xaml
    /// </summary>
    public partial class AddAuctionWindow : Window
    {
        private IWindowFactory _windowFactory;
        public IAuctionService _auctionService;
        public List<Auction> auctions;

        public AddAuctionWindow(IWindowFactory windowFactory, IAuctionService auctionService)
        {
            this._windowFactory = windowFactory;
            this._auctionService = auctionService;
            auctions = _auctionService.GetAuctions();

            InitializeComponent();
        }

        private void CancelAddAuction(object sender, RoutedEventArgs e)
        {
            var adminLiveAuctionWindow = _windowFactory.CreateAdminLiveAuctionWindow();
            adminLiveAuctionWindow.Show();
            this.Close();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            String name = AddName.Text;
            string description = AddDescription.Text;
            string dateString = AddDeadline.Text;


            DateTime deadlineDate;
            if (DateTime.TryParseExact(dateString, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out deadlineDate))
            {
                Console.WriteLine("Parsed date: " + deadlineDate);
            }
            else
            {
                Console.WriteLine("Unable to parse the date string.");
            }
            //String date = AddDeadline.Text;
            int suminput = 0;
            if (!int.TryParse(AddStartingPrice.Text, out suminput))
            {
                MessageBox.Show("Please enter a valid number");
                return;
            }
            this._auctionService.AddBid(name, description, deadlineDate, suminput);
        }
    }
}
