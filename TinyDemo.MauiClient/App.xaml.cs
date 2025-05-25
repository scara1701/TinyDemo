using TinyDemo.ClientLib.Services;
using TinyDemo.MVVM;
using TinyDemo.SharedLib.Services;

namespace TinyDemo.MauiClient
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }



        public App()
        {
            InitializeComponent();

            //Gwen -  Configure services en stel de ServiceProvider in
            var services = new ServiceCollection();
            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();

        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var appShell = ServiceProvider.GetRequiredService<AppShell>();
            return new Window(appShell);
            //return new Window(new AppShell());
        }


        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ILottoService, LottoService>();
            services.AddSingleton<HttpClient>();
            services.AddTransient<MainViewModel>();
            services.AddTransient<MainPage>();
            services.AddSingleton<AppShell>(); //Gwen -  Zorg ervoor dat AppShell wordt geregistreerd
        }

    }
}