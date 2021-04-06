using System;
using System.ComponentModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using SampleUWP.Common;

namespace SampleUWP
{
    public sealed partial class B08 : Page
    {
        // Declare new variable from Employee class definded in Common folder
        readonly Employee _employee;

        public B08()
        {
            InitializeComponent();

            //mainPage.HideDebugBar();
            //mainPage.dM1 = "Testing debug message";
            //mainPage.ShowDebugBar();

            _employee = new Employee();                     // Make new instance
            _employee.PropertyChanged += Employee_Changed;   // Add event to trigger
            Output.DataContext = _employee;                 // Set DataContext source for binding to Grid named Output
            Reset_Click(null, null);                         // Initialize
        }

        // Initialize values and reset on Reset button click
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            _employee.Name = "John Smith";
            _employee.Organization = "Walplace";
            BoundDataModelStatus.Text = string.Empty;
        }

        // If a item in _employee changes then it triggers this event and updates the status message
        private void Employee_Changed(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Name"))
            {
                BoundDataModelStatus.Text = "The property '" + e.PropertyName + "' was changed." + "\n\nNew value: " + _employee.Name;  // Update status TextBlock
            }
        }

        // Event linked to Update button.  Updates name into all three boxes.  UpdateSourceTrigger default, Explicit, and PropertyChanged.
        // For more information read the descriptions shown on each case in the app
        // https://msdn.microsoft.com/en-us/library/windows/apps/windows.ui.xaml.data.binding.updatesourcetrigger.aspx
        //
        private void UpdateDataBtn_Click(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            var expression = NameTxtBox.GetBindingExpression(TextBox.TextProperty);     // NameTxtBox is next to the Update button.  This gets contents in TextBox.
            expression.UpdateSource();                                                  // This updates the 3 TextBoxes, all using "Name: as source.
        }
        

    }
}
