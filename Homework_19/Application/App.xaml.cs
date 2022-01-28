using System.Reflection;
using System.Windows;
using Application;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Models;
using Presentation.View;

namespace Presentation
{
    public partial class App : System.Windows.Application
    {
        public ServiceProvider Provider { get; }

        public App()
        {
            ServiceCollection serviceCollection = new();
            ConfigureServices(serviceCollection);
            Provider = serviceCollection.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IDataAccess, BankProvider>();
            services.AddSingleton<MainWindow>();
            //services.AddMediatR(typeof(MediatREntryPoint).Assembly);
            services.AddMediatR(typeof(App).GetTypeInfo().Assembly);
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = Provider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }
}
