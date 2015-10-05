// CLASSIFICATION: UNCLASSIFIED

using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace excel2earth.kml
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public abstract class TimePrimitive
    {
        public enum dateTimeFormatEnum
        {
            dateTime,
            date,
            gYearMonth,
            gYear
        }

        public Dictionary<dateTimeFormatEnum, string> dateTimeFormats = new Dictionary<dateTimeFormatEnum, string>();

        public TimePrimitive()
        {
            this.dateTimeFormats.Add(dateTimeFormatEnum.dateTime, "yyyy-MM-ddTHH:mm:ssZ");
            this.dateTimeFormats.Add(dateTimeFormatEnum.date, "yyyy-MM-dd");
            this.dateTimeFormats.Add(dateTimeFormatEnum.gYearMonth, "yyyy-MM");
            this.dateTimeFormats.Add(dateTimeFormatEnum.gYear, "yyyy");
        }

        public abstract XNode ToXNode();
    }
}

// CLASSIFICATION: UNCLASSIFIED