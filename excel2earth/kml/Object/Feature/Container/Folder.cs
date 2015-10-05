// CLASSIFICATION: UNCLASSIFIED

using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace excel2earth.kml
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class Folder : Container
    {
        public Folder()
        {
        }

        public Folder(string name)
        {
            this.name = name;
        }

        public override XNode ToXNode()
        {
            XElement xNode = new XElement("Folder");

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