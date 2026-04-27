using AirborneDustMonitor.Core.Email;
using AirborneDustMonitor.Core.Interfaces;
using AirborneDustMonitor.Core.Services;
using AirborneDustMonitor.Infrastructure;
using AirborneDustMonitor.Infrastructure.Configurations;
using AirborneDustMonitor.Infrastructure.Email;
using AirborneDustMonitor.UI.Services;
using AirborneDustMonitor.UI.ViewModels;
using AirborneDustMonitor.UI.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
                    // Az IAppSettingsService singleton, hogy minden modul ugyanazt a konfigurációt lássa
                    // AlertService, DataService, IEmailSender singleton, hogy minden modul ugyanazt a szolgáltatást használja, és ne legyenek több példányban, ami problémákat okozhatna az állapotkezelésben
                    // IWindowService singleton, hogy minden modul ugyanazt az ablakkezelőt használja
                    // A ViewModel-ek singleton, hogy megőrizzék az állapotukat, és ne legyenek több példányban, ami problémákat okozhatna a UI-ban, ezzel követve a MVVM mintát
                    services.AddSingleton<IAppSettingsService, JsonAppSettingsService>();
                    services.AddSingleton<AlertService>();
                    services.AddSingleton<DataService>();
                    services.AddSingleton<IEmailSender, MailKitEmailSender>();
                    services.AddSingleton<IWindowService, WindowService>();
                    services.AddSingleton<MainWindow>();
                    services.AddSingleton<MainWindowViewModel>();
                    services.AddSingleton<SettingsViewModel>();
                    // A SettingsWindow és MapWindow transient, mert bármikor megnyithatónak/bezárhatónak kell hogy legyenek, és nem kell megőrizniük az állapotukat, így nem szükséges singletonként kezelni őket, és a transient életciklus lehetővé teszi, hogy új példányokat hozzunk létre minden alkalommal, amikor megnyitjuk ezeket az ablakokat, ami egyszerűsíti a memória kezelését és elkerüli a potenciális problémákat az állapotkezelésben
                    services.AddTransient<SettingsWindow>();
                    services.AddTransient<MapWindow>();
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
