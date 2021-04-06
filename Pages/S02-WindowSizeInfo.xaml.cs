using System;
using Windows.Foundation;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SampleUWP
{
    public sealed partial class S02 : Page
    {
        // A pointer to MainPage is needed if you want to call methods or variables in MainPage.
        private readonly MainPage mainPage = MainPage.mainPagePointer;

        public S02()
        {
            InitializeComponent();
            // Setup mainScroller to handle scrolling for this page.
            mainPage.MainScrollerOn(horz: ScrollMode.Disabled, vert: ScrollMode.Auto, vertVis: ScrollBarVisibility.Auto);
        }
        
        /// <summary>
        /// Explore various information that can be accessed from Page SizeChange event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // Check out what is passed by 'e'.
            string output = "\nPage_SizeChanged---> ";
            output += "CurrentDeviceFamily=" + mainPage.currentDeviceFamily + ",  ";    // Retrieved when MainPage() loaded but chose to show here.
            //output += String.Format("e.GetTpe={0:}  ", e.GetType());
            //output += String.Format("e.OriginalSource={0:}  ", e.OriginalSource);     // Returns null in this case
            //output += String.Format("e.PreviousSize=({0:0.})  ", e.PreviousSize);
            output += string.Format("New Size=({0:0.})  ", e.NewSize);

            // Check out what is passed by sender and look up some unfamilar stuff.
            if (sender is Page page)
            {
                //output += String.Format("ActualWidth={0:0.}  ", page.ActualWidth);    // Same width as NewSize above
                //output += String.Format("ActualHeight={0:0.}  ", page.ActualHeight);  // Same height as NewSize above
                //output += String.Format("Width={0:0.}  ", page.Width);                //Returns NaN == Not a Number
                //output += String.Format("Height={0:0.}  ", page.Height);              //Returns NaN == Not a Number
                output += string.Format("GetType={0}  ", page.GetType());               //Returns SampleUWP.P01
                //output += String.Format("Parent={0}  ", page.Parent);
                //output += String.Format("Frame={0}  ", page.Frame);                   // Same as Parent above in this case
                //output += String.Format("RenderSize=({0:0.})  ", page.RenderSize);
                //output += String.Format("CancelDirManip={0:}  ", page.CancelDirectManipulations());   // Does nothing and returns False
                DependencyObject parent = page.Parent;
                if(parent is Frame frame )
                {
                    output += "ParentFrameName=" + frame.Name + ",  ";
                    output += string.Format("ParentFrameSize=({0:0.}, {1:0.}).", frame.ActualWidth, frame.ActualHeight);
                }
            }
            PageInfo.Text = output;

            // Get other information and display it too
            WindowBounds();
            WindowAppView();
            WindowDisplayInfo();
        }

        /// <summary>
        /// Force manual update the WindowBounds data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateWinInfo1_Click(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            WindowBounds();
        }

        /// <summary>
        /// Method to explore Window.Current.Bounds options.
        /// </summary>
        private void WindowBounds()
        {
            Rect window = Window.Current.Bounds;
            //double x = window.X;                  // Get/set left side of window
            //double y = window.Y;                  // Get/set top side of window
            double width = window.Width;            // Get/set width of window
            double height = window.Height;          // Get/set height of window
            double left = window.Left;              // Get left side of window
            double top = window.Top;                // Get top of window
            double right = window.Right;            // Get right of window
            double bottom = window.Bottom;          // Get bottom of window

            // More about formating doubles here: http://www.csharp-examples.net/string-format-double/
            string output = "Window.Current.Bounds---> ";
            //output += String.Format("X,Y = ({0:0.}, {1:0.}), ", x, y);
            output += string.Format("Width, Height=({0:0.}, {1:0.}), ", width, height);
            output += string.Format("Left, Top = ({0:0.}, {1:0.}), ", left, top);
            output += string.Format("Right, Bottom = ({0:0.}, {1:0.}).", right, bottom);
            //output += String.Format("(X+Width), (Y+Height) = ({0:0.}, {1:0.}).", x + width, y + height);
            WinInfo1.Text = output;
        }

        /// <summary>
        /// Force manual update the WindowAppView data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateWinInfo2_Click(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            WindowAppView();
        }
        
        /// <summary>
        /// Method to explore ApplicationView options.
        /// </summary>
        private void WindowAppView()
        {
            ApplicationView appView = ApplicationView.GetForCurrentView();
            string output = "ApplicationView---> ";
            output += string.Format("Current Orientation={0},  ", appView.Orientation);
            output += string.Format("AdjacentLeftEdge={0},  ", appView.AdjacentToLeftDisplayEdge);      // Is window set to left edge of display.
            output += string.Format("AdjacentRightEdge={0},  ", appView.AdjacentToRightDisplayEdge);    // Is window set to right edge of display'
            output += string.Format("Full Screen Mode={0},  ", appView.IsFullScreenMode);               // Is window in full-screen mode.
            output += string.Format("Visible Bounds={0}.", appView.VisibleBounds);
            /* Can use following code to set name on application title bar for each page.  But Microsoft is not doing it with their apps.
             * Somewhat redundant since page title is on title bar right below.
            appView.Title = "Home";
            output += String.Format("Title={0}  ", appView.Title);
            */
            WinInfo2.Text = output;
        }

        /// <summary>
        /// Force manual update the WindowDisplayInfo data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateWinInfo3_Click(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            WindowDisplayInfo();
        }

        /// <summary>
        /// Method to explore DisplayInformation options.
        /// </summary>
        private void WindowDisplayInfo()
        {
            DisplayInformation displayInfo = DisplayInformation.GetForCurrentView();
            string output = "DisplayInformation---> ";
            output += string.Format("Current Orientation={0},  ", displayInfo.CurrentOrientation);
            //output += String.Format("NativeOrientation={0}  ", displayInfo.NativeOrientation);
            output += string.Format("API Diagonal={0:0.00}\",  ", displayInfo.DiagonalSizeInInches);
            output += string.Format("Resolution Scale={0},  ", displayInfo.ResolutionScale);              // Enumeration
            output += string.Format("LogicalDPI={0:0.},  ", displayInfo.LogicalDpi);
            output += string.Format("RawDpiX={0:0.},  ", displayInfo.RawDpiX);
            output += string.Format("RawDpiY={0:0.},  ", displayInfo.RawDpiY);
            output += string.Format("RawPixels/ViewPixel={0:0.000}.", displayInfo.RawPixelsPerViewPixel);
            //output += String.Format("ColorProfile={0}  ", displayInfo.GetColorProfileAsync());        // Returns nothing useful
            WinInfo3.Text = output;
        }

        /// <summary>
        /// Toggle view to full screen mode or back.  While in full screen mode activates getMonitorInfo button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToggleFullScreenMode_Click(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            ApplicationView appView = ApplicationView.GetForCurrentView();
            if (appView.IsFullScreenMode)
            {
                appView.ExitFullScreenMode();           // Takes app out of full screen mode.
                GetMonitorInfo.IsEnabled = false;       // Turn button off
            }
            else
            {
                appView.TryEnterFullScreenMode();       // Will attempt to place app in full screen mode.
                GetMonitorInfo.IsEnabled = true;        // Turn button on
            }
        }
        
        /// <summary>
        /// getMonitorInfo button is enabled only after in full screen mode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetMonitorInfo_Click(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            GetMonitorResolution();
        }

        /// <summary>
        /// Get the resolution of the current monitor.  Also calculates the horizontal, vertical, and diagonal dimensions of 
        /// the monitor.  Must be in full screen mode before calling to get proper results.  Timing issues!!!
        /// </summary>
        private void GetMonitorResolution()
        {
            string output;
            ApplicationView appView = ApplicationView.GetForCurrentView();
            if (appView.IsFullScreenMode)
            {
                DisplayInformation displayInfo = DisplayInformation.GetForCurrentView();
                //Enum orient = appView.Orientation;
                double width = appView.VisibleBounds.Width;
                double height = appView.VisibleBounds.Height;
                double scale = (double)displayInfo.ResolutionScale / 100.0;             // Cast scale enumeration into double percentage
                DisplayOrientations currentOrient = displayInfo.CurrentOrientation;
                DisplayOrientations nativeOrient = displayInfo.NativeOrientation;
                double diagSize = (double)displayInfo.DiagonalSizeInInches;
                //double logicDPI = displayInfo.LogicalDpi;
                double xRawDPI = displayInfo.RawDpiX;
                double yRawDPI = displayInfo.RawDpiY;
                _ = displayInfo.RawPixelsPerViewPixel;
                double wRes = width * scale;
                double hRes = height * scale;
                double wInches = wRes / xRawDPI;
                double hInches = hRes / yRawDPI;
                double diagInches = Math.Sqrt(Math.Pow(wInches, 2.0) + Math.Pow(hInches, 2.0));     // Calculate the diagonal dimension.
                output = "GetMonitorResolution---> ";
                //output += String.Format("Width={0:0.}  ", width);
                //output += String.Format("Height={0:0.}  ", height);
                //output += String.Format("Scale={0:0.00}  ", scale);
                output += string.Format("Resolution=({0:0.}, {1:0.}),  ", wRes, hRes);
                output += string.Format("Width={0:0.00}\",  ", wInches);
                output += string.Format("Height={0:0.00}\",  ", hInches);
                output += string.Format("Diagonal={0:0.00}\",  ", diagInches);
                output += string.Format("API Diagonal={0:0.00}\",  ", diagSize);
                output += string.Format("Current Orientation={0},  ", currentOrient);
                output += string.Format("Native Orientation={0}.", nativeOrient);
            }
            else
            {
                output = "Error: Not in full screen mode before calling GetMonitorResolution() method.";
            }
            WinInfo4.Visibility = Visibility.Visible;
            WinInfo4.Text = output;
        }

        /// <summary>
        /// After page loads, set the state of getMonitorInfo button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            ApplicationView appView = ApplicationView.GetForCurrentView();
            if (appView.IsFullScreenMode)
                GetMonitorInfo.IsEnabled = true;
            else
                GetMonitorInfo.IsEnabled = false;
        }

    }
}
