// CLASSIFICATION: UNCLASSIFIED

using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace excel2earth.kml
{
    /// <summary>
    /// <![CDATA[<gx:Tour>]]>
    /// </summary>
    /// <remarks>http://code.google.com/apis/kml/documentation/kmlreference.html#gxtour</remarks> 
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class Tour : Feature
    {
        public Playlist playlist;

        public Tour(Playlist playlist)
        {
            this.playlist = playlist;
        }

        public override XNode ToXNode()
        {
            return new XElement(this.gx + "Tour",
                this.playlist.ToXNode()
            );
        }
    }
}

// CLASSIFICATION: UNCLASSIFIED