// CLASSIFICATION: UNCLASSIFIED

using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace excel2earth.kml
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class Icon
    {
        public string href { get; set; }
        //private int x;
        //private int y;
        //private int w;
        //private int h;

        //public enum refreshModeEnum
        //{
        //    onChange,
        //    onInterval,
        //    onExpire
        //}

        //private refreshModeEnum refreshMode;
        //private float refreshInterval;

        //private enum viewRefreshModeEnum
        //{
        //    never,
        //    onStop,
        //    onRequest,
        //    onRegion
        //}

        //private viewRefreshModeEnum viewRefreshMode;
        //private float viewRefreshTime;
        //private float viewBoundScale;
        //private string viewFormat;
        //private string httpQuery;

        public Icon()
        {
        }

        public Icon(string href)
        {
            this.href = href;
        }

        public Icon(XElement xElement)
        {
            this.href = xElement.Element("href").Value;
        }

        public XNode ToXNode()
        {
            XElement xNode = new XElement("Icon");

            if (!string.IsNullOrEmpty(href))
            {
                xNode.Add
                (
                    new XElement("href",
                        new XText(href)
                    )
                );
            }

            return xNode;
        }
    }
}

// CLASSIFICATION: UNCLASSIFIED