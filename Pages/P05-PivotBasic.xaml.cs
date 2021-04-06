using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SampleUWP
{
    public sealed partial class P05 : Page
    {   
        /*
         * 
         * Intent is to leave this page basic.  Many improvements could be made in this code behind!!!!
         * 
         */

        public P05()
        {
            InitializeComponent();
            
            // Basic Pivot not most efficient way since all pages are loaded at once.  
            // But they do have Reset buttons so behavior somewhat OK.
            b01Frame.Navigate(typeof(B01));
            b02Frame.Navigate(typeof(B02));
            b03Frame.Navigate(typeof(B03));
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            if (pagePivot.SelectedIndex > 0)
            {
                // If not at the first item, go back to the previous one.
                pagePivot.SelectedIndex -= 1;
            }
            else
            {
                // The first PivotItem is selected, so loop around to the last item.
                pagePivot.SelectedIndex = pagePivot.Items.Count - 1;
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            if (pagePivot.SelectedIndex < pagePivot.Items.Count - 1)
            {
                // If not at the last item, go to the next one.
                pagePivot.SelectedIndex += 1;
            }
            else
            {
                // The last PivotItem is selected, so loop around to the first item.
                pagePivot.SelectedIndex = 0;
            }
        }

    }
}
