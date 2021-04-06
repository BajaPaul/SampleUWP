using LibraryCoder.Numerics;
using LibraryCoder.UnitConversions;
using System;
using Windows.UI.Xaml.Controls;

/*
 * This sample shows how to use LibraryUnitConversions.  All conversion are done using decimals.  Library methods check for OverflowException.
 * It then throws the exception bach to the calling method to be dealt with properly.  It is up to user to use these exceptions via try-catch 
 * block or live dangerously.  Exceptions not handled by user will occur in library methods.
 */

namespace SampleUWP.ConsoleCodeSamples
{
    internal partial class Samples
    {

        /// <summary>
        /// Convert a double to its exact decimal representation as a string.  
        /// Found this sample in link from this article:  http://csharpindepth.com/Articles/General/FloatingPoint.aspx
        /// More at MSDN on Doubles: https://msdn.microsoft.com/en-us/library/system.double.aspx
        /// 
        /// </summary>
        /// <param name="outputBlock"></param>
        internal static void Sample06_Use_UnitConversions(TextBlock outputBlock)
        {
            outputBlock.Text = "\nSample06_Use_UnitConversions:\n";

            decimal decimal1;
            decimal decimal2;
            decimal decimal3;
            double  double1;
            double  double2;
            double  double3;
            float   float1;
            float   float2;
            float   float3;
            bool exception;


            outputBlock.Text += "\n\n********************************************************************************************************************";
            // No overflow exception checking needed since initialized values are valid for conversion.
            // Set initial decimal value and cast to double and float.
            outputBlock.Text += "\nTest and compare various conversions using initial decimal value.  \nCast decimal to double and float and do conversion to and from with each type.";
            decimal1 = 123456789.123456789m;
            outputBlock.Text += string.Format("\n\nConvert foot to meter.  Initial value is decimal1=[" + decimal1 + "]");
            double1 = (double)decimal1;
            outputBlock.Text += string.Format("\ndecimal1 cast to double1=[" + double1 + "].");
            float1 = (float)decimal1;
            outputBlock.Text += string.Format("\ndecimal1 cast to float1=[" + float1.ToString(LibNum.fpNumericFormatNone) + "].");

            // Convert values via decimal, double, and float overload methods.
            decimal2 = LibUC.ConvertValue(decimal1, EnumConversionsLength.foot, EnumConversionsLength.meter);
            outputBlock.Text += string.Format("\n\nConvert decimal1=[" + decimal1 + "] from foot to meter.  Result is decimal2=[" + decimal2 + "] meters.");
            double2 = LibUC.ConvertValue(double1, EnumConversionsLength.foot, EnumConversionsLength.meter);
            outputBlock.Text += string.Format("\nConvert double1=[" + double1 + "] from foot to meter.  Result is double2=[" + double2 + "] meters.");
            float2 = LibUC.ConvertValue(float1, EnumConversionsLength.foot, EnumConversionsLength.meter);
            outputBlock.Text += string.Format("\nConvert float1=[" + float1.ToString(LibNum.fpNumericFormatNone) + "] from foot to meter.  Result is float2=[" + float2.ToString(LibNum.fpNumericFormatNone) + "] meters.");
            // Check round-trip.  Convert values back via decimal, double, and float overload methods.
            decimal3 = LibUC.ConvertValue(decimal2, EnumConversionsLength.meter, EnumConversionsLength.foot);
            outputBlock.Text += string.Format("\n\nConvert decimal2=[" + decimal2 + "] back from meter to foot.  Result is decimal3=[" + decimal3 + "] feet.");
            double3 = LibUC.ConvertValue(double2, EnumConversionsLength.meter, EnumConversionsLength.foot);
            outputBlock.Text += string.Format("\nConvert double2=[" + double2 + "] back from meter to foot.  Result is double3=[" + double3 + "] feet.");
            float3 = LibUC.ConvertValue(float2, EnumConversionsLength.meter, EnumConversionsLength.foot);
            outputBlock.Text += string.Format("\nConvert float2=[" + float2.ToString(LibNum.fpNumericFormatNone) + "] back from meter to foot.  Result is float3=[" + float3.ToString(LibNum.fpNumericFormatNone) + "] feet.\n");



            outputBlock.Text += "\n\n********************************************************************************************************************";
            // Proper way to prevent exception is to handle them if they occur.
            // Overflow exception checking needed since initialized values are NOT valid for conversion.  Catch OverflowException.
            // Set initial decimal value and cast to double and float.
            outputBlock.Text += "\nTest and compare various conversions using initial decimal value.  \nCast decimal to double and float and do conversion to and from with each type.";
            decimal1 = 79228162514264337593543950335m;
            outputBlock.Text += string.Format("\n\nConvert meter to foot.  Initial value is decimal1=[" + decimal1 + "]");
            double1 = (double)decimal1;
            outputBlock.Text += string.Format("\ndecimal1 cast to double1=[" + double1.ToString(LibNum.fpNumericFormatNone) + "].");
            float1 = (float)decimal1;
            outputBlock.Text += string.Format("\ndecimal1 cast to float1=[" + float1.ToString(LibNum.fpNumericFormatNone) + "].");
            decimal2 = 123m;    // Set to some known value to see if changed by OverflowException.  Nope!
            double2 = 123d;
            float2 = 123f;
            // Convert values via decimal, double, and float overload methods.
            exception = false;
            try
            {
                decimal2 = LibUC.ConvertValue(decimal1, EnumConversionsLength.meter, EnumConversionsLength.foot);
            }
            catch (OverflowException)
            {
                outputBlock.Text += string.Format("\nCaught overflow exception, could not description decimal1=[" + decimal1 + "] from meter to foot.");
                exception = true;
            }
            if (!exception)
                outputBlock.Text += string.Format("\n\nConvert decimal1=[" + decimal1 + "] from meter to foot.  Result is decimal2=[" + decimal2 + "] feet.");
            exception = false;
            try
            {
                double2 = LibUC.ConvertValue(double1, EnumConversionsLength.meter, EnumConversionsLength.foot);
            }
            catch (OverflowException)
            {
                outputBlock.Text += string.Format("\nCaught overflow exception, could not description double1= [" + double1.ToString(LibNum.fpNumericFormatNone) + "] from meter to foot.");
                exception = true;
            }
            if (!exception)
                outputBlock.Text += string.Format("\nConvert double1=[" + double1 + "] from meter to foot.  Result is double2=[" + double2.ToString(LibNum.fpNumericFormatNone) + "] feet.");
            exception = false;
            try
            {
                float2 = LibUC.ConvertValue(float1, EnumConversionsLength.meter, EnumConversionsLength.foot);
            }
            catch (OverflowException)
            {
                outputBlock.Text += string.Format("\nCaught overflow exception, could not description float1=     [" + float1.ToString(LibNum.fpNumericFormatNone) + "] from meter to foot.");
                exception = true;
            }
            if (!exception)
                outputBlock.Text += string.Format("\nConvert float1=[" + float1.ToString(LibNum.fpNumericFormatNone) + "] from meter to foot.  Result is float2=[" + float2.ToString(LibNum.fpNumericFormatNone) + "] feet.");
            // Check round-trip.  Convert values back via decimal, double, and float overload methods.
            // Round-trip not completed with computed values since OverflowException occured.  Used initilized value of decimal2, double2, and float2 instead.
            decimal3 = LibUC.ConvertValue(decimal2, EnumConversionsLength.foot, EnumConversionsLength.meter);
            outputBlock.Text += string.Format("\n\nConvert decimal2=[" + decimal2 + "] back from foot to meter.  Result is decimal3=[" + decimal3 + "] meters.");
            double3 = LibUC.ConvertValue(double2, EnumConversionsLength.foot, EnumConversionsLength.meter);
            outputBlock.Text += string.Format("\nConvert double2=[" + double2 + "] back from foot to meter.  Result is double3=[" + double3 + "] meters.");
            float3 = LibUC.ConvertValue(float2, EnumConversionsLength.foot, EnumConversionsLength.meter);
            outputBlock.Text += string.Format("\nConvert float2=[" + float2.ToString(LibNum.fpNumericFormatNone) + "] back from foot to meter.  Result is float3=[" + float3.ToString(LibNum.fpNumericFormatNone) + "] meters.\n");


            outputBlock.Text += "\n\n********************************************************************************************************************";
            decimal1 = 123456789.123456789m;
            outputBlock.Text += string.Format("\nConvert miles-per hour (MPH) to kilometers-per-hour (KPH).  Initial value is decimal1=[" + decimal1 + "]");
            double1 = (double)decimal1;
            outputBlock.Text += string.Format("\ndecimal1 cast to double1=[" + double1 + "].");
            float1 = (float)decimal1;
            outputBlock.Text += string.Format("\ndecimal1 cast to float1=[" + float1.ToString(LibNum.fpNumericFormatNone) + "].");
            // Convert values via decimal, double, and float overload methods.
            decimal2 = LibUC.ConvertValue(decimal1, EnumConversionsSpeed.mile_per_hour, EnumConversionsSpeed.kilometer_per_hour);
            outputBlock.Text += string.Format("\n\nConvert decimal1=[" + decimal1 + "] from MPH to KPH.  Result is decimal2=[" + decimal2 + "] KPH.");
            double2 = LibUC.ConvertValue(double1, EnumConversionsSpeed.mile_per_hour, EnumConversionsSpeed.kilometer_per_hour);
            outputBlock.Text += string.Format("\nConvert double1=[" + double1 + "] from MPH to KPH.  Result is double2=[" + double2 + "] KPH.");
            float2 = LibUC.ConvertValue(float1, EnumConversionsSpeed.mile_per_hour, EnumConversionsSpeed.kilometer_per_hour);
            outputBlock.Text += string.Format("\nConvert float1=[" + float1.ToString(LibNum.fpNumericFormatNone) + "] from MPH to KPH.  Result is float2=[" + float2.ToString(LibNum.fpNumericFormatNone) + "] KPH.");
            // Check round-trip.  Convert values back via decimal, double, and float overload methods.
            decimal3 = LibUC.ConvertValue(decimal2, EnumConversionsSpeed.kilometer_per_hour, EnumConversionsSpeed.mile_per_hour);
            outputBlock.Text += string.Format("\n\nConvert decimal2=[" + decimal2 + "] back from KPH to MPH.  Result is decimal3=[" + decimal3 + "] MPH.");
            double3 = LibUC.ConvertValue(double2, EnumConversionsSpeed.kilometer_per_hour, EnumConversionsSpeed.mile_per_hour);
            outputBlock.Text += string.Format("\nConvert double2=[" + double2 + "] back from KPH to MPH.  Result is double3=[" + double3 + "] MPH.");
            float3 = LibUC.ConvertValue(float2, EnumConversionsSpeed.kilometer_per_hour, EnumConversionsSpeed.mile_per_hour);
            outputBlock.Text += string.Format("\nConvert float2=[" + float2.ToString(LibNum.fpNumericFormatNone) + "] back from KPH to MPH.  Result is float3=[" + float3.ToString(LibNum.fpNumericFormatNone) + "] MPH.\n");


            outputBlock.Text += "\n\n********************************************************************************************************************";
            // Test what happens on overflow!  This will cause an Overflow exception!!!
            decimal1 = decimal.MaxValue;
            decimal2 = 1m;      // Toggle between 1 or 0 to cause exception or not!
            decimal3 = 0m;
            exception = false;
            try
            {
                //decimal3 = decimal.Add(decimal1, decimal2);
                decimal3 = decimal1 + decimal2;
            }
            catch (OverflowException)
            {
                outputBlock.Text += string.Format("\nCaught overflow exception, did not complete addition: decimal1=[" + decimal1.ToString() + "], decimal2=[" + decimal2.ToString() + "], decimal3=[" + decimal3.ToString() + "].\n");
                exception = true;
            }
            if (!exception)
            {
                decimal3 = decimal1 + decimal2;
                outputBlock.Text += string.Format("\nNo overflow condition, completed addition: decimal1=[" + decimal1.ToString() + "], decimal2=[" + decimal2.ToString() + "], decimal3=[" + decimal3.ToString() + "].\n");
            }


            outputBlock.Text += "\n\n********************************************************************************************************************";
            // Test what happens on underflow!  This will cause an Overflow exception!!!
            decimal1 = decimal.MinValue;
            decimal2 = 0m;      // Toggle between 1 or 0 to cause exception or not!
            exception = false;
            try
            {
                //decimal3 = decimal.Subtract(decimal1, decimal2);
                decimal3 = decimal1 - decimal2;
            }
            catch (OverflowException)
            {
                outputBlock.Text += string.Format("\nCaught overflow exception, did not complete subtraction: decimal1=[" + decimal1.ToString() + "], decimal2=[" + decimal2.ToString() + "].\n");
                exception = true;
            }
            if (!exception)
            {
                decimal3 = decimal1 - decimal2;
                outputBlock.Text += string.Format("\nNo overflow condition, completed subtraction: decimal1=[" + decimal1.ToString() + "], decimal2=[" + decimal2.ToString() + "], decimal3=[" + decimal3.ToString() + "].\n");
            }
            outputBlock.Text += string.Format("\n\nEnd of conversion sample.\n");
        }
    }
}
