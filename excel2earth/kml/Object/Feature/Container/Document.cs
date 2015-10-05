// CLASSIFICATION: UNCLASSIFIED

using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace excel2earth.kml
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class Document : Container
    {
        public Document()
        {
        }

        public Document(string name)
        {
            this.name = name;
        }

        public Document(XElement xElement)
        {
        }

        public override XNode ToXNode()
        {
            XElement xNode = new XElement("Document");

            if (!string.IsNullOrEmpty(name))
            {
                xNode.Add
                (
                    new XElement("name",
                        new XText(name)
                    )
                );
            }

            return xNode;
        }
    }
}

// CLASSIFICATION: UNCLASSIFIED