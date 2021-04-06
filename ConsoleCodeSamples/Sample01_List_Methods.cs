using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace SampleUWP.ConsoleCodeSamples
{
    internal partial class Samples
    {
        /// <summary>
        /// UWP method showing various manipulations to a List.
        /// Code from this link: https://msdn.microsoft.com/en-us/library/windows/apps/dwb5h52a(v=vs.105).aspx
        /// </summary>
        /// <param name="outputBlock"></param>
        internal static void Sample01_List_Methods(TextBlock outputBlock)
        {
            outputBlock.Text = "\nSample01_List_Methods:\n";
            List<string> dinosaurs = new List<string>();
            outputBlock.Text += string.Format("\nCapacity: {0}", dinosaurs.Capacity) + "\n";
            dinosaurs.Add("Tyrannosaurus");
            dinosaurs.Add("Amargasaurus");
            dinosaurs.Add("Mamenchisaurus");
            dinosaurs.Add("Deinonychus");
            dinosaurs.Add("Compsognathus");
            outputBlock.Text += "\n";
            foreach (string dinosaur in dinosaurs)
            {
                outputBlock.Text += dinosaur + "\n";
            }
            outputBlock.Text += string.Format("\nCapacity: {0}", dinosaurs.Capacity) + "\n";
            outputBlock.Text += string.Format("Count: {0}", dinosaurs.Count) + "\n";
            outputBlock.Text += string.Format("\nContains(\"Deinonychus\"): {0}", dinosaurs.Contains("Deinonychus")) + "\n";
            outputBlock.Text += string.Format("\nInsert(2, \"Compsognathus\")") + "\n";
            dinosaurs.Insert(2, "Compsognathus");
            outputBlock.Text += "\n";
            foreach (string dinosaur in dinosaurs)
            {
                outputBlock.Text += dinosaur + "\n";
            }
            outputBlock.Text += string.Format("\ndinosaurs[3]: {0}", dinosaurs[3]) + "\n";
            outputBlock.Text += "\nRemove(\"Compsognathus\")" + "\n";

            dinosaurs.Remove("Compsognathus");
            outputBlock.Text += "\n";
            foreach (string dinosaur in dinosaurs)
            {
                outputBlock.Text += dinosaur + "\n";
            }
            dinosaurs.TrimExcess();
            outputBlock.Text += "\nTrimExcess()" + "\n";
            outputBlock.Text += string.Format("Capacity: {0}", dinosaurs.Capacity) + "\n";
            outputBlock.Text += string.Format("Count: {0}", dinosaurs.Count) + "\n";
            dinosaurs.Clear();
            outputBlock.Text += "\nClear()" + "\n";
            outputBlock.Text += string.Format("Capacity: {0}", dinosaurs.Capacity) + "\n";
            outputBlock.Text += string.Format("Count: {0}", dinosaurs.Count) + "\n";
            // Check size after clearing and then trimming.
            dinosaurs.TrimExcess();
            outputBlock.Text += "\nTrimExcess()" + "\n";
            outputBlock.Text += string.Format("Capacity: {0}", dinosaurs.Capacity) + "\n";
            outputBlock.Text += string.Format("Count: {0}", dinosaurs.Count) + "\n";
        }
    }
}
