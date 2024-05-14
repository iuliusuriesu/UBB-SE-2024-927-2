using Client.Model.Services;
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
            return new DrugMarketplaceWindow(this, drugMarketplaceService, username);
        }

        public LogInWindow CreateLogInWindow()
        {
            var authenticationService = serviceProvider.GetRequiredService<IAuthenticationService>();
            return new LogInWindow(this, authenticationService);
        }

        public ShoppingCartWindow CreateShoppingCartWindow(string username)
        {
            var drugMarketplaceService = serviceProvider.GetRequiredService<IDrugMarketplaceService>();
            return new ShoppingCartWindow(this, drugMarketplaceService, username);
        }

        public SignUpWindow CreateSignUpWindow()
        {
            var authenticationService = serviceProvider.GetRequiredService<IAuthenticationService>();
            return new SignUpWindow(this, authenticationService);
        }
    }
}
