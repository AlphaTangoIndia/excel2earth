// CLASSIFICATION: UNCLASSIFIED

using System.Drawing;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace excel2earth.kml
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class IconStyle : ColorStyle
    {
        //public override Color color;
        public float scale;
        public float heading;
        public Icon icon;
        
        public struct vec2
        {
            public double x;
            public double y;

            public enum unitsEnum
            {
                fraction,
                pixels,
                insetPixels
            }

            public unitsEnum xunits;
            public unitsEnum yunits;

            public vec2(double x, double y, unitsEnum xunits, unitsEnum yunits)
            {
                this.x = x;
                this.y = y;
                this.xunits = xunits;
                this.yunits = yunits;
            }

            public XNode ToXNode(string elementName)
            {
                return new XElement(elementName,
                    new XAttribute("x", this.x.ToString()),
                    new XAttribute("y", this.y.ToString()),
                    new XAttribute("xunits", this.xunits.ToString()),
                    new XAttribute("yunits", this.yunits.ToString())
                );
            }
        }
        
        public vec2 hotSpot;

        public IconStyle()
        {
        }

        public IconStyle(float scale, Icon icon)
        {
            this.scale = scale;
            this.icon = icon;
        }

        public IconStyle(float scale, Icon icon, vec2 hotSpot)
        {
            this.scale = scale;
            this.icon = icon;
            this.hotSpot = hotSpot;
        }

        public IconStyle(float scale, float heading, Icon icon, vec2 hotSpot)
        {
            this.scale = scale;
            this.heading = heading;
            this.icon = icon;
            this.hotSpot = hotSpot;
        }

        public IconStyle(Color color, colorModeEnum colorMode, float scale, Icon icon, vec2 hotSpot)
        {
            this.color = color;
            this.colorMode = colorMode;
            this.scale = scale;
            this.icon = icon;
            this.hotSpot = hotSpot;
        }

        public IconStyle(Color color, colorModeEnum colorMode, float scale, float heading, Icon icon, vec2 hotSpot)
        {
            this.color = color;
            this.colorMode = colorMode;
            this.scale = scale;
            this.heading = heading;
            this.icon = icon;
            this.hotSpot = hotSpot;
        }

        public IconStyle(XElement xElement)
        {
            foreach (XElement descendant in xElement.Descendants())
            {
                switch (descendant.Name.ToString())
                {
                    case "Scale":
                        float.TryParse(descendant.Value, out this.scale);
                        break;
                    case "Icon":
                        this.icon = new Icon(descendant);
                        break;
                }
            }
        }

        public override XNode ToXNode()
        {
            XElement xNode = new XElement("IconStyle");

            if (this.color != null)
            {
                xNode.Add
                (
                    new XElement("color",
                        new XText(this.ToHexString(this.color))
                    )
                );
            }
            #region need to fix
            if (this.colorMode != new colorModeEnum())
            {
                xNode.Add
                (
                    new XElement("colorMode",
                        new XText(this.colorMode.ToString())
                    )
                );
            } 
            #endregion
            if (this.scale != new double())
            {
                xNode.Add
                (
                    new XElement("scale",
                        new XText(this.scale.ToString())
                    )
                );
            }
            if (this.icon != null)
            {
                xNode.Add(this.icon.ToXNode());
            }
            if (this.hotSpot.x != new double())
            {
                xNode.Add(this.hotSpot.ToXNode("hotSpot"));
            }

            return xNode;
        }
    }
}

// CLASSIFICATION: UNCLASSIFIED