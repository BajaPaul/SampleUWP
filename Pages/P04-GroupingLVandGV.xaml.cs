using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// This GridView Item Grouping sample from: https://msdn.microsoft.com/en-us/library/windows/apps/xaml/hh780627.aspx
// More about grouping here: https://msdn.microsoft.com/en-us/library/system.windows.controls.itemscontrol.groupstyle(v=vs.110).aspx

namespace SampleUWP
{
    public sealed partial class P04 : Page
    {
        // A pointer to MainPage is needed if you want to call methods or variables in MainPage.
        private readonly MainPage mainPage = MainPage.mainPagePointer;

        private DateTime startDate;     // Varible for use in methods below.  this is set by the OnNavigatedTo event below.

        public P04()
        {
            InitializeComponent();

            // Setup mainScroller to handle scrolling for this page.
            mainPage.MainScrollerOn(horz: ScrollMode.Auto, vert: ScrollMode.Auto, horzVis: ScrollBarVisibility.Auto,
                                    vertVis: ScrollBarVisibility.Auto, zoom: ZoomMode.Enabled);

            //mainPage.HideDebugBar();
            //mainPage.dM1 = "Testing debug message";
            //mainPage.dM2 = "Greeting is " + (string)Resources["Greeting"];
            //mainPage.ShowDebugBar();

            // Page caching disabled
            //this.NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.  This is a override so can not change name!
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DateTime.TryParse("3/1/2016", out startDate);       // Set the Start Date by outing it!
            PopulateProjects();
            PopulateActivities();
        }

        private void PopulateActivities()
        {
            List<Activity> Activities = new List<Activity>
            {
                new Activity() { Name = "Activity 1", Complete = true, DueDate = startDate.AddDays(4), Project = "Project 1" },
                new Activity() { Name = "Activity 2", Complete = true, DueDate = startDate.AddDays(5), Project = "Project 1" },
                new Activity() { Name = "Activity 3", Complete = false, DueDate = startDate.AddDays(7), Project = "Project 1" },
                new Activity() { Name = "Activity 4", Complete = false, DueDate = startDate.AddDays(9), Project = "Project 1" },
                new Activity() { Name = "Activity 5", Complete = false, DueDate = startDate.AddDays(14), Project = "Project 1" },
                new Activity() { Name = "Activity A", Complete = true, DueDate = startDate.AddDays(2), Project = "Project 2" },
                new Activity() { Name = "Activity B", Complete = false, DueDate = startDate.AddDays(4), Project = "Project 2" },
                new Activity() { Name = "Activity C", Complete = true, DueDate = startDate.AddDays(5), Project = "Project 2" },
                new Activity() { Name = "Activity D", Complete = false, DueDate = startDate.AddDays(9), Project = "Project 2" },
                new Activity() { Name = "Activity E", Complete = false, DueDate = startDate.AddDays(18), Project = "Project 2" }
            };

            /*
            What follows is a LINQ Query Expression.  See much more at following links:
            https://msdn.microsoft.com/en-us/library/bb397676.aspx
            https://msdn.microsoft.com/en-us/library/bb310804.aspx 
            https://code.msdn.microsoft.com/101-LINQ-Samples-3fb9811b  

            More about grouping items after you queried them.
            https://msdn.microsoft.com/en-us/library/windows/apps/xaml/hh780627.aspx
            https://msdn.microsoft.com/en-us/library/windows/apps/xaml/hh780650.aspx
            */
            var result = from act in Activities
                         group act by act.Project into grp
                         orderby grp.Key
                         select grp;

            // cvs = CollectionViewSource
            cvsActivities.Source = result;   // Store the results here to be loaded by binding process
        }

        private void PopulateProjects()
        {
            List<Project> Projects = new List<Project>();

            Project newProject = new Project
            {
                Name = "Project 1"
            };
            newProject.Activities.Add(new Activity() { Name = "Activity 1", Complete = true, DueDate = startDate.AddDays(4) });
            newProject.Activities.Add(new Activity() { Name = "Activity 2", Complete = true, DueDate = startDate.AddDays(5) });
            newProject.Activities.Add(new Activity() { Name = "Activity 3", Complete = false, DueDate = startDate.AddDays(7) });
            newProject.Activities.Add(new Activity() { Name = "Activity 4", Complete = false, DueDate = startDate.AddDays(9) });
            newProject.Activities.Add(new Activity() { Name = "Activity 5", Complete = false, DueDate = startDate.AddDays(14) });
            Projects.Add(newProject);

            newProject = new Project
            {
                Name = "Project 2"
            };
            newProject.Activities.Add(new Activity() { Name = "Activity A", Complete = true, DueDate = startDate.AddDays(2) });
            newProject.Activities.Add(new Activity() { Name = "Activity B", Complete = false, DueDate = startDate.AddDays(3) });
            newProject.Activities.Add(new Activity() { Name = "Activity C", Complete = true, DueDate = startDate.AddDays(5) });
            newProject.Activities.Add(new Activity() { Name = "Activity D", Complete = false, DueDate = startDate.AddDays(9) });
            newProject.Activities.Add(new Activity() { Name = "Activity E", Complete = false, DueDate = startDate.AddDays(18) });
            Projects.Add(newProject);

            newProject = new Project
            {
                Name = "Project 3"
            };
            // An empty project so test and don't display 
            Projects.Add(newProject);

            // cvs = CollectionViewSource
            cvsProjects.Source = Projects;
        }
    }   

    public class Activity
    {
        public string Name { get; set; }
        public DateTime DueDate { get; set; }
        public bool Complete { get; set; }
        public string Project { get; set; }
    }

    /* ObservableCollection<T> Class represents a dynamic data collection that provides notifications when items get added, removed, 
     * or when the entire list is refreshed.  this class implements the INotifyCollectionChanged interface, as well as the 
     * INotifyPropertyChanged interface.
     * See more at: https://msdn.microsoft.com/en-us/library/windows/apps/ms668604%28v=vs.105%29.aspx?f=255&MSPPError=-2147217396
    */
    public class Project
    {
        public Project()
        {
            Activities = new ObservableCollection<Activity>();
        }
        public string Name { get; set; }
        public ObservableCollection<Activity> Activities { get; private set; }
    }
    
}
