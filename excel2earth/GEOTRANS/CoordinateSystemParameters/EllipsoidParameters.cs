// CLASSIFICATION: UNCLASSIFIED

namespace MSP.CCS
{
    /// <summary>
    /// Ellipsoid Parameters.
    /// 
    /// Originally from GEOTRANS.
    /// </summary>
    public class EllipsoidParameters : CoordinateSystemParameters
    {
        private double semiMajorAxis;
        private double flattening;
        private string ellipsoidCode;

        public EllipsoidParameters()
        {
            this.semiMajorAxis = 0;
            this.flattening = 0;
            this.ellipsoidCode = "   ";
        }

        public EllipsoidParameters(double semiMajorAxis, double flattening, string ellipsoidCode)
        {
            this.semiMajorAxis = semiMajorAxis;
            this.flattening = flattening;
            this.ellipsoidCode = ellipsoidCode;
        }
    }
}

// CLASSIFICATION: UNCLASSIFIED