// CLASSIFICATION: UNCLASSIFIED

namespace MSP.CCS
{
    /// <summary>
    /// Map Projection 5 Parameters.
    /// 
    /// Originally from GEOTRANS.
    /// </summary>
    public class MapProjection5Parameters : CoordinateSystemParameters
    {
        public double centralMeridian;
        public double originLatitude; 
        public double scaleFactor; 
        public double falseEasting; 
        public double falseNorthing; 

        public MapProjection5Parameters()
        {
            this.coordinateType = CoordinateType.Enum.transverseMercator;
            this.centralMeridian = 0;
            this.originLatitude = 0;
            this.scaleFactor = 1.0;
            this.falseEasting = 0;
            this.falseNorthing = 0;
        }

        public MapProjection5Parameters(CoordinateType.Enum coordinateType)
        {
            this.coordinateType = coordinateType;
            this.centralMeridian = 0;
            this.originLatitude = 0;
            this.scaleFactor = 1.0;
            this.falseEasting = 0;
            this.falseNorthing = 0;
        }

        public MapProjection5Parameters(CoordinateType.Enum coordinateType, double centralMeridian, double originLatitude, double scaleFactor, double falseEasting, double falseNorthing)
        {
            this.coordinateType = coordinateType;
            this.centralMeridian = centralMeridian;
            this.originLatitude = originLatitude;
            this.scaleFactor = scaleFactor;
            this.falseEasting = falseEasting;
            this.falseNorthing = falseNorthing;
        }
    }
}

// CLASSIFICATION: UNCLASSIFIED
