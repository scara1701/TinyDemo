using Microsoft.Extensions.DependencyInjection;

namespace TinyDemo.MauiClient
{
    public partial class AppShell : Shell
    {
        private readonly IServiceProvider _serviceProvider;

        public AppShell(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            // Register routing for MainPage
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        }
    }
}
