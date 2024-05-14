﻿using Client.View.WindowFactory;
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
    /// Interaction logic for UserLiveAuctionWindow.xaml
    /// </summary>
    public partial class UserLiveAuctionWindow : Window
    {
        private IWindowFactory _windowFactory;

        public UserLiveAuctionWindow(IWindowFactory windowFactory)
        {
            this._windowFactory = windowFactory;

            InitializeComponent();
        }

        private void NavigateToDetailsPage()
        {
            var auctionDetailsWindow = _windowFactory.CreateAuctionDetailsWindow();
            auctionDetailsWindow.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigateToDetailsPage();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigateToDetailsPage();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            NavigateToDetailsPage();
        }
    }
}
