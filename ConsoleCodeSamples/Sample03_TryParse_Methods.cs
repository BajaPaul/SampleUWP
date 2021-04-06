using LibraryCoder.Numerics;
using System;
using Windows.UI.Xaml.Controls;

namespace SampleUWP.ConsoleCodeSamples
{
    internal partial class Samples
    {
        /// <summary>
        /// If you pass the Double.TryParse method a string that is created by calling the Double.ToString method, the original 
        /// Double value is returned. However, because of a loss of precision, the values may not be equal. In addition, 
        /// attempting to parse the string representation of either MinValue or MaxValue throws an OverflowException, as the 
        /// following example illustrates. Use round-trip specifier "R" to avoid that situation.
        /// Code from this link: https://msdn.microsoft.com/en-us/library/3s27fasw(v=vs.110).aspx
        /// </summary>
        /// <param name="outputBlock"></param>
        internal static void Sample03_TryParse_Methods(TextBlock outputBlock)
        {
            outputBlock.Text = "\nSample03_TryParse_Methods:\n";
            string minValue;
            string maxValue;
            string minValuePlusEpsilon;
            string maxValueMinusEpsilon;
            /* 
             * Double.Epsilon is sometimes used as an absolute measure of the distance between two Double values when testing for equality. 
             * However, Double.Epsilon measures the smallest possible value that can be added to, or subtracted from, a Double whose value 
             * is zero. For most positive and negative Double values, the value of Double.Epsilon is too small to be detected. Therefore, 
             * except for values that are zero, we do not recommend its use in tests for equality.  
             */
            double epsilon = double.Epsilon;    // https://msdn.microsoft.com/en-us/library/system.double.epsilon(v=vs.110).aspx
            double minNumber;
            double maxNumber;
            double minNumberPlusEpsilon;
            double maxNumberMinusEpsilon;
            minNumber = double.MinValue;
            maxNumber = double.MaxValue;
            minNumberPlusEpsilon = minNumber + epsilon;
            maxNumberMinusEpsilon = maxNumber - epsilon;
            // The Round-trip ("R") Format Specifier: https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.110).aspx
            minValue = minNumber.ToString("r");
            maxValue = maxNumber.ToString("r");
            minValuePlusEpsilon = minNumberPlusEpsilon.ToString("r");
            maxValueMinusEpsilon = maxNumberMinusEpsilon.ToString("r");
            outputBlock.Text += string.Format("\nepsilon = {0} for doubles.\n", epsilon);
            outputBlock.Text += string.Format("\nNone of the following strings would have converted back into doubles \nif the Round-trip (\"R\") Format Specifier had not been used.\n");
            if (double.TryParse(minValue, out _))
                outputBlock.Text += string.Format("\nminValue = {0} IS valid double.", minValue);
            else
                outputBlock.Text += string.Format("\nminValue = {0} IS NOT valid double.", minValue);
            if (double.TryParse(maxValue, out _))
                outputBlock.Text += string.Format("\nmaxValue = {0} IS valid double.", maxValue);
            else
                outputBlock.Text += string.Format("\nmaxValue = {0} IS NOT valid double.", maxValue);
            if (double.TryParse(minValuePlusEpsilon, out _))
                outputBlock.Text += string.Format("\nminValuePlusEpsilon = {0} IS valid double.", minValuePlusEpsilon);
            else
                outputBlock.Text += string.Format("\nminValuePlusEpsilon = {0} IS NOT valid double.", minValuePlusEpsilon);
            if (double.TryParse(maxValueMinusEpsilon, out _))
                outputBlock.Text += string.Format("\nmaxValueMinusEpsilon = {0} IS valid double.", maxValueMinusEpsilon);
            else
                outputBlock.Text += string.Format("\nmaxValueMinusEpsilon = {0} IS NOT valid double.", maxValueMinusEpsilon);
            //
            // Following example from: https://msdn.microsoft.com/en-us/library/system.double(v=vs.110).aspx
            //
            double value1 = 100.10142;
            value1 = Math.Sqrt(Math.Pow(value1, 2));
            double value2 = Math.Pow(value1 * 3.51, 2);
            value2 = Math.Sqrt(value2) / 3.51;
            outputBlock.Text += string.Format("\n\n{0} = {1} is {2}", value1, value2, value1.Equals(value2));   // Returns True/False.
            outputBlock.Text += string.Format("\n{0:R} = {1:R} is {2}", value1, value2, value1.Equals(value2));
            outputBlock.Text += string.Format("\n{0:R} - {1:R} = {2}\n", value1, value2, value1 - value2);
            //
            // Following example from: https://msdn.microsoft.com/en-us/library/system.double(v=vs.110).aspx
            //
            double one1 = 0.1 * 10;
            double one2 = 0;
            for (int ctr = 1; ctr <= 10; ctr++)
                one2 += .1;
            outputBlock.Text += string.Format("\n\n{0:R} = {1:R} is {2}", one1, one2, one1.Equals(one2));
            outputBlock.Text += string.Format("\n{0:R} is approximately equal to {1:R} is {2}\n", one1, one2, LibNum.EqualWithinDifference(one1, one2, .000000001)); ;
            //
            // Following example from: https://msdn.microsoft.com/en-us/library/system.math.round(v=vs.110).aspx#Midpoint
            //
            float value = 16.325f;
            outputBlock.Text += string.Format("\n\nWidening Conversion of {0:R} (type {1}) to {2:R} (type {3}): ", value, value.GetType().Name, (double)value, ((double)(value)).GetType().Name);
            outputBlock.Text += string.Format("\n" + (Math.Round(value, 2)));
            outputBlock.Text += string.Format("\n" + Math.Round(value, 2, MidpointRounding.AwayFromZero));

            decimal decValue = (decimal)value;
            outputBlock.Text += string.Format("\nCast of {0:R} (type {1}) to {2} (type {3}): ", value, value.GetType().Name, decValue, decValue.GetType().Name);
            outputBlock.Text += string.Format("\n" + (Math.Round(decValue, 2)));
            outputBlock.Text += string.Format("\n" + (Math.Round(decValue, 2, MidpointRounding.AwayFromZero)) + "\n");
            //
            //
            float v1 = 16.325f;
            float v2 = 16.3254f;
            // Note: Casting float to double does not result in new number with just more zeros as expected!  Must cast float to decimal to get that result.
            double v1Double = (double)v1;
            double v2Double = (double)v2;
            decimal v1Decimal = (decimal)v1;
            decimal v2Decimal = (decimal)v2;
            outputBlock.Text += string.Format("\n\nInitial float values: v1=[" + v1 + "], v2=[" + v2 + "]");
            outputBlock.Text += string.Format("\nCheck casting of floats! v1Double=[" + v1Double + "], v2Double=[" + v2Double + "]\nv1Decimal=[" + v1Decimal + "], v2Decimal=[" + v2Decimal + "]");
            bool EqualCheck = LibNum.EqualByRounding(v1, v2, 3);
            outputBlock.Text += string.Format("\nComparision to 3 digits: EqualCheck=" + EqualCheck + "\n");
        }
    }
}
