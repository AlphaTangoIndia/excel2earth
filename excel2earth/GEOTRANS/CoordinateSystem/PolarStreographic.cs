// CLASSIFICATION: UNCLASSIFIED

using System;

namespace MSP.CCS
{
    /// <summary>
    /// Polar Stereographic.
    /// 
    /// Originally from GEOTRANS.
    /// </summary>
    public class PolarStereographic : CoordinateSystem
    {
        const double PI_OVER_2 = (Math.PI / 2.0);
        const double PI_OVER_4 = (Math.PI / 4.0);
        const double TWO_PI = (2.0 * Math.PI);

        private CoordinateType.Enum coordinateType;

        private double es;                             /* Eccentricity of ellipsoid    */
        private double es_OVER_2;                      /* es / 2.0 */
        private double Southern_Hemisphere;            /* Flag variable */
        private double Polar_tc;
        private double Polar_k90;
        private double Polar_a_mc;                     /* Polar_a * mc */
        private double two_Polar_a;                    /* 2.0 * Polar_a */

        /* Polar Stereographic projection Parameters */
        private double Polar_Standard_Parallel;        /* Latitude of origin in radians */
        private double Polar_Central_Meridian;         /* Longitude of origin in radians */
        private double Polar_False_Easting;            /* False easting in meters */
        private double Polar_False_Northing;           /* False northing in meters */

        /* Maximum variance for easting and northing values for WGS 84. */
        private double Polar_Delta_Easting;
        private double Polar_Delta_Northing;

        private double Polar_Scale_Factor;

        double MIN_SCALE_FACTOR = 0.1;
        double MAX_SCALE_FACTOR = 3.0;

