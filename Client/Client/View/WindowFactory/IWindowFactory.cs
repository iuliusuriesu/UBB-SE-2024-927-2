using Client.View.AdminBiddingView;
using Client.View.BasicUserBiddingView;

namespace Client.View.WindowFactory
{
    public interface IWindowFactory
    {
        LogInWindow CreateLogInWindow();
        DrugMarketplaceWindow CreateDrugMarketplaceWindow(string username);
        ShoppingCartWindow CreateShoppingCartWindow(string username);
        SignUpWindow CreateSignUpWindow();
        MainWindow CreateMainWindow(string username);
        AdminLiveAuctionWindow CreateAdminLiveAuctionWindow();
        AddAuctionWindow CreateAddAuctionWindow();
        UserLiveAuctionWindow CreateUserLiveAuctionWindow();
        AuctionDetailsWindow CreateAuctionDetailsWindow(int auctionIndex);
        EnterAuctionWindow CreateEnterAuctionWindow(int auctionIndex);
    }
}
