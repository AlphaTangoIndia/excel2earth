// CLASSIFICATION: UNCLASSIFIED

using System.Runtime.InteropServices;
using System.Drawing;

namespace excel2earth.kml
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public abstract class SubStyle : Object
    {
        protected string ToHexString(Color color)
        {
            return (color.A.ToString("X2") + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2")).ToLower();
        }
    }
}

// CLASSIFICATION: UNCLASSIFIED