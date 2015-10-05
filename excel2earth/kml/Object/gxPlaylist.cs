using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace excel2earth.kml
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public abstract class Playlist : Object
    {
        public TourPrimitive[] tourPrimitives;

        public Playlist()
        {
            this.tourPrimitives = new TourPrimitive[0];
        }

        public Playlist(TourPrimitive[] tourPrimitives)
        {
            this.tourPrimitives = tourPrimitives;
        }

        public override XNode ToXNode()
        {
            XElement xNode = new XElement(this.gx + "Playlist");

            foreach (TourPrimitive tourPrimitive in this.tourPrimitives)
            {
                xNode.Add(tourPrimitive.ToXNode());
            }

            return xNode;
        }
    }
}