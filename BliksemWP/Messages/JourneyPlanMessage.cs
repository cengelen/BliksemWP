using BliksemWP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BliksemWP.Messages
{
    public class JourneyPlanMessage
    {
        public Stop DepartureStop { get; set; }
        public Stop ArrivalStop { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }

        public JourneyPlanMessage(Stop departureStop, Stop arrivalStop, DateTime date, DateTime time)
        {
            DepartureStop = departureStop;
            ArrivalStop = arrivalStop;
            Date = date;
            Time = time;
        }
    }
}
