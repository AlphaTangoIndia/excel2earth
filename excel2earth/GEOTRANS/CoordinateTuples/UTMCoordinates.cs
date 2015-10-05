// CLASSIFICATION: UNCLASSIFIED

namespace MSP.CCS
{
    /// <summary>
    /// Universal Transverse Mercator Coordinates.
    /// 
    /// Originally from GEOTRANS.
    /// </summary>
    public class UTMCoordinates : CoordinateTuple
    {
        public int zone;
        public char hemisphere;
        public double easting;
        public double northing;

        public UTMCoordinates()
        {
            this.coordinateType = CoordinateType.Enum.universalTransverseMercator;
            this.zone = 32;
            this.hemisphere = 'N';
            this.easting = 0;
            this.northing = 0;
        }

        public UTMCoordinates(CoordinateType.Enum coordinateType)
        {
            this.coordinateType = coordinateType;
            this.zone = 32;
            this.hemisphere = 'N';
            this.easting = 0;
            this.northing = 0;
        }

        public UTMCoordinates(CoordinateType.Enum coordinateType, int zone, char hemisphere, double easting, double northing)
        {
            this.coordinateType = coordinateType;
            this.zone = zone;
            this.hemisphere = hemisphere;
            this.easting = easting;
            this.northing = northing;
        }

        public UTMCoordinates(CoordinateType.Enum coordinateType, string warningMessage, int zone, char hemisphere, double easting, double northing)
        {
            this.coordinateType = coordinateType;
            this.zone = zone;
            this.hemisphere = hemisphere;
            this.easting = easting;
            this.northing = northing;
            this.warningMessage = warningMessage;
        }
    }
}

// CLASSIFICATION: UNCLASSIFIED