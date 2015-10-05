// CLASSIFICATION: UNCLASSIFIED

namespace MSP.CCS
{
    /// <summary>
    /// Universal Polar Stereographic Coordinates.
    /// 
    /// Originally from GEOTRANS.
    /// </summary>
    public class UPSCoordinates : CoordinateTuple
    {
        private char hemisphere;
        private double easting;
        private double northing;

        public UPSCoordinates()
        {
            this.coordinateType = CoordinateType.Enum.universalPolarStereographic;
            this.hemisphere = 'N';
            this.easting = 0;
            this.northing = 0;
        }

        public UPSCoordinates(CoordinateType.Enum coordinateType)
        {
            this.coordinateType = coordinateType;
            this.hemisphere = 'N';
            this.easting = 0;
            this.northing = 0;
        }

        public UPSCoordinates(CoordinateType.Enum coordinateType, char hemisphere, double easting, double northing)
        {
            this.coordinateType = coordinateType;
            this.hemisphere = hemisphere;
            this.easting = easting;
            this.northing = northing;
        }

        public UPSCoordinates(CoordinateType.Enum coordinateType, string warningMessage, char hemisphere, double easting, double northing)
        {
            this.coordinateType = coordinateType;
            this.hemisphere = hemisphere;
            this.easting = easting;
            this.northing = northing;
            this.warningMessage = warningMessage;
        }

        public void set(char hemisphere, double easting, double northing)
        {
            this.hemisphere = hemisphere;
            this.easting = easting;
            this.northing = northing;
        }

        public char getHemisphere()
        {
            return this.hemisphere;
        }

        public double getEasting()
        {
            return this.easting;
        }

        public double getNorthing()
        {
            return this.northing;
        }
    }
}

// CLASSIFICATION: UNCLASSIFIED