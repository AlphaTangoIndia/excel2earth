// CLASSIFICATION: UNCLASSIFIED

using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace excel2earth.kml
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class PolyStyle : ColorStyle
    {

        public override XNode ToXNode()
        {
            return new XElement("PolyStyle");
        }
    }
}


// CLASSIFICATION: UNCLASSIFIED