namespace Client.View.WindowFactory
{
    public interface IWindowFactory
    {
        LogInWindow CreateLogInWindow();

        DrugMarketplaceWindow CreateDrugMarketplaceWindow(string username);

        ShoppingCartWindow CreateShoppingCartWindow(string username);

        SignUpWindow CreateSignUpWindow();
    }
}
