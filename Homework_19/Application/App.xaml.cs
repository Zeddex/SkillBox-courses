using System.Configuration;
using System.Reflection;
using System.Windows;
using Application;
using Application.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Models;
using Presentation.View;
using Presentation.ViewModels;

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
            services.AddDbContext<AppContext>();
            services.AddScoped<IDataAccess, BankProvider>();
            services.AddSingleton<MainWindow>();
            services.AddScoped<MainWindowViewModel, MainWindowViewModel>();
            services.AddMediatR(typeof(MediatREntryPoint).Assembly);
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = Provider.GetService<MainWindow>();
            mainWindow.DataContext = Provider.GetService<MainWindowViewModel>();    // TODO убрать костыль
            mainWindow.Show();
        }
    }
}
