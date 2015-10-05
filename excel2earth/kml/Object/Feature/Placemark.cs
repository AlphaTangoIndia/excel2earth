// CLASSIFICATION: UNCLASSIFIED

using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace excel2earth.kml
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class Placemark : Feature
    {
        public string snippet;
        public int snippetMaxLines;
        public string description;
        public string styleUrl;
        public Geometry geometry = null;
        public TimePrimitive timePrimitive = null;

        public Placemark()
        {
        }

        public Placemark(string name)
        {
            this.name = name;
        }

        public Placemark(string name, Geometry geometry)
        {
            this.name = name;
            this.geometry = geometry;
        }

        public Placemark(string name, string description, string styleUrl, Geometry geometry, TimePrimitive timePrimitive)
        {
            this.name = name;
            this.description = description;
            this.styleUrl = styleUrl;
            this.geometry = geometry;
            this.timePrimitive = timePrimitive;
        }

        public Placemark(string name, string snippet, int snippetMaxLines, string description, string styleUrl, Geometry geometry, TimePrimitive timePrimitive)
        {
            this.name = name;
            this.snippet = snippet;
            this.snippetMaxLines = snippetMaxLines;
            this.description = description;
            this.styleUrl = styleUrl;
            this.geometry = geometry;
            this.timePrimitive = timePrimitive;
        }

        public override XNode ToXNode()
        {
            XElement xNode = new XElement("Placemark");
            
            if (!string.IsNullOrEmpty(this.name))
            {
                xNode.Add
                (
                    new XElement("name",
                        new XText(this.name)
                    )
                );
            }
            
            if (this.snippetMaxLines != new int())
            {
                xNode.Add
                (
                    new XElement("Snippet",
                        new XAttribute("maxLines", this.snippetMaxLines),
                        new XCData(this.snippet)
                    )
                );
            }
            
            if (!string.IsNullOrEmpty(this.description))
            {
                xNode.Add
                (
                    new XElement("description",
                        new XCData(this.description)
                    )
                );
            }
            
            if (!string.IsNullOrEmpty(this.styleUrl))
            {
                xNode.Add
                (
                    new XElement("styleUrl",
                        new XText(this.styleUrl)
                    )
                );
            }

            if (this.timePrimitive != null)
            {
                xNode.Add(this.timePrimitive.ToXNode());
            }
            
            if (this.geometry != null)
            {
                xNode.Add(this.geometry.ToXNode());
            }

            return xNode;
        }
    }
}

// CLASSIFICATION: UNCLASSIFIED