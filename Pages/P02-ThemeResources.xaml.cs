using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

/*
 * NOTE: This code behind is shared between default XAML page and Mobile XAML page.  If you want to provide different resources per 
 * device-family (strings, images, XAML files, HTML pages, etc.) you don't need to detect the device-family in code.  Instead you can 
 * use an MRT qualifier DeviceFamily (such as Logo.DeviceFamily-Mobile.png).  Just make sure you always have a fallback resource 
 * (image, string, etc) for use when the app is running on a device family you've never heard of before.
 */

namespace SampleUWP
{
    public sealed partial class P02 : Page
    {
        // A pointer to MainPage is needed if you want to call methods or variables in MainPage
        private readonly MainPage mainPage = MainPage.mainPagePointer;

        // Initialize buttonWasClicked colors
        private readonly SolidColorBrush redColor = new SolidColorBrush(Colors.Red);
        private readonly SolidColorBrush greenColor = new SolidColorBrush(Colors.Green);

        // This is way to get SystemAccentColor via C#.  This is equivalent to the system theme resource SystemControlHighlightAccentBrush.
        // Comment out since not used.
        //private readonly SolidColorBrush usersSystemAccentColor = new SolidColorBrush((Color)Application.Current.Resources["SystemAccentColor"]);

        public P02()
        {
            this.InitializeComponent();

            // Setup mainScroller to handle scrolling for this page.
            mainPage.MainScrollerOn(horz: ScrollMode.Disabled, vert: ScrollMode.Auto, horzVis: ScrollBarVisibility.Disabled, 
                                    vertVis: ScrollBarVisibility.Auto, zoom: ZoomMode.Disabled);
            
            //this.NavigationCacheMode = NavigationCacheMode.Enabled;     // Cache page state
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

        /// <summary>
        /// On various button clicks, toggle color of the buttonWasClicked TextBlock foreground color between Green and Red.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            if (buttonWasClickedTextBlock.Foreground == redColor)
            {
                buttonWasClickedGrid.BorderBrush = greenColor;
                buttonWasClickedTextBlock.Foreground = greenColor;
            }
            else
            {
                buttonWasClickedGrid.BorderBrush = redColor;
                buttonWasClickedTextBlock.Foreground = redColor;
            }
        }

    }
}
