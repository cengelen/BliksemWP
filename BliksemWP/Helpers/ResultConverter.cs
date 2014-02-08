using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BliksemWP.DataObjects;
using BliksemWP.Enums;

namespace BliksemWP.Helpers
{
    class ResultConverter
    {
        public ResultConverter()
        {
            List<JourneyLeg> journeyLegs = new List<JourneyLeg>();
            string source =
                "0ITIN 4 rides \r\n" + 
                "WALK    -2  -2 40570 19548 08:30:48 08:32:00 +0.0 ;;walk;walk;;Den Haag, Houtkade;Den Haag, Rotonde Houtkade;\r\n" +
                "BUS  3472  28 19548 16109 08:32:00 08:39:00 +0.0 ;Veolia;30;Naaldwijk via Rijswijk;;Den Haag, Rotonde Houtkade;Den Haag, Station Ypenburg;\r\n" +
                "WALK    -2  -2 16109 34397 08:39:00 08:46:16 +0.0 ;;walk;walk;;Den Haag, Station Ypenburg;Den Haag Ypenburg;\r\n" +
                "RAIL   817   7 34397 34089 09:04:00 09:21:00 +0.0 ;NS;Sprinter;Utrecht Centraal;Sprinter;Den Haag Ypenburg;Gouda;\r\n" +
                "WALK    -2  -2 34089 34090 09:21:00 09:21:00 +0.0 ;;walk;walk;;Gouda;Gouda;\r\n" +
                "RAIL  9041   1 34090 33850 09:24:00 10:04:00 +0.0 ;NS;Intercity;Groningen;Intercity;Gouda;Amersfoort;\r\n" +
                "WALK    -2  -2 33850 28112 10:04:00 10:10:00 +0.0 ;;walk;walk;;Amersfoort;Amersfoort, Centraal Station;\r\n" +
                "BUS  6308  22 28112 27055 10:14:00 10:21:00 +0.0 ;Connexxion;78;Leusden Geertrudeshof;;Amersfoort, Centraal Station;Leusden, Heiligenbergerweg;\r\n" +
                "WAIT    -2  -2 27055 27055 10:21:00 10:21:00 +0.0 ;;walk;walk;;Leusden, Heiligenbergerweg;Leusden, Heiligenbergerweg;\r\n";

            var lines = source.Split(new [] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToList();

            for (int i = 0; i < lines.Count(); i++)
            {
                if (i == 0)
                {
                    continue;
                }


                journeyLegs.Add(GetJourneyLeg(lines[i]));
            }
        }

        private static JourneyLeg GetJourneyLeg(string line)
        {
            JourneyLeg journeyLeg = new JourneyLeg();
            var columns = line.Split(new [] {" "}, StringSplitOptions.None);
            journeyLeg.JourneyType = (JourneyLegType)Enum.Parse(typeof(JourneyLegType), columns[0]);
            //TODO: tijd nog opzoeken
            string startTime = columns[9];
            string endTime = columns[10];

            int firstSemiColon = line.IndexOf(';');
            string data = line.Substring(firstSemiColon);
            var fields = data.Split(';');
            journeyLeg.Start = fields[5];
            journeyLeg.Destination = fields[6];

            return journeyLeg;
        }
    }


}
