using System;
using System.Diagnostics;
using System.Resources;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using BliksemWP.Resources;
using Windows.Storage;
using System.IO;
using System.IO.IsolatedStorage;
using SQLiteWinRT;
using BliksemWP.Messages;

namespace BliksemWP
{
    public partial class App : Application
    {
        /// <summary>
        /// The database path.
        /// </summary>
        public static string DB_PATH = Path.Combine(Path.Combine(ApplicationData.Current.LocalFolder.Path, "stops.db"));

        /// <summary>
        /// Provides easy access to the root frame of the Phone Application.
        /// </summary>
        /// <returns>The root frame of the Phone Application.</returns>
        public static PhoneApplicationFrame RootFrame { get; private set; }

        public static JourneyPlanMessage planMessage { get; set; }

        /// <summary>
        /// Constructor for the Application object.
        /// </summary>
        public App()
        {
            // Standard XAML initialization
            InitializeComponent();
            MoveDB();

            // Show graphics profiling information while debugging.
            if (Debugger.IsAttached)
            {
                // Display the current frame rate counters.
                Application.Current.Host.Settings.EnableFrameRateCounter = true;

                // Show the areas of the app that are being redrawn in each frame.
                //Application.Current.Host.Settings.EnableRedrawRegions = true;

                // Enable non-production analysis visualization mode,
                // which shows areas of a page that are handed off to GPU with a colored overlay.
                //Application.Current.Host.Settings.EnableCacheVisualization = true;

                // Prevent the screen from turning off while under the debugger by disabling
                // the application's idle detection.
                // Caution:- Use this under debug mode only. Application that disables user idle detection will continue to run
                // and consume battery power when the user is not using the phone.
                PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            }

        }

        // Code to execute when the application is launching (eg, from Start)
        // This code will not execute when the application is reactivated
        private async void MoveDB()
        {
            StorageFile dbFile = null;
            try
            {
                // Try to get the 
                dbFile = await StorageFile.GetFileFromPathAsync(App.DB_PATH);
            }
            catch (FileNotFoundException)
            {
                if (dbFile == null)
                {
                    // Copy file from installation folder to local folder.
                    // Obtain the virtual store for the application.
                    IsolatedStorageFile iso = IsolatedStorageFile.GetUserStoreForApplication();

                    // Create a stream for the file in the installation folder.
                    using (Stream input = Application.GetResourceStream(new Uri("stops.db", UriKind.Relative)).Stream)
                    {
                        // Create a stream for the new file in the local folder.
                        using (IsolatedStorageFileStream output = iso.CreateFile(App.DB_PATH))
                        {
                            // Initialize the buffer.
                            byte[] readBuffer = new byte[4096];
                            int bytesRead = -1;

                            // Copy the file from the installation folder to the local folder. 
                            while ((bytesRead = input.Read(readBuffer, 0, readBuffer.Length)) > 0)
                            {
                                output.Write(readBuffer, 0, bytesRead);
                            }

                            //PrepareFTSDatabase();
                        }
                    }
                }
            }
        }
    }
}