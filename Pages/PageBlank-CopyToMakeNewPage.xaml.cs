using Windows.UI.Xaml.Controls;

namespace SampleUWP
{
    public sealed partial class PageBlank : Page
    {
        // A pointer to MainPage is needed if you want to call methods or variables in MainPage.
        // private MainPage mainPage = MainPage.mainPagePointer;

        public PageBlank()
        {
            InitializeComponent();
            
            // Cache page results.  Comment out following line if you don't want that to happen.
            // https://msdn.microsoft.com/en-us/library/windows/apps/windows.ui.xaml.controls.frame.aspx?f=255&MSPPError=-2147217396

            //this.NavigationCacheMode = NavigationCacheMode.Enabled;     // Cache page state.

            /*
            // Setup mainScroller to handle scrolling for this page.
            mainPage.MainScrollerOn(horz: ScrollMode.Disabled, vert: ScrollMode.Auto, horzVis: ScrollBarVisibility.Disabled,
                                    vertVis: ScrollBarVisibility.Auto, zoom: ZoomMode.Disabled);
            */
        }
    }
}
