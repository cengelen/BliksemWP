using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;
using System.IO;

namespace BliksemWP
{
    public partial class DownloadUpdatePage : PhoneApplicationPage
    {
        public DownloadUpdatePage()
        {
            InitializeComponent();
            Loaded += DownloadUpdatePage_Loaded;
        }

        void DownloadUpdatePage_Loaded(object sender, RoutedEventArgs e)
        {
            CheckNewerVersionAvailable();

            /*WebClient client1 = new WebClient();
            client1.OpenReadCompleted += new OpenReadCompletedEventHandler(WebClient_OpenReadTimeTableCompleted);
            client1.OpenReadAsync(new Uri("your_URL"));

            WebClient client2 = new WebClient();
            client2.OpenReadCompleted += new OpenReadCompletedEventHandler(WebClient_OpenReadStopsCompleted);
            client2.OpenReadAsync(new Uri("your_URL"));*/
        }

        private void CheckNewerVersionAvailable()
        {
            HttpWebRequest req = HttpWebRequest.CreateHttp("http://1313.nl/timetable.dat");
            req.Method = "HEAD";
            req.BeginGetResponse(new AsyncCallback(VersionCheckResponseCallback), req);
        }

        private void VersionCheckResponseCallback(IAsyncResult asyncResult) {
            HttpWebRequest webRequest = (HttpWebRequest)asyncResult.AsyncState;
            HttpWebResponse webResponse = (HttpWebResponse)webRequest.EndGetResponse(asyncResult);

            String lastModified = webResponse.Headers[HttpRequestHeader.LastModified];
            DateTime lastDate = DateTime.Parse(lastModified);
        }

        void WebClient_OpenReadTimeTableCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            SaveFile(e, App.DATA_FILE_NAME);
        }

        void WebClient_OpenReadStopsCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            SaveFile(e, App.STOPS_DB_NAME);
        }

        private static void SaveFile(OpenReadCompletedEventArgs e, String filename)
        {
            var file = IsolatedStorageFile.GetUserStoreForApplication();

            using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream(filename, FileMode.Create, file))
            {
                byte[] buffer = new byte[1024];
                while (e.Result.Read(buffer, 0, buffer.Length) > 0)
                {
                    stream.Write(buffer, 0, buffer.Length);
                }
            }
        }
    }
}