// CLASSIFICATION: UNCLASSIFIED

namespace MSP.CCS
{
    /// <summary>
    /// Contains the Coordinate System Parameters.
    /// 
    /// Originally from GEOTRANS.
    /// </summary>
    public class CoordinateSystemParameters
    {
        public CoordinateType.Enum coordinateType { get; set; }

        public CoordinateSystemParameters()
        {
        }

        public CoordinateSystemParameters(CoordinateType.Enum coordinateType)
        {
            this.coordinateType = coordinateType;
        }
    }
}

// CLASSIFICATION: UNCLASSIFIED