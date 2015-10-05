// CLASSIFICATION: UNCLASSIFIED

namespace MSP.CCS
{
    /// <summary>
    /// Polar Stereograhic Scale Factor Parameters.
    /// 
    /// Originally from GEOTRANS.
    /// </summary>
    public class PolarStereographicScaleFactorParameters : CoordinateSystemParameters
    {
        public double centralMeridian;
        public double scaleFactor;
        public char hemisphere;
        public double falseEasting;
        public double falseNorthing;

        public PolarStereographicScaleFactorParameters()
        {
            this.coordinateType = CoordinateType.Enum.polarStereographicScaleFactor;
            this.centralMeridian = 0;
            this.scaleFactor = 0;
            this.hemisphere = 'N';
            this.falseEasting = 0;
            this.falseNorthing = 0;
        }

        public PolarStereographicScaleFactorParameters(CoordinateType.Enum coordinateType)
        {
            this.coordinateType = coordinateType;
            this.centralMeridian = 0;
            this.scaleFactor = 0;
            this.hemisphere = 'N';
            this.falseEasting = 0;
            this.falseNorthing = 0;
        }

        public PolarStereographicScaleFactorParameters(CoordinateType.Enum coordinateType, double centralMeridian, double scaleFactor, char hemisphere, double falseEasting, double falseNorthing)
        {
            this.coordinateType = coordinateType;
            this.centralMeridian = centralMeridian;
            this.scaleFactor = scaleFactor;
            this.hemisphere = hemisphere;
            this.falseEasting = falseEasting;
            this.falseNorthing = falseNorthing;
        }
    }
}

// CLASSIFICATION: UNCLASSIFIED
