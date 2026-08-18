using Avalonia;
using Avalonia.Controls;

namespace TinyDemo.AvaloniaClient.Controls;

public partial class NumberControl : UserControl
{
    // 1. Registreer een Avalonia StyledProperty in plaats van een DependencyProperty
    public static readonly StyledProperty<int> NumberProperty =
        AvaloniaProperty.Register<NumberControl, int>(nameof(Number), defaultValue: 0);

    
    // 2. De C# property wrapper gebruikt GetValue en SetValue net als in WPF
    public int Number
    {
        get => GetValue(NumberProperty);
        set => SetValue(NumberProperty, value);
    }
    
    public NumberControl()
    {
        InitializeComponent();
    }
}