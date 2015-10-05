// CLASSIFICATION: UNCLASSIFIED

using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace excel2earth.kml
{
    /// <summary>
    /// A container for zero or more geometry primitives associated with the same feature.
    /// </summary>
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class MultiGeometry : Geometry
    {
        /// <summary>
        /// 0 or more Geometry elements.
        /// </summary>
        public Geometry[] geometries;

        /// <summary>
        /// Creates an empty MultiGeometry object.
        /// </summary>
        public MultiGeometry()
        {
        }

        /// <summary>
        /// Creates a MultiGeometry object.
        /// </summary>
        /// <param name="id">ID Tag</param>
        public MultiGeometry(string id)
        {
            this.id = id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="geometries"></param>
        public MultiGeometry(string id, Geometry[] geometries)
        {
            this.id = id;
            this.geometries = geometries;
        }

        /// <summary>
        /// Converts the MultiGeometry object to an XNode.
        /// </summary>
        /// <returns>A MultiGeometry XNode</returns>
        public override XNode ToXNode()
        {
            XElement xNode = new XElement("MultiGeometry");

            if (!string.IsNullOrEmpty(this.id))
            {
                xNode.Add
                (
                    new XAttribute("id", this.id)
                );
            }

            return xNode;
        }
    }
}

// CLASSIFICATION: UNCLASSIFIED