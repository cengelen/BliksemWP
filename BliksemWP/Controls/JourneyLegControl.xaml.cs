using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using BliksemWP.DataObjects;

namespace BliksemWP.Controls
{
    public partial class JourneyLegControl : UserControl
    {
        private JourneyLeg Leg;

        public JourneyLegControl(JourneyLeg theLeg)
        {
            InitializeComponent();
            Leg = theLeg;
            DepartureTime.Text = Leg.DepartureTime.ToShortTimeString();
            DepartureStop.Text = Leg.Departure;
            ArrivalTime.Text = Leg.ArrivalTime.ToShortTimeString();
            ArrivalStop.Text = Leg.Arrival;

            if (Leg.JourneyType == Enums.JourneyLegType.WALK)
            {
                TransportName.Text = "Loop";
            }
            else
            {
                TransportName.Text = Leg.Agency + " " + Leg.ProductName + " naar " + Leg.Headsign;
            }
            
        }
    }
}
