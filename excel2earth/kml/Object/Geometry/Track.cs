// CLASSIFICATION: UNCLASSIFIED

using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace excel2earth.kml
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class Track : Geometry
    {
        private Coordinates coords;
        public TimeStamp[] whens = new TimeStamp[] {};
        public Orientation[] angles = new Orientation[] {};
        public Model model = null;

        public Track()
        {
        }

        public Track(string id)
        {
            this.id = id;
        }

        public Track(string id, Coordinates coords)
        {
            this.id = id;
            this.coords = coords;
        }

        public Track(string id, Coordinates coords, TimeStamp[] whens, Orientation[] angles)
        {
            this.id = id;
            this.coords = coords;
            this.whens = whens;
            this.angles = angles;
        }

        public override XNode ToXNode()
        {
            XElement xNode = new XElement(this.gx + "Track");

            if (!string.IsNullOrEmpty(this.id))
            {
                xNode.Add
                (
                    new XAttribute("id", this.id)
                );
            }
            if (this.hasAltitude)
            {
                if (this.altitudeMode == altitudeModeEnum.clampToSeaFloor || this.altitudeMode == altitudeModeEnum.relativeToSeaFloor)
                {
                    xNode.Add
                    (
                        new XElement(this.gx + "altitudeMode",
                            new XText(this.altitudeMode.ToString())
                        )
                    );
                }
                else
                {
                    xNode.Add
                    (
                        new XElement("altitudeMode",
                            new XText(this.altitudeMode.ToString())
                        )
                    );
                }
            }
            foreach (TimeStamp when in this.whens)
            {
                xNode.Add
                (
                    ((XElement)when.ToXNode()).Descendants("when")
                );
            }
            foreach (CoordinateSet coordinateSet in this.coords.coordinates)
            {
                xNode.Add
                (
                    new XElement(this.gx + "coord", coordinateSet.getString().Replace(',', ' '))
                );
            }
            foreach (Orientation angle in this.angles)
            {
                xNode.Add
                (
                    new XElement(this.gx + "angles", angle.getString())
                );
            }
            if (this.model != null)
            {
                xNode.Add(this.model.ToXNode());
            }
            return xNode;
        }
    }
}

// CLASSIFICATION: UNCLASSIFIED