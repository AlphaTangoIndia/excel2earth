// CLASSIFICATION: UNCLASSIFIED

using System;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace excel2earth.kml
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class MultiTrack : Geometry
    {
        private bool interpolate;
        private Track[] tracks = new Track[] {};

        public MultiTrack()
        {
        }

        public MultiTrack(string id, bool interpolate)
        {
            this.id = id;
            this.interpolate = interpolate;
        }

        public MultiTrack(string id, bool interpolate, Track[] tracks)
        {
            this.id = id;
            this.interpolate = interpolate;
            this.tracks = tracks;
        }

        public override XNode ToXNode()
        {
            XElement xNode = new XElement(this.gx + "MultiTrack");

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
            xNode.Add
            (
                new XElement(this.gx + "interpolate",
                    new XText(Convert.ToInt32(this.interpolate).ToString())
                )
            );
            foreach (Track track in this.tracks)
            {
                xNode.Add(track.ToXNode());
            }

            return xNode;
        }
    }
}

// CLASSIFICATION: UNCLASSIFIED