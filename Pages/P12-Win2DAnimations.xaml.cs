using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;
using SampleUWP.BeamDesign;
using System;
using System.Numerics;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

/* 
 * Can not figure out how to stop one animation to start another on the same page.  Problem for another day!!!
*/

namespace SampleUWP
{
    public sealed partial class P12 : Page
    {
        // A pointer to MainPage is needed if you want to call methods or variables in MainPage.
        //private MainPage mainPage = MainPage.mainPagePointer;
        
        private int selection = 1;              // Animation selection, show Animate01() on page load.
        private readonly Random random = new Random();   // Used for animation(s) below.

        public P12()
        {
            InitializeComponent();
        }

        private void Canvas_Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            // Do same animation with each button until new animations added and case changed.
            switch (selection)
            {
                case 1:
                    Animate01(sender, args);
                    break;
                case 2:
                    Animate01(sender, args);
                    break;
                case 3:
                    Animate01(sender, args);
                    break;
                case 4:
                    Animate01(sender, args);
                    break;
                case 5:
                    Animate01(sender, args);
                    break;
                case 6:
                    Animate01(sender, args);
                    break;
                default:    // Throw exception so error can be discovered and corrected.
                    throw new NotSupportedException("Invalid switch (selection).");
            }
        }

        private void Canvas_CreateResources(CanvasAnimatedControl sender, CanvasCreateResourcesEventArgs args)
        {
            // Do same animation with each button until new animations added and case changed.
            switch (selection)
            {
                case 1:
                    Animate01_StaticContent(sender, args);
                    break;
                case 2:
                    Animate01_StaticContent(sender, args);
                    break;
                case 3:
                    Animate01_StaticContent(sender, args);
                    break;
                case 4:
                    Animate01_StaticContent(sender, args);
                    break;
                case 5:
                    Animate01_StaticContent(sender, args);
                    break;
                case 6:
                    Animate01_StaticContent(sender, args);
                    break;
                default:    // Throw exception so error can be discovered and corrected.
                    throw new NotSupportedException("Invalid switch (selection).");
            }
        }

        // Win2D Quick Start sample with animation at end: http://microsoft.github.io/Win2D/html/QuickStart.htm

        private void Animate01(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            _ = sender;     // Discard unused parameter.
            _ = args;       // Discard unused parameter.
            float radius = (float)(1 + Math.Sin(args.Timing.TotalTime.TotalSeconds)) * 9f;

            //emboss.Amount = radius;    ******* throwing exception *******
            //args.DrawingSession.DrawImage(emboss);

            blur.BlurAmount = radius;
            args.DrawingSession.DrawImage(blur);
        }

        Microsoft.Graphics.Canvas.Effects.GaussianBlurEffect blur;
        //Microsoft.Graphics.Canvas.Effects.EmbossEffect emboss;

        private void Animate01_StaticContent(CanvasAnimatedControl sender, CanvasCreateResourcesEventArgs args)
        {
            _ = args;       // Discard unused parameter.
            Size size = sender.Size;
            float width = (float)size.Width;
            float height = (float)size.Height;

            /*  Can create a CanvasDrawingSession from a CanvasCommandList. The only difference is that when you draw to the command 
             *  list's drawing session (clds), you are not directly rendering to the CanvasControl. Instead, the command list is an 
             *  intermediate object that stores the results of rendering for later use. */
            CanvasCommandList cl = new CanvasCommandList(sender);
            using (CanvasDrawingSession clds = cl.CreateDrawingSession())   // Using block wraps command list's drawing session and implements IDisposable.
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
            //emboss = new EmbossEffect();
            //emboss.Source = cl;
            //emboss.Amount = 10.0f;

            blur = new GaussianBlurEffect
            {
                Source = cl,
                BlurAmount = 9f     // Bluring radius
            };
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            button01.Focus(FocusState.Programmatic);      // Set focus on first button.
        }
        
        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            canvas.RemoveFromVisualTree();      // Explicitly remove all references to allow Win2D to complete garbage collection.
            canvas = null;
        }
        
        private Vector2 RandomVector2(float width, float height)
        {
            double x = random.NextDouble() * width;
            double y = random.NextDouble() * height;
            return new Vector2((float)x, (float)y);
        }

        private float RandomFloat(float maxValue)
        {
            return (float)random.NextDouble() * maxValue;
        }

        private byte RandomByte(int maxValue)
        {
            return (byte)random.Next(maxValue);
        }

        /* 
        * Can not figure out how to stop one animation to start another on the same page.  Problem for another day!!!
        */
        private void Button01_Click(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            selection = 1;
        }

        // <canvas:CanvasAnimatedControl x:Name="canvas" Grid.Row="1" Draw="Canvas_Draw" CreateResources="Canvas_CreateResources"/>
        private void Button02_Click(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            selection = 2;
        }

        private void Button03_Click(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            selection = 3;
        }

        private void Button04_Click(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            selection = 4;
        }

        private void Button05_Click(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            selection = 5;
        }

        private void Button06_Click(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            selection = 6;
        }
        
    }
}
