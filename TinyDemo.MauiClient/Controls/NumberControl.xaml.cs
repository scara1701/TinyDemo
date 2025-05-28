
using System.ComponentModel;

namespace TinyDemo.MauiClient.Controls;

public partial class NumberControl : ContentView
{
    //https://learn.microsoft.com/en-us/dotnet/maui/user-interface/controls/contentview?view=net-maui-9.0
    public static readonly BindableProperty NumberProperty =
    BindableProperty.Create(nameof(Number), typeof(int), typeof(NumberControl), 0, propertyChanged: OnNumberChanged);

    private static void OnNumberChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (NumberControl)bindable;
    }

    public int Number
    {
        get => (int)GetValue(NumberProperty);
        set {
            SetValue(NumberProperty, value);
            OnPropertyChanged(nameof(Number));
        }
    }

    public NumberControl()
	{
		InitializeComponent();
    }
}