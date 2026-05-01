using Microsoft.UI.Xaml;
using TinyDemo.MVVM;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TinyDemo.WinUIClient
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 

    public sealed partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel;
        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            //datacontext needs to be set on root control, Window class does not have a datacontext
            RootGrid.DataContext = _viewModel;
        }
    }
}
