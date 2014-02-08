using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using BliksemWP.Helpers;
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
            ResultConverter tmp = new ResultConverter();
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

        void btnStart_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/ResultPage.xaml", UriKind.Relative));
        }

        /// <summary>
        /// Full Text Searches sqlite db for stops name to autocomplete
        /// </summary>
        /// <param name="searchText">text to search</param>
        /// <param name="sender">target AutoCompleteBox</param>
        async void SearchForStop(string searchText, object sender)
        {
            List<Stop> items = new List<Stop>();

            // Prepare a SQL statement to be executed
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
            (sender as AutoCompleteBox).ItemsSource = items;
            (sender as AutoCompleteBox).PopulateComplete();
        }

        /// <summary>
        /// Event triggered by typing from user
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">event arguments</param>
        private void Stops_Populating(object sender, PopulatingEventArgs e)
        {
            if (this.db == null)
            {
                // cancel populating
                e.Cancel = true;
            }

            SearchForStop(e.Parameter, sender);

            e.Cancel = true;
        }


       
    }
}