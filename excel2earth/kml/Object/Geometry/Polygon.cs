// CLASSIFICATION: UNCLASSIFIED

using System;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace excel2earth.kml
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class Polygon : Geometry
    {
        public bool extrude;
        public bool tessellate;
        public LinearRing[] boundaries = new LinearRing[] {};

        public Polygon(string id)
        {
            this.id = id;
        }

        public Polygon(string id, LinearRing[] boundaries)
        {
            this.id = id;
            this.boundaries = boundaries;
        }

        public Polygon(string id, bool extrude, bool tessellate, altitudeModeEnum altitudeMode, LinearRing[] boundaries)
        {
            this.id = id;
            this.extrude = extrude;
            this.tessellate = tessellate;
            this.altitudeMode = altitudeMode;
            this.boundaries = boundaries;
        }

        public override XNode ToXNode()
        {
            XElement xNode = new XElement("Polygon");

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

            string bound_is = "outerBoundaryIs";
            
            foreach (LinearRing ring in this.boundaries)
            {
                xNode.Add
                (
                    new XElement(bound_is,
                        ring.ToXNode()
                    )
                );
                bound_is = "innerBoundaryIs";
            }
            
            return xNode;
        }
    }
}

// CLASSIFICATION: UNCLASSIFIED