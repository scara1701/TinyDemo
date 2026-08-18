using Avalonia.Controls;
using TinyDemo.MVVM;

namespace TinyDemo.AvaloniaClient;

public partial class MainWindow : Window
{
    private readonly MainViewModel _viewModel;

    public MainWindow(MainViewModel viewModel)
    {
        _viewModel = viewModel;
        DataContext = viewModel;
        InitializeComponent();
    }
}