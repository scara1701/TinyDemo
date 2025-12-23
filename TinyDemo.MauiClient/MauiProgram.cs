using Microsoft.Extensions.Logging;
using TinyDemo.ClientLib.Services;
using TinyDemo.MVVM;
using TinyDemo.SharedLib.Services;

namespace TinyDemo.MauiClient
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Configure dependency injection
            ConfigureServices(builder.Services);

#if DEBUG
			builder.Logging.AddDebug();
#endif

            var app = builder.Build();
            
            // Create the App instance using the service provider
            var serviceProvider = app.Services;
            var mainApp = serviceProvider.GetRequiredService<App>();
            
            return app;
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // Register services
            services.AddSingleton<ILottoService, LottoService>();
            services.AddSingleton<HttpClient>();
            services.AddSingleton<MainViewModel>();
            
            // Register pages
            services.AddTransient<MainPage>();
            services.AddSingleton<AppShell>();
            
            // Register App with constructor injection
            services.AddSingleton<App>();
        }
    }
}