// CLASSIFICATION: UNCLASSIFIED

using System;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using MSP.CCS;

namespace excel2earth
{
    /// <summary>
    /// Decimal Degrees coordinate format.
    /// </summary>
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class DecimalDegrees : CoordinateFormat
    {
        private GeodeticCoordinates geodeticCoordinates;
        public double Longitude { get; private set; }
        public double Latitude { get; private set; }

        public DecimalDegrees()
        {
        }

        /// <summary>
        /// Contructor takes double values.
        /// </summary>
        /// <param name="Longitude">Longitude</param>
        /// <param name="Latitude">Latitude</param>
        public DecimalDegrees(double Longitude, double Latitude)
        {
            this.Longitude = Longitude;
            this.Latitude = Latitude;
            this.ValidateCoordinates();
        }

        /// <summary>
        /// Constructor takes string values and converts them to double.
        /// </summary>
        /// <param name="Longitude">Longitude</param>
        /// <param name="Latitude">Longitude</param>
        public DecimalDegrees(string Longitude, string Latitude)
        {
            double tempLongitude;
            double tempLatitude;

            if (double.TryParse(Longitude, out tempLongitude))
            {
                this.Longitude = tempLongitude;
            }
            else
            {
                throw new ArgumentException("Invalid Longitude, must by type double.", "Longitude");
            }
            if (double.TryParse(Latitude, out tempLatitude))
            {
                this.Latitude = tempLatitude;
            }
            else
            {
                throw new ArgumentException("Invalid Latitude, must by type double.", "Latitude");
            }

            this.ValidateCoordinates();
        }

        /// <summary>
        /// Validates that the coordinates are within the proper range.
        /// </summary>
        private void ValidateCoordinates()
        {
            double _PI_OVER_180 = Math.PI / 180d;

            if (Math.Abs(Longitude) > 180)
            {
                throw new ArgumentOutOfRangeException("Longitude", "Longitude must be [-180.0, 180.0]");
            }
            if (Math.Abs(Latitude) > 90)
            {
                throw new ArgumentOutOfRangeException("Latitude", "Longitude must be [-90.0, 90.0]");
            }
            
            this.geodeticCoordinates = new GeodeticCoordinates(CoordinateType.Enum.geodetic, this.Longitude * _PI_OVER_180, this.Latitude * _PI_OVER_180);
        }

        /// <summary>
        /// Parses a Degrees, Decimal Minutes coordinate to a string.
        /// </summary>
        /// <param name="coordinate">Coordinate</param>
        /// <param name="type">Coordinate Type</param>
        /// <returns>A string of Degrees, Decimal Minutes</returns>
        private string ParseToDegreesDecimalMinutes(double coordinate, CoordinateReferenceType type)
        {
            char Direction;

            if (type == CoordinateReferenceType.Longitude)
            {
                Direction = (coordinate < 0) ? 'W' : 'E';
            }
            else
            {
                Direction = (coordinate < 0) ? 'S' : 'N';
            }

            coordinate = Math.Abs(coordinate);
            int Degrees = (int)(coordinate);
            double Minutes = (coordinate - Degrees) * 60;

            return Degrees.ToString("00" + ((type == CoordinateReferenceType.Longitude) ? "0" : "")) + "°" + Minutes.ToString("00.######") + "'" + Direction;
        }

        /// <summary>
        /// Parses a Degrees, Minutes, Seconds coordinate to a string.
        /// </summary>
        /// <param name="coordinate">Coordinate</param>
        /// <param name="type">Coordinate Type</param>
        /// <returns>A string of Degrees, Minutes, Seconds</returns>
        private string ParseToDegreesMinutesSeconds(double coordinate, CoordinateReferenceType type)
        {
            char Direction;

            if (type == CoordinateReferenceType.Longitude)
            {
                Direction = (coordinate < 0) ? 'W' : 'E';
            }
            else
            {
                Direction = (coordinate < 0) ? 'S' : 'N';
            }

            coordinate = Math.Abs(coordinate);
            int Degrees = (int)(coordinate);
            int Minutes = (int)((coordinate - Degrees) * 60);
            double Seconds = ((coordinate - Degrees) * 60 - Minutes) * 60;

            return Degrees.ToString("00" + ((type == CoordinateReferenceType.Longitude) ? "0" : "")) + "°" + Minutes.ToString("00") + "'" + Seconds.ToString("00.##") + "\"" + Direction;
        }

        public bool TryParse(string longitude, string latitude, out DecimalDegrees result)
        {
            try
            {
                result = new DecimalDegrees(longitude, latitude);
            }
            catch
            {
                result = null;
                return false;
            }

            return true;
        }

        /// <summary>
        /// Converts the coordinates to Decimal Degress.
        /// </summary>
        /// <returns>Self</returns>
        public override DecimalDegrees ToDecimalDegrees()
        {
            return this;
        }

        /// <summary>
        /// Converts to Degrees, Decimal Minutes
        /// </summary>
        /// <returns>Degrees, Decimal Minutes</returns>
        public override DegreesDecimalMinutes ToDegreesDecimalMinutes()
        {
            string LongitudeDegreesDecimalMinutes = this.ParseToDegreesDecimalMinutes(this.Longitude, CoordinateReferenceType.Longitude);
            string LatitudeDegreesDecimalMinutes = this.ParseToDegreesDecimalMinutes(this.Latitude, CoordinateReferenceType.Latitude);
            
            return new DegreesDecimalMinutes(LongitudeDegreesDecimalMinutes, LatitudeDegreesDecimalMinutes);
        }

        /// <summary>
        /// Converts to Degrees, Minutes, Seconds
        /// </summary>
        /// <returns>Degrees, Minutes, Seconds</returns>
        public override DegreesMinutesSeconds ToDegreesMinutesSeconds()
        {
            string LongitudeDegreesMinutesSeconds = this.ParseToDegreesMinutesSeconds(this.Longitude, CoordinateReferenceType.Longitude);
            string LatitudeDegreesMinutesSeconds = this.ParseToDegreesMinutesSeconds(this.Latitude, CoordinateReferenceType.Latitude);
            
            return new DegreesMinutesSeconds(LongitudeDegreesMinutesSeconds, LatitudeDegreesMinutesSeconds);
        }

        /// <summary>
        /// Converts to Military Grid Reference System
        /// </summary>
        /// <returns>Military Grid Reference System</returns>
        public override MilitaryGridReferenceSystem ToMilitaryGridReferenceSystem()
        {
            MGRSorUSNGCoordinates mgrs = new MGRS().convertFromGeodetic(this.geodeticCoordinates, 5);
            
            return new MilitaryGridReferenceSystem(mgrs);
        }
    }
}

// CLASSIFICATION: UNCLASSIFIED