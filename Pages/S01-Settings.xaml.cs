using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SampleUWP
{
    public sealed partial class S01 : Page
    {
        // A pointer to MainPage is needed if you want to call methods or variables in MainPage.
        private readonly MainPage mainPage = MainPage.mainPagePointer;

        public S01()
        {
            InitializeComponent();

            // Setup mainScroller to handle scrolling for this page.
            mainPage.MainScrollerOn(horz: ScrollMode.Disabled, vert: ScrollMode.Auto, vertVis: ScrollBarVisibility.Auto);
        }

        /// <summary>
        /// Sets focus on s01TextBox.  Works well.  Load app without VS running if having issues.  Debuger seems to get
        /// in way of setting focus properly on app start.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            s01TextBox.Focus(FocusState.Programmatic);
        }

    }
}
