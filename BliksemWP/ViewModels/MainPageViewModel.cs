using BliksemWP.Messages;
using BliksemWP.Model;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BliksemWP.ViewModels
{
    public class MainPageViewModel : PropertyChangedBase
    {
        private readonly INavigationService navigationService;
        private readonly IEventAggregator eventAggregator;

        public MainPageViewModel(IEventAggregator eventAggregator, INavigationService navigationService)
        {
            this.eventAggregator = eventAggregator;
            this.navigationService = navigationService;

            eventAggregator.Subscribe(this);

            Date = DateTime.Now;
            Time = DateTime.Now;
        }

        public Stop DepartureStop
        {
            get;
            set;
        }

        public Stop ArrivalStop
        {
            get;
            set;
        }

        public DateTime Date { get; set; }
        public DateTime Time { get; set; }

        public void PlanJourney()
        {
            App.planMessage = new JourneyPlanMessage(DepartureStop, ArrivalStop, Date, Time);
            navigationService.UriFor<ResultPageViewModel>().Navigate();
            //eventAggregator.Publish(new JourneyPlanMessage(DepartureStop, ArrivalStop, Date, Time));
        }
    }
}
