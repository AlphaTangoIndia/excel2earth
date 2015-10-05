// CLASSIFICATION: UNCLASSIFIED

namespace MSP.CCS
{
    /// <summary>
    /// Map Projection Coordinates.
    /// 
    /// Originally from GEOTRANS.
    /// </summary>
    public class MapProjectionCoordinates : CoordinateTuple
    {
        private double easting;
        private double northing;
        
        public MapProjectionCoordinates()
        {
            this.coordinateType = CoordinateType.Enum.albersEqualAreaConic;
            this.easting = 0;
            this.northing = 0;
        }

        public MapProjectionCoordinates(CoordinateType.Enum coordinateType)
        {
            this.coordinateType = coordinateType;
            this.easting = 0;
            this.northing = 0;
        }

        public MapProjectionCoordinates(CoordinateType.Enum coordinateType, double easting, double northing)
        {
            this.coordinateType = coordinateType;
            this.easting = easting;
            this.northing = northing;
        }

        public MapProjectionCoordinates(CoordinateType.Enum coordinateType, string warningMessage, double easting, double northing)
        {
            this.coordinateType = coordinateType;
            this.easting = easting;
            this.northing = northing;
            this.warningMessage = warningMessage;
        }

        public void set(double easting, double northing)
        {
            this.easting = easting;
            this.northing = northing;
        }

        public void setEasting(double easting)
        {
            this.easting = easting;
        }

        public double getEasting()
        {
            return this.easting;
        }

        public void setNorthing(double northing)
        {
            this.northing = northing;
        }

        public double getNorthing()
        {
            return this.northing;
        }
    }
}

// CLASSIFICATION: UNCLASSIFIED