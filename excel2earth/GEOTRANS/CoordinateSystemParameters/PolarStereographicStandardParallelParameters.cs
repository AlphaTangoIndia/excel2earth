// CLASSIFICATION: UNCLASSIFIED

namespace MSP.CCS
{
    /// <summary>
    /// Polar Stereographic Standard Parallel Parameters.
    /// 
    /// Originally from GEOTRANS.
    /// </summary>
    class PolarStereographicStandardParallelParameters : CoordinateSystemParameters
    {
        public double centralMeridian;
        public double standardParallel;
        public double falseEasting;
        public double falseNorthing;

        public PolarStereographicStandardParallelParameters()
        {
            this.coordinateType = CoordinateType.Enum.polarStereographicStandardParallel;
            this.centralMeridian = 0;
            this.standardParallel = 0;
            this.falseEasting = 0;
            this.falseNorthing = 0;
        }

        public PolarStereographicStandardParallelParameters(CoordinateType.Enum coordinateType)
        {
            this.coordinateType = coordinateType;
            this.centralMeridian = 0;
            this.standardParallel = 0;
            this.falseEasting = 0;
            this.falseNorthing = 0;
        }

        public PolarStereographicStandardParallelParameters(CoordinateType.Enum coordinateType, double centralMeridian, double standardParallel, double falseEasting, double falseNorthing)
        {
            this.coordinateType = coordinateType;
            this.centralMeridian = centralMeridian;
            this.standardParallel = standardParallel;
            this.falseEasting = falseEasting;
            this.falseNorthing = falseNorthing;
        }
    }
}


// CLASSIFICATION: UNCLASSIFIED
