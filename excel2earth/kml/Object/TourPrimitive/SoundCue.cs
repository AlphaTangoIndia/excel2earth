// CLASSIFICATION: UNCLASSIFIED

using System.Xml.Linq;

namespace excel2earth.kml
{
    class SoundCue : TourPrimitive
    {
        public override XNode ToXNode()
        {
            return new XElement("SoundCue");
        }
    }
}

// CLASSIFICATION: UNCLASSIFIED