        /// <summary>
        /// The constructor receives the ellipsoid
        /// parameters and Polar Stereograpic (Standard Parallel) projection parameters as inputs, and
        /// sets the corresponding state variables.  If any errors occur, an 
        /// exception is thrown with a description of the error.
        /// </summary>
        /// <param name="ellipsoidSemiMajorAxis">Semi-major axis of ellipsoid, in meters</param>
        /// <param name="ellipsoidFlattening">Flattening of ellipsoid</param>
        /// <param name="centralMeridian">Longitude down from pole, in radians</param>
        /// <param name="standardParallel">Latitude of true scale, in radians</param>
        /// <param name="falseEasting">Easting (X) at center of projection, in meters</param>
        /// <param name="falseNorthing">Northing (Y) at center of projection, in meters</param>
        public PolarStereographic(double ellipsoidSemiMajorAxis, double ellipsoidFlattening, double centralMeridian, double standardParallel, double falseEasting, double falseNorthing)
        {
            this.coordinateType = CoordinateType.Enum.polarStereographicStandardParallel;
            this.es = 0.08181919084262188000;
            this.es_OVER_2 = .040909595421311;
            this.Southern_Hemisphere = 0;
            this.Polar_tc = 1.0;
            this.Polar_k90 = 1.0033565552493;
            this.Polar_a_mc = 6378137.0;
            this.two_Polar_a = 12756274.0;
            this.Polar_Central_Meridian = 0.0;
            this.Polar_Standard_Parallel = ((Math.PI * 90) / 180);
            this.Polar_False_Easting = 0.0;
            this.Polar_False_Northing = 0.0;
            this.Polar_Scale_Factor = 1.0;
            this.Polar_Delta_Easting = 12713601.0;
            this.Polar_Delta_Northing = 12713601.0;

            double es2;
            double slat, sinolat, cosolat;
            double essin;
            double one_PLUS_es, one_MINUS_es;
            double one_PLUS_es_sinolat, one_MINUS_es_sinolat;
            double pow_es;
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
            if ((standardParallel < -PI_OVER_2) || (standardParallel > PI_OVER_2))
            { /* Origin Latitude out of range */
                errorStatus += ErrorMessages.originLatitude;
            }
            if ((centralMeridian < -Math.PI) || (centralMeridian > TWO_PI))
            { /* Origin Longitude out of range */
                errorStatus += ErrorMessages.centralMeridian;
            }

            if (errorStatus.Length > 0)
            {
                throw new ArgumentException(errorStatus);
            }

            this.semiMajorAxis = ellipsoidSemiMajorAxis;
            this.flattening = ellipsoidFlattening;

            two_Polar_a = 2.0 * semiMajorAxis;

            if (centralMeridian > Math.PI)
            {
                centralMeridian -= TWO_PI;
            }
            if (standardParallel < 0)
            {
                this.Southern_Hemisphere = 1;
                this.Polar_Standard_Parallel = -standardParallel;
                this.Polar_Central_Meridian = -centralMeridian;
            }
            else
            {
                Southern_Hemisphere = 0;
                this.Polar_Standard_Parallel = standardParallel;
                this.Polar_Central_Meridian = centralMeridian;
            }
            this.Polar_False_Easting = falseEasting;
            this.Polar_False_Northing = falseNorthing;

            es2 = 2 * this.flattening - this.flattening * this.flattening;
            es = Math.Sqrt(es2);
            es_OVER_2 = es / 2.0;

            if (Math.Abs(Math.Abs(Polar_Standard_Parallel) - PI_OVER_2) > 1.0e-10)
            {
                sinolat = Math.Sin(Polar_Standard_Parallel);
                essin = es * sinolat;
                pow_es = polarPow(essin);
                cosolat = Math.Cos(Polar_Standard_Parallel);
                double mc = cosolat / Math.Sqrt(1.0 - essin * essin);
                this.Polar_a_mc = semiMajorAxis * mc;
                this.Polar_tc = Math.Tan(PI_OVER_4 - Polar_Standard_Parallel / 2.0) / pow_es;
            }

            one_PLUS_es = 1.0 + es;
            one_MINUS_es = 1.0 - es;
            this.Polar_k90 = Math.Sqrt(Math.Pow(one_PLUS_es, one_PLUS_es) * Math.Pow(one_MINUS_es, one_MINUS_es));

            slat = Math.Sin(Math.Abs(standardParallel));
            one_PLUS_es_sinolat = 1.0 + es * slat;
            one_MINUS_es_sinolat = 1.0 - es * slat;
            this.Polar_Scale_Factor = ((1 + slat) / 2) * (Polar_k90 / Math.Sqrt(Math.Pow(one_PLUS_es_sinolat, one_PLUS_es) * Math.Pow(one_MINUS_es_sinolat, one_MINUS_es)));

            /* Calculate Radius */
            MapProjectionCoordinates tempCoordinates = convertFromGeodetic(new GeodeticCoordinates(CoordinateType.Enum.geodetic, centralMeridian, 0, 0));
            this.Polar_Delta_Northing = tempCoordinates.getNorthing();

            if (this.Polar_False_Northing != 0)
            {
                this.Polar_Delta_Northing -= this.Polar_False_Northing;
            }
            if (Polar_Delta_Northing < 0)
            {
                this.Polar_Delta_Northing = -this.Polar_Delta_Northing;
            }
            this.Polar_Delta_Northing *= 1.01;

            this.Polar_Delta_Easting = this.Polar_Delta_Northing;
        }

