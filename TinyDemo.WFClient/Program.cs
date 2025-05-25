using Microsoft.Extensions.DependencyInjection;
using TinyDemo.ClientLib.Services;
using TinyDemo.SharedLib.Services;

namespace TinyDemo.WFClient
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var services = new ServiceCollection();

            //Gwen - Registreer je afhankelijkheden
            services.AddTransient<ILottoService, LottoService>();

            //Gwen - Voeg MainForm toe aan de ServiceCollection
            services.AddTransient<MainForm>();
            services.AddScoped<HttpClient>();

            //Gwen - Bouw de ServiceProvider
            var serviceProvider = services.BuildServiceProvider();


            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(serviceProvider.GetRequiredService<MainForm>());
        }
    }
}