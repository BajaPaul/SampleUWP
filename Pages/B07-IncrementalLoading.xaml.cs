using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System;
using System.Collections.Specialized;
using SampleUWP.Common;

namespace SampleUWP
{
    public sealed partial class B07 : Page
    {
        /*
        The following statement declares variable employees as a GeneratorIncrementalLoadingClass with class of Employee.
        GeneratorIncrementalLoadingClass.cs is defined in file in Common folder.  It is derived from IncrementalLoadingBase.cs file
        also in Common folder.  More about incremental loading virtualization:
        https://msdn.microsoft.com/en-us/library/windows/apps/xaml/hh780657.aspx
        https://msdn.microsoft.com/en-us/library/windows/apps/windows.ui.xaml.data.isupportincrementalloading.aspx
        */
        private GeneratorIncrementalLoadingClass<Employee> employees;   // See Common folder for class contents

        public B07()
        {
            this.InitializeComponent();

            //mainPage.HideDebugBar();
            //mainPage.dM1 = "Testing debug message";
            //mainPage.ShowDebugBar();
        }

        // This is called to initialize and reset sample when Load Data button is clicked
        private void LoadData_Click(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            if (employees != null)
            {
                employees.CollectionChanged -= Employees_CollectionChanged;    // Delete event since will be added right back below
            }
            // Start loading the data into employees
            employees = new GeneratorIncrementalLoadingClass<Employee>(1000, (count) => {
                return new Employee() { Name = "Name" + count, Organization = "Organization" + count };
            });

            employees.CollectionChanged += Employees_CollectionChanged;    // Add event that triggers when collection changes

            employeesCVS.Source = employees;                                // Set source for binding in XAML
            tbCollectionChangeStatus.Text = string.Empty;                   // Set default status message
        }

        // The collection has changed so automatically update the status message
        private void Employees_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            tbCollectionChangeStatus.Text = string.Format("Collection was changed. Count = {0}", employees.Count);
        }
    }
}
