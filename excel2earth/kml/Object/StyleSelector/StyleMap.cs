// CLASSIFICATION: UNCLASSIFIED

using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace excel2earth.kml
{
    /// <summary>
    /// 
    /// </summary>
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class StyleMap : StyleSelector
    {
        /// <summary>
        /// 
        /// </summary>
        public class Pair
        {
            public enum styleStateEnum
            {
                normal,
                highlight
            }

            public styleStateEnum key;
            public string styleUrl;
            public Style style;

            public Pair(styleStateEnum key, string styleUrl)
            {
                this.key = key;
                this.styleUrl = styleUrl;
            }

            public Pair(styleStateEnum key, Style style)
            {
                this.key = key;
                this.style = style;
            }

            public XNode ToXNode()
            {
                XElement xNode = new XElement("Pair",
                    new XElement("key",
                        new XText(this.key.ToString())
                    )
                );

                if (!string.IsNullOrEmpty(this.styleUrl))
                {
                    xNode.Add
                    (
                        new XElement("styleUrl",
                            new XText(this.styleUrl)
                        )
                    );
                }
                if (this.style != null)
                {
                    xNode.Add(this.style.ToXNode());
                }

                return xNode;
            }
        }

        public Pair normal;
        public Pair highlight;

        public StyleMap(string id, Pair normal, Pair highlight)
        {
            this.id = id;
            this.normal = normal;
            this.highlight = highlight;
        }

        public StyleMap(string id, string normal, string highlight)
        {
            this.id = id;
            this.normal = new Pair(Pair.styleStateEnum.normal, normal);
            this.highlight = new Pair(Pair.styleStateEnum.highlight, highlight);
        }

        public StyleMap(XElement xElement)
        {
        }

        public override XNode ToXNode()
        {
            return new XElement("StyleMap",
                new XAttribute("id", this.id),
                this.normal.ToXNode(),
                this.highlight.ToXNode()
            );
        }
    }
}

// CLASSIFICATION: UNCLASSIFIED