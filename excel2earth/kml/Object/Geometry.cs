// CLASSIFICATION: UNCLASSIFIED

using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace excel2earth.kml
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public abstract class Geometry : Object
    {
        public class CoordinateSet
        {
            private double longitude;
            private double latitude;
            private double altitude;

            public CoordinateSet()
            {
                this.longitude = 0.0;
                this.latitude = 0.0;
                this.altitude = 0.0;
            }

            public CoordinateSet(double latitude, double longitude)
            {
                this.longitude = longitude;
                this.latitude = latitude;
                this.altitude = 0.0;
            }

            public CoordinateSet(double latitude, double longitude, double altitude)
            {
                this.longitude = longitude;
                this.latitude = latitude;
                this.altitude = altitude;
            }

            public string getString()
            {
                return this.longitude.ToString() + "," + this.latitude.ToString() + "," + this.altitude.ToString();
            }

            public double[] get()
            {
                return new double[] { this.longitude, this.latitude, this.altitude };
            }

            public XNode ToXNode()
            {
                return new XText(this.getString());
            }
        }

        public class Coordinates
        {
            public CoordinateSet[] coordinates;

            public Coordinates()
            {
                this.coordinates = new CoordinateSet[] { new CoordinateSet() };
            }

            public Coordinates(CoordinateSet coordinateSet)
            {
                this.coordinates = new CoordinateSet[] { coordinateSet };
            }

            public Coordinates(CoordinateSet[] coordinates)
            {
                this.coordinates = coordinates;
            }

            public XNode ToXNode()
            {
                string coordinatesString = "";
                foreach (CoordinateSet coordinate in this.coordinates)
                {
                    coordinatesString += coordinate.getString() + " ";
                }

                return new XElement("coordinates",
                    new XText(coordinatesString.Trim())
                );
            }
        }

        public bool hasAltitude = false;

        public enum altitudeModeEnum
        {
            clampToGround,
            relativeToGround,
            absolute,
            clampToSeaFloor,
            relativeToSeaFloor
        }

        public altitudeModeEnum altitudeMode;

        public override abstract XNode ToXNode();
    }
}

// CLASSIFICATION: UNCLASSIFIED