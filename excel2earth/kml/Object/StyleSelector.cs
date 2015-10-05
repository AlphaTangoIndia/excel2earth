// CLASSIFICATION: UNCLASSIFIED

using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace excel2earth.kml
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public abstract class StyleSelector : Object
    {
        public override abstract XNode ToXNode();
    }
}

// CLASSIFICATION: UNCLASSIFIED