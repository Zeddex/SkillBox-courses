using System.Windows;
using Domain;
using Domain.Models;
using Homework_19.View;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Homework_19
{
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            ServiceCollection serviceCollection = new();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IDataAccess, BankProvider>();
            services.AddMediatR(typeof(MediatREntryPoint).Assembly);
        }
    }
}
