using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace SampleUWP
{
    public sealed partial class B01 : Page
    {
        public B01()
        {
            InitializeComponent();

            //mainPage.HideDebugBar();
            //mainPage.dM1 = "Testing debug message";
            //mainPage.ShowDebugBar();
        }

        // Top Reset button
        private void TopReset_Click(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            sliderValueConverter.Value = 70;
        }

        // Bottom Reset button
        private void BottomReset_Click(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            sliderOneWayDataSource.Value = 10;
            sliderTwoWayDataSource.Value = 50;
            sliderOneTimeDataSource.Value = 100;
        }
    }

    // For more information on Value Converters, see http://go.microsoft.com/fwlink/?LinkId=254639#data_conversions
    public class S2Formatter : IValueConverter
    {
        //Convert the slider value into Grades
        public object Convert(object value, System.Type type, object parameter, string language)
        {
            string _grade = string.Empty;
            //try parsing the value to int
            if (int.TryParse(value.ToString(), out int _value))
            {
                if (_value < 50)
                    _grade = "F";
                else if (_value < 60)
                    _grade = "D";
                else if (_value < 70)
                    _grade = "C";
                else if (_value < 80)
                    _grade = "B";
                else if (_value < 90)
                    _grade = "A";
                else if (_value < 100)
                    _grade = "A+";
                else if (_value == 100)
                    _grade = "SUPER STAR!";
            }
            return _grade;
        }

        // No need to implement converting back on a one-way binding
        public object ConvertBack(object value, System.Type type, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
