// CLASSIFICATION: UNCLASSIFIED

using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace excel2earth.kml
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class Location : Object
    {
        public Geometry.CoordinateSet coordinates;

        public Location()
        {
            this.coordinates = new Geometry.CoordinateSet();
        }

        public Location(Geometry.CoordinateSet coordinates)
        {
            this.coordinates = coordinates;
        }

        public override XNode ToXNode()
        {
            double[] coords = this.coordinates.get();

            return new XElement("Location",
                new XElement("longitude",
                    new XText(coords[0].ToString())
                ),
                new XElement("latitude",
                    new XText(coords[1].ToString())
                ),
                new XElement("altitude",
                    new XText(coords[2].ToString())
                )
            );
        }
    }
}

// CLASSIFICATION: UNCLASSIFIED