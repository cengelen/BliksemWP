using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BliksemWP.Enums;

namespace BliksemWP.DataObjects
{
    public class JourneyLeg
    {
        public JourneyLegType JourneyType { get; set; }

        public String Agency { get; set; }        

        public string Departure { get; set; }
        public DateTime DepartureTime { get; set; }
        public string Arrival { get; set; }
        public DateTime ArrivalTime { get; set; }

        public String Headsign { get; set; }
        public String ProductCategory { get; set; }
        public String ProductName { get; set; }

    }
}
