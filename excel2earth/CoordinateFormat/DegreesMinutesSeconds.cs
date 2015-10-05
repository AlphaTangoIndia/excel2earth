// CLASSIFICATION: UNCLASSIFIED

using System;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace excel2earth
{
    /// <summary>
    /// Degrees, Minutes, Seconds coordinate format.
    /// </summary>
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class DegreesMinutesSeconds : CoordinateFormat
    {
        public string Longitude;
        public string Latitude;

        public DegreesMinutesSeconds()
        {
        }

        public DegreesMinutesSeconds(string Longitude, string Latitude)
        {
            Regex ValidCoordinate = new Regex(@"^(\d{6,7}|\d{2,3}\D\d{1,2}\D\d{1,2})(\.\d+)?\D?[NSEW]$");

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

        private double ParseCoordinate(string coordinate, CoordinateReferenceType type)
        {
            if (coordinate.Contains("'") && !coordinate.Contains("\""))
            {
                throw new ArgumentException("Coordinate does not contain requisite formatting characters.", coordinate);
            }

            // Regex Patterns
            Regex noSeparator = new Regex(@"^\d{6,7}(\.\d+)?\W?[NSEW]$");
            Regex degrees = new Regex(@"\d{1,3}");
            Regex minutes = new Regex(@"\d{1,2}");
            Regex seconds = new Regex(@"\d{1,2}(\.\d+)?");
            Regex direction = new Regex(@"[NSEW]");

            string DegreesString;
            string MinuteString;
            string SecondString;
            string DirectionString;
            Match DMS = noSeparator.Match(coordinate);

            // Try matching with no separators first
            if (DMS.Success)
            {
                DegreesString = coordinate.Substring(0, 2 + ((type == CoordinateReferenceType.Longitude) ? 1 : 0));
                MinuteString = coordinate.Substring(DegreesString.Length, 2);
                SecondString = seconds.Match(coordinate, DegreesString.Length + MinuteString.Length).Value;
                DirectionString = direction.Match(coordinate).Value;
            }
            else
            {
                Match DegreesMatch = degrees.Match(coordinate);
                Match MinutesMatch = minutes.Match(coordinate, DegreesMatch.Index + DegreesMatch.Length);
                Match SecondsMatch = seconds.Match(coordinate, MinutesMatch.Index + MinutesMatch.Length);
                Match DirectionMatch = direction.Match(coordinate);

                DegreesString = DegreesMatch.Value;
                MinuteString = MinutesMatch.Value;
                SecondString = SecondsMatch.Value;
                DirectionString = DirectionMatch.Value;
            }

            int Degrees;
            int Minutes;
            double Seconds;
            int Direction = (DirectionString == "S" || DirectionString == "W") ? -1 : 1;
            double decimalDegrees;

            if (int.TryParse(DegreesString, out Degrees) &&
                int.TryParse(MinuteString, out Minutes) &&
                double.TryParse(SecondString, out Seconds))
            {
                decimalDegrees = Direction * (Degrees + Minutes / 60d + Seconds / 3600d);
            }
            else
            {
                throw new ArgumentException("Unable to parse coorindate.", type.ToString());
            }

            return decimalDegrees;
        }

        public bool TryParse(string longitude, string latitude, out DegreesMinutesSeconds result)
        {
            try
            {
                result = new DegreesMinutesSeconds(longitude, latitude);
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
        /// <returns>Coordintes in Decimal Degrees</returns>
        public override DecimalDegrees ToDecimalDegrees()
        {
            double LongitudeDecimalDegrees = this.ParseCoordinate(this.Longitude, CoordinateReferenceType.Longitude);
            double LatitudeDecimalDegrees = this.ParseCoordinate(this.Latitude, CoordinateReferenceType.Latitude);
            return new DecimalDegrees(LongitudeDecimalDegrees, LatitudeDecimalDegrees);
        }

        public override DegreesDecimalMinutes ToDegreesDecimalMinutes()
        {
            return this.ToDecimalDegrees().ToDegreesDecimalMinutes();
        }

        public override DegreesMinutesSeconds ToDegreesMinutesSeconds()
        {
            return this;
        }

        public override MilitaryGridReferenceSystem ToMilitaryGridReferenceSystem()
        {
            return this.ToDecimalDegrees().ToMilitaryGridReferenceSystem();
        }
    }
}

// CLASSIFICATION: UNCLASSIFIED