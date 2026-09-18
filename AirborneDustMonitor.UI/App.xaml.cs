using AirborneDustMonitor.Core.Email;
using AirborneDustMonitor.Core.Interfaces;
using AirborneDustMonitor.Core.Services;
using AirborneDustMonitor.Infrastructure;
using AirborneDustMonitor.Infrastructure.Configurations;
using AirborneDustMonitor.Infrastructure.Email;
using AirborneDustMonitor.Infrastructure.Logging;
using AirborneDustMonitor.UI.Services;
using AirborneDustMonitor.UI.ViewModels;
using AirborneDustMonitor.UI.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Windows;

namespace AirborneDustMonitor.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddHostedService<SensorPollingService>();
                    services.AddDbContext<SensorDbContext>();
                    services.AddScoped<ISensorDb, SensorDb>();
                    services.AddSingleton<IAppSettingsService, JsonAppSettingsService>();
                    services.AddSingleton<AlertService>();
                    services.AddSingleton<DataService>();
                    services.AddSingleton<IEmailSender, MailKitEmailSender>();
                    services.AddSingleton<IWindowService, WindowService>();
                    services.AddSingleton<MainWindow>();
                    services.AddTransient<MainWindowViewModel>();
                    services.AddTransient<SettingsViewModel>();
                    services.AddTransient<SettingsWindow>();
                    services.AddTransient<MapWindow>();
                    services.AddApplicationLogging();
                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            await _host.StartAsync();
            var mainWindow = _host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            using (_host)
            {
                await _host.StopAsync();
            }
        }
    }
}
