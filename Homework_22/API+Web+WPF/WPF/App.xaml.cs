using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Homework_22_WPF.Views;
using Homework_22_WPF.ViewModels;
using System.Windows;

namespace Homework_22_WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //public ServiceProvider Provider { get; }

        //public App()
        //{
        //    ServiceCollection serviceCollection = new();
        //    ConfigureServices(serviceCollection);
        //    Provider = serviceCollection.BuildServiceProvider();
        //}

        //private void ConfigureServices(IServiceCollection services)
        //{
        //    services.AddSingleton<MainWindow>();
        //    services.AddScoped<MainWindowViewModel, MainWindowViewModel>();
        //    services.AddMediatR(typeof(MediatREntryPoint).Assembly);
        //}

    }
}
