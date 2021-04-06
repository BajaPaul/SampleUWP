using LibraryCoder.UtilitiesMisc;
using System;
using Windows.UI.Xaml.Controls;

/*
 * This sample runs various code snipplets initializing, using, and validating enumerations.
 * 
 * 
 * This bit of code was thowing me for a loop since could not convert a enum into a system base Enum without compiler error!!!!
 * 
 * Big revelation was to cast the enum using "newEnum = oldEnum as Enum".
 * 
 * Seems simple but would not convert any other way!!!  Days spent searching for this small discovery.
 * 
 * Note: Keyword 'as' always returns null on fault versus an exception, therefor always check if not null
 * immediately after using the 'as' keyword before proceeding.
 * 
 * newEnum = oldEnum as Enum;       // Compiler would not convert this without using 'as' keyword!!!
 * if (getEnum != null)
 * {
 *     ........
 * }
 * 
 * Also note that comparing one enum to another with the '==' operator may not show a match.
 * 
 * Use Equals operator to compare two enums.
 * 
 * if (item.conversion.Equals(currentType.saveEnumInput))
 * {
 *     ........
 * }
 *  
 */

namespace SampleUWP.ConsoleCodeSamples
{
    internal partial class Samples
    {

        enum ArrivalStatus { Unknown = -3, Late = -1, OnTime = 0, Early = 1 };
        enum Color { Red = 1, Blue = 2, Green = 3 }
        [Flags] enum Colors { None = 0, Red = 1, Green = 2, Blue = 4 };


        // DescriptionAttribute is not available for UWP apps. So con not use with Enums!  Two alternatives shown here:
        // http://stackoverflow.com/questions/18912697/system-componentmodel-descriptionattribute-in-portable-class-library
        //
        // Various methods to use the attributes here: http://stackoverflow.com/questions/4367723/get-enum-from-description-attribute


        //public enum InstallationType {[Display(Description = "Forward of Bulk Head")] FORWARD, [Display(Description = "Rear of Bulk Head")] REAR, [Display(Description = "Roof Mounted")] ROOF}

        //enum Animal
        //{
        //    [Display(Description = "")]
        //    NotSet = 0,

        //    [Display(Description = "Giant Panda")]
        //    GiantPanda = 1,

        //    [Display(Description = "Lesser Spotted Anteater")]
        //    LesserSpottedAnteater = 2
        //}


        /***** Use .Net code above versus building enumeration description handling from scratch since this works with UWP apps.  *****/
        // For more information: https://msdn.microsoft.com/en-us/library/system.componentmodel.dataannotations.displayattribute(v=vs.110).aspx


        // Sample code not used or tested but SAVE for future use.  VS not thowing any errors.
        //
        // Build your own DescriptionAttribute.....
        //
        //[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]


        //public class EnumDescriptionAttribute : Attribute
        //{
        //    private readonly string description;
        //    public string Description { get { return description; } }
        //    public EnumDescriptionAttribute(string description)
        //    {
        //        this.description = description;
        //    }
        //}

        // Sample code not used or tested but SAVE for future use.  VS not thowing any errors.
        //
        //enum Foo {[EnumDescription("abc")] A, [EnumDescription("def")] B }

        // Sample code not used or tested but SAVE for future use.  VS not thowing any errors.
        //
        //enum UserColours
        //{
        //    [EnumDescription("Burnt Orange")]
        //    BurntOrange = 1,
        //    [EnumDescription("Bright Pink")]
        //    BrightPink = 2,
        //    [EnumDescription("Dark Green")]
        //    DarkGreen = 3,
        //    [EnumDescription("Sky Blue")]
        //    SkyBlue = 4
        //}

        // Sample code not used or tested but SAVE for future use.  VS not thowing any errors.
        //
        //public static string GetEnumDescriptionFromEnumValue(Enum value)
        //{
        //    EnumDescriptionAttribute attribute = value.GetType()
        //        .GetField(value.ToString())
        //        .GetCustomAttributes(typeof(EnumDescriptionAttribute), false)
        //        .SingleOrDefault() as EnumDescriptionAttribute;
        //    return attribute == null ? value.ToString() : attribute.Description;
        //}

