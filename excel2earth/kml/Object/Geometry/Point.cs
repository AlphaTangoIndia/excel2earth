// CLASSIFICATION: UNCLASSIFIED

using System;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace excel2earth.kml
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class Point : Geometry
    {
        public bool extrude;
        private Coordinates coordinates;

        public Point(Coordinates coordinates)
        {
            this.coordinates = coordinates;
        }

        public Point(bool extrude, altitudeModeEnum altitudeMode, Coordinates coordinates)
        {
            this.extrude = extrude;
            this.altitudeMode = altitudeMode;
            this.coordinates = coordinates;
        }

        public override XNode ToXNode()
        {
            XElement xNode = new XElement("Point");

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