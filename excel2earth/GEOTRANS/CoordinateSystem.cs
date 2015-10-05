// CLASSIFICATION: UNCLASSIFIED

namespace MSP.CCS
{
    /// <summary>
    /// Contains the coordinate system information.
    /// 
    /// Originally from GEOTRANS.
    /// </summary>
    public class CoordinateSystem
    {
        public double semiMajorAxis;
        public double flattening;
        public string ellipsoidCode;

        /// <summary>
        /// The constructor defaults the ellipsoid parameters to WGS84.
        /// </summary>
        public CoordinateSystem()
        {
            this.semiMajorAxis = 6378137.0;
            this.flattening = 1 / 298.257223563;
            this.ellipsoidCode = "WE";
        }

        /// <summary>
        /// The constructor receives the ellipsoid parameters and
        /// as inputs.
        /// </summary>
        /// <param name="semiMajorAxis">Semi-major axis of ellipsoid, in meters</param>
        /// <param name="flattening">Flattening of ellipsoid</param>
        public CoordinateSystem(double semiMajorAxis, double flattening)
        {
            this.semiMajorAxis = semiMajorAxis;
            this.flattening = flattening;
        }
    }
}

// CLASSIFICATION: UNCLASSIFIED