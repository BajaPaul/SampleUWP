using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Printing;
using System;
using System.Collections.Generic;
using System.Numerics;
using Windows.Foundation;
using Windows.Graphics.Printing;
using Windows.Graphics.Printing.OptionDetails;
using Windows.UI.Xaml;

namespace SampleUWP.BeamDesign
{
    /// <summary>
    /// Group of methods and variables to print single page from Win2D Canvas declared on each page.
    /// </summary>
    internal class W2DPrint     // Shorthand for Win2D Printing.
    {
        // A pointer to MainPage is needed if you want to call methods or variables in MainPage.
        // private MainPage mainPage = MainPage.mainPagePointer;

        // For info on Action delegate see C# Player's Guide P.205.
        // More about delegates at: https://msdn.microsoft.com/en-us/library/ms173172.aspx
        // Create delegate to pass PageDrawer methods to printing methods herein using generic Drawer PrintDrawer.

        /// <summary>
        /// Print preview requires access to the method that does the drawing.  Set PrintDrawer = PageDrawer 
        /// on each page to share Drawer and enable printing.
        /// </summary>
        public static Action<CanvasDrawingSession, Vector2, Vector2> PrintDrawer;

        /// <summary>
        /// This is text that will show in the title bar of the print dialog.
        /// </summary>
        public static string printTitle;

        /// <summary>
        /// Object that enables printing of Win2D content.  Passes CanvasPrintDocument to PrintTaskSourceRequestedArg.SetSource
        /// </summary>
        public static CanvasPrintDocument printDocument;
        
        public static async void ButtonPrintOptionSelected(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            BDLib.ColorsPrint();     // Draw all items using print color (Black).  Only applies to colors defined in Blib.
            if (printTitle == null)
                printTitle = "Win2D Printer";           // Set default if nothing else was set.
            // Dispose existing CanvasPrintDocument
            if (printDocument != null)
            {
                printDocument.Dispose();
            }
            printDocument = new CanvasPrintDocument();  // Create new instant of CanvasPrintDocument.
            printDocument.PrintTaskOptionsChanged += PrintDocument_PrintTaskOptionsChanged;     // Hook this event to be notified when print options change.
            printDocument.Preview += PrintDocument_Preview;                                     // Hook this event to draw preview in print dialog.
            printDocument.Print += PrintDocument_Print;                                         // Hook this event to print the document.
            PrintManager printManager = PrintManager.GetForCurrentView();   // When app registers "intention" to print, it creates a print contract with the Print Manager.
            printManager.PrintTaskRequested += OnPrintTaskRequested;        // Hook this event to setup print job when a request to print has occured.
            try
            {
                await PrintManager.ShowPrintUIAsync();                      // Show print dialog and wait until it completes.
            }
            finally
            {
                printManager.PrintTaskRequested -= OnPrintTaskRequested;    // Unhook event since print task complete to allow other apps to print.
            }
        }
                
        /// <summary>
        /// Event to be notified when print options change.  Stripped all the meat out of this.  See Win2D Print Example for 
        /// much more detail.  This event handles paper size changes and initiate new formating and determines new page count.  
        /// Works fine as is since only dealing with single page that does not change.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private static void PrintDocument_PrintTaskOptionsChanged(CanvasPrintDocument sender, CanvasPrintTaskOptionsChangedEventArgs args)
        {
            uint pageCount = 1;
            sender.SetPageCount(pageCount);   // Set final page count as used by the print preview dialog.
            args.NewPreviewPageNumber = pageCount;  // Set page that will display in print preview.
        }

        /// <summary>
        /// Event to draw preview in print dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private static void PrintDocument_Preview(CanvasPrintDocument sender, CanvasPreviewEventArgs args)
        {
            Rect imageableRect = args.PrintTaskOptions.GetPageDescription(1).ImageableRect;
            Vector2 leftTop = new Vector2((float)imageableRect.X, (float)imageableRect.Y);
            Vector2 rightBot = new Vector2(leftTop.X + (float)imageableRect.Width, leftTop.Y + (float)imageableRect.Height);
            PrintDrawer?.Invoke(args.DrawingSession, leftTop, rightBot);    // Invoke the delegate PrintDrawer.
        }

        /// <summary>
        /// Event to print the document in print dialog when Print button is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private static void PrintDocument_Print(CanvasPrintDocument sender, CanvasPrintEventArgs args)
        {
            using (CanvasDrawingSession ds = args.CreateDrawingSession())   // Using block wraps drawing session and implements IDisposable garbage collection.
            {
                Rect imageableRect = args.PrintTaskOptions.GetPageDescription(1).ImageableRect;
                Vector2 leftTop = new Vector2((float)imageableRect.X, (float)imageableRect.Y);
                Vector2 rightBot = new Vector2(leftTop.X + (float)imageableRect.Width, leftTop.Y + (float)imageableRect.Height);
                PrintDrawer?.Invoke(ds, leftTop, rightBot);    // Invoke the delegate PrintDrawer.
            }
        }

        /// <summary>
        /// Event to setup print job when a request to print has occured.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private static void OnPrintTaskRequested(PrintManager sender, PrintTaskRequestedEventArgs args)
        {
            // String below shows on title bar of print dialog window.
            PrintTask printTask = args.Request.CreatePrintTask( printTitle, (createPrintTaskArgs) =>
            {
                createPrintTaskArgs.SetSource(printDocument);
            });

            // PrintTaskOptions class: https://msdn.microsoft.com/en-us/library/windows/apps/windows.graphics.printing.printtaskoptions.aspx

            // PrintTaskOptionDetails.GetFromPrintTaskOptions.getFromPrintTaskOptions method:
            // https://msdn.microsoft.com/en-us/library/windows/apps/windows.graphics.printing.optiondetails.printtaskoptiondetails.getfromprinttaskoptions.aspx
            PrintTaskOptionDetails detailedOptions = PrintTaskOptionDetails.GetFromPrintTaskOptions(printTask.Options);

            // More about PrintCustomItemListOptionDetails:
            // https://msdn.microsoft.com/en-us/library/windows/apps/windows.graphics.printing.optiondetails.printcustomitemlistoptiondetails.aspx
            //PrintCustomItemListOptionDetails pageRange = detailedOptions.CreateItemListOption("PageRange", "Page Range");
            //pageRange.AddItem("PrintAll", "Print All");
            //pageRange.AddItem("PrintFirstPage", "Print Only First Page");
            IList<string> displayedOptions = printTask.Options.DisplayedOptions;
            displayedOptions.Clear();
            
            displayedOptions.Add(StandardPrintTaskOptions.Orientation);
            displayedOptions.Add(StandardPrintTaskOptions.MediaSize);
            //displayedOptions.Add(StandardPrintTaskOptions.ColorMode);
            displayedOptions.Add(StandardPrintTaskOptions.Copies);
            //displayedOptions.Add("PageRange");

            detailedOptions.OptionChanged += PrintDetailedOptions_OptionChanged;    // Hook event that is raised when any advanced print task options change.
        }

        /// <summary>
        /// Event that is raised when any advanced print task options change.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private static void PrintDetailedOptions_OptionChanged(PrintTaskOptionDetails sender, PrintTaskOptionChangedEventArgs args)
        {
            // More on various print options: https://msdn.microsoft.com/en-us/library/windows/apps/windows.graphics.printing.optiondetails.aspx
            if (args.OptionId == null)              // Gets ID of print task option that changed.
            {
                printDocument.InvalidatePreview();  // Redraw the preview when any print task options change.
            }
        }

    }
}
 