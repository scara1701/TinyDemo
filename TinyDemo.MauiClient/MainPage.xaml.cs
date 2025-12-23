using TinyDemo.MVVM;

namespace TinyDemo.MauiClient
{
    public partial class MainPage : ContentPage
    {
        private readonly MainViewModel _viewModel;

        public MainPage(MainViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
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
