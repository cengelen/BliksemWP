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

        public string Start { get; set; }
        public string Destination { get; set; }
    }
}
