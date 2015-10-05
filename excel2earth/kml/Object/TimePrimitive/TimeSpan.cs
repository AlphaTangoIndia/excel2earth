// CLASSIFICATION: UNCLASSIFIED

using System;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace excel2earth.kml
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class TimeSpan : TimePrimitive
    {
        public enum StopType
        {
            begin,
            end
        }

        private DateTime begin;
        private dateTimeFormatEnum dateTimeFormat1;
        private DateTime end;
        private dateTimeFormatEnum dateTimeFormat2;

        public TimeSpan(DateTime dateTime, dateTimeFormatEnum dateTimeFormat1, StopType stopType)
        {
            if (stopType == StopType.begin)
            {
                this.begin = dateTime;
            }
            else
            {
                this.end = dateTime;
            }

            this.dateTimeFormat1 = dateTimeFormat1;
        }

        public TimeSpan(DateTime begin, dateTimeFormatEnum dateTimeFormat1, DateTime end, dateTimeFormatEnum dateTimeFormat2)
        {
            this.begin = begin;
            this.dateTimeFormat1 = dateTimeFormat1;
            this.end = end;
            this.dateTimeFormat2 = dateTimeFormat2;
        }

        public override XNode ToXNode()
        {
            XElement xNode = new XElement("TimeSpan");

            if (this.begin != new DateTime())
            {
                xNode.Add
                (
                    new XElement("begin",
                        new XText(this.begin.ToUniversalTime().ToString(this.dateTimeFormats[this.dateTimeFormat1]))
                    )
                );
            }

            if (this.end != new DateTime())
            {
                xNode.Add
                (
                    new XElement("end",
                        new XText(this.end.ToUniversalTime().ToString(this.dateTimeFormats[this.dateTimeFormat2]))
                    )
                );
            }

            return xNode;
        }
    }
}

// CLASSIFICATION: UNCLASSIFIED