// CLASSIFICATION: UNCLASSIFIED

using System;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace excel2earth
{
    /// <summary>
    /// Degrees, Decimal Minutes coordinate format.
    /// </summary>
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class DegreesDecimalMinutes : CoordinateFormat
    {
        public string Longitude { get; private set; }
        public string Latitude { get; private set; }

        public DegreesDecimalMinutes()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Longitude"></param>
        /// <param name="Latitude"></param>
        public DegreesDecimalMinutes(string Longitude, string Latitude)
        {
            Regex ValidCoordinate = new Regex(@"^(\d{4,5}|\d{2,3}\D\d{1,2})(\.\d+)?\D?[NSEW]$");

            if (ValidCoordinate.Match(Longitude).Success)
            {
                this.Longitude = Longitude.ToUpper();
            }
            else
            {
                throw new ArgumentException("Unable to validate coordinate.", "Longitude");
            }

            if (ValidCoordinate.Match(Latitude).Success)
            {
                this.Latitude = Latitude.ToUpper();
            }
            else
            {
                throw new ArgumentException("Unable to validate coordinate.", "Latitude");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coordinate"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private double ParseCoordinate(string coordinate, CoordinateReferenceType type)
        {
            if (coordinate.Contains("\""))
            {
                throw new ArgumentException("Coordinate contains invalid character.", coordinate);
            }

            // Regex Patterns
            Regex noSeparator = new Regex(@"^\d{4,5}(\.\d+)?\W?[NSEW]$");
            Regex degrees = new Regex(@"\d{1,3}");
            Regex minutes = new Regex(@"\d{1,2}(\.\d+)?");
            Regex direction = new Regex(@"[NSEW]");

            string DegreesString;
            string MinuteString;
            string DirectionString;
            Match DM = noSeparator.Match(coordinate);

            // Try matching with no separators first
            if (DM.Success)
            {
                DegreesString = coordinate.Substring(0, 2 + ((type == CoordinateReferenceType.Longitude) ? 1 : 0));
                MinuteString = minutes.Match(coordinate, DegreesString.Length).Value;
                DirectionString = direction.Match(coordinate).Value;
            }
            else
            {
                Match DegreesMatch = degrees.Match(coordinate);
                Match MinutesMatch = minutes.Match(coordinate, DegreesMatch.Index + DegreesMatch.Length);
                Match DirectionMatch = direction.Match(coordinate);

                DegreesString = DegreesMatch.Value;
                MinuteString = MinutesMatch.Value;
                DirectionString = DirectionMatch.Value;
            }

            int Degrees;
            double Minutes;
            int Direction = (DirectionString == "S" || DirectionString == "W") ? -1 : 1;
            double DecimalDegrees;

            if (int.TryParse(DegreesString, out Degrees) && double.TryParse(MinuteString, out Minutes))
            {
                DecimalDegrees = Direction * (Degrees + Minutes / 60d);
            }
            else
            {
                throw new ArgumentException("Unable to parse coorindate.", type.ToString());
            }

            return DecimalDegrees;
        }

        public bool TryParse(string longitude, string latitude, out DegreesDecimalMinutes result)
        {
            try
            {
                result = new DegreesDecimalMinutes(longitude, latitude);
                return true;
            }
            catch
            {
                result = null;
                return false;
            }
        }

        /// <summary>
        /// Converts the coordinates to Decimal Degrees.
        /// </summary>
        /// <returns>Coordintes in Decimal Degrees.</returns>
        public override DecimalDegrees ToDecimalDegrees()
        {
            double LongitudeDecimalDegrees = this.ParseCoordinate(this.Longitude, CoordinateReferenceType.Longitude);
            double LatitudeDecimalDegrees = this.ParseCoordinate(this.Latitude, CoordinateReferenceType.Latitude);
            
            return new DecimalDegrees(LongitudeDecimalDegrees, LatitudeDecimalDegrees);
        }

        public override DegreesDecimalMinutes ToDegreesDecimalMinutes()
        {
            return this;
        }

        public override DegreesMinutesSeconds ToDegreesMinutesSeconds()
        {
            return this.ToDecimalDegrees().ToDegreesMinutesSeconds();
        }

        public override MilitaryGridReferenceSystem ToMilitaryGridReferenceSystem()
        {
            return this.ToDecimalDegrees().ToMilitaryGridReferenceSystem();
        }
    }
}

// CLASSIFICATION: UNCLASSIFIED