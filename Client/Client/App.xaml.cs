using Client.Model.Repositories;
using Client.Model.Services;
using Client.View.WindowFactory;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider serviceProvider;

        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            this.serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IProductRepository, ProductRepository>();

            services.AddSingleton<IAuthenticationService, AuthenticationService>();
            services.AddSingleton<IDrugMarketplaceService, DrugMarketplaceService>();

            services.AddSingleton<IWindowFactory, WindowFactory>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var windowFactory = serviceProvider.GetRequiredService<IWindowFactory>();
            var logInWindow = windowFactory.CreateLogInWindow();
            logInWindow.Show();
        }
    }
}
