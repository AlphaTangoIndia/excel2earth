// CLASSIFICATION: UNCLASSIFIED

namespace MSP.CCS
{
    /// <summary>
    /// Universal Transverse Mercator Parameters
    /// </summary>
    public class UTMParameters : CoordinateSystemParameters
    {
        /// <summary>
        /// The UTM zone.
        /// </summary>
        public long zone;
        /// <summary>
        /// The UTM zone override.
        /// </summary>
        public long zoneOverride;

        /// <summary>
        /// Constructs a new set of UTM Parameters with default values.
        /// </summary>
        public UTMParameters()
        {
            this.coordinateType = CoordinateType.Enum.universalTransverseMercator;
            this.zone = 32;
            this.zoneOverride = 0;
        }

        /// <summary>
        /// Constructs a new set of UTM Parameters with default values.
        /// </summary>
        /// <param name="coordinateType">Coordinate Type</param>
        public UTMParameters(CoordinateType.Enum coordinateType)
        {
            this.coordinateType = coordinateType;
            this.zone = 32;
            this.zoneOverride = 0;
        }

        /// <summary>
        /// Constructs a new set of UTM Parameters.
        /// </summary>
        /// <param name="coordinateType">Coordinate Type</param>
        /// <param name="zoneOverride">Zone Override</param>
        public UTMParameters(CoordinateType.Enum coordinateType, long zoneOverride)
        {
            this.coordinateType = coordinateType;
            this.zone = 0;
            this.zoneOverride = zoneOverride;
        }

        /// <summary>
        /// Constructs a new set of UTM Parameters.
        /// </summary>
        /// <param name="coordinateType">Coordinate Type</param>
        /// <param name="zone">Zone</param>
        /// <param name="zoneOverride">Zone Override</param>
        public UTMParameters(CoordinateType.Enum coordinateType, long zone, long zoneOverride)
        {
            this.coordinateType = coordinateType;
            this.zone = zone;
            this.zoneOverride = zoneOverride;
        }
    }
}

// CLASSIFICATION: UNCLASSIFIED