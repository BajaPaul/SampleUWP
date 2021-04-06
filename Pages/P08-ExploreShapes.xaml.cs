using Windows.UI.Xaml.Controls;

namespace SampleUWP
{
    public sealed partial class P08 : Page
    {
        // A pointer to MainPage is needed if you want to call methods or variables in MainPage.
        private readonly MainPage mainPage = MainPage.mainPagePointer;

        public P08()
        {
            this.InitializeComponent();

            // Setup mainScroller to handle scrolling for this page.
            mainPage.MainScrollerOn(horz: ScrollMode.Disabled, vert: ScrollMode.Auto, vertVis: ScrollBarVisibility.Auto);

            //mainPage.HideDebugBar();
            //mainPage.dM1 = "Testing debug message";
            //mainPage.ShowDebugBar();

            // Cache page results.  Comment out following line if you don't want that to happen
            // https://msdn.microsoft.com/en-us/library/windows/apps/windows.ui.xaml.controls.frame.aspx?f=255&MSPPError=-2147217396
            //this.NavigationCacheMode = NavigationCacheMode.Enabled;
        }
        
    }
}