        /// <summary>
        /// The constructor receives the ellipsoid
        /// parameters and Polar Stereograpic (Scale Factor) projection parameters as inputs, and
        /// sets the corresponding state variables.  If any errors occur, an 
        /// exception is thrown with a description of the error.
        /// </summary>
        /// <param name="ellipsoidSemiMajorAxis">Semi-major axis of ellipsoid, in meters</param>
        /// <param name="ellipsoidFlattening">Flattening of ellipsoid</param>
        /// <param name="centralMeridian">Longitude down from pole, in radians</param>
        /// <param name="scaleFactor">Scale Factor</param>
        /// <param name="hemisphere">Hemisphere</param>
        /// <param name="falseEasting">Easting (X) at center of projection, in meters</param>
        /// <param name="falseNorthing">Northing (Y) at center of projection, in meters</param>
        public PolarStereographic(double ellipsoidSemiMajorAxis, double ellipsoidFlattening, double centralMeridian, double scaleFactor, char hemisphere, double falseEasting, double falseNorthing)
        {
            this.coordinateType = CoordinateType.Enum.polarStereographicScaleFactor;
            this.es = 0.08181919084262188000;
            this.es_OVER_2 = .040909595421311;
            this.Southern_Hemisphere = 0;
            this.Polar_tc = 1.0;
            this.Polar_k90 = 1.0033565552493;
            this.Polar_a_mc = 6378137.0;
            this.two_Polar_a = 12756274.0;
            this.Polar_Central_Meridian = 0.0;
            this.Polar_Standard_Parallel = PI_OVER_2;
            this.Polar_False_Easting = 0.0;
            this.Polar_False_Northing = 0.0;
            this.Polar_Scale_Factor = 1.0;
            this.Polar_Delta_Easting = 12713601.0;
            this.Polar_Delta_Northing = 12713601.0;

            double es2;
            double sinolat, cosolat;
            double essin;
            double pow_es;
            double mc;
            double one_PLUS_es, one_MINUS_es;
            double one_PLUS_es_sk, one_MINUS_es_sk;
            double sk, sk_PLUS_1;
            double tolerance = 1.0e-15;
            int count = 30;
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
            if ((scaleFactor < MIN_SCALE_FACTOR) || (scaleFactor > MAX_SCALE_FACTOR))
            {
                errorStatus += ErrorMessages.scaleFactor;
            }
            if ((centralMeridian < -Math.PI) || (centralMeridian > TWO_PI))
            { /* Origin Longitude out of range */
                errorStatus += ErrorMessages.centralMeridian;
            }
            if ((hemisphere != 'N') && (hemisphere != 'S'))
            {
                errorStatus += ErrorMessages.hemisphere;
            }

            if (errorStatus.Length > 0)
            {
               // throw CoordinateConversionException(errorStatus);
            }

            this.semiMajorAxis = ellipsoidSemiMajorAxis;
            this.flattening = ellipsoidFlattening;
            this.Polar_Scale_Factor = scaleFactor;
            this.Polar_False_Easting = falseEasting;
            this.Polar_False_Northing = falseNorthing;

            this.two_Polar_a = 2.0 * this.semiMajorAxis;
            es2 = 2 * this.flattening - this.flattening * this.flattening;
            es = Math.Sqrt(es2);
            es_OVER_2 = es / 2.0;

            one_PLUS_es = 1.0 + es;
            one_MINUS_es = 1.0 - es;
            this.Polar_k90 = Math.Sqrt(Math.Pow(one_PLUS_es, one_PLUS_es) * Math.Pow(one_MINUS_es, one_MINUS_es));

            sk = 0;
            sk_PLUS_1 = -1 + 2 * this.Polar_Scale_Factor;
            while (Math.Abs(sk_PLUS_1 - sk) > tolerance && count > 0)
            {
                sk = sk_PLUS_1;
                one_PLUS_es_sk = 1.0 + es * sk;
                one_MINUS_es_sk = 1.0 - es * sk;
                sk_PLUS_1 = ((2 * this.Polar_Scale_Factor * Math.Sqrt(Math.Pow(one_PLUS_es_sk, one_PLUS_es) * Math.Pow(one_MINUS_es_sk, one_MINUS_es))) / this.Polar_k90) - 1;
                count--;
            }

            if (count > 0)
            {
                //throw new ArgumentException(ErrorMessages.originLatitude);
            }

            double standardParallel = 0.0;
            if (sk_PLUS_1 >= -1.0 && sk_PLUS_1 <= 1.0)
            {
                standardParallel = Math.Asin(sk_PLUS_1);
            }
            else
            {
                throw new ArgumentException(ErrorMessages.originLatitude);
            }

            if (hemisphere == 'S')
            {
                standardParallel *= -1.0;
            }

            if (centralMeridian > Math.PI)
            {
                centralMeridian -= TWO_PI;
            }
            if (standardParallel < 0)
            {
                Southern_Hemisphere = 1;
                Polar_Standard_Parallel = -standardParallel;
                Polar_Central_Meridian = -centralMeridian;
            }
            else
            {
                Southern_Hemisphere = 0;
                Polar_Standard_Parallel = standardParallel;
                Polar_Central_Meridian = centralMeridian;
            }

            sinolat = Math.Sin(Polar_Standard_Parallel);

            if (Math.Abs(Math.Abs(Polar_Standard_Parallel) - PI_OVER_2) > 1.0e-10)
            {
                essin = es * sinolat;
                pow_es = polarPow(essin);
                cosolat = Math.Cos(Polar_Standard_Parallel);
                mc = cosolat / Math.Sqrt(1.0 - essin * essin);
                Polar_a_mc = semiMajorAxis * mc;
                Polar_tc = Math.Tan(PI_OVER_4 - Polar_Standard_Parallel / 2.0) / pow_es;
            }

            /* Calculate Radius */
            MapProjectionCoordinates tempCoordinates = convertFromGeodetic(new GeodeticCoordinates(CoordinateType.Enum.geodetic, centralMeridian, 0));
            Polar_Delta_Northing = tempCoordinates.getNorthing();

            if (this.Polar_False_Northing > 0)
            {
                this.Polar_Delta_Northing -= this.Polar_False_Northing;
            }
            if (this.Polar_Delta_Northing < 0)
            {
                this.Polar_Delta_Northing = -this.Polar_Delta_Northing;
            }
            this.Polar_Delta_Northing *= 1.01;

            this.Polar_Delta_Easting = this.Polar_Delta_Northing;
        }

