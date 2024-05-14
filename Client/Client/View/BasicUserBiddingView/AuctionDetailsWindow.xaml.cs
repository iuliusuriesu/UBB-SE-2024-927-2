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

namespace Client.View.BasicUserBiddingView
{
    /// <summary>
    /// Interaction logic for AuctionDetailsWindow.xaml
    /// </summary>
    public partial class AuctionDetailsWindow : Window
    {
        private IWindowFactory _windowFactory;

        public AuctionDetailsWindow(IWindowFactory windowFactory)
        {
            this._windowFactory = windowFactory;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var userLiveAuctionWindow = _windowFactory.CreateUserLiveAuctionWindow();
            userLiveAuctionWindow.Show();
            this.Close();
        }

        private void JoinAuction(object sender, RoutedEventArgs e)
        {
            var enterAuctionWindow = _windowFactory.CreateEnterAuctionWindow();
            enterAuctionWindow.Show();
            this.Close();
        }
    }
}
