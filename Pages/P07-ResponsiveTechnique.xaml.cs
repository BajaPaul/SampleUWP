using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SampleUWP
{
    public sealed partial class P07 : Page
    {   
        public P07()
        {
            InitializeComponent();
            
            //mainPage.HideDebugBar();
            //mainPage.dM1 = "Testing debug message";
            //mainPage.ShowDebugBar();

            // Cache page results.  Comment out following line if you don't want that to happen
            // https://msdn.microsoft.com/en-us/library/windows/apps/windows.ui.xaml.controls.frame.aspx?f=255&MSPPError=-2147217396
            // this.NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        private void SplitViewButton_Click(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        /// <summary>
        /// Use this event to launch websites in separate thread.  Note: NavigateUri versus Tag often seems to cause duplicate pages spawning.
        /// Sample XAML Code that triggers this event is on next line:
        /// <HyperlinkButton Content = "Website Title" Tag="http://websiteURL.com" Style="{ThemeResource TextBlockButtonStyle}" Click="Website_Click"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Website_Click(object sender, RoutedEventArgs e)
        {
            _ = e;          // Discard unused parameter.
            await Windows.System.Launcher.LaunchUriAsync(new Uri(((HyperlinkButton)sender).Tag.ToString()));
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            ContentDialog d = new ContentDialog
            {
                Title = "Not implemented",
                Content = "The buttons are for illustrative purposes only and do not perform any action",
                PrimaryButtonText = "OK"
            };

            await d.ShowAsync();            // FYI, this can only be used in methods declared async
            /* See more about "async" and "await" at:
             * https://msdn.microsoft.com/en-us/library/hh156513.aspx
             * https://msdn.microsoft.com/en-us/library/hh191443.aspx
            */
        }

    }
}
