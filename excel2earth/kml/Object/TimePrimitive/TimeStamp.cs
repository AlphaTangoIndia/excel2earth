// CLASSIFICATION: UNCLASSIFIED

using System;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace excel2earth.kml
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class TimeStamp : TimePrimitive
    {
        private DateTime when;
        private dateTimeFormatEnum dateTimeFormat;

        public TimeStamp(DateTime when, dateTimeFormatEnum dateTimeFormat)
        {
            this.when = when;
            this.dateTimeFormat = dateTimeFormat;
        }
        
        public override XNode ToXNode()
        {
            return new XElement("TimeStamp",
                new XElement("when",
                    new XText(this.when.ToUniversalTime().ToString(this.dateTimeFormats[this.dateTimeFormat]))
                )
            );
        }
    }
}

// CLASSIFICATION: UNCLASSIFIED