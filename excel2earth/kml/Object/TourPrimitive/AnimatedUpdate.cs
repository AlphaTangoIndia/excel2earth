// CLASSIFICATION: UNCLASSIFIED

using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace excel2earth.kml
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class AnimatedUpdate : TourPrimitive
    {
        public override XNode ToXNode()
        {
            return new XElement("AnimatedUpdate");
        }
    }
}

// CLASSIFICATION: UNCLASSIFIED