using CurrencyConverter.Api;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;

namespace CurrencyConverter
{
    public partial class App
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((context, builder) =>
                { _ = builder.AddJsonFile($"appsettings.json"); })
                .ConfigureServices((context, services) => { ConfigureServices(context.Configuration, services); })
                .Build();
        }

        private static void ConfigureServices(IConfiguration configuration,
            IServiceCollection services)
        {
            services.Configure<AppSettings.AppSettings>
                (configuration.GetSection(nameof(AppSettings)));

            services.AddTransient<IDataGateway>(s => new ApiDataGateway(configuration.GetSection(nameof(AppSettings))));
            services.AddSingleton<MainWindow>();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();

            var mainWindow = _host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (_host)
            {
                await _host.StopAsync(TimeSpan.FromSeconds(5));
            }

            base.OnExit(e);
        }
    }
}
