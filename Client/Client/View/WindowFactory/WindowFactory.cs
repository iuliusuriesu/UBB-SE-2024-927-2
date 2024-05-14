using Client.Model.Services;
using Client.View.AdminBiddingView;
using Client.View.BasicUserBiddingView;
using Client.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Client.View.WindowFactory
{
    public class WindowFactory : IWindowFactory
    {
        private readonly IServiceProvider serviceProvider;

        public WindowFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public DrugMarketplaceWindow CreateDrugMarketplaceWindow(string username)
        {
            var drugMarketplaceService = serviceProvider.GetRequiredService<IDrugMarketplaceService>();
            var productViewModel = serviceProvider.GetRequiredService<IProductViewModel>();
            return new DrugMarketplaceWindow(this, drugMarketplaceService, productViewModel, username);
        }

        public LogInWindow CreateLogInWindow()
        {
            var authenticationService = serviceProvider.GetRequiredService<IAuthenticationService>();
            return new LogInWindow(this, authenticationService);
        }

        public ShoppingCartWindow CreateShoppingCartWindow(string username)
        {
            var shoppingCartViewModel = serviceProvider.GetRequiredService<IShoppingCartViewModel>();
            return new ShoppingCartWindow(this, shoppingCartViewModel, username);
        }

        public SignUpWindow CreateSignUpWindow()
        {
            var authenticationService = serviceProvider.GetRequiredService<IAuthenticationService>();
            return new SignUpWindow(this, authenticationService);
        }

        public MainWindow CreateMainWindow(string username)
        {
            var authenticationService = serviceProvider.GetRequiredService<IAuthenticationService>();
            return new MainWindow(this, authenticationService, username);
        }

        public AdminLiveAuctionWindow CreateAdminLiveAuctionWindow()
        {
            return new AdminLiveAuctionWindow(this);
        }

        public AddAuctionWindow CreateAddAuctionWindow()
        {
            return new AddAuctionWindow(this);
        }

        public UserLiveAuctionWindow CreateUserLiveAuctionWindow()
        {
            return new UserLiveAuctionWindow(this);
        }

        public AuctionDetailsWindow CreateAuctionDetailsWindow()
        {
            return new AuctionDetailsWindow(this);
        }

        public EnterAuctionWindow CreateEnterAuctionWindow()
        {
            return new EnterAuctionWindow(this);
        }
    }
}
