// CLASSIFICATION: UNCLASSIFIED

using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace excel2earth.kml
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class Orientation : Object
    {
        private double heading;
        private double tilt;
        private double roll;

        public Orientation()
        {
            this.heading = 0.0;
            this.tilt = 0.0;
            this.roll = 0.0;
        }

        public Orientation(double heading, double tilt, double roll)
        {
            this.heading = heading;
            this.tilt = tilt;
            this.roll = roll;
        }

        public override XNode ToXNode()
        {
            return new XElement("Orientation",
                new XElement("heading",
                    new XText(this.heading.ToString())
                ),
                new XElement("tilt",
                    new XText(this.tilt.ToString())
                ),
                new XElement("roll",
                    new XText(this.roll.ToString())
                )
            );
        }

        public string getString()
        {
            return this.heading.ToString() + " " + this.tilt.ToString() + " " + this.roll.ToString();
        }
    }
}

// CLASSIFICATION: UNCLASSIFIED