        /// <summary>
        /// The function getStandardParallelParameters returns the current
        /// ellipsoid parameters and Polar (Standard Parallel) projection parameters
        /// </summary>
        /// <returns></returns>
        PolarStereographicStandardParallelParameters getStandardParallelParameters()
        {
            /*
             *  ellipsoidSemiMajorAxis          : Semi-major axis of ellipsoid, in meters         (output)
             *  ellipsoidFlattening             : Flattening of ellipsoid					      (output)
             *  centralMeridian                 : Longitude down from pole, in radians            (output)
             *  standardParallel                : Latitude of true scale, in radians              (output)
             *  falseEasting                    : Easting (X) at center of projection, in meters  (output)
             *  falseNorthing                   : Northing (Y) at center of projection, in meters (output)
             */

            return new PolarStereographicStandardParallelParameters(CoordinateType.Enum.polarStereographicStandardParallel, Polar_Central_Meridian, Polar_Standard_Parallel, Polar_False_Easting, Polar_False_Northing);
        }

        /// <summary>
        /// The function getScaleFactorParameters returns the current
        /// ellipsoid parameters and Polar (Scale Factor) projection parameters.
        /// </summary>
        /// <returns></returns>
        public PolarStereographicScaleFactorParameters getScaleFactorParameters()
        {
            /*
             *  ellipsoidSemiMajorAxis          : Semi-major axis of ellipsoid, in meters         (output)
             *  ellipsoidFlattening             : Flattening of ellipsoid					      (output)
             *  centralMeridian                 : Longitude down from pole, in radians            (output)
             *  scaleFactor                     : Scale factor                                    (output)
             *  falseEasting                    : Easting (X) at center of projection, in meters  (output)
             *  falseNorthing                   : Northing (Y) at center of projection, in meters (output)
             */

            if (this.Southern_Hemisphere == 0)
            {
                return new PolarStereographicScaleFactorParameters(CoordinateType.Enum.polarStereographicScaleFactor, Polar_Central_Meridian, Polar_Scale_Factor, 'N', Polar_False_Easting, Polar_False_Northing);
            }
            else
            {
                return new PolarStereographicScaleFactorParameters(CoordinateType.Enum.polarStereographicScaleFactor, Polar_Central_Meridian, Polar_Scale_Factor, 'S', Polar_False_Easting, Polar_False_Northing);
            }
        }

