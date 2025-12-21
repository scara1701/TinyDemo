using TinyDemo.MVVM;

namespace TinyDemo.MauiClient
{
    public partial class MainPage : ContentPage
    {
        private readonly MainViewModel _viewModel;
        
        //Injecting viewmodel in cosntructor causes exception???
        //public MainPage(MainViewModel viewModel)
        public MainPage()
        {
            InitializeComponent();
            _viewModel = App.ServiceProvider.GetRequiredService<MainViewModel>();
            BindingContext = _viewModel;

        }
        
        protected override void OnDisappearing()
        {
            // Dispose the view model when the page is no longer visible
            if (_viewModel is IDisposable disposable)
            {
                disposable.Dispose();
            }
            base.OnDisappearing();
        }
    }
}
