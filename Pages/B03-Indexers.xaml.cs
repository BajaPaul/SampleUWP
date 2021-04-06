using SampleUWP.Common;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SampleUWP
{
    public sealed partial class B03 : Page
    {
        public B03()
        {
            this.InitializeComponent();

            //mainPage.HideDebugBar();
            //mainPage.dM1 = "Testing debug message";
            //mainPage.ShowDebugBar();

            Reset_Click(null, null);   // Can call with nulls since no values in method are looking for sender or e.
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            DataContext = new Teams();      // Required to make the binding work on XAML page.
        }
        
    }
}