        /// <summary>
        /// The function convertFromGeodetic converts geodetic
        /// coordinates (latitude and longitude) to Polar Stereographic coordinates
        /// (easting and northing), according to the current ellipsoid
        /// and Polar Stereographic projection parameters. If any errors occur, 
        /// an exception is thrown with a description of the error.
        /// </summary>
        /// <param name="geodeticCoordinates">Geodetic Coordinates containing Longitude and Latitude, in radians</param>
        /// <returns>Map Projection Coordinates containing easting (X) and Northing (Y), in meters</returns>
        public MapProjectionCoordinates convertFromGeodetic(GeodeticCoordinates geodeticCoordinates)
        {
            double dlam;
            double slat;
            double essin;
            double t;
            double rho;
            double pow_es;
            double easting, northing;
            string errorStatus = "";

            double longitude = geodeticCoordinates.longitude;
            double latitude = geodeticCoordinates.latitude;

            if ((latitude < -PI_OVER_2) || (latitude > PI_OVER_2))
            {   /* latitude out of range */
                errorStatus += ErrorMessages.latitude;
            }
            else if ((latitude < 0) && (Southern_Hemisphere == 0))
            {   /* latitude and Origin Latitude in different hemispheres */
                errorStatus += ErrorMessages.latitude;
            }
            else if ((latitude > 0) && (Southern_Hemisphere == 1))
            {   /* latitude and Origin Latitude in different hemispheres */
                errorStatus += ErrorMessages.latitude;
            }
            if ((longitude < -Math.PI) || (longitude > TWO_PI))
            {  /* longitude out of range */
                errorStatus += ErrorMessages.longitude;
            }

            if (errorStatus.Length > 0)
            {
                throw new ArgumentException(errorStatus);
            }

            if (Math.Abs(Math.Abs(latitude) - PI_OVER_2) < 1.0e-10)
            {
                easting = Polar_False_Easting;
                northing = Polar_False_Northing;
            }
            else
            {
                if (Southern_Hemisphere != 0)
                {
                    longitude *= -1.0;
                    latitude *= -1.0;
                }
                dlam = longitude - this.Polar_Central_Meridian;
                if (dlam > Math.PI)
                {
                    dlam -= TWO_PI;
                }
                if (dlam < -Math.PI)
                {
                    dlam += TWO_PI;
                }
                slat = Math.Sin(latitude);
                essin = es * slat;
                pow_es = polarPow(essin);
                t = Math.Tan(PI_OVER_4 - latitude / 2.0) / pow_es;

                if (Math.Abs(Math.Abs(this.Polar_Standard_Parallel) - PI_OVER_2) > 1.0e-10)
                {
                    rho = this.Polar_a_mc * t / this.Polar_tc;
                }
                else
                {
                    rho = this.two_Polar_a * t / this.Polar_k90;
                }

                if (Southern_Hemisphere != 0)
                {
                    easting = -(rho * Math.Sin(dlam) - this.Polar_False_Easting);
                    northing = rho * Math.Cos(dlam) + this.Polar_False_Northing;
                }
                else
                {
                    easting = rho * Math.Sin(dlam) + this.Polar_False_Easting;
                    northing = -rho * Math.Cos(dlam) + this.Polar_False_Northing;
                }
            }

            return new MapProjectionCoordinates(coordinateType, easting, northing);
        }

