using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.Geometry;
using Microsoft.Graphics.Canvas.Text;
using Microsoft.Graphics.Canvas.UI.Xaml;
using SampleUWP.BeamDesign;
using System;
using System.Numerics;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SampleUWP
{
    public sealed partial class P11 : Page
    {
        // User accent color for use in this page.
        private Color accentColor;

        private int selection = 1;              // Drawing session selection, show drawing session Drawing01() on page load.
        private readonly Random random = new Random();   // Used for QuickStart sample, Drawing01().

        // CanvasControl Class:  http://microsoft.github.io/Win2D/html/T_Microsoft_Graphics_Canvas_UI_Xaml_CanvasControl.htm

        public P11()
        {
            InitializeComponent();

            // TODO: Following color may have changed since using acrylic background now!
            //accentColor = mainPage.usersSystemAccentColor;      // Get user accent color for this page use.

            // Alternative to get rid of error.
            accentColor = Colors.Blue;      // Get user accent color for this page use.
        }

        /// <summary>
        /// canvas.Invalidate(); statements below sends notice that CanvasControl needs to redraw.  This triggers a Draw event 
        /// which calls this Canvas_Draw method.  This Canvas_Draw is declared in XAML canvas as method to invoke on Draw event.  
        /// Note that Draw events are automatically triggered any time the canvas size changes (which typically happen when 
        /// the page size changes).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void Canvas_Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            switch (selection)
            {
                case 1:
                    Drawing01(sender, args.DrawingSession);
                    break;
                case 2:
                    Drawing02(sender, args.DrawingSession);
                    break;
                case 3:
                    Drawing03(sender, args.DrawingSession);
                    break;
                case 4:
                    Drawing04(sender, args.DrawingSession);
                    break;
                case 5:
                    Drawing05(sender, args.DrawingSession);
                    break;
                case 6:
                    Drawing06(sender, args.DrawingSession);
                    break;
                default:    // Throw exception so error can be discovered and corrected.
                    throw new NotSupportedException("Invalid switch (selection).");
            }
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
            button01.Focus(FocusState.Programmatic);      // Set focus on first button.
        }

        /// <summary>
        /// Invoked when page is unloaded.  Win2D and underlying DirectX needs to be garbaged collected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            canvas.RemoveFromVisualTree();      // Explicitly remove all references to allow Win2D to complete garbage collection.
            canvas = null;
        }

        /// <summary>
        /// Used for QuickStart sample, Drawing01().
        /// </summary>
        /// <param name="width">Width of area to generate random X's.</param>
        /// <param name="height">Height of area to generate random Y's.</param>
        /// <returns>Random Point</returns>
        private Vector2 RandomVector2(float width, float height)
        {
            double x = random.NextDouble() * width;
            double y = random.NextDouble() * height;
            return new Vector2((float)x, (float)y);
        }

        /// <summary>
        /// Used for QuickStart sample, Drawing01().
        /// </summary>
        /// <param name="maxValue"></param>
        /// <returns>Float less than maxValue</returns>
        private float RandomFloat(float maxValue)
        {
            return (float)random.NextDouble() * maxValue;
        }

        /// <summary>
        /// Used for QuickStart sample, Drawing01().
        /// </summary>
        /// <param name="maxValue"></param>
        /// <returns>Byte less than maxValue</returns>
        private byte RandomByte(int maxValue)
        {
            return (byte)random.Next(maxValue);
        }

        /// <summary>
        /// Drawing01 drawing session.  QuickStart1 Sample: http://microsoft.github.io/Win2D/html/QuickStart.htm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ds"></param>
        private void Drawing01(CanvasControl sender, CanvasDrawingSession ds)
        {
            float width = (float)sender.ActualWidth;
            float height = (float)sender.ActualHeight;
            ds.DrawRectangle(0, 0, width, height, Colors.Yellow, BDLib.pen2);    // Draw border
            for (int i = 0; i < 50; i++)
            {
                ds.DrawText("Hello, World!", RandomVector2(width, height), Color.FromArgb(255, RandomByte(256), RandomByte(256), RandomByte(256)), BDLib.textCC);
                ds.DrawCircle(RandomVector2(width, height), RandomFloat(160), Color.FromArgb(255, RandomByte(256), RandomByte(256), RandomByte(256)));
                ds.DrawLine(RandomVector2(width, height), RandomVector2(width, height), Color.FromArgb(255, RandomByte(256), RandomByte(256), RandomByte(256)));
            }
        }

        /// <summary>
        /// Drawing02 drawing session.  StrokeStyle.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ds"></param>
        private void Drawing02(CanvasControl sender, CanvasDrawingSession ds)
        {
            float width = (float)sender.ActualWidth;
            float height = (float)sender.ActualHeight;
            float xMid = width / 2;
            float yMid = height / 2;
            float radius = (float)Math.Min((width * 0.5f), (height * 0.5f));    // Get smaller of width or height for radius.
            Vector2 topL = new Vector2(0, 0);
            Vector2 topR = new Vector2(width, 0);
            Vector2 botL = new Vector2(0, height);
            Vector2 botR = new Vector2(width, height);
            Vector2 midpoint = new Vector2(xMid, yMid);
            /*  
             *  More on CanvasStrokeStyle Class at:
             *   http://microsoft.github.io/Win2D/html/T_Microsoft_Graphics_Canvas_Geometry_CanvasStrokeStyle.htm
             *   http://microsoft.github.io/Win2D/html/T_Microsoft_Graphics_Canvas_Geometry_CanvasDashStyle.htm
             *   http://microsoft.github.io/Win2D/html/T_Microsoft_Graphics_Canvas_Geometry_CanvasCapStyle.htm
             *   http://microsoft.github.io/Win2D/html/T_Microsoft_Graphics_Canvas_Geometry_CanvasLineJoin.htm
            */
            float[] lDash = { 4, 4 };
            float[] lDot = { 0, 4 };                // Zero describes a dot.
            float[] lDashDot = { 4, 4, 0, 4 };
            float[] lDashDotDot = { 4, 4, 0, 4, 0, 4 };
            CanvasCapStyle CapStyle0 = CanvasCapStyle.Flat;
            CanvasCapStyle CapStyle1 = CanvasCapStyle.Square;
            CanvasCapStyle CapStyle2 = CanvasCapStyle.Round;
            CanvasCapStyle CapStyle3 = CanvasCapStyle.Triangle;
            CanvasStrokeStyle strokeStyle0 = new CanvasStrokeStyle
            {
                CustomDashStyle = lDash,
                StartCap = CapStyle0,
                EndCap = CapStyle0,
                DashCap = CapStyle0
            };
            strokeStyle0.CustomDashStyle = lDash;
            ds.DrawCircle(midpoint, 300, Colors.Cyan, 6, strokeStyle0);
            CanvasStrokeStyle strokeStyle1 = new CanvasStrokeStyle
            {
                StartCap = CapStyle1,
                EndCap = CapStyle1,
                DashCap = CapStyle1,
                CustomDashStyle = lDot
            };
            ds.DrawEllipse(midpoint, 500, 300, Colors.Red, 6, strokeStyle1);
            CanvasStrokeStyle strokeStyle2 = new CanvasStrokeStyle
            {
                StartCap = CapStyle2,
                EndCap = CapStyle2,
                DashCap = CapStyle2,
                CustomDashStyle = lDashDot
            };
            ds.DrawEllipse(midpoint, 250, 400, Colors.Green, 6, strokeStyle2);
            CanvasStrokeStyle strokeStyle3 = new CanvasStrokeStyle
            {
                StartCap = CapStyle3,
                EndCap = CapStyle3,
                DashCap = CapStyle3,
                CustomDashStyle = lDashDotDot
            };
            ds.DrawEllipse(midpoint, 600, 200, Colors.Blue, 6, strokeStyle3);
            // Draw border rectangle with diag lines and circle.  Note that Rectangles have no override that uses Vector2's.
            // Confirm behavior of Blib StrokeStyles here.
            ds.DrawRectangle(0, 0, width, height, accentColor, 6, BDLib.dash);
            ds.DrawLine(topL, botR, accentColor, 6, BDLib.dashDot);
            ds.DrawLine(botL, topR, accentColor, 6, BDLib.dashDotDot);
            ds.DrawCircle(xMid, yMid, radius, accentColor, 6, BDLib.dot);
        }

        /// <summary>
        /// Drawing03 drawing session.  Geometry.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ds"></param>
        private void Drawing03(CanvasControl sender, CanvasDrawingSession ds)
        {
            float width = (float)sender.ActualWidth;
            float height = (float)sender.ActualHeight;
            float wDivide = width / 4;
            float hDivide = height / 6;

            // Set up Vector for CanvasGeometry below.
            _ = new Vector2(0, 0);
            _ = new Vector2(25, 0);
            _ = new Vector2(0, 25);
            _ = new Vector2(25, 25);
            _ = new Vector2(0, 0);
            Vector2 pt1 = new Vector2(wDivide, hDivide);
            Vector2 pt2 = new Vector2(wDivide, hDivide * 2);
            Vector2 pt3 = new Vector2(wDivide, hDivide * 3);
            _ = new Vector2(wDivide, hDivide * 4);
            _ = new Vector2(wDivide, hDivide * 5);


            /* Setup the canvas geometries.  These can then be used in the CanvasDrawingSession.DrawGeometry Method.
             * http://microsoft.github.io/Win2D/html/Overload_Microsoft_Graphics_Canvas_CanvasDrawingSession_DrawGeometry.htm
             *
             * ICanvasResourceCreator allows resource constructors to accept either a CanvasDevice OR a CanvasControl.
             * http://microsoft.github.io/Win2D/html/T_Microsoft_Graphics_Canvas_ICanvasResourceCreator.htm 
             */

            // Rectangles do not have Vector2 overloads
            CanvasGeometry geometry01 = CanvasGeometry.CreateRectangle(canvas.Device, 0, 0, 300, 350);
            CanvasGeometry geometry02 = CanvasGeometry.CreateRoundedRectangle(canvas, 0, 0, 400, 200, 50, 50);
            CanvasGeometry geometry03 = CanvasGeometry.CreateEllipse(canvas, 0, 0, 150, 75);
            
            ds.DrawGeometry(geometry01, pt1, Colors.Turquoise, BDLib.pen1);
            BDLib.TickMark(ds, pt1, 10, Colors.Turquoise);
            ds.DrawGeometry(geometry02, pt2, Colors.Fuchsia, BDLib.pen2);
            BDLib.TickMark(ds, pt2, 10, Colors.Fuchsia);
            ds.DrawGeometry(geometry03, pt3, Colors.Orange, BDLib.pen3);
            BDLib.TickMark(ds, pt3, 10, Colors.Orange);
        }

        /// <summary>
        /// Drawing04 drawing session.  TextFormat.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ds"></param>
        private void Drawing04(CanvasControl sender, CanvasDrawingSession ds)
        {
            float width = (float)sender.ActualWidth;
            float height = (float)sender.ActualHeight;
            float xMid = width / 2;
            float yMid = height / 2;
            float radius = (float)Math.Min((width * 0.5f), (height * 0.5f));    // Get smaller of width or height for radius.

            // Draw border rectangle with diag lines and circle.  Note that Rectangles have no override that uses Vector2's.  
            ds.DrawRectangle(0, 0, width, height, accentColor, BDLib.pen1);
            ds.DrawLine(0, 0, width, height, accentColor, BDLib.pen1);
            ds.DrawLine(0, height, width, 0, accentColor, BDLib.pen1);
            ds.DrawCircle(xMid, yMid, radius, accentColor, BDLib.pen1);

            float vD = height / 10;
            CanvasTextFormat textFormat101 = new CanvasTextFormat()
            {
                FontFamily = "Consolas",
                FontSize = 15,
                HorizontalAlignment = CanvasHorizontalAlignment.Center,
                VerticalAlignment = CanvasVerticalAlignment.Center
            };
            Vector2 t01 = new Vector2(xMid, vD);
            BDLib.TickMark(ds, t01, 5, Colors.Green);
            ds.DrawText("Hello World! See me?", t01, Colors.Green, textFormat101);

            Vector2 t02 = new Vector2(xMid, vD * 2);
            BDLib.TickMark(ds, t02, 5, Colors.White);
            ds.DrawText("Hello Paul Ghilino", t02, Colors.White,
                new CanvasTextFormat
                {
                    FontFamily = "Segoe UI",
                    FontSize = 15,
                    HorizontalAlignment = CanvasHorizontalAlignment.Center,
                    VerticalAlignment = CanvasVerticalAlignment.Top
                });

            CanvasTextFormat textFormat102 = new CanvasTextFormat
            {
                FontWeight = Windows.UI.Text.FontWeights.ExtraBold,
                WordWrapping = CanvasWordWrapping.NoWrap
            };
            Vector2 t03 = new Vector2(xMid, vD * 3);
            BDLib.TickMark(ds, t03, 5, Colors.Cyan);
            ds.DrawText("Hello World! See me?", t03, Colors.Cyan, textFormat102);

            var labelFormat101 = new CanvasTextFormat()
            {
                FontFamily = "Comic Sans MS",
                FontSize = 12,
                VerticalAlignment = CanvasVerticalAlignment.Bottom,
                HorizontalAlignment = CanvasHorizontalAlignment.Left
            };
            Vector2 t04 = new Vector2(xMid, vD * 4);
            BDLib.TickMark(ds, t04, 5, Colors.Red);
            ds.DrawText("Hello World! See me?", t04, Colors.Red, labelFormat101);

            var numberFormat = new CanvasTextFormat()
            {
                FontFamily = "Comic Sans MS",
                FontSize = 18,
                VerticalAlignment = CanvasVerticalAlignment.Top,
                HorizontalAlignment = CanvasHorizontalAlignment.Left
            };
            Vector2 t05 = new Vector2(xMid, vD * 5);
            BDLib.TickMark(ds, t05, 5, Colors.Blue);
            ds.DrawText("Hello World! See me?", t05, Colors.Blue, numberFormat);

            var pageNumberFormat = new CanvasTextFormat()
            {
                FontFamily = "Comic Sans MS",
                FontSize = 10,
                VerticalAlignment = CanvasVerticalAlignment.Bottom,
                HorizontalAlignment = CanvasHorizontalAlignment.Center
            };
            Vector2 t06 = new Vector2(xMid, vD * 6);
            BDLib.TickMark(ds, t06, 5, Colors.Violet);
            ds.DrawText("Hello World! See me?", t06, Colors.Violet, pageNumberFormat);

            var titleFormat5 = new CanvasTextFormat()
            {
                FontFamily = "Comic Sans MS",
                FontSize = 24,
                VerticalAlignment = CanvasVerticalAlignment.Top,
                HorizontalAlignment = CanvasHorizontalAlignment.Left
            };
            Vector2 t07 = new Vector2(xMid, vD * 7);
            BDLib.TickMark(ds, t07, 5, Colors.Fuchsia);
            ds.DrawText("The C# Player's Guide", t07, Colors.Fuchsia, titleFormat5);

            var textFormat6 = new CanvasTextFormat()
            {
                FontFamily = "Segoe UI Black",
                HorizontalAlignment = CanvasHorizontalAlignment.Right,
                VerticalAlignment = CanvasVerticalAlignment.Center,
                FontSize = 20,
                LineSpacing = 20
            };
            var textTime = string.Format("{0}, {1}", DateTime.Now.ToString("ddd"), DateTime.Now.ToString("HH:mm"));
            Vector2 t08 = new Vector2(xMid, vD * 8);
            BDLib.TickMark(ds, t08, 5, Colors.White);
            ds.DrawText(textTime, t08, Colors.White, textFormat6);

            Vector2 t09 = new Vector2(xMid, vD * 9);
            BDLib.TickMark(ds, t09, 5, Colors.Gold);
            ds.DrawText(string.Format("Generated by Win2D: {0} {1}", DateTime.Now.ToString("d"), DateTime.Now.ToString("t")), t09, Colors.Gold,
                        new CanvasTextFormat() { HorizontalAlignment = CanvasHorizontalAlignment.Left, VerticalAlignment = CanvasVerticalAlignment.Top,
                        FontSize = 12 });

        }

        /// <summary>
        /// Drawing05 drawing session.  QuickStart2, Image:  http://microsoft.github.io/Win2D/html/QuickStart.htm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ds"></param>
        private void Drawing05(CanvasControl sender, CanvasDrawingSession ds)
        {
            float width = (float)sender.ActualWidth;
            float height = (float)sender.ActualHeight;
            /*  Can create a CanvasDrawingSession from a CanvasCommandList. The only difference is that when you draw to the command 
             *  list's drawing session (clds), you are not directly rendering to the CanvasControl. Instead, the command list is an 
             *  intermediate object that stores the results of rendering for later use. */
            CanvasCommandList cl = new CanvasCommandList(sender);
            using (CanvasDrawingSession clds = cl.CreateDrawingSession())   // Using block wraps drawing session and implements IDisposable garbage collection.
            {
                clds.DrawRectangle(0, 0, width, height, Colors.Yellow, BDLib.pen2);    // Draw border
                for (int i = 0; i < 50; i++)
                {
                    clds.DrawText("Hello, World!", RandomVector2(width, height), Color.FromArgb(255, RandomByte(256), RandomByte(256), RandomByte(256)), BDLib.textCC);
                    clds.DrawCircle(RandomVector2(width, height), RandomFloat(160), Color.FromArgb(255, RandomByte(256), RandomByte(256), RandomByte(256)));
                    clds.DrawLine(RandomVector2(width, height), RandomVector2(width, height), Color.FromArgb(255, RandomByte(256), RandomByte(256), RandomByte(256)));
                }
            }
            // Find many more effects at Microsoft.Graphics.Canvas.Effects.....
            Microsoft.Graphics.Canvas.Effects.EmbossEffect emboss = new EmbossEffect
            {
                Source = cl,
                Amount = 2.0f
            };
            ds.DrawImage(emboss);

            //Microsoft.Graphics.Canvas.Effects.GaussianBlurEffect blur = new GaussianBlurEffect();
            //blur.Source = cl;
            //blur.BlurAmount = 2.0f;     // Bluring radius
            //ds.DrawImage(blur);
        }

        /// <summary>
        /// Drawing06 drawing session.  NotUsed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ds"></param>
        private void Drawing06(CanvasControl sender, CanvasDrawingSession ds)
        {
            float width = (float)sender.ActualWidth;
            float height = (float)sender.ActualHeight;
            float xMid = width / 2;
            float yMid = height / 2;
            float radius = (float)Math.Min((width * 0.5f), (height * 0.5f));    // Get smaller of width or height for radius.

            // Draw border rectangle with diag lines and circle.  Note that Rectangles have no override that uses Vector2's.  
            ds.DrawRectangle(0, 0, width, height, accentColor, BDLib.pen1);
            ds.DrawLine(0, 0, width, height, accentColor, BDLib.pen1);
            ds.DrawLine(0, height, width, 0, accentColor, BDLib.pen1);
            ds.DrawCircle(xMid, yMid, radius, accentColor, BDLib.pen1);
        }

        /* canvas.Invalidate(); statements below sends notice that CanvasControl needs to redraw.  This triggers a Draw event 
         * which calls Canvas_Draw method above.  Canvas_Draw is declared in XAML canvas as method to invoke on Draw event.
         * Note that Draw events are automatically triggered any time the canvas size changes (which typically happen when
         *  the page size changes).
        */

        private void Button01_Click(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            selection = 1;
            canvas.Invalidate();    // Send notice that drawing needs to be redrawn.
        }

        private void Button02_Click(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            selection = 2;
            canvas.Invalidate();
        }

        private void Button03_Click(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            selection = 3;
            canvas.Invalidate();
        }

        private void Button04_Click(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            selection = 4;
            canvas.Invalidate();
        }

        private void Button05_Click(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            selection = 5;
            canvas.Invalidate();
        }

        private void Button06_Click(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            selection = 6;
            canvas.Invalidate();
        }

    }
}
