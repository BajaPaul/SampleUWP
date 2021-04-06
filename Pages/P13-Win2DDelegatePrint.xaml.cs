using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using SampleUWP.BeamDesign;
using System;
using System.Numerics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// Main link:  http://microsoft.github.io/Win2D/html/T_Microsoft_Graphics_Canvas_Printing_CanvasPrintDocument.htm

// Windows.Graphics.Printing namespace:
// https://msdn.microsoft.com/en-us/library/windows/apps/windows.graphics.printing.aspx

namespace SampleUWP
{
    public sealed partial class P13 : Page
    {
        // A pointer to MainPage is needed if you want to call methods or variables in MainPage.
        private readonly MainPage mainPage = MainPage.mainPagePointer;
        
        public P13()
        {
            this.InitializeComponent();
            W2DPrint.PrintDrawer = PageDrawer;              // Set print delegate to PageDrawer method on this page
            W2DPrint.printTitle = "Page P13 Printer";       // Set the title bar text in the print dialog.
        }
        
        /// <summary>
        /// Use to point to drawing session vs. hosting drawing session directly.  Printing will use same PageDrawer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void Canvas_Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            BDLib.ColorsDisplay();       // Draw all items using display color.  Only applies to colors defined in Blib.
            Vector2 leftTop = new Vector2(0, 0);        // on display use 0,0 as leftTop.
            Vector2 rightBot = sender.Size.ToVector2();
            // Can use W2DPrint.PrintDrawer delegate or call PageDrawing method direct.
            // W2DPrint.PrintDrawer(args.DrawingSession, leftTop, rightBot);
            PageDrawer(args.DrawingSession, leftTop, rightBot);
        }
        
        /// <summary>
        /// Use same Drawer regardless of display or printing.  With printing added, can not assume leftTop is 0,0 
        /// since need to offset for required printer margins when printing.  If using colors defined in LibBeamItems, then 
        /// display colors can be toggled from color to black.
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="leftTop"></param>
        /// <param name="rightBot"></param>
        private void PageDrawer(CanvasDrawingSession ds, Vector2 leftTop, Vector2 rightBot)
        {
            float width = rightBot.X - leftTop.X;
            float height = rightBot.Y - leftTop.Y;
            float lineThickness = 4;
            float halfthick = lineThickness / 2;

            Vector2 ltOffset = new Vector2(leftTop.X + halfthick, leftTop.Y + halfthick);
            Vector2 rbOffset = new Vector2(rightBot.X - halfthick, rightBot.Y - halfthick);

            Vector2 midpoint = new Vector2(ltOffset.X + (rbOffset.X - ltOffset.X) / 2, ltOffset.Y + (rbOffset.Y - ltOffset.Y) / 2);
            float radius = (float)Math.Min((width / 2), (height / 2));    // Get smaller of width or height for radius.
            Vector2 textPoint = new Vector2(midpoint.X, midpoint.Y - radius / 2);

            ds.DrawLine(leftTop, rightBot, BDLib.colorBeam, lineThickness);
            ds.DrawLine(leftTop.X, rightBot.Y, rightBot.X, leftTop.Y, BDLib.colorShear, lineThickness);
            ds.DrawRectangle(ltOffset.X, ltOffset.Y, width - lineThickness, height - lineThickness, BDLib.colorMoment, lineThickness);
            ds.DrawCircle(midpoint, radius - lineThickness * 1.5f, BDLib.colorVector, lineThickness);
            ds.DrawText("Top of Page", textPoint, BDLib.colorText, BDLib.textCB);
        }

        /// <summary>
        /// Forward print request to W2DPrint to do the printing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonPrint_Click(object sender, RoutedEventArgs e)
        {
            W2DPrint.ButtonPrintOptionSelected(sender, e);
        }

        /// <summary>
        /// Clean up all Win2D items when leaving page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            if (W2DPrint.printTitle != null)         // Clear the text in the print dialog title bar.
                W2DPrint.printTitle = null;
            if (W2DPrint.printDocument != null)
            {
                W2DPrint.printDocument.Dispose();    // Get rid of printDocument to complete print garbage collection.
                W2DPrint.printDocument = null;
            }
            canvas.RemoveFromVisualTree();      // Explicitly remove all canvas references to allow Win2D to complete garbage collection.
            canvas = null;
        }

    }
}

