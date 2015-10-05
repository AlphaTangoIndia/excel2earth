// CLASSIFICATION: UNCLASSIFIED

using System;

namespace MSP.CCS
{
    /// <summary>
    /// Universal Polar Stereographic.
    /// 
    /// Originally from GEOTRANS.
    /// </summary>
    public class UPS : CoordinateSystem
    {
        const double EPSILON = 1.75e-7;
        const double PI_OVER = (Math.PI / 2.0);           /* PI over 2 */
        const double MAX_LAT = PI_OVER;    /* 90 degrees in radians */
        const double MAX_ORIGIN_LAT = 81.114528 * (Math.PI / 180.0);
        const double MIN_NORTH_LAT = 83.5 * (Math.PI / 180.0);
        const double MAX_SOUTH_LAT = -79.5 * (Math.PI / 180.0);
        const double MIN_EAST_NORTH = 0.0;
        const double MAX_EAST_NORTH = 4000000.0;

        private double UPS_False_Easting = 2000000;
        private double UPS_False_Northing = 2000000;
        //private double UPS_Origin_Latitude = MAX_ORIGIN_LAT;
        private double UPS_Origin_Longitude = 0.0;

        //private double semiMajorAxis;
        //private double flattening;

        /// <summary>
        /// The constructor receives the ellipsoid parameters and sets
        /// the corresponding state variables. If any errors occur, an exception is 
        /// thrown with a description of the error.
        /// </summary>
        /// <param name="ellipsoidSemiMajorAxis">Semi-major axis of ellipsoid in meters</param>
        /// <param name="ellipsoidFlattening">Flattening of ellipsoid</param>
        public UPS(double ellipsoidSemiMajorAxis, double ellipsoidFlattening)
        {
            //this.UPS_Origin_Latitude = MAX_ORIGIN_LAT;

            double inv_f = 1 / ellipsoidFlattening;
            string errorStatus = "";

            if (ellipsoidSemiMajorAxis <= 0.0)
            { /* Semi-major axis must be greater than zero */
                errorStatus += ErrorMessages.semiMajorAxis;
            }

            if ((inv_f < 250) || (inv_f > 350))
            { /* Inverse flattening must be between 250 and 350 */
                errorStatus += ErrorMessages.ellipsoidFlattening;
            }

            if (errorStatus.Length > 0)
            {
                throw new ArgumentException(errorStatus);
            }

            this.semiMajorAxis = ellipsoidSemiMajorAxis;
            this.flattening = ellipsoidFlattening;
        }

        /// <summary>
        /// The function convertFromGeodetic converts geodetic (latitude and
        /// longitude) coordinates to UPS (hemisphere, easting, and northing)
        /// coordinates, according to the current ellipsoid parameters. 
        /// If any errors occur, an exception is thrown with a description 
        /// of the error.
        /// </summary>
        /// <param name="geodeticCoordinates"></param>
        /// <returns></returns>
        public UPSCoordinates convertFromGeodetic(GeodeticCoordinates geodeticCoordinates)
        {
            /*
             *    latitude      : Latitude in radians                       (input)
             *    longitude     : Longitude in radians                      (input)
             *    hemisphere    : Hemisphere either 'N' or 'S'              (output)
             *    easting       : Easting/X in meters                       (output)
             *    northing      : Northing/Y in meters                      (output)
             */

            char hemisphere;
            string errorStatus = "";

            double longitude = geodeticCoordinates.longitude;
            double latitude = geodeticCoordinates.latitude;

            if ((latitude < -MAX_LAT) || (latitude > MAX_LAT))
            {   /* latitude out of range */
                errorStatus += ErrorMessages.latitude;
            }
            else if ((latitude < 0) && (latitude >= (MAX_SOUTH_LAT + EPSILON)))
            {
                errorStatus += ErrorMessages.latitude;
            }
            else if ((latitude >= 0) && (latitude < (MIN_NORTH_LAT - EPSILON)))
            {
                errorStatus += ErrorMessages.latitude;
            }
            if ((longitude < -Math.PI) || (longitude > (2 * Math.PI)))
            {  /* longitude out of range */
                errorStatus += ErrorMessages.longitude;
            }

            if (errorStatus.Length > 0)
            {
                throw new ArgumentException(errorStatus);
            }

            if (latitude < 0)
            {
                //this.UPS_Origin_Latitude = -MAX_ORIGIN_LAT;
                hemisphere = 'S';
            }
            else
            {
                //this.UPS_Origin_Latitude = MAX_ORIGIN_LAT;
                hemisphere = 'N';
            }

            PolarStereographic polarStereographic = new PolarStereographic(this.semiMajorAxis, this.flattening, this.UPS_Origin_Longitude, 0.994, hemisphere, this.UPS_False_Easting, this.UPS_False_Northing);
            MapProjectionCoordinates polarStereographicCoordinates = polarStereographic.convertFromGeodetic(geodeticCoordinates);

            double easting = polarStereographicCoordinates.getEasting();
            double northing = polarStereographicCoordinates.getNorthing();

            return new UPSCoordinates(CoordinateType.Enum.universalPolarStereographic, hemisphere, easting, northing);
        }

