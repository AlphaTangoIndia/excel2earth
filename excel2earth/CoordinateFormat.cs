// CLASSIFICATION: UNCLASSIFIED

using System.Runtime.InteropServices;

namespace excel2earth
{
    /// <summary>
    /// Provides an abstract class for formatting coordinate types.
    /// </summary>
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    public abstract class CoordinateFormat
    {
        /// <summary>
        /// The coordinate type.
        /// </summary>
        public enum CoordinateReferenceType
        {
            Latitude,
            Longitude,
            Grid
        }

        /// <summary>
        /// Converts the coordinates to Decimal Degrees.
        /// </summary>
        /// <returns>Coordintes in Decimal Degrees</returns>
        public abstract DecimalDegrees ToDecimalDegrees();
        /// <summary>
        /// Converts the coordinates to Degrees, Decimal Minutes.
        /// </summary>
        /// <returns>Coordinates in Degrees, Decimal Minutes</returns>
        public abstract DegreesDecimalMinutes ToDegreesDecimalMinutes();
        /// <summary>
        /// Converts the coordinates to Degrees, Minutes, Seconds.
        /// </summary>
        /// <returns>Coordinates in Degrees, Minutes, Seconds</returns>
        public abstract DegreesMinutesSeconds ToDegreesMinutesSeconds();
        /// <summary>
        /// Converts the coordinates to Military Grid Reference System.
        /// </summary>
        /// <returns>Coordinates in Military Grid Reference System</returns>
        public abstract MilitaryGridReferenceSystem ToMilitaryGridReferenceSystem();
    }
}

// CLASSIFICATION: UNCLASSIFIED