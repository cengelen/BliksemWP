using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using BliksemWP.Resources;
using System.IO.IsolatedStorage;
using Windows.Storage;
using SQLiteWinRT;
using System.Collections;
using System.Diagnostics;

namespace BliksemWP
{
    public partial class MainPage : PhoneApplicationPage
    {
        Database db;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            SetupDB();

            Loaded += MainPage_Loaded;
        }

        async void SetupDB()
        {
            // Get the file from the install location  
            StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync("stops.db");

            // Create a new SQLite instance for the file 
            this.db = new Database(file);

            // Open the database asynchronously
            await this.db.OpenAsync(SqliteOpenMode.OpenRead);
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            btnStart.Click += btnStart_Click;
        }

        void btnStart_Click(object sender, RoutedEventArgs e)
        {
            
        }

        async void SearchForStop(string searchText)
        {
            List<Stop> items = new List<Stop>();
            /*items.Add(new Stop() { StopIndex = 0, StopName = "Bla" });
            items.Add(new Stop() { StopIndex = 1, StopName = "Bla 1" });
            items.Add(new Stop() { StopIndex = 2, StopName = "Bla 2" });
            items.Add(new Stop() { StopIndex = 3, StopName = "Bla 3" });*/

            // Prepare a SQL statement to be executed
            //var statement = await this.db.PrepareStatementAsync("SELECT stopindex, stopname FROM stops_fts;");
            var statement = await this.db.PrepareStatementAsync("SELECT stopindex, stopname FROM stops_fts WHERE stopname MATCH ?;");
            string formattedText = "*" + searchText.Trim().Replace(" ", "* *") + "*";
            statement.BindTextParameterAt(1, formattedText);

            // Loop through all the results and add to the collection
            while (await statement.StepAsync())
            {
                Stop stop = new Stop() { StopIndex = statement.GetIntAt(0), StopName = statement.GetTextAt(1)  };
                items.Add(stop);
            }
            Debug.WriteLine(formattedText + ": "+ items.Count);
            from.ItemsSource = items;
            from.PopulateComplete();
        }

        private void from_Populating(object sender, PopulatingEventArgs e)
        {
            if (this.db == null)
            {
                // cancel populating
                e.Cancel = true;
            }

            SearchForStop(e.Parameter);

            e.Cancel = true;
        }


       
    }
}