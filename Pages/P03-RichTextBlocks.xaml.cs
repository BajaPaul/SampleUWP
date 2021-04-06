using Windows.UI.Xaml.Controls;

namespace SampleUWP
{
    public sealed partial class P03 : Page
    {
        // A pointer to MainPage is needed if you want to call methods or variables in MainPage
        private readonly MainPage mainPage = MainPage.mainPagePointer;

        public P03()
        {
            InitializeComponent();

            // Setup mainScroller to handle scrolling for this page.
            mainPage.MainScrollerOn(horz: ScrollMode.Auto, vert: ScrollMode.Auto, horzVis: ScrollBarVisibility.Auto, vertVis: ScrollBarVisibility.Auto, zoom: ZoomMode.Enabled);
            
            //this.NavigationCacheMode = NavigationCacheMode.Enabled;     // Cache page state
        }

        // See more about RichTextBlocks: https://msdn.microsoft.com/en-us/library/windows/apps/windows.ui.xaml.controls.richtextblock.aspx
        // Is this the right control? https://msdn.microsoft.com/en-us/windows/uwp/controls-and-patterns/rich-text-block

        /*

        // Create a RichTextBlock, a Paragraph and a Run.
        RichTextBlock richTextBlock = new RichTextBlock();
        Paragraph paragraph = new Paragraph();
        Run run = new Run();

        // Customize some properties on the RichTextBlock.
        richTextBlock.IsTextSelectionEnabled = true;
        richTextBlock.TextWrapping = TextWrapping.Wrap;
        run.Text = "This is some sample text to show the wrapping behavior.";
        richTextBlock.Width = 200;

        // Add the Run to the Paragraph, the Paragraph to the RichTextBlock.
        paragraph.Inlines.Add(run);
        richTextBlock.Blocks.Add(paragraph);

        // Add the RichTextBlock to the visual tree (assumes stackPanel is decalred in XAML).
        stackPanel.Children.Add(richTextBlock);

        RichTextBlock richTextBlock = new RichTextBlock();
        richTextBlock.IsTextSelectionEnabled = true;
        richTextBlock.SelectionHighlightColor = new SolidColorBrush(Windows.UI.Colors.Pink);
        richTextBlock.FontFamily = new FontFamily("Arial");

        Paragraph paragraph = new Paragraph();
        Run run = new Run();
        run.Foreground = new SolidColorBrush(Windows.UI.Colors.Blue);
        run.FontWeight = Windows.UI.Text.FontWeights.Light;
        run.Text = "This is some";

        Span span = new Span();
        span.FontWeight = Windows.UI.Text.FontWeights.SemiBold;

        Run run1 = new Run();
        run1.FontStyle = Windows.UI.Text.FontStyle.Italic;
        run1.Text = " sample text to ";

        Run run2 = new Run();
        run2.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
        run2.Text = " demonstrate some properties";

        span.Inlines.Add(run1);
        span.Inlines.Add(run2);
        paragraph.Inlines.Add(run);
        paragraph.Inlines.Add(span);
        richTextBlock.Blocks.Add(paragraph);

        // Add the RichTextBlock to the visual tree (assumes stackPanel is decalred in XAML).
        stackPanel.Children.Add(richTextBlock);

        */
    }
}
