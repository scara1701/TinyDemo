namespace TinyDemo.MauiClient.Controls;

public partial class NumberControl : ContentView
{

    public static readonly BindableProperty NumberProperty =
    BindableProperty.Create(nameof(Number), typeof(int), typeof(NumberControl), default(int));

    public int Number
    {
        get => (int)GetValue(NumberProperty);
        set => SetValue(NumberProperty, value);
    }


    public NumberControl()
	{
		InitializeComponent();
	}
}