        ///*
        // * Sample code not used or tested but SAVE for future use.
        // * 
        // * This is not working...
        // * 
        //public static T GetEnumValueFromDescription<T>(string description)
        //{
        //    var type = typeof(T);
        //    if (!type.IsEnum)
        //        throw new ArgumentException();
        //    FieldInfo[] fields = type.GetFields();
        //    var field = fields
        //                    .SelectMany(f => f.GetCustomAttributes(
        //                        typeof(DisplayAttribute), false), (
        //                            f, a) => new { Field = f, Att = a })
        //                    .Where(a => ((DisplayAttribute)a.Att)
        //                        .Description == description).SingleOrDefault();
        //    return field == null ? default(T) : (T)field.Field.GetRawConstantValue();
        //}
        //*/


        ///*
        // * Sample code not used or tested but SAVE for future use.
        // * 
        // * 
        //public static string GetDescriptionFromEnumValueOriginal(Enum value)
        //{
        //    DescriptionAttribute attribute = value.GetType()
        //        .GetField(value.ToString())
        //        .GetCustomAttributes(typeof(DescriptionAttribute), false)
        //        .SingleOrDefault() as DescriptionAttribute;
        //    return attribute == null ? value.ToString() : attribute.Description;
        //}

        //public static T GetEnumValueFromDescriptionOriginal<T>(string description)
        //{
        //    var type = typeof(T);
        //    if (!type.IsEnum)
        //        throw new ArgumentException();
        //    FieldInfo[] fields = type.GetFields();
        //    var field = fields
        //                    .SelectMany(f => f.GetCustomAttributes(
        //                        typeof(DescriptionAttribute), false), (
        //                            f, a) => new { Field = f, Att = a })
        //                    .Where(a => ((DescriptionAttribute)a.Att)
        //                        .Description == description).SingleOrDefault();
        //    return field == null ? default(T) : (T)field.Field.GetRawConstantValue();
        //}
        //*/


