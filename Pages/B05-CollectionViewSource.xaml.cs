using SampleUWP.Common;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System;
using System.Linq;

namespace SampleUWP
{
    public sealed partial class B05 : Page
    {
        public B05()
        {
            this.InitializeComponent();

            //mainPage.HideDebugBar();
            //mainPage.dM1 = "Testing debug message";
            //mainPage.ShowDebugBar();

            Reset_Click(null, null);
        }

        /*
        What follows is a LINQ Query Expression.  See much more at following links:
        https://msdn.microsoft.com/en-us/library/bb397676.aspx
        https://msdn.microsoft.com/en-us/library/bb310804.aspx 
        https://code.msdn.microsoft.com/101-LINQ-Samples-3fb9811b  

        More about grouping items after you queried them.
        https://msdn.microsoft.com/en-us/library/windows/apps/xaml/hh780627.aspx
        https://msdn.microsoft.com/en-us/library/windows/apps/xaml/hh780650.aspx
        */

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            Teams teams = new Teams();
            var result = from t in teams
                         group t by t.City into g
                         orderby g.Key
                         select new { g.Key, Items = g }; ;
            
            groupInfoCVS.Source = result;   // Store the results here to be loaded by binding process
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
        

    }
}
