using System;
using System.Net.Http;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using TinyDemo.ClientLib.Services;
using TinyDemo.MVVM;
using TinyDemo.SharedLib.Services;

namespace TinyDemo.AvaloniaClient;

public partial class App : Application
{
    public static IServiceProvider ServiceProvider { get; private set; }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        
        //Gwen -  Configure services en stel de ServiceProvider in
        var services = new ServiceCollection();
        ConfigureServices(services);
        ServiceProvider = services.BuildServiceProvider();
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            desktop.MainWindow = mainWindow;
        }

        base.OnFrameworkInitializationCompleted();
    }
    
    private void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<ILottoService, LottoService>();
        services.AddSingleton<HttpClient>();
        services.AddSingleton<MainViewModel>(); // Changed from Transient to Singleton
        services.AddTransient<MainWindow>();
    }
}