using System;
using System.Collections.Generic;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace SampleUWP
{
    public sealed partial class P15 : Page
    {
        private readonly string outputText = "Use above ComboBox to change font of this text:  Current font is ";

        private List<Tuple<string, FontFamily>> ComboBoxFonts { get; } = new List<Tuple<string, FontFamily>>()
        {
            new Tuple<string, FontFamily>("Arial", new FontFamily("Arial")),
            new Tuple<string, FontFamily>("Comic Sans MS", new FontFamily("Comic Sans MS")),
            new Tuple<string, FontFamily>("Courier New", new FontFamily("Courier New")),
            new Tuple<string, FontFamily>("Segoe UI", new FontFamily("Segoe UI")),
            new Tuple<string, FontFamily>("Times New Roman", new FontFamily("Times New Roman"))
        };

        public P15()
        {
            InitializeComponent();
            
            // Setup Example3 via following code behind versus XAML binding shown on following line.
            // <ComboBox x:Name="cboxExample3" ItemsSource="{x:Bind comboBoxFonts}" DisplayMemberPath="Item1" SelectedValuePath="Item2".../>
            CboxSample3.ItemsSource = ComboBoxFonts;
            CboxSample3.DisplayMemberPath = "Item1";
            CboxSample3.SelectedValuePath = "Item2";
            CboxSample3.SelectedIndex = 2;

            // Create list and add items to it.
            List<string> cbox4ItemSource = new List<string>
            {
                "Item 0",
                "Item 1",
                "Item 2",
                "Item 3",
                "Item 4"
            };
            CboxSample4.ItemsSource = cbox4ItemSource;  // Set the list as ComboBox source.
            CboxSample4.SelectedIndex = 0;              // Select item in ComboBox to trigger box setup.  Otherwise box will appear empty.
        }

        /// <summary>
        /// Invoked when color changed in ColorComboBox.  Changes color of rectangle below ComboBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboxSample1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            string colorName = e.AddedItems[0].ToString();
            Color color;
            switch (colorName)
            {
                case "Red":
                    color = Colors.Red;
                    break;
                case "Green":
                    color = Colors.Green;
                    break;
                case "Blue":
                    color = Colors.Blue;
                    break;
                case "Yellow":
                    color = Colors.Yellow;
                    break;
            }
            Control1Output.Fill = new SolidColorBrush(color);       // Change color of rectangle below ComboBox.
        }

        /// <summary>
        /// Invoked when ComboBox cboxExample2 is loaded to set initial font of text.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboxSample2_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            CboxSample2.SelectedIndex = 1;     // Set initial font to "Segoe UI".
        }

        /// <summary>
        /// Invoked when ComboBox cboxExample2 item is changed and updates text string accordingly.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboxSample2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _ = e;          // Discard unused parameter.
            if (sender is ComboBox comboBox)
                Control2Output.Text = outputText + ComboBoxFonts[comboBox.SelectedIndex].Item1 + ".";   // Update text string when a new font is selected.
        }

        /// <summary>
        /// Invoked when ComboBox cboxExample3 item is changed and updates text string accordingly.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboxSample3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _ = e;          // Discard unused parameter.
            if (sender is ComboBox comboBox)
                Control3Output.Text = outputText + ComboBoxFonts[comboBox.SelectedIndex].Item1 + ".";   // Update text string when a new font is selected.
        }

        /// <summary>
        /// Invoked when ComboBox cboxExample4 item is changed and updates text string accordingly.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboxSample4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            // Don't need to use 'sender' casting code used above if event is tied to a specific ComboBox.
            Control4Output.Text = "Selected item: " + CboxSample4.SelectedItem.ToString(); ;   // Update text string when selected.
        }

    }

    /*
     * 
     *  Don't really need to go to difficulty of using templates below to set up ComboBox binding when so much simpler to do it in code behind via ItemsSource.
     * 
     */


    // Used for binding.  XAML calls this via ItemTemplate in Page.Resources to change the text font of TextBlock Control2Output.
    internal class Example2ValueToFontFamilyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null && value is FontFamily)
            {
                return (FontFamily)value;           // Return font value selected from ComboBox.
            }
            return new FontFamily("Segoe UI");      // Default
        }

        // No need to implement converting back on a one-way binding
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    // Used for binding.  XAML calls this via ItemTemplate in Page.Resources to change the text font of TextBlock Control2Output.
    internal class Example3ValueToFontFamilyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null && value is FontFamily)
            {
                return (FontFamily)value;           // Return font value selected from ComboBox.
            }
            return new FontFamily("Segoe UI");      // Default
        }

        // No need to implement converting back on a one-way binding
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

}
