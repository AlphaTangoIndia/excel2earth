// CLASSIFICATION: UNCLASSIFIED

using System.Drawing;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace excel2earth.kml
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public abstract class ColorStyle : SubStyle
    {
        public enum colorModeEnum
        {
            normal,
            random
        }
        
        public Color color = Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF);
        public colorModeEnum colorMode;

        public ColorStyle()
        {
        }
    }
}

// CLASSIFICATION: UNCLASSIFIED