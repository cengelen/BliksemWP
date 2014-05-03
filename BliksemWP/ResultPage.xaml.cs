using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using BliksemWP.Controls;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using BliksemWP.Helpers;
using BliksemWP.DataObjects;

namespace BliksemWP
{
    public partial class ResultPage : PhoneApplicationPage
    {
        private int fromStopId;
        private int toStopId;

        public ResultPage()
        {
            InitializeComponent();
            Loaded += ResultPage_Loaded;
        }

        void ResultPage_Loaded(object sender, RoutedEventArgs e)
        {
            String fromString, toString;
            if (NavigationContext.QueryString.TryGetValue("from", out fromString))
            {
                fromStopId = Convert.ToInt32(fromString);
            }
            if (NavigationContext.QueryString.TryGetValue("to", out toString))
            {
                toStopId = Convert.ToInt32(toString);
            } 
            
            var a = new NcxPppp.LibRrrr();
            var reisadvies = a.route(App.DATA_FILE_PATH, fromStopId, toStopId);
            if (reisadvies.Length > 1) {
                Console.Write("Got reisadvies" + reisadvies);
                ResultConverter c = new ResultConverter(reisadvies);
                List<JourneyLeg> journeyLegs = c.GetLegs();
                foreach (JourneyLeg leg in journeyLegs) {
                    spJourneyLegs.Children.Add(new JourneyLegControl(leg));
                }
                DepartureName.Text = journeyLegs[0].Departure;
                ArrivalName.Text = journeyLegs[journeyLegs.Count - 1].Arrival;
            }
            
        }
    }
}