        /// <summary>
        /// The function convertToGeodetic converts Polar
        /// Stereographic coordinates (easting and northing) to geodetic
        /// coordinates (latitude and longitude) according to the current ellipsoid
        /// and Polar Stereographic projection Parameters. If any errors occur, 
        /// an exception is thrown with a description of the error.
        /// </summary>
        /// <param name="mapProjectionCoordinates"></param>
        /// <returns></returns>
        public GeodeticCoordinates convertToGeodetic(MapProjectionCoordinates mapProjectionCoordinates)
        {
            /*
             *  easting          : Easting (X), in meters                   (input)
             *  northing         : Northing (Y), in meters                  (input)
             *  longitude        : Longitude, in radians                    (output)
             *  latitude         : Latitude, in radians                     (output)
             *
             */

            double dy = 0, dx = 0;
            double rho = 0;
            double t;
            double PHI, sin_PHI;
            double tempPHI = 0.0;
            double essin;
            double pow_es;
            double delta_radius;
            double longitude, latitude;
            string errorStatus = "";

            double easting = mapProjectionCoordinates.getEasting();
            double northing = mapProjectionCoordinates.getNorthing();

            double min_easting = this.Polar_False_Easting - this.Polar_Delta_Easting;
            double max_easting = this.Polar_False_Easting + this.Polar_Delta_Easting;
            double min_northing = this.Polar_False_Northing - this.Polar_Delta_Northing;
            double max_northing = this.Polar_False_Northing + this.Polar_Delta_Northing;

            if (easting > max_easting || easting < min_easting)
            { /* easting out of range */
                errorStatus += ErrorMessages.easting;
            }
            if (northing > max_northing || northing < min_northing)
            { /* northing out of range */
                errorStatus += ErrorMessages.northing;
            }

            if (errorStatus.Length > 0)
            {
                throw new ArgumentException(errorStatus);
            }

            dy = northing - this.Polar_False_Northing;
            dx = easting - this.Polar_False_Easting;

            /* Radius of point with origin of false easting, false northing */
            rho = Math.Sqrt(dx * dx + dy * dy);

            delta_radius = Math.Sqrt(this.Polar_Delta_Easting * this.Polar_Delta_Easting + this.Polar_Delta_Northing * this.Polar_Delta_Northing);

            if (rho > delta_radius)
            { /* Point is outside of projection area */
                throw new ArgumentException(ErrorMessages.radius);
            }

            if ((dy == 0.0) && (dx == 0.0))
            {
                latitude = PI_OVER_2;
                longitude = this.Polar_Central_Meridian;

            }
            else
            {
                if (this.Southern_Hemisphere != 0)
                {
                    dy *= -1.0;
                    dx *= -1.0;
                }

                if (Math.Abs(Math.Abs(this.Polar_Standard_Parallel) - PI_OVER_2) > 1.0e-10)
                {
                    t = rho * this.Polar_tc / (this.Polar_a_mc);
                }
                else
                {
                    t = rho * Polar_k90 / (two_Polar_a);
                }
                PHI = PI_OVER_2 - 2.0 * Math.Atan(t);
                while (Math.Abs(PHI - tempPHI) > 1.0e-10)
                {
                    tempPHI = PHI;
                    sin_PHI = Math.Sin(PHI);
                    essin = es * sin_PHI;
                    pow_es = polarPow(essin);
                    PHI = PI_OVER_2 - 2.0 * Math.Atan(t * pow_es);
                }
                latitude = PHI;
                longitude = Polar_Central_Meridian + Math.Atan2(dx, -dy);

                if (longitude > Math.PI)
                {
                    longitude -= TWO_PI;
                }
                else if (longitude < -Math.PI)
                {
                    longitude += TWO_PI;
                }

                // Force distorted values
                if (latitude > PI_OVER_2)  /* force distorted values to 90, -90 degrees */
                {
                    latitude = PI_OVER_2;
                }
                else if (latitude < -PI_OVER_2)
                {
                    latitude = -PI_OVER_2;
                }

                if (longitude > Math.PI)  /* force distorted values to 180, -180 degrees */
                {
                    longitude = Math.PI;
                }
                else if (longitude < -Math.PI)
                {
                    longitude = -Math.PI;
                }
            }
            if (Southern_Hemisphere != 0)
            {
                latitude *= -1.0;
                longitude *= -1.0;
            }

            return new GeodeticCoordinates(CoordinateType.Enum.geodetic, longitude, latitude);
        }

        private double polarPow(double esSin)
        {
            return Math.Pow((1.0 - esSin) / (1.0 + esSin), this.es_OVER_2);
        }
    }
}

// CLASSIFICATION: UNCLASSIFIED