using System;
using System.Windows;
using Application;
using Domain;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Models;

namespace Homework_19
{
    public partial class App : System.Windows.Application
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
