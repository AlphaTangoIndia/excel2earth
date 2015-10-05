// CLASSIFICATION: UNCLASSIFIED

using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace excel2earth.kml
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public abstract class Object
    {
        public string id;
        public XNamespace gx = "http://www.google.com/kml/ext/2.2";

        public abstract XNode ToXNode();
    }
}

// CLASSIFICATION: UNCLASSIFIED