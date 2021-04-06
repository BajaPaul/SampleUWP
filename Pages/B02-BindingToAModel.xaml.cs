using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.ComponentModel;
using SampleUWP.Common;

namespace SampleUWP
{
    public sealed partial class B02 : Page
    {
        // A pointer to MainPage is needed if you want to call methods or variables in MainPage.
        private readonly MainPage mainPage = MainPage.mainPagePointer;

        private Employee _employee;

        public B02()
        {
            this.InitializeComponent();

            //mainPage.HideDebugBar();
            //mainPage.dM1 = "Testing debug message";
            //mainPage.ShowDebugBar();

            Reset_Click(null, null);    // Call event below with no/null arguments to initialize defaults, since 2-way binding
        }
        
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            _employee = new Employee
            {
                Name = "John Doe",
                Organization = "Walplace",
                Age = 30
            };     // Create new instance with scope to this whole page

            _employee.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(employeeChanged);
                        
            mainPage.DataContext = _employee;
            tbBoundDataModelStatus.Text = "";
        }

        private void employeeChanged(object sender, PropertyChangedEventArgs e)
        {
            tbBoundDataModelStatus.Text = "The property:'" + e.PropertyName + "' was changed";
        }

    }
}
