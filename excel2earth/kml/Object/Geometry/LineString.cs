// CLASSIFICATION: UNCLASSIFIED

using System;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace excel2earth.kml
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class LineString : Geometry
    {
        public bool extrude;
        public bool tessellate;
        private Coordinates coordinates;

        public LineString(Coordinates coordinates)
        {
            this.coordinates = coordinates;
        }

        public LineString(string id, Coordinates coordinates)
        {
            this.id = id;
            this.coordinates = coordinates;
        }

        public LineString(bool extrude, bool tessellate, altitudeModeEnum altitudeMode, Coordinates coordinates)
        {
            this.extrude = extrude;
            this.tessellate = tessellate;
            this.altitudeMode = altitudeMode;
            this.coordinates = coordinates;
        }

        public LineString(string id, bool extrude, bool tessellate, altitudeModeEnum altitudeMode, Coordinates coordinates)
        {
            this.id = id;
            this.extrude = extrude;
            this.tessellate = tessellate;
            this.altitudeMode = altitudeMode;
            this.coordinates = coordinates;
        }

        public override XNode ToXNode()
        {
            XElement xNode = new XElement("LineString");

            if (!string.IsNullOrEmpty(this.id))
            {
                xNode.Add
                (
                    new XAttribute("id", this.id)
                );
            }
            if (this.hasAltitude)
            {
                if (this.altitudeMode == altitudeModeEnum.absolute || this.altitudeMode == altitudeModeEnum.relativeToGround || this.altitudeMode == altitudeModeEnum.relativeToSeaFloor)
                {
                    xNode.Add
                    (
                        new XElement("extrude",
                            new XText(Convert.ToByte(this.extrude).ToString())
                        )
                    );
                }
                if (this.altitudeMode == altitudeModeEnum.clampToGround || this.altitudeMode == altitudeModeEnum.clampToSeaFloor)
                {
                    xNode.Add
                    (
                        new XElement("tessellate",
                            new XText(Convert.ToByte(this.tessellate).ToString())
                        )
                    );
                }
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
            
            xNode.Add(this.coordinates.ToXNode());

            return xNode;
        }
    }
}

// CLASSIFICATION: UNCLASSIFIED