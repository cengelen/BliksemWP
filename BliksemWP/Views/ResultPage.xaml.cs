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

namespace BliksemWP.Views
{
    public partial class ResultPage : PhoneApplicationPage
    {
        public ResultPage()
        {
            InitializeComponent();
            Loaded += ResultPage_Loaded;
        }

        void ResultPage_Loaded(object sender, RoutedEventArgs e)
        {
            spJourneyLegs.Children.Add(new JourneyLegControl());
        }
    }
}