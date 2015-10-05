using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace excel2earth.kml
{
    public class LatLonBox : Object
    {
        public override XNode ToXNode()
        {
            return new XElement("LatLonBox");
        }
    }
}