        internal static void Sample09_Enum_Code_Sampler(TextBlock outputBlock)
        {
            outputBlock.Text = "\nSample09_Enum_Code_Sampler:\n";
            outputBlock.Text += "\nThis sample runs various code snipplets initializing, using, and validating enumerations.\n";

            outputBlock.Text += "\nExample01: Check if integer is valid member of an enum: \nCode from: https://msdn.microsoft.com/en-us/library/system.enum.aspx \n";
            int[] values = { -3, -1, 0, 1, 5, int.MaxValue };
            ArrivalStatus status;
            foreach (int value in values)
            {
                if (Enum.IsDefined(typeof(ArrivalStatus), value))   // Check if int is members of enum.
                    status = (ArrivalStatus)value;                  // Cast int to enum.
                else
                    status = ArrivalStatus.Unknown;                 // Not member of enum so cast to 'Unknown'.
                outputBlock.Text += string.Format("\nConverted {0:N0} to {1}", value, status);
            }
            // Displays the following output:
            //       Converted -3 to Unknown
            //       Converted -1 to Late
            //       Converted 0 to OnTime
            //       Converted 1 to Early
            //       Converted 5 to Unknown
            //       Converted 2,147,483,647 to Unknown

            status = ArrivalStatus.Early;
            var aNumber = (int)Convert.ChangeType(status, Enum.GetUnderlyingType(typeof(ArrivalStatus)));
            outputBlock.Text += string.Format("\nConverted {0} to {1}\n", status, aNumber);
            // Displays the following output:
            //       Converted Early to 1

            outputBlock.Text += "\nExample02: Parse, TryParse, Enum.IsDefined methods: \nCode from: https://msdn.microsoft.com/en-us/library/system.enum.aspx \n";
            // Note that the parsing methods will successfully convert string representations of numbers that are not members 
            // of a particular enumeration if the strings can be converted to a value of the enumeration's underlying type.
            // To prevent this, the IsDefined method can be called to ensure that the result of the parsing method is a valid 
            // enumeration value.
            string numberString = "-1";
            string name = "Early";
            ArrivalStatus status1;
            try
            {
                status1 = (ArrivalStatus)Enum.Parse(typeof(ArrivalStatus), numberString);
                if (!(Enum.IsDefined(typeof(ArrivalStatus), status1)))      // Validate if converted value is member of enum.
                    status1 = ArrivalStatus.Unknown;
                outputBlock.Text += string.Format("\nConverted '{0}' to {1}", numberString, status1);
            }
            catch (FormatException)     // Parse throws exception if it could not make conversion.
            {
                outputBlock.Text += string.Format("\nUnable to convert '{0}' to an ArrivalStatus value.", numberString);
            }
            // equivalent TryParse sample.
            if (Enum.TryParse<ArrivalStatus>(name, out ArrivalStatus status2))    // Generic with T = enum.
            {
                if (!(Enum.IsDefined(typeof(ArrivalStatus), status2)))  // Validate if converted value is member of enum.
                    status2 = ArrivalStatus.Unknown;
                outputBlock.Text += string.Format("\nConverted '{0}' to {1}", name, status2);
            }
            else
            {
                outputBlock.Text += string.Format("\nUnable to convert '{0}' to an ArrivalStatus value.", numberString);
            }
            // Displays the following output:
            //       Converted '-1' to Late
            //       Converted 'Early' to Early
            //

            outputBlock.Text += "\n\nExample03: Format enumeration values: \nCode from: https://msdn.microsoft.com/en-us/library/c3s1ez6e.aspx \n";
            // Format enumeration values:
            string[] formats = { "G", "F", "D", "X" };
            status = ArrivalStatus.Late;
            foreach (var fmt in formats)
                outputBlock.Text += string.Format("\n" + status.ToString(fmt));
            // Displays the following output:
            //       Late
            //       Late
            //       -1
            //       FFFFFFFF
            Color myColor = Color.Green;
            outputBlock.Text += string.Format("\nThe value of myColor is {0}.", myColor.ToString("G"));
            outputBlock.Text += string.Format("\nThe value of myColor is {0}.", myColor.ToString("F"));
            outputBlock.Text += string.Format("\nThe value of myColor is {0}.", myColor.ToString("D"));
            outputBlock.Text += string.Format("\nThe value of myColor is 0x{0}.\n", myColor.ToString("X"));
            // Displays the following output to the console:
            //       The value of myColor is Green.
            //       The value of myColor is Green.
            //       The value of myColor is 3.
            //       The value of myColor is 0x00000003.

            outputBlock.Text += "\nExample04: Iterating enumeration members: \nCode from: https://msdn.microsoft.com/en-us/library/system.enum.aspx \n";
            // Iterating enumeration members:  You can enumerate members in either of two ways.
            // GetNames Method:
            string[] names = Enum.GetNames(typeof(ArrivalStatus));      // Get an arrary of name values from a enum.
            Array.Sort(names);
            outputBlock.Text += string.Format("\nGetNames Method: Sorted members of {0}:", typeof(ArrivalStatus).Name);
            foreach (string item in names)  // Iterate the array of names.
            {
                status = (ArrivalStatus)Enum.Parse(typeof(ArrivalStatus), item);    // Convert the name back into enum.
                outputBlock.Text += string.Format("\n   {0} ({0:D})", status);
            }
            // Displays the following output:
            //       Members of ArrivalStatus:
            //          Early (1)
            //          Late (-1)
            //          OnTime (0)
            //          Unknown (-3
            // 
            // GetValues Method
            var enumValues = Enum.GetValues(typeof(ArrivalStatus));     // Can't really replace 'var' with a type since it is 'Array'.
            outputBlock.Text += string.Format("\n\nGetValues Method: Unsorted members of {0}:", typeof(ArrivalStatus).Name);
            foreach (var item in enumValues)
            {
                status = (ArrivalStatus)Enum.ToObject(typeof(ArrivalStatus), item);
                outputBlock.Text += string.Format("\n   {0} ({0:D})", status);
            }
            // The example displays the following output:
            //       Members of ArrivalStatus:
            //          OnTime (0)
            //          Early (1)
            //          Unknown (-3)
            //          Late (-1)


            /*
             * 
             * 
             * Big deal here!!!!!!!
             * 
             * 
             */

            outputBlock.Text += "\nExample05: Iterating enumeration members directly!:";
            // Apparently can iterate enumerations directly contrary to what is said above.  Which is more efficeint depends on what compiler is doing.
            //
            // Basically, all that was done here was to eliminate the 'var' variable 'enumValues' and move code directly to the 'foreach' loop.
            //
            outputBlock.Text += string.Format("\n\nDirect iterating method: Members of {0}:", typeof(ArrivalStatus).Name);
            foreach (ArrivalStatus item in Enum.GetValues(typeof(ArrivalStatus)))
            {
                outputBlock.Text += string.Format("\n   {0} ({0:D})", item);
            }




            outputBlock.Text += "\n\nExample06: Nice TyParse sample: \nCode from: https://msdn.microsoft.com/en-us/library/dd991317.aspx \n";
            string[] colorStrings = { "0", "2", "8", "blue", "Blue", "Yellow", "Red, Green" };  // Note last string item...
            foreach (string colorString in colorStrings)
            {
                if (Enum.TryParse(colorString, true, out Colors colorValue))
                    if (Enum.IsDefined(typeof(Colors), colorValue) | colorValue.ToString().Contains(","))
                        outputBlock.Text += string.Format("\nConverted '{0}' to {1}.", colorString, colorValue.ToString());
                    else
                        outputBlock.Text += string.Format("\n{0} is not an underlying value of the Colors enumeration.", colorString);
                else
                    outputBlock.Text += string.Format("\n{0} is not a member of the Colors enumeration.", colorString);
            }
            // The example displays the following output:
            //       Converted '0' to None.
            //       Converted '2' to Green.
            //       8 is not an underlying value of the Colors enumeration.
            //       Converted 'blue' to Blue.
            //       Converted 'Blue' to Blue.
            //       Yellow is not a member of the Colors enumeration.
            //       Converted 'Red, Green' to Red, Green.

            outputBlock.Text += "\n\nExample07: Enum Descrption attribute sample using DisplayAtrribute: ";
                        
            //outputBlock.Text += "\n" + LibUM.EnumShowValues<Animal>();
            //outputBlock.Text += "\n" + LibUM.EnumShowValues<InstallationType>(true);
            //outputBlock.Text += "\n" + LibUM.EnumShowValues<ConversionType>();
            //outputBlock.Text += "\n" + LibUM.EnumShowValues<Angle>(true);

            outputBlock.Text += "\n\nExample08: Off subject of Enum's but need to test LibUM.ReverseString() \nmethod using strings with control characters.\n";
            // Test LibUM.ReverseString() methods.  Seems to be picking up '\n' characters correctly.

            //outputBlock.Text += "\n" + LibUM.ReverseString(LibUM.EnumShowValues<Angle>(true));

            string string1 = "\nUnicode in C#:   Hamburger=\uE700,  Home=\uE80F,  Back=\uE72B,  Forward=\uE72A,  Page=\uE7C3";
            string string2 = "\nUnicode in XAML: \nHamburger = &#xE700, \nHome=&#xE80F, \nBack=&#xE72B, \nForward=&#xE72A, \nPage=&#xE7C3";
            outputBlock.Text += string1;
            outputBlock.Text += "\n\nReversed string above:\n" + LibUM.ReverseString(string1);
            outputBlock.Text += string2;
            outputBlock.Text += "\n\nReversed string above:\n " + LibUM.ReverseString(string2);
            outputBlock.Text += "\n\n";     // Finish up with a couple of LF's.
        }

    }
}
