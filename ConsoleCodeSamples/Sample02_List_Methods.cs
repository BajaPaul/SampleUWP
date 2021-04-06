using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

// Found sample at: https://msdn.microsoft.com/en-us/library/b0zbh7b6(v=vs.110).aspx

namespace SampleUWP.ConsoleCodeSamples
{
    internal partial class Samples
    {
        /// <summary>
        /// UWP method showing various manipulations to a List including List<T>.Find Method (Predicate<T>)
        /// Code from this link: https://msdn.microsoft.com/en-us/library/x0b5b5bc(v=vs.110).aspx
        /// </summary>
        /// <param name="outputBlock"></param>
        internal static void Sample02_List_Methods(TextBlock outputBlock)
        {
            outputBlock.Text = "\nSample02_List_Methods:\n\n";
            List<Part> parts = new List<Part>
            {
                // Add parts to the list.
                new Part() { PartName = "crank arm", PartId = 1234 },
                new Part() { PartName = "chain ring", PartId = 1334 },
                new Part() { PartName = "regular seat", PartId = 1434 },
                new Part() { PartName = "banana seat", PartId = 1444 },
                new Part() { PartId = 1510 },    // Name intentionally left null for sort sample below.
                new Part() { PartName = "cassette", PartId = 1534 },
                new Part() { PartName = "shift lever", PartId = 1634 }
            };    // Create a list of parts.
            // Write out the parts in the list. This will call the overridden ToString method in the Part class
            foreach (Part aPart in parts)
            {
                outputBlock.Text += aPart + "\n";
            }
            // Check the list for part #1734. This calls the IEquitable.Equals method
            // of the Part class, which checks the PartId for equality.
            outputBlock.Text += string.Format("\nContains: Part with Id=1734: {0}", parts.Contains(new Part { PartId = 1734, PartName = "" }));
            // Find items where name Contains "seat".  Can also use Equal to find exact match.
            outputBlock.Text += string.Format("\nFind: Part where name contains \"seat\": {0}", parts.Find(x => x.PartName.Contains("seat")));
            // Check if an item with Id 1444 exists.
            outputBlock.Text += string.Format("\nExists: Part with Id=1444: {0}\n", parts.Exists(x => x.PartId == 1444));


            // Insert a new item at position 2.
            outputBlock.Text += string.Format("\nInsert(2, \"1834\")\n");
            parts.Insert(2, new Part() { PartName = "brake lever", PartId = 1834 });
            foreach (Part aPart in parts)
            {
                outputBlock.Text += aPart + "\n";
            }

            outputBlock.Text += string.Format("\nParts[3]: {0}\n", parts[3]);


            // Call Sort on the list. This will use the default comparer, 
            //which is the Compare method implemented in Part.


            //parts.Sort();     // TODO: This line throwing exception.  Disable for now until desire to fix!



            outputBlock.Text += string.Format("\nSort by part number via parts.Sort()\n");
            foreach (Part aPart in parts)
            {
                outputBlock.Text += aPart + "\n";
            }

            // This shows calling the Sort(Comparison(T) overload using 
            // an anonymous method for the Comparison delegate. 
            // This method treats null as the lesser of two values.
            parts.Sort(delegate (Part x, Part y)
            {
                if (x.PartName == null && y.PartName == null) return 0;
                else if (x.PartName == null) return -1;
                else if (y.PartName == null) return 1;
                else return x.PartName.CompareTo(y.PartName);
            });
            outputBlock.Text += string.Format("\nSort by part name via parts.Sort(delegate (Part x, Part y)...)\n");
            foreach (Part aPart in parts)
            {
                outputBlock.Text += aPart + "\n";
            }

            outputBlock.Text += string.Format("\nRemove(\"1534\")\n");

            // This will remove part 1534 even though the PartName is different,
            // because the Equals method only checks PartId for equality.
            parts.Remove(new Part() { PartId = 1534, PartName = "cogs" });
            
            foreach (Part aPart in parts)
            {
                outputBlock.Text += aPart + "\n";
            }
            outputBlock.Text += string.Format("\nRemoveAt(3)\n");
            // This will remove the part at index 3.
            parts.RemoveAt(3);
            
            foreach (Part aPart in parts)
            {
                outputBlock.Text += aPart + "\n";
            }


        }


        // Predicate<T> Delegate Samples:  https://msdn.microsoft.com/en-us/library/bfcke1bz(v=vs.110).aspx

    }
}
