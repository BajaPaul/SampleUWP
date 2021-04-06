using LibraryCoder.Numerics;
using Windows.UI.Xaml.Controls;

namespace SampleUWP.ConsoleCodeSamples
{
    internal partial class Samples
    {
        /// <summary>
        /// Numeric format strings and decimal, double, and float, minimum and maximum values.
        /// </summary>
        /// <param name="outputBlock"></param>
        internal static void Sample04_Numeric_Format_Strings(TextBlock outputBlock)
        {
            outputBlock.Text = "\nSample04_Numeric_Format_Strings:\n";
            decimal minDecimal = decimal.MinValue;
            decimal maxDecimal = decimal.MaxValue;
            double minDouble = double.MinValue;
            double maxDouble = double.MaxValue;
            float minFloat = float.MinValue;
            float maxFloat = float.MaxValue;
            string minValue;
            string maxValue;
            // Output Decimal minimums and maximums
            outputBlock.Text += string.Format("\nDecimals have 28 significant digit precision.  Round-trip (r) specifier is not supported for decimal types.");
            // Round-trip modifier doesn't work with decimals!!!!
            minValue = minDecimal.ToString("n");        // N format Specifier used
            maxValue = maxDecimal.ToString("n");
            outputBlock.Text += string.Format("\n\nminDecimal(n)=" + minValue);
            outputBlock.Text += string.Format("\nmaxDecimal(n)= " + maxValue);
            minValue = minDecimal.ToString("g");        // G format Specifier used
            maxValue = maxDecimal.ToString("g");
            outputBlock.Text += string.Format("\n\nminDecimal(g)=" + minValue);
            outputBlock.Text += string.Format("\nmaxDecimal(g)= " + maxValue);
            minValue = minDecimal.ToString(LibNum.fpNumericFormatNone);   // Custom 1 format Specifier used
            maxValue = maxDecimal.ToString(LibNum.fpNumericFormatNone);
            outputBlock.Text += string.Format("\n\nminDecimal(Library.fpNumericFormatNone)=" + minValue);
            outputBlock.Text += string.Format("\nmaxDecimal(Library.fpNumericFormatNone)= " + maxValue);
            minValue = minDecimal.ToString(LibNum.fpNumericFormatScientific);   // Custom 2 format Specifier used
            maxValue = maxDecimal.ToString(LibNum.fpNumericFormatScientific);
            outputBlock.Text += string.Format("\n\nminDecimal(Library.fpNumericFormatScientific)=" + minValue);
            outputBlock.Text += string.Format("\nmaxDecimal(Library.fpNumericFormatScientific)= " + maxValue);
            // Output Double minimums and maximums.
            outputBlock.Text += string.Format("\n\nDoubles have 15 significant digit precision.  Round-trip (r) specifier reveals 2 additional internal digits of precision.");
            minValue = minDouble.ToString("r");        // R format Specifier used.
            maxValue = maxDouble.ToString("r");
            outputBlock.Text += string.Format("\n\nminDouble(r)=" + minValue);
            outputBlock.Text += string.Format("\nmaxDouble(r)= " + maxValue);
            minValue = minDouble.ToString("n");        // N format Specifier used
            maxValue = maxDouble.ToString("n");
            outputBlock.Text += string.Format("\n\nminDouble(n)=" + minValue);   // This is really long string, set up a right margin??
            outputBlock.Text += string.Format("\nmaxDouble(n)= " + maxValue);
            minValue = minDouble.ToString("g");        // G format Specifier used
            maxValue = maxDouble.ToString("g");
            outputBlock.Text += string.Format("\n\nminDouble(g)=" + minValue);
            outputBlock.Text += string.Format("\nmaxDouble(g)= " + maxValue);
            minValue = minDouble.ToString(LibNum.fpNumericFormatNone);   // Custom 1 format Specifier used
            maxValue = maxDouble.ToString(LibNum.fpNumericFormatNone);
            outputBlock.Text += string.Format("\n\nminDouble(Library.fpNumericFormatNone)=" + minValue);
            outputBlock.Text += string.Format("\nmaxDouble(Library.fpNumericFormatNone)= " + maxValue);
            minValue = minDouble.ToString(LibNum.fpNumericFormatScientific);   // Custom 2 format Specifier used
            maxValue = maxDouble.ToString(LibNum.fpNumericFormatScientific);
            outputBlock.Text += string.Format("\n\nminDouble(Library.fpNumericFormatScientific)=" + minValue);
            outputBlock.Text += string.Format("\nmaxDouble(Library.fpNumericFormatScientific)= " + maxValue);
            // Output Float minimums and maximums.
            outputBlock.Text += string.Format("\n\nFloats have 7 significant digit precision.  Round-trip (r) specifier reveals 2 additional internal digits of precision.");
            minValue = minFloat.ToString("r");            // R format Specifier used.
            maxValue = maxFloat.ToString("r");
            outputBlock.Text += string.Format("\n\nminFloat(r)=" + minValue);
            outputBlock.Text += string.Format("\nmaxFloat(r)= " + maxValue);
            minValue = minFloat.ToString("n");            // N format Specifier used
            maxValue = maxFloat.ToString("n");
            outputBlock.Text += string.Format("\n\nminFloat(n)=" + minValue);
            outputBlock.Text += string.Format("\nmaxFloat(n)= " + maxValue);
            minValue = minFloat.ToString("g");            // G format Specifier used
            maxValue = maxFloat.ToString("g");
            outputBlock.Text += string.Format("\n\nminFloat(g)=" + minValue);
            outputBlock.Text += string.Format("\nmaxFloat(g)= " + maxValue);
            minValue = minFloat.ToString(LibNum.fpNumericFormatNone);       // Custom 1 format Specifier used
            maxValue = maxFloat.ToString(LibNum.fpNumericFormatNone);
            outputBlock.Text += string.Format("\n\nminFloat(Library.fpNumericFormatNone)=" + minValue);
            outputBlock.Text += string.Format("\nmaxFloat(Library.fpNumericFormatNone)= " + maxValue);
            minValue = minFloat.ToString(LibNum.fpNumericFormatScientific);     // Custom 2 format Specifier used
            maxValue = maxFloat.ToString(LibNum.fpNumericFormatScientific);
            outputBlock.Text += string.Format("\n\nminFloat(Library.fpNumericFormatScientific)=" + minValue);
            outputBlock.Text += string.Format("\nmaxFloat(Library.fpNumericFormatScientific)= " + maxValue + "\n");
        }
    }
}
