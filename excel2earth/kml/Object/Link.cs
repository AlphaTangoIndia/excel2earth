// CLASSIFICATION: UNCLASSIFIED

using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace excel2earth.kml
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class Link
    {
        private string href;
        //private enum RefreshMode
        //{
        //    onChange,
        //    onInterval,
        //    onExpire
        //}
        //RefreshMode refreshModeEnum;
        //private float refreshInterval;
        //private enum ViewRefreshMode
        //{
        //    never,
        //    onStop,
        //    onRequest,
        //    onRegion
        //}
        //ViewRefreshMode viewRefreshModeEnum;
        //float viewRefreshTime;
        //float viewBoundScale;
        //string viewFormat; // BBOX=[bboxWest],[bboxSouth],[bboxEast],[bboxNorth]
        //string httpQuery;

        public Link()
        {
            this.href = "";
        }

        public Link(string link)
        {
            this.href = link;
        }

        public XNode ToXNode()
        {
            return new XElement("Link",
                new XElement("href",
                    new XText(this.href)
                )
            );
        }
    }
}

// CLASSIFICATION: UNCLASSIFIED