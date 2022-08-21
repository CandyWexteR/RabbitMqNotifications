using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMqTestNotifications.Server.ViewModels;
using RabbitMqTestNotifications.Server.Views;
using ReactiveUI;
using Splat;
using Splat.Microsoft.Extensions.DependencyInjection;

namespace RabbitMqTestNotifications.Server.Repositories
{
    public class AppBootstrapper : IScreen
    {
        public AppBootstrapper()
        {
            Router = new RoutingState();
            Init();
        }

        public RoutingState Router { get; }

        public IServiceProvider Container { get; private set; }

        void Init()
        {
            var host = Host
                .CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services.UseMicrosoftDependencyResolver();
                    var resolver = Locator.CurrentMutable;
                    resolver.InitializeSplat();
                    resolver.InitializeReactiveUI();

                    // Configure our local services and access the host configuration
                    ConfigureServices(services);
                })
                .UseEnvironment(Environments.Development)
                .Build();

            // Since MS DI container is a different type,
            // we need to re-register the built container with Splat again
            Container = host.Services;
            Container.UseMicrosoftDependencyResolver();
        }

        void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<AppBootstrapper>();
            services.AddSingleton<IScreen, AppBootstrapper>();

            services.AddSingleton<ConnectionFactory>(_ => new ConnectionFactory() { HostName = "localhost" });
            services.AddSingleton<MessageViewModel>();
            services.AddSingleton<IViewFor<MessageViewModel>, MessageView>();
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<IViewFor<MainWindowViewModel>, MainWindowView>();
        }
    }
}