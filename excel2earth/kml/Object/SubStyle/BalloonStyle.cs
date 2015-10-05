// CLASSIFICATION: UNCLASSIFIED

using System.Drawing;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace excel2earth.kml
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class BalloonStyle : SubStyle
    {
        private Color bgColor;
        private Color textColor;
        private string text;

        public BalloonStyle()
        {
            this.bgColor = Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF);
            this.textColor = Color.FromArgb(0xFF, 0x00, 0x00, 0x00);
            this.text = "";
        }

        public BalloonStyle(Color bgColor, Color textColor, string text)
        {
            this.bgColor = bgColor;
            this.textColor = textColor;
            this.text = text;
        }

        public override XNode ToXNode()
        {
            return new XElement("BalloonStyle",
                new XElement("bgColor",
                    new XText(this.ToHexString(this.bgColor))
                ),
                new XElement("textColor",
                    new XText(this.ToHexString(this.textColor))
                ),
                new XElement("text",
                    new XText(text)
                )
            );
        }
    }
}


// CLASSIFICATION: UNCLASSIFIED