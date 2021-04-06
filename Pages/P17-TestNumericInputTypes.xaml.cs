using LibraryCoder.Numerics;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace SampleUWP
{
    public sealed partial class P17 : Page
    {
        // A pointer to MainPage is needed if you want to call methods or variables in MainPage.
        private readonly MainPage mainPage = MainPage.mainPagePointer;

        // Toggle output format between these options using 'Output Toggle' button.
        private enum OutputFormat { None, Separator, Scientific, Double, DoubleX10 };  

        // Enumeration used by 'Output Toggle' button to toggle outputed format appearance.
        private OutputFormat outputFormat;

        // Value of prefix on output format button.  Change here to change everywhere.
        private readonly string outputFormatButtonText = "Output Format = ";

        // Save last set of values.  Any format changes will use these values and update output in TextBlock lastTblkOutput defined below.
        private TextBlock   lastTblkOutput;
        private EnumNumericType lastNumericType;
        private decimal     lastTblkValueDecimal;   // Use for all NumericTypes but double and float.
        private double      lastTblkValueDouble;    // Doubles and floats are special cases since some of their values can not be cast to a decimal
        private float       lastTblkValueFloat;     // without throwing an exception.  All other types can be cast to decimal and handled using decimal code.

        public P17()
        {
            InitializeComponent();
            
            // Hide XAML layout rectangles by setting to same color as RelativePanel Background;
            rectLayoutCenter.Fill = rpanel.Background;
            rectLayoutLeft.Fill = rpanel.Background; ;
            rectLayoutRight.Fill = rpanel.Background;

            outputFormat = OutputFormat.None;
            butOutputToggle.Content = outputFormatButtonText + OutputFormat.None.ToString();   // Change content of output format button from default set im XAML.
            butOutputToggle.IsEnabled = false;      // Disable button until an output TextBox is used.

            // Setup mainScroller to handle scrolling for this page.
            mainPage.MainScrollerOn(horz: ScrollMode.Disabled, vert: ScrollMode.Auto, horzVis: ScrollBarVisibility.Disabled, vertVis: ScrollBarVisibility.Auto, zoom: ZoomMode.Enabled);
        }

    
        /// <summary>
        /// Invoked when page is loaded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.

            tboxDecimal.Focus(FocusState.Programmatic);     // Set focus on Decimal TextBox.
        }

        /* Comment Consolidation for following methods.  Goal: Use time to edit code versus endlessly editing mutiple comments.
         * 
         * Numeric Entry: Check if TextBox entry is a valid numeric entry.  It is necessary to monitor three events for each NumericType TextBox:
         *      #1: Check TextBox if KeyDown=Enter.
         *      #2: Check TextBox if LostFocus.
         *      #3: Check TextBox if TextChanged still results in valid numeric entry of type desired.
         * 
         * #1: private void NumericType_TextBox_KeyDown(object sender, KeyRoutedEventArgs e)
         *          Invoked when user presses any key while in NumericType TextBox.  It ignores everything until ENTER key is pressed.
         *          When ENTER key is pressed then get string from TextBox and to convert to matching numeric.
         *          Call: LibNum.TextBoxGetNumericType(TextBox, TextBoxUpdate);
         *              TextBox = Get numeric string from TextBox and convert to matching numeric.  Update of TextBox is optional.
         *              TextBoxUpdate = Yes/No.  If Yes, update the TextBox with new TryParse value.  Cursor position will be maintained to extent possible.
         *              
         * #2: private void NumericType_TextBox_LostFocus(object sender, RoutedEventArgs e)
         *          Invoked when NumericType TextBox loses focus.  On focus lost, get string from TextBox and to convert to matching numeric.
         *          Call: LibNum.TextBoxGetNumericType(TextBox, TextBoxUpdate);
         *              TextBox = Get numeric string from TextBox and convert to matching numeric.  Update of TextBox is optional.
         *              TextBoxUpdate = Yes/No.  If Yes, update the TextBox with new TryParse value.  Cursor position will be maintained to extent possible.
         *              
         * #3: private void NumericType_TextBox_TextChanged(object sender, TextChangedEventArgs e)
         *          Invoked when any character is entered in the TextBox.  This method insures that character entered continues to provide a string that will 
         *          convert to a valid numeric of type specified.  Any character that does not match specified numeric type is deleted.  Also does other formatting.
         *          Call: LibNum.NumericTextBoxTextChanged(TextBox, NumericType);
         *              TextBox = Get last character inputed from TextBox and check if it is valid for specified NumericType.  Delete character otherwise.
         *              NumericType = One of the supported numeric types defined in the Numerictype enumeration.
         */

        /// <summary>
        /// Invoked when user presses  output format toggle button, butOutputToggle.  Toggle the output value through various formats.
        /// Order is: None, Separator, Scientific, Double, DoubleX10, then back to None.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButOutputToggle_Click(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.

            switch (outputFormat)
            {
                case OutputFormat.None:
                    outputFormat = OutputFormat.Separator;
                    butOutputToggle.Content = outputFormatButtonText + OutputFormat.Separator.ToString();       // Must match line above
                    break;
                case OutputFormat.Separator:
                    outputFormat = OutputFormat.Scientific;
                    butOutputToggle.Content = outputFormatButtonText + OutputFormat.Scientific.ToString();      // Must match line above
                    break;
                case OutputFormat.Scientific:
                    outputFormat = OutputFormat.Double;
                    butOutputToggle.Content = outputFormatButtonText + OutputFormat.Double.ToString();          // Must match line above
                    break;
                case OutputFormat.Double:
                    outputFormat = OutputFormat.DoubleX10;
                    butOutputToggle.Content = outputFormatButtonText + "Double × 10ⁿ";     // Special case, display this instead of enumeration text.
                    break;
                case OutputFormat.DoubleX10:
                    outputFormat = OutputFormat.None;
                    butOutputToggle.Content = outputFormatButtonText + OutputFormat.None.ToString();            // Must match line above
                    break;
                default:    // Throw exception so error can be discovered and corrected.
                    throw new NotSupportedException("Invalid switch (outputFormat).");
            }
            ShowConversion();   // Update the output shown to user.
        }

        /// <summary>
        /// Show calculated conversion in tblockOutput TextBlock either formatted or raw depending on outputFormatted value.
        /// More on string formatting: https://msdn.microsoft.com/en-us/library/26etazsy(v=vs.110).aspx
        /// </summary>
        private void ShowConversion()
        {
            switch (outputFormat)
            {
                case OutputFormat.None:     //  Display raw ouput with no formatting.
                    if (lastNumericType == EnumNumericType._double)
                    {
                        lastTblkOutput.Text = lastTblkValueDouble.ToString(LibNum.fpNumericFormatNone);
                    }
                    else
                    {
                        if (lastNumericType == EnumNumericType._float)
                        {
                            lastTblkOutput.Text = lastTblkValueFloat.ToString(LibNum.fpNumericFormatNone);
                        }
                        else    // Not double or float.
                        {
                            lastTblkOutput.Text = lastTblkValueDecimal.ToString(LibNum.fpNumericFormatNone);
                        }
                    }
                    break;
                case OutputFormat.Separator:    // Display Separator ouput.
                    if (lastNumericType == EnumNumericType._double)
                    {
                        lastTblkOutput.Text = lastTblkValueDouble.ToString(LibNum.fpNumericFormatSeparator);
                    }
                    else
                    {
                        if (lastNumericType == EnumNumericType._float)
                        {
                            lastTblkOutput.Text = lastTblkValueFloat.ToString(LibNum.fpNumericFormatSeparator);
                        }
                        else    // Not double or float.
                        {       // Decimal=29 Digits so need 29 "#'s" after decimal not to lose anything.
                            lastTblkOutput.Text = lastTblkValueDecimal.ToString(LibNum.fpNumericFormatSeparator);
                        }
                    }
                    break;
                case OutputFormat.Scientific:   // Display Scientific ouput.
                    if (lastNumericType == EnumNumericType._double)
                    {
                        lastTblkOutput.Text = lastTblkValueDouble.ToString(LibNum.fpNumericFormatScientific);
                    }
                    else
                    {
                        if (lastNumericType == EnumNumericType._float)
                        {
                            lastTblkOutput.Text = lastTblkValueFloat.ToString(LibNum.fpNumericFormatScientific);
                        }
                        else    // Not double or float.
                        {
                            lastTblkOutput.Text = lastTblkValueDecimal.ToString(LibNum.fpNumericFormatScientific);
                        }
                    }
                    break;
                case OutputFormat.Double:   // Display Double ouput.
                    if (lastNumericType == EnumNumericType._double)
                    {
                        lastTblkOutput.Text = lastTblkValueDouble.ToString(LibNum.fpNumericFormatNone);     // Display the output as a double.  Needed to paste in other apps that do not accept decimal.
                    }
                    else
                    {
                        if (lastNumericType == EnumNumericType._float)
                        {
                            // Converting float to double requires some trickery to keep same base value with just more zeros added at the end.
                            lastTblkOutput.Text = Convert.ToDouble(lastTblkValueFloat.ToString()).ToString(LibNum.fpNumericFormatNone);
                            //lastTblkOutput.Text = Convert.ToDouble(lastTblkValueFloat).ToString();
                        }
                        else    // Not double or float.
                        {
                            lastTblkOutput.Text = Convert.ToDouble(lastTblkValueDecimal).ToString(LibNum.fpNumericFormatNone);
                        }
                    }
                    break;
                case OutputFormat.DoubleX10:    // Display FormatAsPowerOfTen() output.  Convert value to double and format as 1.23456x10² versus 1.12346E+2.
                    if (lastNumericType == EnumNumericType._double)
                    {
                        lastTblkOutput.Text = LibNum.FormatAsPowerOfTen(lastTblkValueDouble, 15, EnumRemoveTrailingZeros.Yes);
                        //lastTblkOutput.Text = Library.FormatAsPowerOfTen(lastTblkValueDouble, 3, RemoveTrailingZeros.No);
                    }
                    else
                    {
                        if (lastNumericType == EnumNumericType._float)
                        {
                            // Converting float to double requires some trickery to keep same base value with just more zeros added at the end.
                            lastTblkOutput.Text = LibNum.FormatAsPowerOfTen(Convert.ToDouble(lastTblkValueFloat.ToString()), 15, EnumRemoveTrailingZeros.Yes);
                            //lastTblkOutput.Text = Library.FormatAsPowerOfTen(Convert.ToDouble(lastTblkValueFloat.ToString()), 3, RemoveTrailingZeros.No);
                        }
                        else    // Not double or float.
                        {
                            lastTblkOutput.Text = LibNum.FormatAsPowerOfTen((double)lastTblkValueDecimal, 15, EnumRemoveTrailingZeros.Yes);
                            //lastTblkOutput.Text = Library.FormatAsPowerOfTen((double)lastTblkValueDecimal, 3, RemoveTrailingZeros.No);
                        }
                    }
                    break;
                default:    // Throw exception so error can be discovered and corrected.
                    throw new NotSupportedException("Invalid switch (outputFormat).");
            }
        }

        /// <summary>
        ///  Initialize and enable output format toggle button, butOutputToggle, when Enter key is pressed.  Doubles and floats are special cases since
        /// some of their values can not be cast to a decimal without throwing an exception.  All other types can be cast to decimal and handled using decimal code.
        /// </summary>
        /// <param name="textBlock"></param>
        /// <param name="numericType"></param>
        /// <param name="decimalInput"></param>
        /// <param name="doubleInput"></param>
        /// <param name="floatInput"></param>
        private void FormatTextBlock(TextBlock textBlock, EnumNumericType numericType, decimal decimalInput, double doubleInput, float floatInput)
        {
            lastTblkOutput = textBlock;
            lastNumericType = numericType;
            lastTblkValueDecimal = decimalInput;
            lastTblkValueDouble = doubleInput;
            lastTblkValueFloat = floatInput;
            outputFormat = OutputFormat.None;       // Reset toggle button to default on each new pass.
            butOutputToggle.Content = outputFormatButtonText + OutputFormat.None.ToString();    // Reset toggle button text to default.
            butOutputToggle.IsEnabled = true;       // Enable output format toggle button now that Enter key has been pressed.
        }

        /// <summary>
        /// Reset and disable output format toggle button, butOutputToggle, any time a new character is entered.  Remains disabled until Enter key is pressed.
        /// </summary>
        private void FormatTextBlockReset()
        {
            outputFormat = OutputFormat.None;
            butOutputToggle.Content = outputFormatButtonText + OutputFormat.None.ToString();    // Reset toggle button text to default.
            butOutputToggle.IsEnabled = false;   // Disable output format toggle button.
        }
        
        /********** BEGIN: tboxDecimal entry: ***********/
        
        private void TboxDecimal_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.

            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                e.Handled = true;
                decimal inputDecimal = LibNum.TextBoxGetDecimal(tboxDecimal, EnumTextBoxUpdate.Yes);
                tblockDecimal.Text = inputDecimal.ToString(LibNum.fpNumericFormatNone);
                FormatTextBlock(tblockDecimal, EnumNumericType._decimal, inputDecimal, 0d, 0f);     // Enable user to reformat TextBlock output via butOutputToggle button.
            }
        }
        
        private void TboxDecimal_LostFocus(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            decimal inputDecimal = LibNum.TextBoxGetDecimal(tboxDecimal, EnumTextBoxUpdate.No);
            tblockDecimal.Text = inputDecimal.ToString(LibNum.fpNumericFormatNone);
        }
        
        private void TboxDecimal_TextChanged(object sender, TextChangedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            LibNum.NumericTextBoxTextChanged(tboxDecimal, EnumNumericType._decimal);
            FormatTextBlockReset();
        }

        /********** BEGIN: tboxDouble entry: ***********/
        
        private void TboxDouble_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                e.Handled = true;
                double inputDouble = LibNum.TextBoxGetDouble(tboxDouble, EnumTextBoxUpdate.Yes); 
                tblockDouble.Text = inputDouble.ToString(LibNum.fpNumericFormatNone);
                FormatTextBlock(tblockDouble, EnumNumericType._double, 0m, inputDouble, 0f);
            }
        }
 
        private void TboxDouble_LostFocus(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            double inputDouble = LibNum.TextBoxGetDouble(tboxDouble, EnumTextBoxUpdate.No); 
            tblockDouble.Text = inputDouble.ToString(LibNum.fpNumericFormatNone);
        }
       
        private void TboxDouble_TextChanged(object sender, TextChangedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            LibNum.NumericTextBoxTextChanged(tboxDouble, EnumNumericType._double);
            FormatTextBlockReset();
        }

        /********** BEGIN: tboxFloat entry: ***********/
  
        private void TboxFloat_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            if (e.Key == Windows.System.VirtualKey.Enter) 
            {
                e.Handled = true; 
                float inputFloat = LibNum.TextBoxGetFloat(tboxFloat, EnumTextBoxUpdate.Yes); 
                tblockFloat.Text = inputFloat.ToString(LibNum.fpNumericFormatNone);
                FormatTextBlock(tblockFloat, EnumNumericType._float, 0m, 0d, inputFloat);
            }
        }
         
        private void TboxFloat_LostFocus(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            float inputFloat = LibNum.TextBoxGetFloat(tboxFloat, EnumTextBoxUpdate.No);  
            tblockFloat.Text = inputFloat.ToString(LibNum.fpNumericFormatNone);
        } 
        
        private void TboxFloat_TextChanged(object sender, TextChangedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            LibNum.NumericTextBoxTextChanged(tboxFloat, EnumNumericType._float);
            FormatTextBlockReset();
        }
        

        /********** BEGIN: tboxLong entry: ***********/
        
        private void TboxLong_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                e.Handled = true;
                long inputLong = LibNum.TextBoxGetLong(tboxLong, EnumTextBoxUpdate.Yes); 
                tblockLong.Text = inputLong.ToString();
                FormatTextBlock(tblockLong, EnumNumericType._long, inputLong, 0d, 0f);
            }
        }
        
        private void TboxLong_LostFocus(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            long inputLong = LibNum.TextBoxGetLong(tboxLong, EnumTextBoxUpdate.No);
            tblockLong.Text = inputLong.ToString();
        }
        
        private void TboxLong_TextChanged(object sender, TextChangedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            LibNum.NumericTextBoxTextChanged(tboxLong, EnumNumericType._long);
            FormatTextBlockReset();
        }

        /********** BEGIN: tboxUlong entry: ***********/
        
        private void TboxUlong_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                e.Handled = true;
                ulong inputUlong = LibNum.TextBoxGetUlong(tboxUlong, EnumTextBoxUpdate.Yes); 
                tblockUlong.Text = inputUlong.ToString();
                FormatTextBlock(tblockUlong, EnumNumericType._ulong,inputUlong, 0d, 0f);
            }
        }

        private void TboxUlong_LostFocus(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            ulong inputUlong = LibNum.TextBoxGetUlong(tboxUlong, EnumTextBoxUpdate.No);
            tblockUlong.Text = inputUlong.ToString();
        }
        
        private void TboxUlong_TextChanged(object sender, TextChangedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            LibNum.NumericTextBoxTextChanged(tboxUlong, EnumNumericType._ulong);
            FormatTextBlockReset();
        }

        /********** BEGIN: tboxInt entry: ***********/
        
        private void TboxInt_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                e.Handled = true;
                int inputInt = LibNum.TextBoxGetInt(tboxInt, EnumTextBoxUpdate.Yes);  
                tblockInt.Text = inputInt.ToString();
                FormatTextBlock(tblockInt, EnumNumericType._int, inputInt, 0d, 0f);
            }
        }
        
        private void TboxInt_LostFocus(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            int inputInt = LibNum.TextBoxGetInt(tboxInt, EnumTextBoxUpdate.No);
            tblockInt.Text = inputInt.ToString();
        }
        
        private void TboxInt_TextChanged(object sender, TextChangedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            LibNum.NumericTextBoxTextChanged(tboxInt, EnumNumericType._int);
        }

        /********** BEGIN: tboxUint entry: ***********/

        private void TboxUint_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                e.Handled = true;
                uint inputUint = LibNum.TextBoxGetUint(tboxUint, EnumTextBoxUpdate.Yes);
                tblockUint.Text = inputUint.ToString();
                FormatTextBlock(tblockUint, EnumNumericType._uint, inputUint, 0d, 0f);
            }
        }
        
        private void TboxUint_LostFocus(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            uint inputUint = LibNum.TextBoxGetUint(tboxUint, EnumTextBoxUpdate.No);
            tblockUint.Text = inputUint.ToString();
        }

        private void TboxUint_TextChanged(object sender, TextChangedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            LibNum.NumericTextBoxTextChanged(tboxUint, EnumNumericType._uint);
        }

        /********** BEGIN: tboxShort entry: ***********/

        private void TboxShort_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                e.Handled = true;
                short inputShort = LibNum.TextBoxGetShort(tboxShort, EnumTextBoxUpdate.Yes);
                tblockShort.Text = inputShort.ToString();
                FormatTextBlock(tblockShort, EnumNumericType._short, inputShort, 0d, 0f);
            }
        }

        private void TboxShort_LostFocus(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            short inputShort = LibNum.TextBoxGetShort(tboxShort, EnumTextBoxUpdate.No);
            tblockShort.Text = inputShort.ToString();
        }
        
        private void TboxShort_TextChanged(object sender, TextChangedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            LibNum.NumericTextBoxTextChanged(tboxShort, EnumNumericType._short);
        }

        /********** BEGIN: tboxUshort entry: ***********/

        private void TboxUshort_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                e.Handled = true;
                ushort inputUshort = LibNum.TextBoxGetUshort(tboxUshort, EnumTextBoxUpdate.Yes);
                tblockUshort.Text = inputUshort.ToString();
                FormatTextBlock(tblockUshort, EnumNumericType._int, inputUshort, 0d, 0f);
            }
        }
        
        private void TboxUshort_LostFocus(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            ushort inputUshort = LibNum.TextBoxGetUshort(tboxUshort, EnumTextBoxUpdate.No); 
            tblockUshort.Text = inputUshort.ToString();
        }

        private void TboxUshort_TextChanged(object sender, TextChangedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            LibNum.NumericTextBoxTextChanged(tboxUshort, EnumNumericType._ushort);
        }

        /********** BEGIN: tboxSbyte entry: ***********/

        private void TboxSbyte_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                e.Handled = true;
                sbyte inputSbyte = LibNum.TextBoxGetSbyte(tboxSbyte, EnumTextBoxUpdate.Yes); 
                tblockSbyte.Text = inputSbyte.ToString();
                FormatTextBlock(tblockSbyte, EnumNumericType._sbyte, inputSbyte, 0d, 0f);
            }
        }
        
        private void TboxSbyte_LostFocus(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            sbyte inputSbyte = LibNum.TextBoxGetSbyte(tboxSbyte, EnumTextBoxUpdate.No); 
            tblockSbyte.Text = inputSbyte.ToString();
        }
        
        private void TboxSbyte_TextChanged(object sender, TextChangedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            LibNum.NumericTextBoxTextChanged(tboxSbyte, EnumNumericType._sbyte);
        }

        /********** BEGIN: tboxByte entry: ***********/

        private void TboxByte_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                e.Handled = true;
                byte inputByte = LibNum.TextBoxGetByte(tboxByte, EnumTextBoxUpdate.Yes); 
                tblockByte.Text = inputByte.ToString();
                FormatTextBlock(tblockByte, EnumNumericType._byte, inputByte, 0d, 0f);
            }
        }
        
        private void TboxByte_LostFocus(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            byte inputByte = LibNum.TextBoxGetByte(tboxByte, EnumTextBoxUpdate.No);
            tblockByte.Text = inputByte.ToString();
        }

        private void TboxByte_TextChanged(object sender, TextChangedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            LibNum.NumericTextBoxTextChanged(tboxByte, EnumNumericType._byte);
        }

    }
}
