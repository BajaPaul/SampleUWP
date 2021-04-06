using Windows.UI.Xaml.Controls;

/*
 * This sample shows the decimal values of all possible fractions of various assigned numbers.
 */

namespace SampleUWP.ConsoleCodeSamples
{
    internal partial class Samples
    {
        internal static void Sample07_Fractions_Of_Various_Numbers(TextBlock outputBlock)
        {
            outputBlock.Text = "\nSample07_Fractions_Of_Various_Numbers:\n";
            outputBlock.Text += "\nSample shows value of all fractions of various assigned numbers.";

            // The dividend is divided by the divisor to get the quotient.  See https://en.wikipedia.org/wiki/Division_%28mathematics%29
            
            const string separator = "\n\n********************************************************************";
            decimal dividend;

            outputBlock.Text += separator;
            dividend = 6;
            outputBlock.Text += "\nNumber=" + dividend;
            for (int i = 1; i <= dividend; i++)
            {
                outputBlock.Text += "\n" + i + "/" + dividend + " = " + i / dividend;
            }

            outputBlock.Text += separator;
            dividend = 8;
            outputBlock.Text += "\nNumber=" + dividend;
            for (int i = 1; i <= dividend; i++)
            {
                outputBlock.Text += "\n" + i + "/" + dividend + " = " + i / dividend;
            }

            outputBlock.Text += separator;
            dividend = 9;
            outputBlock.Text += "\nNumber=" + dividend;
            for (int i = 1; i <= dividend; i++)
            {
                outputBlock.Text += "\n" + i + "/" + dividend + " = " + i / dividend;
            }

            outputBlock.Text += separator;
            dividend = 12;
            outputBlock.Text += "\nNumber=" + dividend;
            for (int i = 1; i <= dividend; i++)
            {
                outputBlock.Text += "\n" + i + "/" + dividend + " = " + i / dividend;
            }

            outputBlock.Text += separator;
            dividend = 15;
            outputBlock.Text += "\nNumber=" + dividend;
            for (int i = 1; i <= dividend; i++)
            {
                outputBlock.Text += "\n" + i + "/" + dividend + " = " + i / dividend;
            }

            outputBlock.Text += separator;
            dividend = 16;
            outputBlock.Text += "\nNumber=" + dividend;
            for (int i = 1; i <= dividend; i++)
            {
                outputBlock.Text += "\n" + i + "/" + dividend + " = " + i / dividend;
            }

            outputBlock.Text += separator;
            dividend = 18;
            outputBlock.Text += "\nNumber=" + dividend;
            for (int i = 1; i <= dividend; i++)
            {
                outputBlock.Text += "\n" + i + "/" + dividend + " = " + i / dividend;
            }

            outputBlock.Text += separator;
            dividend = 32;
            outputBlock.Text += "\nNumber=" + dividend;
            for (int i = 1; i <= dividend; i++)
            {
                outputBlock.Text += "\n" + i + "/" + dividend + " = " + i / dividend;
            }

            outputBlock.Text += "\n";
        }
    }
}
