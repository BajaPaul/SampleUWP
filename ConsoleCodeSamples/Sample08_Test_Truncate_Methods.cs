using LibraryCoder.Numerics;
using Windows.UI.Xaml.Controls;

/*
 * Verify that the LibraryNumerics LibNum.TruncateFloatingPoint() method overloads are working correctly..
 */

namespace SampleUWP.ConsoleCodeSamples
{
    internal partial class Samples
    {
        internal static void Sample08_Test_Truncate_Methods(TextBlock outputBlock)
        {
            outputBlock.Text = "\nSample08_Test_Truncate_Methods:\n";

            outputBlock.Text += "\nVerify that the LibraryNumerics LibNum.TruncateFloatingPoint() method overloads are working correctly.\n";

            outputBlock.Text += "\nDecimal Overload Test: *************************************************************************\n";
            decimal valDecimal = 359.99999999999999999999999969m;
            int digits = 6;
            outputBlock.Text += "\n    TruncateFloatingPoint(" + valDecimal + ", " + digits + ") = [" + LibNum.TruncateFloatingPoint(valDecimal, digits) + "]";
            digits = 0;
            outputBlock.Text += "\n    TruncateFloatingPoint(" + valDecimal + ", " + digits + ") = [" + LibNum.TruncateFloatingPoint(valDecimal, digits) + "]";
            valDecimal = -359.999999995m;
            digits = 10;
            outputBlock.Text += "\n    TruncateFloatingPoint(" + valDecimal + ", " + digits + ") = [" + LibNum.TruncateFloatingPoint(valDecimal, digits) + "]";
            digits = 9;
            outputBlock.Text += "\n    TruncateFloatingPoint(" + valDecimal + ", " + digits + ") = [" + LibNum.TruncateFloatingPoint(valDecimal, digits) + "]";
            digits = 8;
            outputBlock.Text += "\n    TruncateFloatingPoint(" + valDecimal + ", " + digits + ") = [" + LibNum.TruncateFloatingPoint(valDecimal, digits) + "]";
            valDecimal = 3.5999999999999999999999999997m;
            digits = 6;
            outputBlock.Text += "\n    TruncateFloatingPoint(" + valDecimal + ", " + digits + ") = [" + LibNum.TruncateFloatingPoint(valDecimal, digits) + "]";
            valDecimal = -3.5999999999999999999999999997m; ;
            digits = 8;
            outputBlock.Text += "\n    TruncateFloatingPoint(" + valDecimal + ", " + digits + ") = [" + LibNum.TruncateFloatingPoint(valDecimal, digits) + "]";
            valDecimal = 0.0249999999999999999999996133m; ;
            digits = 8;
            outputBlock.Text += "\n    TruncateFloatingPoint(" + valDecimal + ", " + digits + ") = [" + LibNum.TruncateFloatingPoint(valDecimal, digits) + "]";
            valDecimal = -0.0249999999999999999999996133m;
            digits = 6;
            outputBlock.Text += "\n    TruncateFloatingPoint(" + valDecimal + ", " + digits + ") = [" + LibNum.TruncateFloatingPoint(valDecimal, digits) + "]";
            valDecimal = 0.1499999999999999999999999978m;
            digits = 4;
            outputBlock.Text += "\n    TruncateFloatingPoint(" + valDecimal + ", " + digits + ") = [" + LibNum.TruncateFloatingPoint(valDecimal, digits) + "]";
            valDecimal = -0.1499999999999999999999999978m;
            digits = 4;
            outputBlock.Text += "\n    TruncateFloatingPoint(" + valDecimal + ", " + digits + ") = [" + LibNum.TruncateFloatingPoint(valDecimal, digits) + "]";
            valDecimal = 123456.5m;
            digits = 4;
            outputBlock.Text += "\n    TruncateFloatingPoint(" + valDecimal + ", " + digits + ") = [" + LibNum.TruncateFloatingPoint(valDecimal, digits) + "]";
            valDecimal = -123456.5m;
            digits = 4;
            outputBlock.Text += "\n    TruncateFloatingPoint(" + valDecimal + ", " + digits + ") = [" + LibNum.TruncateFloatingPoint(valDecimal, digits) + "]\n";


            outputBlock.Text += "\nDouble Overload Test: *************************************************************************\n";
            double valDouble = 359.99999999969d;
            digits = 6;
            outputBlock.Text += "\n    TruncateFloatingPoint(" + valDouble + ", " + digits + ") = [" + LibNum.TruncateFloatingPoint(valDouble, digits) + "]";
            digits = 0;
            outputBlock.Text += "\n    TruncateFloatingPoint(" + valDouble + ", " + digits + ") = [" + LibNum.TruncateFloatingPoint(valDouble, digits) + "]";
            valDouble = -359.999999995d;
            digits = 10;
            outputBlock.Text += "\n    TruncateFloatingPoint(" + valDouble + ", " + digits + ") = [" + LibNum.TruncateFloatingPoint(valDouble, digits) + "]";
            digits = 9;
            outputBlock.Text += "\n    TruncateFloatingPoint(" + valDouble + ", " + digits + ") = [" + LibNum.TruncateFloatingPoint(valDouble, digits) + "]";
            digits = 8;
            outputBlock.Text += "\n    TruncateFloatingPoint(" + valDouble + ", " + digits + ") = [" + LibNum.TruncateFloatingPoint(valDouble, digits) + "]";
            valDouble = 3.5999999999997d;
            digits = 6;
            outputBlock.Text += "\n    TruncateFloatingPoint(" + valDouble + ", " + digits + ") = [" + LibNum.TruncateFloatingPoint(valDouble, digits) + "]";
            valDouble = -3.5999999999997d; ;
            digits = 8;
            outputBlock.Text += "\n    TruncateFloatingPoint(" + valDouble + ", " + digits + ") = [" + LibNum.TruncateFloatingPoint(valDouble, digits) + "]";
            valDouble = 0.024999999996133d; ;
            digits = 8;
            outputBlock.Text += "\n    TruncateFloatingPoint(" + valDouble + ", " + digits + ") = [" + LibNum.TruncateFloatingPoint(valDouble, digits) + "]";
            valDouble = -0.024999999996133d;
            digits = 6;
            outputBlock.Text += "\n    TruncateFloatingPoint(" + valDouble + ", " + digits + ") = [" + LibNum.TruncateFloatingPoint(valDouble, digits) + "]";
            valDouble = 0.14999999999978d;
            digits = 4;
            outputBlock.Text += "\n    TruncateFloatingPoint(" + valDouble + ", " + digits + ") = [" + LibNum.TruncateFloatingPoint(valDouble, digits) + "]";
            valDouble = -0.14999999999978d;
            digits = 4;
            outputBlock.Text += "\n    TruncateFloatingPoint(" + valDouble + ", " + digits + ") = [" + LibNum.TruncateFloatingPoint(valDouble, digits) + "]";
            valDouble = 123456.5d;
            digits = 4;
            outputBlock.Text += "\n    TruncateFloatingPoint(" + valDouble + ", " + digits + ") = [" + LibNum.TruncateFloatingPoint(valDouble, digits) + "]";
            valDouble = -123456.5d;
            digits = 4;
            outputBlock.Text += "\n    TruncateFloatingPoint(" + valDouble + ", " + digits + ") = [" + LibNum.TruncateFloatingPoint(valDouble, digits) + "]\n";
            

            outputBlock.Text += "\nFloat Overload Test: *************************************************************************\n";
            float valFloat = 359.9969f;
            digits = 3;
            outputBlock.Text += "\n    TruncateFloatingPoint(" + valFloat + ", " + digits + ") = [" + LibNum.TruncateFloatingPoint(valFloat, digits) + "]";
            digits = 0;
            outputBlock.Text += "\n    TruncateFloatingPoint(" + valFloat + ", " + digits + ") = [" + LibNum.TruncateFloatingPoint(valFloat, digits) + "]";
            valFloat = -359.9969f;
            digits = 10;
            outputBlock.Text += "\n    TruncateFloatingPoint(" + valFloat + ", " + digits + ") = [" + LibNum.TruncateFloatingPoint(valFloat, digits) + "]";
            digits = 4;
            outputBlock.Text += "\n    TruncateFloatingPoint(" + valFloat + ", " + digits + ") = [" + LibNum.TruncateFloatingPoint(valFloat, digits) + "]";
            digits = 3;
            outputBlock.Text += "\n    TruncateFloatingPoint(" + valFloat + ", " + digits + ") = [" + LibNum.TruncateFloatingPoint(valFloat, digits) + "]";
            valFloat = 3.599997f;
            digits = 4;
            outputBlock.Text += "\n    TruncateFloatingPoint(" + valFloat + ", " + digits + ") = [" + LibNum.TruncateFloatingPoint(valFloat, digits) + "]";
            valFloat = -3.599997f; ;
            digits = 3;
            outputBlock.Text += "\n    TruncateFloatingPoint(" + valFloat + ", " + digits + ") = [" + LibNum.TruncateFloatingPoint(valFloat, digits) + "]";
            valFloat = 0.02499613f; ;
            digits = 4;
            outputBlock.Text += "\n    TruncateFloatingPoint(" + valFloat + ", " + digits + ") = [" + LibNum.TruncateFloatingPoint(valFloat, digits) + "]";
            valFloat = -0.02499613f;
            digits = 6;
            outputBlock.Text += "\n    TruncateFloatingPoint(" + valFloat + ", " + digits + ") = [" + LibNum.TruncateFloatingPoint(valFloat, digits) + "]";
            valFloat = 0.1499978f;
            digits = 4;
            outputBlock.Text += "\n    TruncateFloatingPoint(" + valFloat + ", " + digits + ") = [" + LibNum.TruncateFloatingPoint(valFloat, digits) + "]";
            valFloat = -0.1499978f;
            digits = 4;
            outputBlock.Text += "\n    TruncateFloatingPoint(" + valFloat + ", " + digits + ") = [" + LibNum.TruncateFloatingPoint(valFloat, digits) + "]";
            valFloat = 123456.5f;
            digits = 4;
            outputBlock.Text += "\n    TruncateFloatingPoint(" + valFloat + ", " + digits + ") = [" + LibNum.TruncateFloatingPoint(valFloat, digits) + "]";
            valFloat = -123456.5f;
            digits = 4;
            outputBlock.Text += "\n    TruncateFloatingPoint(" + valFloat + ", " + digits + ") = [" + LibNum.TruncateFloatingPoint(valFloat, digits) + "]\n";
        }
    }
}
