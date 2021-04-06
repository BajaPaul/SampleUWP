using System;
using Windows.UI.Xaml.Controls;

namespace SampleUWP.ConsoleCodeSamples
{
    internal partial class Samples
    {
        /* More at MSDN on Doubles: https://msdn.microsoft.com/en-us/library/system.double.aspx
         * 
         * The Double value type represents a double-precision 64-bit number with values ranging from negative 1.79769313486232e308 
         * to positive 1.79769313486232e308, as well as positive or negative zero, PositiveInfinity, NegativeInfinity, and not a 
         * number (NaN). It is intended to represent values that are extremely large (such as distances between planets or galaxies) 
         * or extremely small (the molecular mass of a substance in kilograms) and that often are imprecise (such as the distance from 
         * earth to another solar system), The Double type complies with the IEC 60559:1989 (IEEE 754) standard for binary floating-point 
         * arithmetic.
         * 
         * The Double data type stores double-precision floating-point values in a 64-bit binary format: 
         * Significand or mantissa = bits 0-51.  Exponent = bits 52-62.  Sign (0 = Positive, 1 = Negative) = bit 63.
         * 
         * Just as decimal fractions are unable to precisely represent some fractional values (such as 1/3 or Math.PI), binary fractions 
         * are unable to represent some fractional values. For example, 1/10, which is represented precisely by .1 as a decimal fraction, 
         * is represented by .001100110011 as a binary fraction, with the pattern "0011" repeating to infinity. In this case, the 
         * floating-point value provides an imprecise representation of the number that it represents. Performing additional mathematical 
         * operations on the original floating-point value often tends to increase its lack of precision. For example, if we compare the 
         * result of multiplying .1 by 10 and adding .1 to .1 nine times, we see that addition, because it has involved eight more operations, 
         * has produced the less precise result. Note that this disparity is apparent only if we display the two Double values by using the 
         * "R" standard numeric format string, which if necessary displays all 17 digits of precision supported by the Double type.
         */

        /// <summary>
        /// Convert a double to its exact decimal representation as a string.  
        /// Found this sample in link from this article:  http://csharpindepth.com/Articles/General/FloatingPoint.aspx
        /// More at MSDN on Doubles: https://msdn.microsoft.com/en-us/library/system.double.aspx
        /// 
        /// </summary>
        /// <param name="outputBlock"></param>
        internal static void Sample05_DoubleConverter(TextBlock outputBlock)
        {
            outputBlock.Text = "\nSample05_DoubleConverter:\n";
            outputBlock.Text += "\nDouble is a double-precision floating-point value stored in a 64-bit binary format:";
            outputBlock.Text += "\nSignificand = bits 0-51.  Exponent = bits 52-62.  Sign = bit 63 (0=Positive, 1=Negative).";
            outputBlock.Text += "\n\nSamples below extend precision of doubles to illustrate what is really happening.";
            outputBlock.Text += "\nIn reality, doubles only retain 15 significant digits, using rounding, plus exponent and sign.\n";

            double dblValue = 1.0 / 10.0;
            string dcResult = DoubleConverter.ToExactString(dblValue);
            int lenResult = dcResult.Length;
            outputBlock.Text += string.Format("\nMath=1/10, Value=[" + dblValue + "], Result=[" + dcResult  + "], len=[" + lenResult + "]");

            dblValue = 1.0 / 3.0;
            dcResult = DoubleConverter.ToExactString(dblValue);
            lenResult = dcResult.Length;
            outputBlock.Text += string.Format("\n\nMath=1/3, Value=[" + dblValue + "], Result=[" + dcResult + "], len=[" + lenResult + "]");

            dblValue = Math.PI;
            dcResult = DoubleConverter.ToExactString(dblValue);
            lenResult = dcResult.Length;
            outputBlock.Text += string.Format("\n\nMath=PI, Value=[" + dblValue + "], Result=[" + dcResult + "], len=[" + lenResult + "]");

            dblValue = 12.5 / 3.0;
            dcResult = DoubleConverter.ToExactString(dblValue);
            lenResult = dcResult.Length;
            outputBlock.Text += string.Format("\n\nMath=12.5/3, Value=[" + dblValue + "], Result=[" + dcResult + "], len=[" + lenResult + "]");

            dblValue = 12.0 /4.0;
            dcResult = DoubleConverter.ToExactString(dblValue);
            lenResult = dcResult.Length;
            outputBlock.Text += string.Format("\n\nMath=12/4, Value=[" + dblValue + "], Result=[" + dcResult + "], len=[" + lenResult + "]");

            dblValue = -2.0 / 3.0;
            dcResult = DoubleConverter.ToExactString(dblValue);
            lenResult = dcResult.Length;
            outputBlock.Text += string.Format("\n\nMath=-2/3, Value=[" + dblValue + "], Result=[" + dcResult + "], len=[" + lenResult + "]");

            dblValue = 10.0 / 2.0;
            dcResult = DoubleConverter.ToExactString(dblValue);
            lenResult = dcResult.Length;
            outputBlock.Text += string.Format("\n\nMath=10/2, Value=[" + dblValue + "], Result=[" + dcResult + "], len=[" + lenResult + "]");

            dblValue = double.MinValue;
            dcResult = DoubleConverter.ToExactString(dblValue);
            lenResult = dcResult.Length;
            outputBlock.Text += string.Format("\n\nMath=MinValue, Value=[" + dblValue + "], Result=[" + dcResult + "], len=[" + lenResult + "]");

            dblValue = double.MaxValue;
            dcResult = DoubleConverter.ToExactString(dblValue);
            lenResult = dcResult.Length;
            outputBlock.Text += string.Format("\n\nMath=MaxValue, Value=[" + dblValue + "], Result=[" + dcResult + "], len=[" + lenResult + "]\n");

        }
    }
}
