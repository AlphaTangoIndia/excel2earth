// CLASSIFICATION: UNCLASSIFIED

namespace MSP.CCS
{
    /// <summary>
    /// Error Messages.
    /// 
    /// Originally from GEOTRANS.
    /// </summary>
    public class ErrorMessages
    {
        public static string geoidFileOpenError = "Unable to locate geoid data file\n";
        public static string geoidFileParseError = "Unable to read geoid file\n";

        public static string ellipsoidFileOpenError = "Unable to locate ellipsoid data file: ellips.dat\n";
        public static string ellipsoidFileCloseError = "Unable to close ellipsoid file: ellips.dat\n";
        public static string ellipsoidFileParseError = "Unable to read ellipsoid file: ellips.dat\n";
        public static string ellipsoidOverflow = "Ellipsoid table overflow\n";
        public static string ellipse = "Ellipsoid library not initialized\n";
        public static string invalidEllipsoidCode = "Invalid ellipsoid code\n";

        public static string datumFileOpenError = "Unable to locate datum data file\n";
        public static string datumFileCloseError = "Unable to close datum file\n";
        public static string datumFileParseError = "Unable to read datum file\n";
        public static string datumDomain = "Invalid local datum domain of validity\n";
        public static string datumOverflow = "Datum table overflow";
        public static string datumRotation = "Rotation values must be between -60.0 and 60.0";
        public static string datumSigma = "Standard error values must be positive, or -1 if unknown\n";
        public static string datumType = "Invalid datum type\n";
        public static string invalidDatumCode = "Invalid datum code\n";

        public static string notUserDefined = "Specified code not user defined\n";
        public static string ellipseInUse = "Ellipsoid is in use by a datum\n";

        // Parameter error messages
        public static string semiMajorAxis = "Ellipsoid semi-major axis must be greater than zero\n";
        public static string ellipsoidFlattening = "Inverse flattening must be between 250 and 350\n";
        public static string orientation = "Orientation out of range\n";
        public static string originLatitude = "Origin Latitude (or Standard Parallel or Latitude of True Scale) out of range\n";
        public static string originLongitude = "Origin Longitude (or Longitude Down from Pole) out of range\n";
        public static string centralMeridian = "Central Meridian out of range\n";
        public static string scaleFactor = "Scale Factor out of range\n";
        public static string zone = "Invalid Zone\n";
        public static string zoneOverride = "Invalid Zone Override\n";
        public static string standardParallel1 = "Invalid 1st Standard Parallel\n";
        public static string standardParallel2 = "Invalid 2nd Standard Parallel\n";
        public static string standardParallel1_2 = "1st & 2nd Standard Parallels cannot both be zero\n";
        public static string standardParallelHemisphere = "Standard Parallels cannot be equal and opposite latitudes\n";
        public static string precision = "Precision must be between 0 and 5\n";
        public static string bngEllipsoid = "British National Grid ellipsoid must be Airy\n";
        public static string nzmgEllipsoid = "New Zealand Map Grid ellipsoid must be International\n";
        public static string latitude1 = "Latitude 1 out of range\n";
        public static string latitude2 = "Latitude 2 out of range\n";
        public static string latitude1_2 = "Latitude 1 and Latitude 2 cannot be equal\n";
        public static string longitude1 = "Longitude 1 out of range\n";
        public static string longitude2 = "Longitude 2 out of range\n";
        public static string omercHemisphere = "Point 1 and Point 2 cannot be in different hemispheres\n";
        public static string hemisphere = "Invalid Hemisphere\n";
        public static string radius = "Easting/Northing too far from center of projection\n";


        #region Coordinate error messages
        public static string latitude = "Latitude out of range\n";
        public static string longitude = "Longitude out of range\n";
        public static string easting = "Easting/X out of range\n";
        public static string northing = "Northing/Y out of range\n";
        public static string projection = "Point projects into a circle\n";
        public static string invalidArea = "Coordinates are outside valid area\n";
        public static string bngstaticString = "Invalid British National Grid static string\n";
        public static string garsstaticString = "Invalid GARS static string\n";
        public static string georefstaticString = "Invalid GEOREF static string\n";
        public static string mgrsstaticString = "Invalid MGRS static string\n";
        public static string usngstaticString = "Invalid USNG static string\n";

        public static string invalidIndex = "Index value outside of valid range\n";
        public static string invalidName = "Invalid name\n";
        public static string invalidType = "Invalid coordinate system type\n";

        public static string longitude_min = "The longitude minute part of the static string is greater than 60\n";
        public static string latitude_min = "The latitude minute part of the static string is greater than 60\n";
        #endregion
    }
}

// CLASSIFICATION: UNCLASSIFIED