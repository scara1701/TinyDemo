using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Net.Http;
using System.Windows;
using TinyDemo.ClientLib.Services;
using TinyDemo.MVVM;
using TinyDemo.SharedLib.Services;

namespace TinyDemo.WPFClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);

            ServiceProvider = services.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();


            base.OnStartup(e);
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ILottoService, LottoService>();
            services.AddSingleton<HttpClient>();
            services.AddTransient<MainViewModel>();
            services.AddSingleton<MainWindow>();

        }

    }
}
