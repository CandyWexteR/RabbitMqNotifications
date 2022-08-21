using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMqTestNotifications.Server.Repositories;
using RabbitMqTestNotifications.Server.ViewModels;
using RabbitMqTestNotifications.Server.Views;
using ReactiveUI;
using Splat;

namespace RabbitMqTestNotifications.Server
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public AppBootstrapper Bootstrapper { get; protected set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Bootstrapper = new AppBootstrapper();

            var shellview = (MainWindowView)Locator.Current.GetService(typeof(IViewFor<MainWindowViewModel>));
            
            shellview?.Show();
        }
    }
}