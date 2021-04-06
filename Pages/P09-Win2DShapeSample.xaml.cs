using System;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;

namespace SampleUWP
{
    public sealed partial class P09 : Page
    {
        /// <summary>
        /// Class to define drawing sessions, which are methods, and their names used in page menu ListBox.
        /// </summary>
        private class Shape
        {
            public string Name { get; set; }
            public Action<CanvasControl, CanvasDrawingSession> Drawer { get; set; }
        }

        private readonly List<Shape> availableShapes;

        // CanvasControl Class:  http://microsoft.github.io/Win2D/html/T_Microsoft_Graphics_Canvas_UI_Xaml_CanvasControl.htm

        public P09()
        {
            InitializeComponent();
            
            availableShapes = new List<Shape>       // Set up menu and associated drawing sessions.  The drawing sessions are methods defined below.
            {
                new Shape() { Name = "Line",              Drawer = DrawLine             },
                new Shape() { Name = "Rectangle",         Drawer = DrawRectangle        },
                new Shape() { Name = "Rounded Rectangle", Drawer = DrawRoundedRectangle },
                new Shape() { Name = "Circle",            Drawer = DrawCircles          }
            };
        }

        /// <summary>
        /// Invoked when page loaded.  Load the List into the ListBox and select the first item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            this.shapes.ItemsSource = from shapeName in availableShapes select shapeName.Name;      // This eliminates need for bound convertor as used in MainPage().
            this.shapes.SelectedIndex = 0;      // Select the first item in the list
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
            canvas.RemoveFromVisualTree();      // Explicitly remove references to allow the Win2D controls to get garbage collected.
            canvas = null;
        }

        /// <summary>
        /// Invoked by ListBox control named shapes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Shapes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            canvas.Invalidate();       // This send notice that CanvasControl needs redrawn, this will trigger a Draw event to redraw it.
        }

        /// <summary>
        /// Invoked by Draw event initiated when a different shape was selected via Shapes_SelectionChanged() event above.
        /// This is setup in XAML via <canvas:CanvasControl x:Name="canvas" Draw="Canvas_Draw"/> but is triggered by Shapes_SelectionChanged() 
        /// above which request a redraw and initiates the Draw event this is listening too.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void Canvas_Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            Shape currentShape = availableShapes[shapes.SelectedIndex];     // Get current selected drawing session.
            currentShape.Drawer(sender, args.DrawingSession);               // And then draw it.
        }

        /// <summary>
        /// Line drawing session.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ds"></param>
        private void DrawLine(CanvasControl sender, CanvasDrawingSession ds)
        {
            float width = (float)sender.ActualWidth;
            float height = (float)sender.ActualHeight;
            float middle = height / 2;
            int steps = Math.Min((int)(width / 10), 40);    // Get smaller of w/10 or 40, the default.

            for (int i = 0; i < steps; ++i)
            {
                float mu = (float)i / steps;            // Percentage, 0 to 1
                float a = (float)(mu * Math.PI * 2);    // Value that grows proporinatly with mu

                Color color = GradientColor(mu);

                float x = width * mu;
                float y = (float)(middle + Math.Sin(a) * (middle * 0.3));   // Middle plus +/- Sin() amplifier

                float strokeWidth = (float)(Math.Cos(a) + 1) * 5;           // Width of line is based on Cos() amplifier

                ds.DrawLine(x, 0, x, y, color, strokeWidth);                // Draw top line
                ds.DrawLine(x, y, x, height, color, 10 - strokeWidth);      // Draw bot line, stroke must be an abs()
            }
        }

        /// <summary>
        /// Rectangle drawing session.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ds"></param>
        private void DrawRectangle(CanvasControl sender, CanvasDrawingSession ds)
        {
            var width = (float)sender.ActualWidth;
            var height = (float)sender.ActualHeight;
            int steps = Math.Min((int)(width / 10), 20);

            for (int i = 0; i < steps; ++i)
            {
                var mu = (float)i / steps;
                var color = GradientColor(mu);
                mu *= 0.5f;
                var x = mu * width;
                var y = mu * height;
                var xx = (1 - mu) * width;
                var yy = (1 - mu) * height;
                ds.DrawRectangle(x, y, xx - x, yy - y, color, 2.0f);
            }
        }

        /// <summary>
        /// Rounded rectangle drawing session.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ds"></param>
        private void DrawRoundedRectangle(CanvasControl sender, CanvasDrawingSession ds)
        {
            var width = (float)sender.ActualWidth;
            var height = (float)sender.ActualHeight;

            int steps = Math.Min((int)(width / 30), 10);

            for (int i = 0; i < steps; ++i)
            {
                var mu = (float)i / steps;

                var color = GradientColor(mu);

                mu *= 0.5f;
                var x = mu * width;
                var y = mu * height;

                var xx = (1 - mu) * width;
                var yy = (1 - mu) * height;

                var radius = mu * 50.0f;

                ds.DrawRoundedRectangle(
                    x, y,
                    xx - x, yy - y,
                    radius, radius,
                    color,
                    2.0f);
            }
        }

        /// <summary>
        /// Circle drawing session.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ds"></param>
        private void DrawCircles(CanvasControl sender, CanvasDrawingSession ds)
        {
            float width = (float)sender.ActualWidth;
            float height = (float)sender.ActualHeight;

            float endpointMargin = Math.Min(width, height) / 8;
            float controlMarginX = endpointMargin * 4;
            float controlMarginY = endpointMargin * 2;

            for (int i = 0; i < 25; i++)
            {
                Vector2[] bez = new Vector2[4];
                int n = (i * 24) + 9 - (i / 2);

                for (int k = 0; k < 3; k++)
                {
                    int j = 4 - (2 * k);
                    bez[k].X = (0 + (((n >> (j + 1)) & 1) * (width - controlMarginX)));
                    bez[k].Y = (0 + (((n >> j) & 1) * (height - controlMarginY)));
                }
                bez[3].X = width - endpointMargin; // Collect the ends in the lower right
                bez[3].Y = height - endpointMargin;

                const int nSteps = 80;
                const float tStep = 1.0f / nSteps;
                float t = 0;
                for (int step = 0; step < nSteps; step++)
                {
                    float s = 1 - t;
                    float ss = s * s;
                    float sss = ss * s;
                    float tt = t * t;
                    float ttt = tt * t;
                    float x = (sss * bez[0].X) + (3 * ss * t * bez[1].X) + (3 * s * tt * bez[2].X) + (ttt * bez[3].X);
                    float y = (sss * bez[0].Y) + (3 * ss * t * bez[1].Y) + (3 * s * tt * bez[2].Y) + (ttt * bez[3].Y);
                    float radius = ttt * endpointMargin;
                    float strokeWidth = (0.5f - Math.Abs(ss - 0.5f)) * 10;

                    ds.DrawCircle(x, y, radius, GradientColor(t), strokeWidth);
                    t += tStep;
                }
            }
        }

        /// <summary>
        /// Called by drawing saessions to vary color across session.
        /// </summary>
        /// <param name="mu">mu is a percentage, 0 to 1.</param>
        /// <returns></returns>
        private static Color GradientColor(float mu)
        {
            byte c = (byte)((Math.Sin(mu * Math.PI * 2) + 1) * 127.5);

            return Color.FromArgb(255, (byte)(255 - c), c, 220);        // Color FromArgb(int alpha, int red, int green, int blue)

        }

    }
}
