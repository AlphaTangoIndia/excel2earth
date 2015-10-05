// CLASSIFICATION: UNCLASSIFIED

using System;

namespace MSP.CCS
{
    /// <summary>
    /// Universal Transverse Mercator.
    /// 
    /// Originally from GEOTRANS.
    /// </summary>
    class UTM : CoordinateSystem
    {
        const double EPSILON = 1.75e-7;                     /* approx 1.0e-5 degrees (~1 meter) in radians */
        const double PI_OVER_180 = Math.PI / 180.0;         /* PI                        */
        const double MIN_LAT = ((-80.5 * Math.PI) / 180.0); /* -80.5 degrees in radians    */
        const double MAX_LAT = ((84.5 * Math.PI) / 180.0);  /* 84.5 degrees in radians     */
        const double MIN_EASTING = 100000.0;
        const double MAX_EASTING = 900000.0;
        const double MIN_NORTHING = 0.0;
        const double MAX_NORTHING = 10000000.0;

        //private double semiMajorAxis;
        //private double flattening;
        private int zoneOverride;

        public UTM()
        {
            //CoordinateSystem CoordSys = new CoordinateSystem();
            //CoordSys.getEllipsoidParameters(ref this.semiMajorAxis, ref this.flattening);
            //this.zoneOverride = 0;
        }

        /// <summary>
        /// The constructor receives the ellipsoid parameters and
        /// UTM zone zoneOverride parameter as inputs, and sets the corresponding state
        /// variables.  If any errors occur, an exception is thrown with a description 
        /// of the error.
        /// </summary>
        /// <param name="ellipsoidSemiMajorAxis">Semi-major axis of ellipsoid, in meters</param>
        /// <param name="ellipsoidFlattening">Flattening of ellipsoid</param>
        /// <param name="zoneOverride">UTM zoneOverride zone, zero indicates no zoneOverride</param>
        public UTM(double ellipsoidSemiMajorAxis, double ellipsoidFlattening, int zoneOverride)
        {
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

            if ((zoneOverride < 0) || (zoneOverride > 60))
            {
                errorStatus += ErrorMessages.zoneOverride;
            }

            if (errorStatus.Length > 0)
            {
                throw new ArgumentException(errorStatus);
            }

            this.semiMajorAxis = ellipsoidSemiMajorAxis;
            this.flattening = ellipsoidFlattening;
            this.zoneOverride = zoneOverride;
        }

        /// <summary>
        /// The function getParameters returns the current ellipsoid
        /// parameters and UTM zone zoneOverride parameter.
        /// </summary>
        /// <returns>
        /// ellipsoidSemiMajorAxis    : Semi-major axis of ellipsoid, in meters       (output)
        /// ellipsoidFlattening       : Flattening of ellipsoid                       (output)
        /// zoneOverride              : UTM zoneOverride zone, zero indicates no zoneOverride (output)
        /// </returns>
        public UTMParameters getParameters()
        {
            return new UTMParameters(CoordinateType.Enum.universalTransverseMercator, this.zoneOverride);
        }

