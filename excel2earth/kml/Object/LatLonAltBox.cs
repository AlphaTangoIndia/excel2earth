﻿// CLASSIFICATION: UNCLASSIFIED

using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace excel2earth.kml
{
    public class LatLonAltBox : Object
    {
        public override XNode ToXNode()
        {
            return new XElement("LatLonAltBox");
        }
    }
}

// CLASSIFICATION: UNCLASSIFIED