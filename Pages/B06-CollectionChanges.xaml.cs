using SampleUWP.Common;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;   // Required for this sample to add Observable collection functionality
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SampleUWP
{
    public sealed partial class B06 : Page
    {
        /*
        The following statement is what this sample is all about.  This declares variable ocTeams as a 
        ObservableCollection with class of Team.  An ObservableCollection is a dynamic collection that provides
        notification via NotifyCollectionChangedEventArgs event when items get added or removed, or whole 
        collection is refreshed.
        */
        private ObservableCollection<Team> ocTeams;

        public B06()
        {
            this.InitializeComponent();

            //mainPage.HideDebugBar();
            //mainPage.dM1 = "Testing debug message";
            //mainPage.ShowDebugBar();

            btnRemoveTeam.Click += BtnRemoveTeam_Click;     // This event is added to Remove Team button via code versus XAML
            Reset_Click(null, null);                  // Set up the sorce so binding can find and load it
        }

        // This is called to initialize and reset sample via the Reset button
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            _ = sender;     // Discard unused parameter.
            _ = e;          // Discard unused parameter.
            if (ocTeams!=null)
                ocTeams.CollectionChanged -= OCTeams_CollectionChanged;    // Need to remove this since it will be added back right below
            
            ocTeams = new ObservableCollection<Team>(new Teams());      // past a new instance of the Teams to the new instance of ocTeams
            ocTeams.CollectionChanged += OCTeams_CollectionChanged;    // Add event that triggers when collection changes
                        
            teamsCVS.Source = ocTeams;                      // Set up source for binding team names and colors
            statusChangedTextBlock.Text = "ObservableCollection Status";     // Set default status message in TextBlock
        }

        // This event occurs when Remove Team button is clicked.  It was bound to button via code above versus through XAML.
        private void BtnRemoveTeam_Click(object sender, RoutedEventArgs e)
        {
            if (ocTeams.Count > 0)      // Then delete the selected team
            {
                int index=0;
                if (lvTeams.SelectedItem != null)       // Don't do anything if a team is not selected
                    index = lvTeams.SelectedIndex;
                ocTeams.RemoveAt(index);                // Delete the team.  Once deleted the following event is
                                                        // triggered and status TextBox is automatically updated.
                                                        // Real slick but may be just about as easy to do manually though code...
            }
        }

        // Automatically update status message in TextBlock after change in ObservableCollection triggers this event
        private void OCTeams_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            statusChangedTextBlock.Text = string.Format("Collection was changed. Count = {0}", ocTeams.Count);
        }

    }
}
