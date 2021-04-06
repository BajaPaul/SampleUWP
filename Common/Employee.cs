using System.ComponentModel;

// More information on INotifyPropertyChanged can be found @ http://go.microsoft.com/fwlink/?LinkId=254639#change_notification
// For another way to accomplish this, see SampleDataSource.cs and BindableBase.cs in the Grid Application C# project template.
// More at: http://jeffhandley.com/archive/2008/07/09/binding-to-nullable-values-in-xaml.aspx

//Implement INotifiyPropertyChanged interface to subscribe for property change notifications

namespace SampleUWP.Common      // This is used in samples B02-Reset_Click, B07-IncrementalLoading, B08-UpdateSourceTrigger, and B09-Fallback-TargetNull.
{
    public class Employee : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _name;
        private string _organization;
        private int? _age;              // Question mark makes int? a nullable type.  If it hasn't been assigned, it equals null.
                                        // See more about Nullables at: https://msdn.microsoft.com/en-us/library/2cf62fcy.aspx

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    RaisePropertyChanged("Name");
                }
            }
        }

        public string Organization
        {
            get { return _organization; }
            set
            {
                if (_organization != value)
                {
                    _organization = value;
                    RaisePropertyChanged("Organization");
                }
            }
        }

        public int? Age
        {
            get { return _age; }
            set
            {
                if (_age != value)
                {
                    _age = value;
                    RaisePropertyChanged("Age");
                }
            }
        }

        protected void RaisePropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
