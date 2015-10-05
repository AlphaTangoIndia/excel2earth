// CLASSIFICATION: UNCLASSIFIED

namespace MSP.CCS
{
    /// <summary>
    /// Geodetic Coordinates.
    /// 
    /// Originally from GEOTRANS.
    /// </summary>
    public class GeodeticCoordinates : CoordinateTuple
    {
        public double longitude;
        public double latitude;
        public double height;

        public GeodeticCoordinates()
        {
            this.coordinateType = CoordinateType.Enum.geodetic;
            this.longitude = 0.0;
            this.latitude = 0.0;
            this.height = 0.0;
        }

        public GeodeticCoordinates(CoordinateType.Enum coordinateType)
        {
            this.coordinateType = coordinateType;
            this.longitude = 0.0;
            this.latitude = 0.0;
            this.height = 0.0;
        }

        public GeodeticCoordinates(CoordinateType.Enum coordinateType, double longitude, double latitude)
        {
            this.coordinateType = coordinateType;
            this.longitude = longitude;
            this.latitude = latitude;
            this.height = 0.0;
        }

        public GeodeticCoordinates(CoordinateType.Enum coordinateType, double longitude, double latitude, double height)
        {
            this.coordinateType = coordinateType;
            this.longitude = longitude;
            this.latitude = latitude;
            this.height = height;
        }

        public GeodeticCoordinates(CoordinateType.Enum coordinateType, string warningMessage, double longitude, double latitude, double height)
        {
            this.coordinateType = coordinateType;
            this.longitude = longitude;
            this.latitude = latitude;
            this.height = height;
            this.warningMessage = warningMessage;
        }
    }
}

// CLASSIFICATION: UNCLASSIFIED