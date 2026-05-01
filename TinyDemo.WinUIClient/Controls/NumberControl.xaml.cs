using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TinyDemo.WinUIClient.Controls
{
    public sealed partial class NumberControl : UserControl
    {
        static readonly DependencyProperty NumberProperty = DependencyProperty.Register("Number", typeof(int), typeof(NumberControl), new PropertyMetadata(0));

        public NumberControl()
        {
            InitializeComponent();
        }

        public int Number
        {
            get { return (int)GetValue(NumberProperty); }
            set { SetValue(NumberProperty, value); }
        }
    }
}
