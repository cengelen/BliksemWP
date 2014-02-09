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
    // http://wp.qmatteoq.com/first-steps-with-caliburn-micro-in-windows-phone-8-messaging/
    public class ResultPageViewModel : PropertyChangedBase, IHandle<JourneyPlanMessage>
    {
        private readonly IEventAggregator eventAggregator;

        public ResultPageViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;

            eventAggregator.Subscribe(this);

            JourneyPlanMessage message = App.planMessage;
            DepartureStop = message.DepartureStop;
            ArrivalStop = message.ArrivalStop;
            Date = message.Date;
            Time = message.Time;

        }

        private Stop _departureStop;
        public Stop DepartureStop
        {
            get { return _departureStop; }
            set
            {
                if(_departureStop != value)
                {
                    _departureStop = value;
                    NotifyOfPropertyChange(() => DepartureStop);
                }
            }
        }

        public Stop ArrivalStop
        {
            get;
            set;
        }

        public DateTime Date { get; set; }
        public DateTime Time { get; set; }

        public void Handle(JourneyPlanMessage message)
        {
           
        }
    }
}
