using TinyDemo.MVVM;

namespace TinyDemo.MauiClient
{
    public partial class MainPage : ContentPage
    {
        //Injecting viewmodel in cosntructor causes exception???
        //public MainPage(MainViewModel viewModel)
        public MainPage()
        {
            InitializeComponent();
            var viewModel = App.ServiceProvider.GetRequiredService<MainViewModel>();
            BindingContext = viewModel;

        }
    }
}