        /// <summary>
        /// The function convertFromGeodetic converts geodetic (latitude and
        /// longitude) coordinates to UTM projection (zone, hemisphere, easting and
        /// northing) coordinates according to the current ellipsoid and UTM zone
        /// zoneOverride parameters.  If any errors occur, an exception is thrown with a description 
        /// of the error.
        /// </summary>
        /// <param name="geodeticCoordinates"></param>
        /// <returns></returns>
        public UTMCoordinates convertFromGeodetic(GeodeticCoordinates geodeticCoordinates)
        {
            /*
             *
             *    longitude         : Longitude in radians                (input)
             *    latitude          : Latitude in radians                 (input)
             *    zone              : UTM zone                            (output)
             *    hemisphere        : North or South hemisphere           (output)
             *    easting           : Easting (X) in meters               (output)
             *    northing          : Northing (Y) in meters              (output)
             */

            long Lat_Degrees;
            long Long_Degrees;
            int temp_zone;
            char hemisphere;
            double False_Northing = 0;
            string errorStatus = "";

            double longitude = geodeticCoordinates.longitude;
            double latitude = geodeticCoordinates.latitude;

            if ((latitude < (MIN_LAT - EPSILON)) || (latitude >= (MAX_LAT + EPSILON)))
            { /* latitude out of range */
                errorStatus += ErrorMessages.latitude;
            }
            if ((longitude < -Math.PI) || (longitude > (2 * Math.PI)))
            { /* longitude out of range */
                errorStatus += ErrorMessages.longitude;
            }

            if (errorStatus.Length > 0)
            {
                throw new ArgumentException(errorStatus);
            }

            if ((latitude > -1.0e-9) && (latitude < 0))
            {
                latitude = 0.0;
            }

            if (longitude < 0)
            {
                longitude += (2 * Math.PI) + 1.0e-10;
            }

            Lat_Degrees = (long)(latitude * 180.0 / Math.PI);
            Long_Degrees = (long)(longitude * 180.0 / Math.PI);

            if (longitude < Math.PI)
            {
                temp_zone = (int)(31 + ((longitude * 180.0 / Math.PI) / 6.0));
            }
            else
            {
                temp_zone = (int)(((longitude * 180.0 / Math.PI) / 6.0) - 29);
            }

            if (temp_zone > 60)
            {
                temp_zone = 1;
            }

            if (zoneOverride > 0)
            {
                if ((temp_zone == 1) && (this.zoneOverride == 60))
                {
                    temp_zone = (int)this.zoneOverride;
                }
                else if ((temp_zone == 60) && (zoneOverride == 1))
                {
                    temp_zone = (int)this.zoneOverride;
                }
                else if (((temp_zone - 1) <= zoneOverride) && (zoneOverride <= (temp_zone + 1)))
                {
                    temp_zone = (int)this.zoneOverride;
                }
                else
                {
                    throw new ArgumentException(ErrorMessages.zoneOverride);
                }
            }

            TransverseMercator transverseMercator = getTransverseMercator(temp_zone);

            if (latitude < 0)
            {
                False_Northing = 10000000.0;
                hemisphere = 'S';
            }
            else
            {
                hemisphere = 'N';
            }

            MapProjectionCoordinates transverseMercatorCoordinates = transverseMercator.convertFromGeodetic(new GeodeticCoordinates(CoordinateType.Enum.geodetic, longitude, latitude));
            double easting = transverseMercatorCoordinates.getEasting();
            double northing = transverseMercatorCoordinates.getNorthing() + False_Northing;

            if ((easting < MIN_EASTING) || (easting > MAX_EASTING))
            {
                
                throw new ArgumentException(ErrorMessages.easting);
            }

            if ((northing < MIN_NORTHING) || (northing > MAX_NORTHING))
            {
                throw new ArgumentException(ErrorMessages.northing);
            }

            return new UTMCoordinates(CoordinateType.Enum.universalTransverseMercator, temp_zone, hemisphere, easting, northing);
        }

        /// <summary>
        /// The function convertToGeodetic converts UTM projection (zone,
        /// hemisphere, easting and northing) coordinates to geodetic (latitude
        /// and longitude) coordinates, according to the current ellipsoid
        /// parameters. If any errors occur, an exception is thrown with a description 
        /// of the error.
        /// </summary>
        /// <param name="utmCoordinates"></param>
        /// <returns></returns>
        public GeodeticCoordinates convertToGeodetic(UTMCoordinates utmCoordinates)
        {
            /*
             *    zone              : UTM zone                               (input)
             *    hemisphere        : North or South hemisphere              (input)
             *    easting           : Easting (X) in meters                  (input)
             *    northing          : Northing (Y) in meters                 (input)
             *    longitude         : Longitude in radians                   (output)
             *    latitude          : Latitude in radians                    (output)
             */

            double False_Northing = 0;
            string errorStatus = "";

            int zone = (int)utmCoordinates.zone;
            char hemisphere = utmCoordinates.hemisphere;
            double easting = utmCoordinates.easting;
            double northing = utmCoordinates.northing;

            if ((zone < 1) || (zone > 60))
            {
                errorStatus += ErrorMessages.zone;
            }
            if ((hemisphere != 'S') && (hemisphere != 'N'))
            {
                errorStatus += ErrorMessages.hemisphere;
            }
            if ((easting < MIN_EASTING) || (easting > MAX_EASTING))
            {
                errorStatus += ErrorMessages.easting;
            }
            if ((northing < MIN_NORTHING) || (northing > MAX_NORTHING))
            {
                errorStatus += ErrorMessages.northing;
            }

            if (errorStatus.Length > 0)
            {
                throw new ArgumentException(errorStatus);
            }

            TransverseMercator transverseMercator = getTransverseMercator(zone);

            if (hemisphere == 'S')
            {
                False_Northing = 10000000;
            }

            GeodeticCoordinates geodeticCoordinates = transverseMercator.convertToGeodetic(new MapProjectionCoordinates(CoordinateType.Enum.transverseMercator, easting, northing - False_Northing));
            //geodeticCoordinates.setWarningMessage("");

            double latitude = geodeticCoordinates.latitude;

            if ((latitude < (MIN_LAT - EPSILON)) || (latitude >= (MAX_LAT + EPSILON)))
            {   /* latitude out of range */
                throw new ArgumentException(ErrorMessages.northing);
            }

            return geodeticCoordinates;
        }

        private TransverseMercator getTransverseMercator(int zone)
        {
            double centralMeridian;

            if (zone >= 31)
            {
                centralMeridian = ((6 * zone - 183) * PI_OVER_180);
            }
            else
            {
                centralMeridian = ((6 * zone + 177) * PI_OVER_180);
            }

            return new TransverseMercator(this.semiMajorAxis, this.flattening, centralMeridian, 0, 500000.0, 0.0, 0.9996);
        }
    }
}

// CLASSIFICATION: UNCLASSIFIED