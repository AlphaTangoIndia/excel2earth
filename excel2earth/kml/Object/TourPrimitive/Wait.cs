// CLASSIFICATION: UNCLASSIFIED

using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace excel2earth.kml
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class Wait : TourPrimitive
    {
        public override XNode ToXNode()
        {
            return new XElement("Wait");
        }
    }
}

// CLASSIFICATION: UNCLASSIFIED