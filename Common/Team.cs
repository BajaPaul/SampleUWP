using System.Collections.Generic;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace SampleUWP.Common
{
    // This Data Source is used by three different pages.  This has a custom string indexer.
    //
    public class Team
    {
        readonly Dictionary<string, object> league;

        public Team()   // Constructor
        {
            league = new Dictionary<string, object>();
        }

        // Name is the key, and whatever it is set to is the Value.
        // Dictionary can look up the Value using the key to access it.
        // Same for City and Color.
        public string Name { get; set; }
        public string City { get; set; }
        public SolidColorBrush Color { get; set; }

        // This is how you create a custom indexer in C#
        public object this[string indexer]
        {
            get
            {
                return league[indexer];
            }
            set
            {
                league[indexer] = value;
            }
        }

        //public void Insert(string key, object value)
        //{
        //    league.Add(key, value);
        //}

    }

    // This class is used to demonstrate grouping.  Teams is a List derived from generic List<Team>.
    public class Teams : List<Team>
    {
        public Teams()  // This is a constructor
        {
            Add(new Team() { Name = "The Reds", City = "Liverpool", Color = new SolidColorBrush(Colors.Green) });
            Add(new Team() { Name = "The Red Devils", City = "Manchester", Color = new SolidColorBrush(Colors.Magenta) });
            Add(new Team() { Name = "The Blues", City = "London", Color = new SolidColorBrush(Colors.Orange) });

            // decalre and intialize new team
            Team team = new Team() { Name = "The Gunners", City = "London", Color = new SolidColorBrush(Colors.Red) };
            team["Gaffer"] = "le Professeur";   // This adds more Keys and Values to dictionary
            team["Skipper"] = "Mr Gooner";
            Add(team);

            Add(new Team() { Name="KC Sporting", City = "Kansas City", Color = new SolidColorBrush(Colors.Blue) });
        }
    }
}

