using SampleUWP.ConsoleCodeSamples;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SampleUWP
{
    public sealed partial class P16 : Page
    {
        // A pointer to MainPage is needed if you want to call methods or variables in MainPage.
        private readonly MainPage mainPage = MainPage.mainPagePointer;

        public P16()
        {
            InitializeComponent();

            // Cache page results.  Comment out following line if you don't want that to happen
            // https://msdn.microsoft.com/en-us/library/windows/apps/windows.ui.xaml.controls.frame.aspx?f=255&MSPPError=-2147217396

            //this.NavigationCacheMode = NavigationCacheMode.Enabled;

            // Setup mainScroller to handle scrolling for this page.  Only needed to run below samples.
            mainPage.MainScrollerOn(horz: ScrollMode.Auto, vert: ScrollMode.Auto, horzVis: ScrollBarVisibility.Auto, vertVis: ScrollBarVisibility.Auto, zoom: ZoomMode.Disabled);
        }

        /// <summary>
        /// Invoked when page loaded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            tblockOutput.Text = "\nConsole is Empty\n";
            button01.Focus(FocusState.Programmatic);      // Set focus on first button.
        }

        private void Button01_Click(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            tblockOutput.Text = string.Empty;
            Samples.Sample01_List_Methods(tblockOutput);
        }

        private void Button02_Click(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            tblockOutput.Text = string.Empty;
            Samples.Sample02_List_Methods(tblockOutput);
        }

        private void Button03_Click(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            tblockOutput.Text = string.Empty;
            Samples.Sample03_TryParse_Methods(tblockOutput);
        }

        private void Button04_Click(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            tblockOutput.Text = string.Empty;
            Samples.Sample04_Numeric_Format_Strings(tblockOutput);
        }

        private void Button05_Click(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            tblockOutput.Text = string.Empty;
            Samples.Sample05_DoubleConverter(tblockOutput);
        }

        private void Button06_Click(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            tblockOutput.Text = string.Empty;
            Samples.Sample06_Use_UnitConversions(tblockOutput);
        }

        private void Button07_Click(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            tblockOutput.Text = string.Empty;
            Samples.Sample07_Fractions_Of_Various_Numbers(tblockOutput);
        }

        private void Button08_Click(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            tblockOutput.Text = string.Empty;
            Samples.Sample08_Test_Truncate_Methods(tblockOutput);
        }

        private void Button09_Click(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            tblockOutput.Text = string.Empty;
            Samples.Sample09_Enum_Code_Sampler(tblockOutput);
        }

        private void Button10_Click(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            tblockOutput.Text = string.Empty;
            tblockOutput.Text = "\nNot Used\n";
        }
    }
}
