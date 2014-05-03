using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BliksemWP.DataObjects;
using BliksemWP.Enums;
using System.Globalization;

namespace BliksemWP.Helpers
{
    class ResultConverter
    {
        private List<JourneyLeg> legs;
        
        public ResultConverter(string source)
        {
            legs = new List<JourneyLeg>();
            //string source =
            //    "0ITIN 4 rides \r\n" + 
            //    "WALK    -2  -2 40570 19548 08:30:48 08:32:00 +0.0 ;;walk;walk;;Den Haag, Houtkade;Den Haag, Rotonde Houtkade;\r\n" +
            //    "BUS  3472  28 19548 16109 08:32:00 08:39:00 +0.0 ;Veolia;30;Naaldwijk via Rijswijk;;Den Haag, Rotonde Houtkade;Den Haag, Station Ypenburg;\r\n" +
            //    "WALK    -2  -2 16109 34397 08:39:00 08:46:16 +0.0 ;;walk;walk;;Den Haag, Station Ypenburg;Den Haag Ypenburg;\r\n" +
            //    "RAIL   817   7 34397 34089 09:04:00 09:21:00 +0.0 ;NS;Sprinter;Utrecht Centraal;Sprinter;Den Haag Ypenburg;Gouda;\r\n" +
            //    "WALK    -2  -2 34089 34090 09:21:00 09:21:00 +0.0 ;;walk;walk;;Gouda;Gouda;\r\n" +
            //    "RAIL  9041   1 34090 33850 09:24:00 10:04:00 +0.0 ;NS;Intercity;Groningen;Intercity;Gouda;Amersfoort;\r\n" +
            //    "WALK    -2  -2 33850 28112 10:04:00 10:10:00 +0.0 ;;walk;walk;;Amersfoort;Amersfoort, Centraal Station;\r\n" +
            //    "BUS  6308  22 28112 27055 10:14:00 10:21:00 +0.0 ;Connexxion;78;Leusden Geertrudeshof;;Amersfoort, Centraal Station;Leusden, Heiligenbergerweg;\r\n" +
            //    "WAIT    -2  -2 27055 27055 10:21:00 10:21:00 +0.0 ;;walk;walk;;Leusden, Heiligenbergerweg;Leusden, Heiligenbergerweg;\r\n";

            var lines = source.Split(new [] {"\n"}, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToList();

            for (int i = 0; i < lines.Count(); i++)
            {
                if (i == 0)
                {
                    continue;
                }
                if (lines[i].StartsWith("ITIN")) // For now only parse first itinerary
                {
                    return;
                }


                legs.Add(GetJourneyLeg(lines[i]));
            }
        }
        public List<JourneyLeg> GetLegs()
        {
            return legs;
        }

        private static JourneyLeg GetJourneyLeg(string line)
        {
            JourneyLeg journeyLeg = new JourneyLeg();
            var columns = line.Split(new [] {";"}, StringSplitOptions.None);
            journeyLeg.JourneyType = (JourneyLegType)Enum.Parse(typeof(JourneyLegType), columns[0]);

            String format = "HH:mm:ss";
            try
            {
                journeyLeg.DepartureTime = DateTime.ParseExact(columns[5], format, CultureInfo.InvariantCulture);
                journeyLeg.ArrivalTime = DateTime.ParseExact(columns[6], format, CultureInfo.InvariantCulture);
            } catch (FormatException f) {
                Console.Write("Couldn't read " + columns[5], f);
            }

            journeyLeg.Departure = columns[12];
            journeyLeg.Arrival = columns[13];

            journeyLeg.Agency = columns[8];
            journeyLeg.Headsign = columns[10];
            journeyLeg.ProductName = columns[9];
            journeyLeg.ProductCategory = columns[11];

            return journeyLeg;
        }
    }


}
