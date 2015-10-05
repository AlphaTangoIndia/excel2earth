// CLASSIFICATION: UNCLASSIFIED

using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace excel2earth.kml
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class Scale : Object
    {
        private double x;
        private double y;
        private double z;

        public Scale()
        {
            this.x = 1.0;
            this.y = 1.0;
            this.z = 1.0;
        }

        public Scale(double scale)
        {
            this.x = scale;
            this.y = scale;
            this.z = scale;
        }

        public Scale(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public override XNode ToXNode()
        {
            return new XElement("Scale",
                new XElement("x",
                    new XText(this.x.ToString())
                ),
                new XElement("y",
                    new XText(this.y.ToString())
                ),
                new XElement("z",
                    new XText(this.z.ToString())
                )
            );
        }
    }
}

// CLASSIFICATION: UNCLASSIFIED