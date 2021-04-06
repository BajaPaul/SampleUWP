using System;
using System.ComponentModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using SampleUWP.Common;

namespace SampleUWP
{
    public sealed partial class B09 : Page
    {
        // Declare new variable from Employee class definded in Common folder
        readonly Employee _employee;

        public B09()
        {
            InitializeComponent();

            _employee = new Employee();                     // Make new instance
            Output.DataContext = _employee;                 // Set DataContext source for binding to Grid named Output
            Reset_Click(null, null);                         // Initialize

            _employee.PropertyChanged += Employee_Changed;   // Set PropertyChanged event in code behind
            ShowEmployeeInfo(EmployeeDataModel);            // Initialize TextBlock in 1st column
        }

        // Initialize or reset values on Reset button click
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            _employee.Name = "John Smith";
            _employee.Organization = "Walplace";
            _employee.Age = null;

            // To reset Bindings with NullTargetValue and FallbackValue is necessary to reassign the Bindings
            BindingExpression ageBindingExp = AgeTextBox.GetBindingExpression(TextBox.TextProperty);        // Age is NullTargetValue
            Binding ageBinding = ageBindingExp.ParentBinding;
            AgeTextBox.SetBinding(TextBox.TextProperty, ageBinding);

            BindingExpression salaryBindingExp = SalaryTextBox.GetBindingExpression(TextBox.TextProperty);  // Salary is FallbackValue
            Binding salaryBinding = salaryBindingExp.ParentBinding;
            SalaryTextBox.SetBinding(TextBox.TextProperty, salaryBinding);

            tbBoundDataModelStatus.Text = string.Empty;     // Set default value of status TextBlock
        }

        // Display this aoutmatically in status TextBlock when a property changes.
        private void Employee_Changed(object sender, PropertyChangedEventArgs e)
        {
            tbBoundDataModelStatus.Text = "The property '" + e.PropertyName + "' changed.";     // Handy!

            tbBoundDataModelStatus.Text += "\n\nNew values are:\n";                             // Interesting way to add to a string
            ShowEmployeeInfo(tbBoundDataModelStatus);                                           // Call method below to add more text to the status string
        }

        // Add more to staus TextBlock
        private void ShowEmployeeInfo(TextBlock textBlock)
        {
            textBlock.Text += "\nName: " + _employee.Name;
            textBlock.Text += "\nOrganization: " + _employee.Organization;
            if (_employee.Age == null)
            {
                textBlock.Text += "\nAge: Null";
            }
            else
            {
                textBlock.Text += "\nAge: " + _employee.Age;
            }
        }
        
        // This event happens when Age TextBox lose focus.  Age is initialized null. Seek a valid int for age.
        private void AgeTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            if (int.TryParse(AgeTextBox.Text, out int age))
            {
                _employee.Age = age;
            }
        }
    }
}
