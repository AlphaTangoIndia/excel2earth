// CLASSIFICATION: UNCLASSIFIED

using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace excel2earth.kml
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class Style : StyleSelector
    {
        public IconStyle iconStyle;
        public LabelStyle labelStyle;
        public LineStyle lineStyle;
        public PolyStyle polyStyle;
        public BalloonStyle balloonStyle;
        public ListStyle listStyle;

        public Style(string id, IconStyle iconStyle)
        {
            this.id = id;
            this.iconStyle = iconStyle;
        }

        public Style(string id, IconStyle iconStyle, ListStyle listStyle)
        {
            this.id = id;
            this.iconStyle = iconStyle;
            this.listStyle = listStyle;
        }

        public Style(string id, IconStyle iconStyle, LabelStyle labelStyle, LineStyle lineStyle, PolyStyle polyStyle, BalloonStyle balloonStyle, ListStyle listStyle)
        {
            this.id = id;
            this.iconStyle = iconStyle;
            this.labelStyle = labelStyle;
            this.lineStyle = lineStyle;
            this.polyStyle = polyStyle;
            this.balloonStyle = balloonStyle;
            this.listStyle = listStyle;
        }

        public Style(Style style)
        {
            this.id = style.id;
            this.iconStyle = style.iconStyle;
            this.labelStyle = style.labelStyle;
            this.lineStyle = style.lineStyle;
            this.polyStyle = style.polyStyle;
            this.balloonStyle = style.balloonStyle;
            this.listStyle = style.listStyle;
        }
        
        public Style(XElement xElement)
        {
            foreach (XElement descendant in xElement.DescendantNodes())
            {
                switch (descendant.Name.ToString())
                {
                    case "IconStyle":
                        this.iconStyle = new IconStyle(descendant);
                        break;
                    //case "LabelStyle":
                    //    this.labelStyle = new LabelStyle(descendant);
                    //    break;
                    //case "LineStyle":
                    //    this.lineStyle = new LineStyle(descendant);
                    //    break;
                    //case "PolyStyle":
                    //    this.polyStyle = new PolyStyle(descendant);
                    //    break;
                    //case "BalloonStyle":
                    //    this.balloonStyle = new BalloonStyle(descendant);
                    //    break;
                    //case "ListStyle":
                    //    this.listStyle = new ListStyle(descendant);
                    //    break;
                }
            }
        }

        public override XNode ToXNode()
        {
            XElement xNode = new XElement("Style");
            
            if(!string.IsNullOrEmpty(this.id))
            {
                xNode.Add(new XAttribute("id", this.id));
            }
            if (this.iconStyle != null)
            {
                xNode.Add(this.iconStyle.ToXNode());
            }
            if (this.labelStyle != null)
            {
                xNode.Add(this.labelStyle.ToXNode());
            }
            if (this.lineStyle != null)
            {
                xNode.Add(this.lineStyle.ToXNode());
            }
            if (this.polyStyle != null)
            {
                xNode.Add(this.polyStyle.ToXNode());
            }
            if (this.balloonStyle != null)
            {
                xNode.Add(this.balloonStyle.ToXNode());
            }
            if (this.listStyle != null)
            {
                xNode.Add(this.listStyle.ToXNode());
            }

            return xNode;
        }
    }
}

// CLASSIFICATION: UNCLASSIFIED