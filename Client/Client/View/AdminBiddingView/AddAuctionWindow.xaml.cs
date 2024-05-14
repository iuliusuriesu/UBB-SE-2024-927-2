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

        public AddAuctionWindow(IWindowFactory windowFactory)
        {
            this._windowFactory = windowFactory;

            InitializeComponent();
        }
    }
}
