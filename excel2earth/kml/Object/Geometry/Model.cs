// CLASSIFICATION: UNCLASSIFIED

using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace excel2earth.kml
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class Model : Geometry
    {
        public class ResourceMap
        {
            private string targetHref;
            private string sourceHref;

            public ResourceMap()
            {
                this.targetHref = "";
                this.sourceHref = "";
            }

            public ResourceMap(string targetHref, string sourceHref)
            {
                this.targetHref = targetHref;
                this.sourceHref = sourceHref;
            }

            public XNode ToXNode()
            {
                return new XElement("ResourceMap",
                    new XElement("Alias",
                        new XElement("targetHref",
                            new XText(this.targetHref)),
                        new XElement("sourceHref",
                            new XText(this.sourceHref)
                        )
                    )
                );
            }
        }

        private Location location;
        private Orientation orientation;
        private Scale scale;
        private Link link;
        private ResourceMap resourceMap;

        public Model()
        {
        }

        public Model(string id)
        {
            this.id = id;
        }

        public Model(string id, Location location, Orientation orientation, Scale scale, Link link, ResourceMap resourceMap)
        {
            this.id = id;
            this.location = location;
            this.orientation = orientation;
            this.scale = scale;
            this.link = link;
            this.resourceMap = resourceMap;
        }

        public Model(Location location, Orientation orientation, Scale scale, Link link, ResourceMap resourceMap)
        {
            this.location = location;
            this.orientation = orientation;
            this.scale = scale;
            this.link = link;
            this.resourceMap = resourceMap;
        }

        public Model(Orientation orientation, Scale scale, Link link)
        {
            this.orientation = orientation;
            this.scale = scale;
            this.link = link;
        }

        public Model(Location location, Orientation orientation, Scale scale, Link link)
        {
            this.location = location;
            this.orientation = orientation;
            this.scale = scale;
            this.link = link;
        }

        public Model(altitudeModeEnum altitudeMode, Location location, Orientation orientation, Scale scale, Link link)
        {
            this.altitudeMode = altitudeMode;
            this.location = location;
            this.orientation = orientation;
            this.scale = scale;
            this.link = link;
        }

        public override XNode ToXNode()
        {
            XElement xNode = new XElement("Model");

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
            if (this.location != null)
            {
                xNode.Add
                (
                    this.location.ToXNode()
                );
            }
            if (this.orientation != null)
            {
                xNode.Add
                (
                    this.orientation.ToXNode()
                );
            }
            if (this.scale != null)
            {
                xNode.Add
                (
                    this.scale.ToXNode()
                );
            }
            if (this.link != null)
            {
                xNode.Add
                (
                    this.link.ToXNode()
                );
            }
            if (this.resourceMap != null)
            {
                xNode.Add
                (
                    this.resourceMap.ToXNode()
                );
            }
            return xNode;
        }
    }
}

// CLASSIFICATION: UNCLASSIFIED