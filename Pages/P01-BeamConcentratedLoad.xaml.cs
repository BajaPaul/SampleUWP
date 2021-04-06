using LibraryCoder.UnitConversions;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using SampleUWP.BeamDesign;
using System;
using System.Diagnostics;
using System.Numerics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace SampleUWP
{
    public sealed partial class P01 : Page
    {
        // A pointer to MainPage is needed if you want to call methods or variables in MainPage.
        // private MainPage mainPage = MainPage.mainPagePointer;
        
        // Set up drawing session variables and initialize them for first draw on page load.
        // Need to use separate values in drawing session or input changes trigger premature draws.
        // Drawing sessions use floats vs. doubles.
        // Forces up are positive and forces down are negative.
        private float dsLength = 10;            // Beam length
        private float dsLoadPosition = 5;       // Position of load on beam from left
        private float dsLoad = -1000;           // Beam load down (negative)
        private float dsReactionL = 500;        // Equals -dsLoad / 2 (positive)
        private float dsReactionR = 500;        // Equals -dsLoad / 2 (positive)
        private float dsShearL = 500;           // Equals dsReactionL (positive)
        private float dsShearR = -500;          // Equals -dsReactionR (negative)
        private float dsMoment = 2500;          // Equals dsLoad * dsLength / 4 
        private float dsLoadMax = 1000;         // Equals dsLoad
        private float dsShearMax = 1000;        // Equals dsLoad
        private float dsMomentMax = 2500;       // Equals dsLoad * dsLength / 4 

        // Input variables, use doubles for beam calculations.
        private double bLength;
        private double bLoadPosition;
        private double bLoad;           // Enter from TextBox as positive but convert to negative in calculations!!!
        //private double bElasticity;   // Not used yet.
        //private double bInertia;      // Not used yet.

        // Do not allow beam calculation until all user input is valid, equals true.
        private bool bLengthValid = false;
        private bool bLoadPositionValid = false;
        private bool bLoadValid = false;

        // Use SI units if true, otherwise use UCS units.
        private bool siUnits = false;

        // CanvasControl Class:  http://microsoft.github.io/Win2D/html/T_Microsoft_Graphics_Canvas_UI_Xaml_CanvasControl.htm

        public P01()    
        {
            this.InitializeComponent();
            W2DPrint.PrintDrawer = PageDrawer;                                          // Set print delegate to PageDrawer method on this page.
            W2DPrint.printTitle = BDType01.beamDiagramLable;  // Set the title bar text in the print dialog.
            BDLib.UnitsUSC();        // Set default units to USC vs. SI.
            ChangeUnits();

            // Win2D doesn't like caching and will throw exception.  No canvas present when coming back to page since
            // cleaned up Win2D before leaving page.
            //this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Required;     // Cache page state.
        }

        /*
        Rect imageableRect = args.PrintTaskOptions.GetPageDescription(1).ImageableRect;
        Vector2 leftTop = new Vector2((float)imageableRect.X, (float)imageableRect.Y);
        Vector2 rightBot = new Vector2(leftTop.X + (float)imageableRect.Width, leftTop.Y + (float)imageableRect.Height);
        */

        /// <summary>
        /// This is invoked by XAML from canvas:CanvasControl.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void Beam_Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            BDLib.ColorsDisplay();       // Draw all items using display color.  Only applies to colors defined in Blib.
            Vector2 leftTop = new Vector2(0, 0);            // On display use 0,0 as leftTop.
            Vector2 rightBot = sender.Size.ToVector2();
            PageDrawer(args.DrawingSession, leftTop, rightBot);
        }

        /// <summary>
        /// Use same Drawer regardless of display or printing.  With printing added, can not assume leftTop is 0,0 
        /// since need to offset for required printer margins.  If using colors defined in LibBeamItems, then display colors 
        /// can be toggled from color to black.
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="leftTop"></param>
        /// <param name="rightBot"></param>
        private void PageDrawer(CanvasDrawingSession ds, Vector2 leftTop, Vector2 rightBot)
        {
            BDType01.BeamDraw(ds, leftTop, rightBot, dsLength, dsLoadPosition, dsLoad, dsReactionL, dsReactionR, 
                                                        dsShearL, dsShearR, dsMoment, dsLoadMax, dsShearMax, dsMomentMax);
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

        private void ButtonCalc_Click(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.

                            // Calculation nomenclature from AISC Beam Diagrams and Formulas.
                            // Positive values are up, negaitive are down.  Drawing coordinates are opposite so somewhat confusing!
            if (!siUnits)
            {

                // Experimental to see if can access unit conversions....

                // Test decimal methods.
                decimal L5 = LibUC.ConvertValue((decimal)bLength, EnumConversionsLength.foot, EnumConversionsLength.meter);
                Debug.WriteLine("ButtonCalc_Click(): Test decimal methods: bLength=[" + bLength + "] feet converted to [" + L5 + "] meters");
                decimal L6 = LibUC.ConvertValue(L5, EnumConversionsLength.meter, EnumConversionsLength.foot);
                Debug.WriteLine("ButtonCalc_Click(): Test decimal methods: bLength=[" + L5 + "] meters converted back to [" + L6 + "] feet");

                // Test double methods.
                double L7 = LibUC.ConvertValue(bLength, EnumConversionsLength.foot, EnumConversionsLength.meter);
                Debug.WriteLine("ButtonCalc_Click(): Test double methods: bLength=[" + bLength + "] feet converted to [" + L7 + "] meters");
                double L8 = LibUC.ConvertValue(L7, EnumConversionsLength.meter, EnumConversionsLength.foot);
                Debug.WriteLine("ButtonCalc_Click(): Test double methods: bLength=[" + L7 + "] meters converted back to [" + L8 + "] feet");

                // Test float methods.
                float L9 = LibUC.ConvertValue((float)bLength, EnumConversionsLength.foot, EnumConversionsLength.meter);
                Debug.WriteLine("ButtonCalc_Click(): Test float methods: bLength=[" + bLength + "] feet converted to [" + L9 + "] meters");
                float L10 = LibUC.ConvertValue((float)L5, EnumConversionsLength.meter, EnumConversionsLength.foot);
                Debug.WriteLine("ButtonCalc_Click(): Test float methods: bLength=[" + L9 + "] meters converted back to [" + L10 + "] feet");
                
            }

            double L = bLength;
            double a = bLoadPosition;
            double P = -bLoad;
            double b = bLength - a;
            double Rr = -P * a / L;
            double Rl = -P - Rr;
            double Vl = 0;      // Special Case: Shear is zero at beam ends, otherwise is equal to reaction.
            if (a > 0)
                Vl = Rl;
            double Vr = 0;
            if (a < L)
                Vr = -Rr;
            double Mp = -P * a * b / L;
            double Mmax = -P * L / 4;      // Used for scaling moment diagram.  Max moment occurs when load is at center of beam.

            // Transfer calculated values to drawing session and initiate redraw.

            dsLength = (float)L;
            dsLoadPosition = (float)a;
            dsLoad = (float)P;
            dsReactionL = (float)Rl;
            dsReactionR = (float)Rr;
            dsShearL = (float)Vl;
            dsShearR = (float)Vr;
            dsMoment = (float)Mp;
            dsLoadMax = (float)P;
            dsShearMax = (float)P;
            dsMomentMax = (float)Mmax;
            canvas.Invalidate();                // Redraw the canvas/beam.
            buttonPrint.IsEnabled = true;       // Enable the print button since results calculated.
        }

        private void CboxUnits_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            string beamUnits = e.AddedItems[0].ToString();
            switch (beamUnits)
            {
                case "USC":
                    status.Text = "USC units selected";
                    siUnits = false;
                    BDLib.UnitsUSC();
                    ChangeUnits();
                    break;
                case "SI":
                    status.Text = "SI units selected";
                    siUnits = true;
                    BDLib.UnitsSI();
                    ChangeUnits();
                    break;
                default:    // Throw exception so error can be discovered and corrected.
                    throw new NotSupportedException("Invalid switch (beamUnits).");
            }
        }

        // Keep local since changes local page values????
        private void ChangeUnits()
        {
            tboxLength.PlaceholderText = BDLib.pTextLength;
            tboxLoadPosition.PlaceholderText = BDLib.pTextLoadPosition;
            tboxLoad.PlaceholderText = BDLib.pTextLoad;
            tboxElasticity.PlaceholderText = BDLib.pTextElasticity;
            tboxInertia.PlaceholderText = BDLib.pTextInertia;


            // Whole bunch of issues here.  Redraw using current or defalt values.....

            canvas.Invalidate();
        }

        /// <summary>
        /// Invoked when user presses 'Enter' key.  Then passes control to TboxLength_LostFocus().
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TboxLength_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            if (e.Key == Windows.System.VirtualKey.Enter)    // Check if 'Enter' key was pressed.  Ignore everything else.
            {
                TboxLength_LostFocus(null, null);
            }
        }

        /// <summary>
        /// Invoked when focus moves from beam length TextBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TboxLength_LostFocus(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            // Get bLength
            if (double.TryParse(tboxLength.Text, out bLength))      // Returns "True" if the string was converted to a double.
            {
                if (bLength > 0)
                {
                    bLengthValid = true;    // Converted text to double > 0.
                    status.Text = string.Empty;
                }
                else
                {
                    bLengthValid = false;   // Converted text to double but <= 0.
                    status.Text = "Invalid beam length, not greater than zero";
                }
            }
            else
            {
                bLengthValid = false;   // Could not convert text to double.
                status.Text = "Invalid beam length, not a number.";
            }
            if (bLengthValid && bLoadPositionValid && bLoadValid)
            {
                buttonCalc.IsEnabled = true;
                buttonCalc.Focus(FocusState.Programmatic);  // Set focus on Calc button since have all needed input.
            }
            else
                buttonCalc.IsEnabled = false;
        }

        /// <summary>
        /// Invoked when user presses 'Enter' key.  Then passes control to TboxLoadPosition_LostFocus().
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TboxLoadPosition_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            if (e.Key == Windows.System.VirtualKey.Enter)    // Check if 'Enter' key was pressed.  Ignore everything else.
            {
                TboxLoadPosition_LostFocus(null, null);
            }
        }

        /// <summary>
        /// Invoked when focus moves from load position TextBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TboxLoadPosition_LostFocus(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            // Get bLoadPosition
            if (double.TryParse(tboxLoadPosition.Text, out bLoadPosition))      // Returns "True" if the string was converted to a double.
            {
                if (bLoadPosition >= 0 && bLoadPosition <= bLength)
                {
                    bLoadPositionValid = true;    // Converted text to double.
                    status.Text = string.Empty;
                }
                else
                {
                    bLoadPositionValid = false;   // Converted text to double but < 0 or greater than bEnumConversionsLength.
                    status.Text = "Invalid load postion, does not fall on beam.";
                }
            }
            else
            {
                bLoadPositionValid = false;   // Could not convert text to double.
                status.Text = "Invalid load position, not a number.";
            }
            if (bLengthValid && bLoadPositionValid && bLoadValid)
            {
                buttonCalc.IsEnabled = true;
                buttonCalc.Focus(FocusState.Programmatic);  // Set focus on Calc button since have all needed input.
            }
            else
                buttonCalc.IsEnabled = false;
        }

        /// <summary>
        /// Invoked when user presses 'Enter' key.  Then passes control to TboxLoad_LostFocus().
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TboxLoad_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            if (e.Key == Windows.System.VirtualKey.Enter)    // Check if 'Enter' key was pressed.  Ignore everything else.
            {
                TboxLoad_LostFocus(null, null);
            }
        }

        /// <summary>
        /// Invoked when focus moves from load TextBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TboxLoad_LostFocus(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            // Get bLoad
            if (double.TryParse(tboxLoad.Text, out bLoad))      // Returns "True" if the string was converted to a double.
            {
                if (bLoad > 0)      //Load needs to be positive or will need to duplicate solution text layout to accomodate negative loading.
                {
                    bLoadValid = true;    // Converted text to double.
                    status.Text = string.Empty;
                }
                else
                {
                    bLoadValid = false;   // Converted text to double but <= 0.
                    status.Text = "Invalid beam load, not greater than zero";
                }
            }
            else
            {
                bLoadValid = false;   // Could not convert text to double.
                status.Text = "Invalid beam load, not a number.";
            }
            if (bLengthValid && bLoadPositionValid && bLoadValid)
            {
                buttonCalc.IsEnabled = true;
                buttonCalc.Focus(FocusState.Programmatic);  // Set focus on Calc button since have all needed input.
            }
            else
                buttonCalc.IsEnabled = false;
        }

        /// <summary>
        /// Invoked when user presses 'Enter' key.  Then passes control to TboxElasticity_LostFocus().
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TboxElasticity_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            if (e.Key == Windows.System.VirtualKey.Enter)    // Check if 'Enter' key was pressed.  Ignore everything else.
            {
                TboxElasticity_LostFocus(null, null);
            }
        }
        
        private void TboxElasticity_LostFocus(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.

        }

        /// <summary>
        /// Invoked when user presses 'Enter' key.  Then passes control to TboxInertia_LostFocus().
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TboxInertia_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            if (e.Key == Windows.System.VirtualKey.Enter)    // Check if 'Enter' key was pressed.  Ignore everything else.
            {
                TboxInertia_LostFocus(null, null);
            }
        }

        private void TboxInertia_LostFocus(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.

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
            tboxLength.Focus(FocusState.Programmatic);  // Set focus on beam length TextBox.
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