        /// <summary>
        /// The function convertToGeodetic converts UPS (hemisphere, easting, 
        /// and northing) coordinates to geodetic (latitude and longitude) coordinates
        /// according to the current ellipsoid parameters.  If any errors occur, an 
        /// exception is thrown with a description of the error.
        /// </summary>
        /// <param name="upsCoordinates"></param>
        /// <returns></returns>
        public GeodeticCoordinates convertToGeodetic(UPSCoordinates upsCoordinates)
        {
            /*
             *    hemisphere    : Hemisphere either 'N' or 'S'              (input)
             *    easting       : Easting/X in meters                       (input)
             *    northing      : Northing/Y in meters                      (input)
             *    latitude      : Latitude in radians                       (output)
             *    longitude     : Longitude in radians                      (output)
             */

            string errorStatus = "";

            char hemisphere = upsCoordinates.getHemisphere();
            double easting = upsCoordinates.getEasting();
            double northing = upsCoordinates.getNorthing();

            if ((hemisphere != 'N') && (hemisphere != 'S'))
            {
                errorStatus += ErrorMessages.hemisphere;
            }
            if ((easting < MIN_EAST_NORTH) || (easting > MAX_EAST_NORTH))
            {
                errorStatus += ErrorMessages.easting;
            }
            if ((northing < MIN_EAST_NORTH) || (northing > MAX_EAST_NORTH))
            {
                errorStatus += ErrorMessages.northing;
            }

            //this.UPS_Origin_Latitude = MAX_ORIGIN_LAT;
            //if (hemisphere == 'N')
            //{
            //    UPS_Origin_Latitude = MAX_ORIGIN_LAT;
            //}
            //else if (hemisphere == 'S')
            //{
            //    UPS_Origin_Latitude = -MAX_ORIGIN_LAT;
            //}

            if (errorStatus.Length > 0)
            {
                throw new ArgumentException(errorStatus);
            }

            PolarStereographic polarStereographic = new PolarStereographic(this.semiMajorAxis, this.flattening, this.UPS_Origin_Longitude, 0.994, hemisphere, this.UPS_False_Easting, this.UPS_False_Northing);
            GeodeticCoordinates geodeticCoordinates = polarStereographic.convertToGeodetic(new MapProjectionCoordinates(CoordinateType.Enum.polarStereographicStandardParallel, easting, northing));

            double latitude = geodeticCoordinates.latitude;

            if ((latitude < 0) && (latitude >= (MAX_SOUTH_LAT + EPSILON)))
            {
                throw new ArgumentException(ErrorMessages.latitude);
            }
            if ((latitude >= 0) && (latitude < (MIN_NORTH_LAT - EPSILON)))
            {
                throw new ArgumentException(ErrorMessages.latitude);
            }

            return geodeticCoordinates;
        }
    }
}

// CLASSIFICATION: UNCLASSIFIED