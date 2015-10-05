// CLASSIFICATION: UNCLASSIFIED

using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace excel2earth.kml
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class LabelStyle : ColorStyle
    {

        public override XNode ToXNode()
        {
            return new XElement("LabelStyle");
        }
    }
}


// CLASSIFICATION: UNCLASSIFIED