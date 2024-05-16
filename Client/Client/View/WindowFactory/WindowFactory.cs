using Client.Model.Repositories;
using Client.Model.Services;
using Client.View.AdminBiddingView;
using Client.View.BasicUserBiddingView;
using Client.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

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
            var auctionRepo = serviceProvider.GetRequiredService<IAuctionRepository>();
            var auctionService = serviceProvider.GetRequiredService<IAuctionService>();
            var bidService = serviceProvider.GetRequiredService<IBidService>();
            return new AdminLiveAuctionWindow(this, auctionService, bidService);
        }

        public AddAuctionWindow CreateAddAuctionWindow()
        {
            var auctionService = serviceProvider.GetRequiredService<IAuctionService>();
            return new AddAuctionWindow(this, auctionService);
        }

        public UserLiveAuctionWindow CreateUserLiveAuctionWindow()
        {
            var auctionService = serviceProvider.GetRequiredService<IAuctionService>();
            var bidService = serviceProvider.GetRequiredService<IBidService>();

            return new UserLiveAuctionWindow(this, bidService, auctionService);
        }

        public AuctionDetailsWindow CreateAuctionDetailsWindow(int auctionIndex)
        {
            var auctionService = serviceProvider.GetRequiredService<IAuctionService>();
            var bidService = serviceProvider.GetRequiredService<IBidService>();
            return new AuctionDetailsWindow(this, auctionService, bidService, auctionIndex);
        }

        public EnterAuctionWindow CreateEnterAuctionWindow(int auctionIndex)
        {
            var auctionService = serviceProvider.GetRequiredService<IAuctionService>();
            var bidService = serviceProvider.GetRequiredService<IBidService>();
            return new EnterAuctionWindow(this, auctionService, bidService, auctionIndex);
        }
    }
}
