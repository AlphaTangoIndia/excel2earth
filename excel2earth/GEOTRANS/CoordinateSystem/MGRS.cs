// CLASSIFICATION: UNCLASSIFIED

using System;
using System.Collections.Generic;

namespace MSP.CCS
{
    /// <summary>
    /// Military Grid Reference System.
    /// 
    /// Originally from GEOTRANS.
    /// </summary>
    public class MGRS : CoordinateSystem
    {
        const double EPSILON = 1.75e-7;   /* approx 1.0e-5 degrees (~1 meter) in radians */

        const int LETTER_A = 0;  // ARRAY INDEX FOR LETTER A
        const int LETTER_B = 1;  // ARRAY INDEX FOR LETTER B
        const int LETTER_C = 2;  // ARRAY INDEX FOR LETTER C
        const int LETTER_D = 3;  // ARRAY INDEX FOR LETTER D
        const int LETTER_E = 4;  // ARRAY INDEX FOR LETTER E
        const int LETTER_F = 5;  // ARRAY INDEX FOR LETTER F
        const int LETTER_G = 6;  // ARRAY INDEX FOR LETTER G
        const int LETTER_H = 7;  // ARRAY INDEX FOR LETTER H
        const int LETTER_I = 8;  // ARRAY INDEX FOR LETTER I
        const int LETTER_J = 9;  // ARRAY INDEX FOR LETTER J
        const int LETTER_K = 10; // ARRAY INDEX FOR LETTER K
        const int LETTER_L = 11; // ARRAY INDEX FOR LETTER L
        const int LETTER_M = 12; // ARRAY INDEX FOR LETTER M
        const int LETTER_N = 13; // ARRAY INDEX FOR LETTER N
        const int LETTER_O = 14; // ARRAY INDEX FOR LETTER O
        const int LETTER_P = 15; // ARRAY INDEX FOR LETTER P
        const int LETTER_Q = 16; // ARRAY INDEX FOR LETTER Q
        const int LETTER_R = 17; // ARRAY INDEX FOR LETTER R
        const int LETTER_S = 18; // ARRAY INDEX FOR LETTER S
        const int LETTER_T = 19; // ARRAY INDEX FOR LETTER T
        const int LETTER_U = 20; // ARRAY INDEX FOR LETTER U
        const int LETTER_V = 21; // ARRAY INDEX FOR LETTER V
        const int LETTER_W = 22; // ARRAY INDEX FOR LETTER W
        const int LETTER_X = 23; // ARRAY INDEX FOR LETTER X
        const int LETTER_Y = 24; // ARRAY INDEX FOR LETTER Y
        const int LETTER_Z = 25; // ARRAY INDEX FOR LETTER Z

        const int MGRS_LETTERS = 3;

        const double ONEHT = 100000.0;      /* ONE HUNDRED THOUSAND                  */
        const double TWOMIL = 2000000.0;    /* TWO MILLION                           */

        const double PI_OVER_2 = (Math.PI / 2.0);
        const double PI_OVER_180 = (Math.PI / 180.0);

        const double MIN_EASTING = 100000.0;
        const double MAX_EASTING = 900000.0;
        const double MIN_NORTHING = 0.0;
        const double MAX_NORTHING = 10000000.0;
        const int MAX_PRECISION = 5;   /* Maximum precision of easting & northing */
        const double MIN_MGRS_NON_POLAR_LAT = -80.0 * (Math.PI / 180.0); /* -80 degrees in radians    */
        const double MAX_MGRS_NON_POLAR_LAT = 84.0 * (Math.PI / 180.0);  /* 84 degrees in radians     */

        const double MIN_EAST_NORTH = 0.0;
        const double MAX_EAST_NORTH = 3999999.0;

        const double MIN_LAT = ((-80.5 * Math.PI) / 180.0); /* -80.5 degrees in radians    */
        const double MAX_LAT = ((84.5 * Math.PI) / 180.0);  /* 84.5 degrees in radians     */

        const double _6 = Math.PI / 30.0;
        const double _8 = Math.PI / 22.5;
        const double _72 = (72.0 * (Math.PI / 180.0));
        const double _80 = (80.0 * (Math.PI / 180.0));
        const double _80_5 = (80.5 * (Math.PI / 180.0));
        const double _84_5 = (84.5 * (Math.PI / 180.0));

        const double _500000 = 500000.0;

        /*
         *    CLARKE_1866 : Ellipsoid code for CLARKE_1866
         *    CLARKE_1880 : Ellipsoid code for CLARKE_1880
         *    BESSEL_1841 : Ellipsoid code for BESSEL_1841
         *    BESSEL_1841_NAMIBIA : Ellipsoid code for BESSEL 1841 (NAMIBIA)
         */
        const string CLARKE_1866 = "CC";
        const string CLARKE_1880 = "CD";
        const string BESSEL_1841 = "BR";
        const string BESSEL_1841_NAMIBIA = "BN";

        private struct Latitude_Band
        {
            public int letter;             /* letter representing latitude band  */
            public double min_northing;    /* minimum northing for latitude band */
            public double north;           /* upper latitude for latitude band   */
            public double south;           /* lower latitude for latitude band   */
            public double northing_offset; /* latitude band northing offset      */

            public Latitude_Band(int letter, double min_northing, double north, double south, double northing_offset)
            {
                this.letter = letter;
                this.min_northing = min_northing;
                this.north = north;
                this.south = south;
                this.northing_offset = northing_offset;
            }
        };

        private Latitude_Band[] Latitude_Band_Table = (new Latitude_Band[]
        {
            new Latitude_Band (LETTER_C, 1100000.0, -72.0, -80.5, 0.0),
            new Latitude_Band (LETTER_D, 2000000.0, -64.0, -72.0, 2000000.0),
            new Latitude_Band (LETTER_E, 2800000.0, -56.0, -64.0, 2000000.0),
            new Latitude_Band (LETTER_F, 3700000.0, -48.0, -56.0, 2000000.0),
            new Latitude_Band (LETTER_G, 4600000.0, -40.0, -48.0, 4000000.0),
            new Latitude_Band (LETTER_H, 5500000.0, -32.0, -40.0, 4000000.0),
            new Latitude_Band (LETTER_J, 6400000.0, -24.0, -32.0, 6000000.0),
            new Latitude_Band (LETTER_K, 7300000.0, -16.0, -24.0, 6000000.0),
            new Latitude_Band (LETTER_L, 8200000.0, -8.0, -16.0, 8000000.0),
            new Latitude_Band (LETTER_M, 9100000.0, 0.0, -8.0, 8000000.0),
            new Latitude_Band (LETTER_N, 0.0, 8.0, 0.0, 0.0),
            new Latitude_Band (LETTER_P, 800000.0, 16.0, 8.0, 0.0),
            new Latitude_Band (LETTER_Q, 1700000.0, 24.0, 16.0, 0.0),
            new Latitude_Band (LETTER_R, 2600000.0, 32.0, 24.0, 2000000.0),
            new Latitude_Band (LETTER_S, 3500000.0, 40.0, 32.0, 2000000.0),
            new Latitude_Band (LETTER_T, 4400000.0, 48.0, 40.0, 4000000.0),
            new Latitude_Band (LETTER_U, 5300000.0, 56.0, 48.0, 4000000.0),
            new Latitude_Band (LETTER_V, 6200000.0, 64.0, 56.0, 6000000.0),
            new Latitude_Band (LETTER_W, 7000000.0, 72.0, 64.0, 6000000.0),
            new Latitude_Band (LETTER_X, 7900000.0, 84.5, 72.0, 6000000.0)
        });

        struct UPS_Constant
        {
            public int letter;            /* letter representing latitude band      */
            public int ltr2_low_value;    /* 2nd letter range - low number         */
            public int ltr2_high_value;   /* 2nd letter range - high number          */
            public int ltr3_high_value;   /* 3rd letter range - high number (UPS)   */
            public double false_easting;   /* False easting based on 2nd letter      */
            public double false_northing;  /* False northing based on 3rd letter     */

            public UPS_Constant(int letter, int ltr2_low_value, int ltr2_high_value, int ltr3_high_value, double false_easting, double false_northing)
            {
                this.letter = letter;
                this.ltr2_low_value = ltr2_low_value;
                this.ltr2_high_value = ltr2_high_value;
                this.ltr3_high_value = ltr3_high_value;
                this.false_easting = false_easting;
                this.false_northing = false_northing;
            }
        };

        private UPS_Constant[] UPS_Constant_Table = (new UPS_Constant[]
        {
            new UPS_Constant (LETTER_A, LETTER_J, LETTER_Z, LETTER_Z, 800000.0, 800000.0),
            new UPS_Constant (LETTER_B, LETTER_A, LETTER_R, LETTER_Z, 2000000.0, 800000.0),
            new UPS_Constant (LETTER_Y, LETTER_J, LETTER_Z, LETTER_P, 800000.0, 1300000.0),
            new UPS_Constant (LETTER_Z, LETTER_A, LETTER_J, LETTER_P, 2000000.0, 1300000.0)
        });

        //private CoordinateSystem CoordSys = new CoordinateSystem();

        //private double semiMajorAxis;
        //private double flattening;
        //private string ellipsoidCode = "   ";

        private UPS ups;
        private UTM utm;

        /// <summary>
        /// The function makeMGRSString constructs an MGRS string
        /// from its component parts.
        /// </summary>
        /// <param name="zone">UTM Zone</param>
        /// <param name="letters">MGRS coordinate string letters</param>
        /// <param name="easting">Easting value</param>
        /// <param name="northing">Northing value</param>
        /// <param name="precision">Precision level of MGRS string</param>
        /// <returns></returns>
        private string makeMGRSString(long zone, int[] letters, double easting, double northing, long precision)
        {
            double divisor;
            string MGRSString = "";

            if (zone > 0)
            {
                MGRSString = zone.ToString();
            }
            else
            {
                MGRSString = "  "; // 2 spaces
            }

            for (int i = 0; i < 3; i++)
            {
                MGRSString += (char)(letters[i] + (int)'A');
            }

            divisor = Math.Pow(10.0, (5.0 - precision));

            // Easting
            easting = Math.IEEERemainder(easting, 1e5);
            if (easting >= 99999.5)
            {
                easting = 99999.0;
            }
            else if (easting < 0)
            {
                easting = 1e5 + easting;
            }
            
            MGRSString += ((long)(easting / divisor)).ToString("D" + precision.ToString());

            // Northing
            northing = Math.IEEERemainder(northing, 1e5);
            if (northing >= 99999.5)
            {
                northing = 99999.0;
            }
            else if (northing < 0)
            {
                northing = 1e5 + northing;
            }

            MGRSString += ((long)(northing / divisor)).ToString("D" + precision.ToString());

            return MGRSString;
        }

        /// <summary>
        /// The function breakMGRSString breaks down an MGRS
        /// coordinate string into its component parts.
        /// </summary>
        /// <param name="_MGRSString">MGRS coordinate string</param>
        /// <param name="zone">UTM Zone</param>
        /// <param name="letters">MGRS coordinate string letters</param>
        /// <param name="easting">Easting value</param>
        /// <param name="northing">Northing value</param>
        /// <param name="precision">Precision level of MGRS string</param>
        private void breakMGRSString(string _MGRSString, ref long zone, ref long[] letters, ref double easting, ref double northing, ref long precision)
        {
            string strZone = "";
            string strLetters = "";
            string strNorthingEasting = "";

            int i = 0;

            string MGRSString = _MGRSString.ToUpper();

            while (MGRSString[i] == ' ')
            {
                i++;  /* skip any leading blanks */
            }

            // Zone
            while (Char.IsDigit(MGRSString[i]))
            {
                strZone += MGRSString[i++].ToString();
            }
            if (strZone.Length > 0)
            {
                zone = (long)Convert.ToInt32(strZone);
                if ((zone < 1) || (zone > 60))
                {
                    throw new ArgumentException(ErrorMessages.mgrsstaticString);
                }
            }
            else
            {
                zone = 0;
            }
            // Letters
            while (Char.IsLetter(MGRSString[i]))
            {
                strLetters += MGRSString[i++].ToString();
            }
            if (strLetters.Length == 3)
            {
                for (int j = 0; j < 3; j++)
                {
                    letters[j] = (long)strLetters[j] - (long)'A';
                    if ((letters[j] == LETTER_I) || (letters[j] == LETTER_O))
                    {
                        throw new ArgumentException(ErrorMessages.mgrsstaticString);
                    }
                }
            }
            else
            {
                throw new ArgumentException(ErrorMessages.mgrsstaticString);
            }

            while (i < MGRSString.Length)
            {
                if (Char.IsDigit(MGRSString[i]))
                {
                    strNorthingEasting += MGRSString[i++].ToString();
                }
                else
                {
                    break;
                }
            }

            if ((strNorthingEasting.Length <= 10) && (strNorthingEasting.Length % 2 == 0))
            {
                int n = (int)(strNorthingEasting.Length / 2);
                double multiplier = Math.Pow(10.0, 5.0 - n);
                /* get easting & northing */
                precision = (long)n;
                if (n > 0)
                {
                    easting = Convert.ToDouble(strNorthingEasting.Substring(0, n)) * multiplier;
                    northing = Convert.ToDouble(strNorthingEasting.Substring(n, n)) * multiplier;
                }
                else
                {
                    easting = 0.0;
                    northing = 0.0;
                }
            }
            else
            {
                throw new ArgumentException(ErrorMessages.mgrsstaticString);
            }
        }

        public MGRS()
        {
            //this.CoordSys.getEllipsoidParameters(ref this.semiMajorAxis, ref this.flattening);
            this.ups = new UPS(this.semiMajorAxis, this.flattening);
            this.utm = new UTM(this.semiMajorAxis, this.flattening, 0);
        }

        /// <summary>
        /// The constructor receives the ellipsoid parameters and sets
        /// the corresponding state variables. If any errors occur, an
        /// exception is thrown with a description of the error.
        /// </summary>
        /// <param name="ellipsoidSemiMajorAxis">Semi-major axis of ellipsoid in meters</param>
        /// <param name="ellipsoidFlattening">Flattening of ellipsoid</param>
        /// <param name="ellipsoidCode">2-letter code for ellipsoid</param>
        public MGRS(double ellipsoidSemiMajorAxis, double ellipsoidFlattening, string ellipsoidCode)
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

            if (errorStatus.Length > 0)
            {
                throw new ArgumentException(errorStatus);
            }

            this.semiMajorAxis = ellipsoidSemiMajorAxis;
            this.flattening = ellipsoidFlattening;
            this.ellipsoidCode = ellipsoidCode;

            ups = new UPS(this.semiMajorAxis, this.flattening);
            utm = new UTM(this.semiMajorAxis, this.flattening, 0);
        }
        
        /// <summary>
        /// The function getParameters returns the current ellipsoid
        /// parameters.
        /// </summary>
        /// <returns>
        /// ellipsoidSemiMajorAxis     : Semi-major axis of ellipsoid, in meters (output)
        /// ellipsoidFlattening        : Flattening of ellipsoid                 (output)
        /// ellipsoidCode              : 2-letter code for ellipsoid             (output)
        /// </returns>
        public EllipsoidParameters getParameters()
        {
            return new EllipsoidParameters(this.semiMajorAxis, this.flattening, this.ellipsoidCode);
        }

        /// <summary>
        /// The function convertFromGeodetic converts Geodetic (latitude and
        /// longitude) coordinates to an MGRS coordinate string, according to the
        /// current ellipsoid parameters.  If any errors occur, an exception
        /// is thrown with a description of the error.
        /// </summary>
        /// <param name="geodeticCoordinates">Geodetic Coordinates</param>
        /// <param name="precision">Precision level of MGRS string (0 to 5)</param>
        /// <returns>MGRS Coordinates</returns>
        public MGRSorUSNGCoordinates convertFromGeodetic(GeodeticCoordinates geodeticCoordinates, long precision)
        {
            MGRSorUSNGCoordinates mgrsorUSNGCoordinates = new MGRSorUSNGCoordinates();
            string errorStatus = "";

            double latitude = geodeticCoordinates.latitude;
            double longitude = geodeticCoordinates.longitude;

            if ((latitude < -PI_OVER_2) || (latitude > PI_OVER_2))
            { /* latitude out of range */
                errorStatus = ErrorMessages.latitude;
            }
            if ((longitude < -Math.PI) || (longitude > (2 * Math.PI)))
            { /* longitude out of range */
                errorStatus = ErrorMessages.longitude;
            }
            if ((precision < 0) || (precision > MAX_PRECISION))
            {
                errorStatus = ErrorMessages.precision;
            }

            if (errorStatus.Length > 0)
            {
                throw new ArgumentException(errorStatus);
            }

            // If the latitude is within the valid mgrs non polar range [-80, 84),
            // convert to mgrs using the utm path, otherwise convert to mgrs using the ups path
            if ((latitude >= MIN_MGRS_NON_POLAR_LAT) && (latitude < MAX_MGRS_NON_POLAR_LAT))
            {
                mgrsorUSNGCoordinates = fromUTM(utm.convertFromGeodetic(geodeticCoordinates), longitude, latitude, precision);
            }
            else
            {
                mgrsorUSNGCoordinates = fromUPS(ups.convertFromGeodetic(geodeticCoordinates), precision);
            }

            return mgrsorUSNGCoordinates;
        }

        public GeodeticCoordinates convertToGeodetic(MGRSorUSNGCoordinates mgrsorUSNGCoordinates)
        {
            /*
            * The function convertToGeodetic converts an MGRS coordinate string
            * to Geodetic (latitude and longitude) coordinates
            * according to the current ellipsoid parameters.  If any errors occur,
            * an exception is thrown with a description of the error.
            *
            *    MGRS       : MGRS coordinate string           (input)
            *    latitude   : Latitude in radians              (output)
            *    longitude  : Longitude in radians             (output)
            *
            */

            long zone = new long();
            long[] letters = new long[MGRS_LETTERS];
            double mgrs_easting = new double();
            double mgrs_northing = new double();
            long precision = new long();

            GeodeticCoordinates geodeticCoordinates;

            breakMGRSString(mgrsorUSNGCoordinates.MGRSString, ref zone, ref letters, ref mgrs_easting, ref mgrs_northing, ref precision);

            if (zone > 0)
            {
                geodeticCoordinates = utm.convertToGeodetic(toUTM(zone, letters, mgrs_easting, mgrs_northing, precision));
            }
            else
            {
                geodeticCoordinates = ups.convertToGeodetic(toUPS(letters, mgrs_easting, mgrs_northing));
            }

            return geodeticCoordinates;
        }

        public MGRSorUSNGCoordinates convertFromUTM(UTMCoordinates utmCoordinates, long precision)
        {
            /*
             * The function convertFromUTM converts UTM (zone, easting, and
             * northing) coordinates to an MGRS coordinate string, according to the
             * current ellipsoid parameters.  If any errors occur, an exception is
             * thrown with a description of the error.
             *
             *    zone       : UTM zone                         (input)
             *    hemisphere : North or South hemisphere        (input)
             *    easting    : Easting (X) in meters            (input)
             *    northing   : Northing (Y) in meters           (input)
             *    precision  : Precision level of MGRS string   (input)
             *    MGRSString : MGRS coordinate string           (output)
             */

            string errorStatus = "";

            long zone = utmCoordinates.zone;
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
            if ((precision < 0) || (precision > MAX_PRECISION))
            {
                errorStatus += ErrorMessages.precision;
            }

            if (errorStatus.Length > 0)
            {
                // throw CoordinateConversionException(errorStatus);
            }

            GeodeticCoordinates geodeticCoordinates = utm.convertToGeodetic(utmCoordinates);

            //If the latitude is within the valid mgrs non polar range [-80, 84),
            //convert to mgrs using the utm path, otherwise convert to mgrs using the ups path
            MGRSorUSNGCoordinates mgrsorUSNGCoordinates;
            double latitude = geodeticCoordinates.latitude;

            if ((latitude >= (MIN_MGRS_NON_POLAR_LAT - EPSILON)) && (latitude < (MAX_MGRS_NON_POLAR_LAT + EPSILON)))
            {
                mgrsorUSNGCoordinates = fromUTM(utmCoordinates, geodeticCoordinates.longitude, latitude, precision);
            }
            else
            {
                mgrsorUSNGCoordinates = fromUPS(ups.convertFromGeodetic(geodeticCoordinates), precision);
            }

            return mgrsorUSNGCoordinates;
        }

        public MGRSorUSNGCoordinates convertFromUPS(ref UPSCoordinates upsCoordinates, long precision)
        {
            /*
             * The function convertFromUPS converts UPS (hemisphere, easting,
             * and northing) coordinates to an MGRS coordinate string according to
             * the current ellipsoid parameters.  If any errors occur, an
             * exception is thrown with a description of the error.
             *
             *    hemisphere    : Hemisphere either 'N' or 'S'     (input)
             *    easting       : Easting/X in meters              (input)
             *    northing      : Northing/Y in meters             (input)
             *    precision     : Precision level of MGRS string   (input)
             *    MGRSString    : MGRS coordinate string           (output)
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
            if ((precision < 0) || (precision > MAX_PRECISION))
            {
                errorStatus += ErrorMessages.precision;
            }

            if (errorStatus.Length > 0)
            {
                throw new ArgumentException(errorStatus);
            }

            GeodeticCoordinates geodeticCoordinates = ups.convertToGeodetic(upsCoordinates);

            //If the latitude is within the valid mgrs polar range [-90, -80) or [84, 90],
            //convert to mgrs using the ups path, otherwise convert to mgrs using the utm path
            MGRSorUSNGCoordinates mgrsorUSNGCoordinates = new MGRSorUSNGCoordinates();
            double latitude = geodeticCoordinates.latitude;

            if ((latitude < (MIN_MGRS_NON_POLAR_LAT + EPSILON)) || (latitude >= (MAX_MGRS_NON_POLAR_LAT - EPSILON)))
            {
                mgrsorUSNGCoordinates = fromUPS(upsCoordinates, precision);
            }
            else
            {
                mgrsorUSNGCoordinates = fromUTM(utm.convertFromGeodetic(geodeticCoordinates), geodeticCoordinates.longitude, latitude, precision);
            }

            return mgrsorUSNGCoordinates;
        }

        public UPSCoordinates convertToUPS(MGRSorUSNGCoordinates mgrsorUSNGCoordinates)
        {
            /*
             * The function convertToUPS converts an MGRS coordinate string
             * to UPS (hemisphere, easting, and northing) coordinates, according
             * to the current ellipsoid parameters. If any errors occur, an
             * exception is thrown with a description of the error.
             *
             *    MGRSString    : MGRS coordinate string           (input)
             *    hemisphere    : Hemisphere either 'N' or 'S'     (output)
             *    easting       : Easting/X in meters              (output)
             *    northing      : Northing/Y in meters             (output)
             */

            long zone = 0;
            long[] letters = new long[3];
            long precision = 0;
            double mgrs_easting = 0.0;
            double mgrs_northing = 0.0;
            UPSCoordinates upsCoordinates = new UPSCoordinates();

            breakMGRSString(mgrsorUSNGCoordinates.MGRSString, ref zone, ref letters, ref mgrs_easting, ref mgrs_northing, ref precision);
            
            if (zone == 0)
            {
                upsCoordinates = toUPS(letters, mgrs_easting, mgrs_northing);
            }
            else
            {
                upsCoordinates = ups.convertFromGeodetic(utm.convertToGeodetic(toUTM(zone, letters, mgrs_easting, mgrs_northing, precision)));
            }

            return upsCoordinates;
        }

        public MGRSorUSNGCoordinates fromUTM(UTMCoordinates utmCoordinates, double longitude, double latitude, long precision)
        {
            /*
             * The function fromUTM calculates an MGRS coordinate string
             * based on the zone, latitude, easting and northing.
             *
             *    zone       : Zone number             (input)
             *    hemisphere : Hemisphere              (input)
             *    longitude  : Longitude in radians    (input)
             *    latitude   : Latitude in radians     (input)
             *    easting    : Easting                 (input)
             *    northing   : Northing                (input)
             *    precision  : Precision               (input)
             *    MGRSString : MGRS coordinate string  (output)
             */

            double pattern_offset = 0.0;      /* Pattern offset for 3rd letter               */
            double grid_northing = 0.0;       /* Northing used to derive 3rd letter of MGRS  */
            long ltr2_low_value = 0;        /* 2nd letter range - low number               */
            long ltr2_high_value = 0;       /* 2nd letter range - high number              */
            int[] letters = new int[3];  /* Number location of 3 letters in alphabet    */
            int zoneOverride = 0;
            int natural_zone = 0;

            long zone = utmCoordinates.zone;
            char hemisphere = utmCoordinates.hemisphere;
            double easting = utmCoordinates.easting;
            double northing = utmCoordinates.northing;

            getLatitudeLetter(latitude, ref letters[0]);

            easting = Math.Round(easting);

            //Check if the point is within it's natural zone
            //If it is not, put it there
            if (longitude < Math.PI)
            {
                natural_zone = (int)(31 + ((longitude) / _6));
            }
            else
            {
                natural_zone = (int)(((longitude) / _6) - 29);
            }

            if (natural_zone > 60)
            {
                natural_zone = 1;
            }
            if (zone != natural_zone)
            { // reconvert to override zone
                UTM utmOverride = new UTM(semiMajorAxis, flattening, natural_zone);
                UTMCoordinates utmCoordinatesOverride = utmOverride.convertFromGeodetic(new GeodeticCoordinates(CoordinateType.Enum.geodetic, longitude, latitude));

                zone = utmCoordinatesOverride.zone;
                hemisphere = utmCoordinatesOverride.hemisphere;
                easting = utmCoordinatesOverride.easting;
                northing = utmCoordinatesOverride.northing;
            }

            easting = Math.Round(easting);

            /* UTM special cases */
            if (letters[0] == LETTER_V) // V latitude band
            {
                if ((zone == 31) && (easting >= _500000))
                {
                    zoneOverride = 32;  // extension of zone 32V
                }
            }
            else if (letters[0] == LETTER_X)
            {
                if ((zone == 32) && (easting < _500000)) // extension of zone 31X
                {
                    zoneOverride = 31;
                }
                else if (((zone == 32) && (easting >= _500000)) || // western extension of zone 33X
                         ((zone == 34) && (easting < _500000))) // eastern extension of zone 33X
                {
                    zoneOverride = 33;
                }
                else if (((zone == 34) && (easting >= _500000)) || // western extension of zone 35X
                         ((zone == 36) && (easting < _500000))) // eastern extension of zone 35X
                {
                    zoneOverride = 35;
                }
                else if ((zone == 36) && (easting >= _500000)) // western extension of zone 37X
                {
                    zoneOverride = 37;
                }
            }

            if (zoneOverride != 0)
            { // reconvert to override zone
                UTM utmOverride = new UTM(semiMajorAxis, flattening, zoneOverride);
                UTMCoordinates utmCoordinatesOverride = utmOverride.convertFromGeodetic(new GeodeticCoordinates(CoordinateType.Enum.geodetic, longitude, latitude));

                zone = utmCoordinatesOverride.zone;
                hemisphere = utmCoordinatesOverride.hemisphere;
                easting = utmCoordinatesOverride.easting;
                northing = utmCoordinatesOverride.northing;
            }

            easting = Math.Round(easting);
            northing = Math.Round(northing);
            double divisor = Math.Pow(10.0, (5.0 - precision));
            easting = (long)(easting / divisor) * divisor;
            northing = (long)(northing / divisor) * divisor;

            if (latitude <= 0.0 && northing == 1.0e7)
            {
                latitude = 0.0;
                northing = 0.0;
            }

            getGridValues(zone, ref ltr2_low_value, ref ltr2_high_value, ref pattern_offset);

            grid_northing = northing;

            while (grid_northing >= TWOMIL)
            {
                grid_northing = grid_northing - TWOMIL;
            }
            grid_northing = grid_northing + pattern_offset;
            if (grid_northing >= TWOMIL)
            {
                grid_northing = grid_northing - TWOMIL;
            }

            letters[2] = (int)(grid_northing / ONEHT);
            if (letters[2] > LETTER_H)
            {
                letters[2] = letters[2] + 1;
            }

            if (letters[2] > LETTER_N)
            {
                letters[2] = letters[2] + 1;
            }

            letters[1] = (int)(ltr2_low_value + ((long)(easting / ONEHT) - 1));
            if ((ltr2_low_value == LETTER_J) && (letters[1] > LETTER_N))
            {
                letters[1] = letters[1] + 1;
            }

            return new MGRSorUSNGCoordinates(CoordinateType.Enum.militaryGridReferenceSystem, makeMGRSString(zone, letters, easting, northing, precision));
        }

        public UTMCoordinates toUTM(long zone, long[] letters, double easting, double northing, long precision)
        {
            /*
             * The function toUTM converts an MGRS coordinate string
             * to UTM projection (zone, hemisphere, easting and northing) coordinates
             * according to the current ellipsoid parameters.  If any errors occur,
             * an exception is thrown with a description of the error.
             *
             *    MGRSString : MGRS coordinate string           (input)
             *    zone       : UTM zone                         (output)
             *    hemisphere : North or South hemisphere        (output)
             *    easting    : Easting (X) in meters            (output)
             *    northing   : Northing (Y) in meters           (output)
             */

            char hemisphere = '\0';
            double min_northing = 0.0;
            double northing_offset = 0.0;
            long ltr2_low_value = 0;
            long ltr2_high_value = 0;
            double pattern_offset = 0.0;
            double upper_lat_limit = 0.0;     /* North latitude limits based on 1st letter  */
            double lower_lat_limit = 0.0;     /* South latitude limits based on 1st letter  */
            double grid_easting = 0.0;        /* Easting for 100,000 meter grid square      */
            double grid_northing = 0.0;       /* Northing for 100,000 meter grid square     */
            
            double latitude = 0.0;
            double divisor = 1.0;
            UTMCoordinates utmCoordinates = new UTMCoordinates();

            if ((letters[0] == LETTER_X) && ((zone == 32) || (zone == 34) || (zone == 36)))
            {
                throw new ArgumentException(ErrorMessages.mgrsstaticString);
            }
            else if ((letters[0] == LETTER_V) && (zone == 31) && (letters[1] > LETTER_D))
            {
                throw new ArgumentException(ErrorMessages.mgrsstaticString);
            }
            else
            {
                if (letters[0] < LETTER_N)
                {
                    hemisphere = 'S';
                }
                else
                {
                    hemisphere = 'N';
                }

                getGridValues(zone, ref ltr2_low_value, ref ltr2_high_value, ref pattern_offset);

                /* Check that the second letter of the MGRS string is within
                 * the range of valid second letter values
                 * Also check that the third letter is valid */
                if ((letters[1] < ltr2_low_value) || (letters[1] > ltr2_high_value) || (letters[2] > LETTER_V))
                {
                    throw new ArgumentException(ErrorMessages.mgrsstaticString);
                }

                grid_easting = (double)((letters[1]) - ltr2_low_value + 1) * ONEHT;
                if ((ltr2_low_value == LETTER_J) && (letters[1] > LETTER_O))
                {
                    grid_easting = grid_easting - ONEHT;
                }

                double row_letter_northing = (double)(letters[2]) * ONEHT;
                if (letters[2] > LETTER_O)
                {
                    row_letter_northing = row_letter_northing - ONEHT;
                }

                if (letters[2] > LETTER_I)
                {
                    row_letter_northing = row_letter_northing - ONEHT;
                }

                if (row_letter_northing >= TWOMIL)
                {
                    row_letter_northing = row_letter_northing - TWOMIL;
                }

                getLatitudeBandMinNorthing(letters[0], ref min_northing, ref northing_offset);

                grid_northing = row_letter_northing - pattern_offset;
                if (grid_northing < 0)
                {
                    grid_northing += TWOMIL;
                }

                grid_northing += northing_offset;

                if (grid_northing < min_northing)
                    grid_northing += TWOMIL;

                easting += grid_easting;
                northing += grid_northing;

                utmCoordinates = new UTMCoordinates(CoordinateType.Enum.universalTransverseMercator, (int)zone, hemisphere, easting, northing);

                /* check that point is within Zone Letter bounds */
                GeodeticCoordinates geodeticCoordinates = utm.convertToGeodetic(utmCoordinates);

                divisor = Math.Pow(10.0, (double)precision);
                getLatitudeRange(letters[0], ref upper_lat_limit, ref lower_lat_limit);

                latitude = geodeticCoordinates.latitude;

                if (!(((lower_lat_limit - PI_OVER_180 / divisor) <= latitude) && (latitude <= (upper_lat_limit + PI_OVER_180 / divisor))))
                {
                    utmCoordinates.warningMessage = WarningMessages.latitude;
                }
            }

            return utmCoordinates;
        }

        public MGRSorUSNGCoordinates fromUPS(UPSCoordinates upsCoordinates, long precision)
        {
            /*
             * The function fromUPS converts UPS (hemisphere, easting,
             * and northing) coordinates to an MGRS coordinate string according to
             * the current ellipsoid parameters.
             *
             *    hemisphere    : Hemisphere either 'N' or 'S'     (input)
             *    easting       : Easting/X in meters              (input)
             *    northing      : Northing/Y in meters             (input)
             *    precision     : Precision level of MGRS string   (input)
             *    MGRSString    : MGRS coordinate string           (output)
             */

            double false_easting;       /* False easting for 2nd letter                 */
            double false_northing;      /* False northing for 3rd letter                */
            double grid_easting;        /* Easting used to derive 2nd letter of MGRS    */
            double grid_northing;       /* Northing used to derive 3rd letter of MGRS   */
            long ltr2_low_value;        /* 2nd letter range - low number                */
            int[] letters = new int[3];  /* Number location of 3 letters in alphabet     */
            double divisor = 0.0;
            int index = 0;

            char hemisphere = upsCoordinates.getHemisphere();
            double easting = upsCoordinates.getEasting();
            double northing = upsCoordinates.getNorthing();

            easting = Math.Round(easting);
            northing = Math.Round(northing);
            divisor = Math.Pow(10.0, (5.0 - precision));
            easting = (long)(easting / divisor) * divisor;
            northing = (long)(northing / divisor) * divisor;

            if (hemisphere == 'N')
            {
                if (easting >= TWOMIL)
                {
                    letters[0] = LETTER_Z;
                }
                else
                {
                    letters[0] = LETTER_Y;
                }

                index = letters[0] - 22;
                ltr2_low_value = UPS_Constant_Table[index].ltr2_low_value;
                false_easting = UPS_Constant_Table[index].false_easting;
                false_northing = UPS_Constant_Table[index].false_northing;
            }
            else
            {
                if (easting >= TWOMIL)
                {
                    letters[0] = LETTER_B;
                }
                else
                {
                    letters[0] = LETTER_A;
                }

                ltr2_low_value = UPS_Constant_Table[letters[0]].ltr2_low_value;
                false_easting = UPS_Constant_Table[letters[0]].false_easting;
                false_northing = UPS_Constant_Table[letters[0]].false_northing;
            }

            grid_northing = northing;
            grid_northing = grid_northing - false_northing;
            letters[2] = (int)(grid_northing / ONEHT);

            if (letters[2] > LETTER_H)
            {
                letters[2] = letters[2] + 1;
            }

            if (letters[2] > LETTER_N)
            {
                letters[2] = letters[2] + 1;
            }

            grid_easting = easting;
            grid_easting = grid_easting - false_easting;
            letters[1] = (int)(ltr2_low_value + ((long)(grid_easting / ONEHT)));

            if (easting < TWOMIL)
            {
                if (letters[1] > LETTER_L)
                {
                    letters[1] = letters[1] + 3;
                }
                if (letters[1] > LETTER_U)
                {
                    letters[1] = letters[1] + 2;
                }
            }
            else
            {
                if (letters[1] > LETTER_C)
                {
                    letters[1] = letters[1] + 2;
                }
                if (letters[1] > LETTER_H)
                {
                    letters[1] = letters[1] + 1;
                }
                if (letters[1] > LETTER_L)
                {
                    letters[1] = letters[1] + 3;
                }
            }

            return new MGRSorUSNGCoordinates(CoordinateType.Enum.militaryGridReferenceSystem, makeMGRSString(0, letters, easting, northing, precision));
        }

        public UPSCoordinates toUPS(long[] letters, double easting, double northing)
        {
            /*
             * The function toUPS converts an MGRS coordinate string
             * to UPS (hemisphere, easting, and northing) coordinates, according
             * to the current ellipsoid parameters. If any errors occur, an
             * exception is thrown with a description of the error.
             *
             *    MGRSString    : MGRS coordinate string           (input)
             *    hemisphere    : Hemisphere either 'N' or 'S'     (output)
             *    easting       : Easting/X in meters              (output)
             *    northing      : Northing/Y in meters             (output)
             */

            long ltr2_high_value = 0;         /* 2nd letter range - high number             */
            long ltr3_high_value = 0;         /* 3rd letter range - high number (UPS)       */
            long ltr2_low_value = 0;          /* 2nd letter range - low number              */
            double false_easting = 0.0;       /* False easting for 2nd letter               */
            double false_northing = 0.0;      /* False northing for 3rd letter              */
            double grid_easting = 0.0;        /* easting for 100,000 meter grid square      */
            double grid_northing = 0.0;       /* northing for 100,000 meter grid square     */
            char hemisphere = 'N';
            long index = 0;

            if ((letters[0] == LETTER_Y) || (letters[0] == LETTER_Z))
            {
                hemisphere = 'N';
                index = letters[0] - 22;
                ltr2_low_value = UPS_Constant_Table[index].ltr2_low_value;
                ltr2_high_value = UPS_Constant_Table[index].ltr2_high_value;
                ltr3_high_value = UPS_Constant_Table[index].ltr3_high_value;
                false_easting = UPS_Constant_Table[index].false_easting;
                false_northing = UPS_Constant_Table[index].false_northing;
            }
            else if ((letters[0] == LETTER_A) || (letters[0] == LETTER_B))
            {
                hemisphere = 'S';
                ltr2_low_value = UPS_Constant_Table[letters[0]].ltr2_low_value;
                ltr2_high_value = UPS_Constant_Table[letters[0]].ltr2_high_value;
                ltr3_high_value = UPS_Constant_Table[letters[0]].ltr3_high_value;
                false_easting = UPS_Constant_Table[letters[0]].false_easting;
                false_northing = UPS_Constant_Table[letters[0]].false_northing;
            }
            else
            {
                throw new ArgumentException(ErrorMessages.mgrsstaticString);
            }

            /* Check that the second letter of the MGRS string is within
             * the range of valid second letter values
             * Also check that the third letter is valid */
            if ((letters[1] < ltr2_low_value) || (letters[1] > ltr2_high_value) || ((letters[1] == LETTER_D) || (letters[1] == LETTER_E) || (letters[1] == LETTER_M) || (letters[1] == LETTER_N) || (letters[1] == LETTER_V) || (letters[1] == LETTER_W)) || (letters[2] > ltr3_high_value))
            {
                throw new ArgumentException(ErrorMessages.mgrsstaticString);
            }

            grid_northing = (double)letters[2] * ONEHT + false_northing;
            if (letters[2] > LETTER_I)
            {
                grid_northing = grid_northing - ONEHT;
            }

            if (letters[2] > LETTER_O)
            {
                grid_northing = grid_northing - ONEHT;
            }

            grid_easting = (double)((letters[1]) - ltr2_low_value) * ONEHT + false_easting;
            if (ltr2_low_value != LETTER_A)
            {
                if (letters[1] > LETTER_L)
                {
                    grid_easting = grid_easting - 300000.0;
                }

                if (letters[1] > LETTER_U)
                {
                    grid_easting = grid_easting - 200000.0;
                }
            }
            else
            {
                if (letters[1] > LETTER_C)
                {
                    grid_easting = grid_easting - 200000.0;
                }

                if (letters[1] > LETTER_I)
                {
                    grid_easting = grid_easting - ONEHT;
                }

                if (letters[1] > LETTER_L)
                {
                    grid_easting = grid_easting - 300000.0;
                }
            }

            easting = grid_easting + easting;
            northing = grid_northing + northing;

            return new UPSCoordinates(CoordinateType.Enum.universalPolarStereographic, hemisphere, easting, northing);
        }

        private void getGridValues(long zone, ref long ltr2_low_value, ref long ltr2_high_value, ref double pattern_offset)
        {
            /*
             * The function getGridValues sets the letter range used for
             * the 2nd letter in the MGRS coordinate string, based on the set
             * number of the utm zone. It also sets the pattern offset using a
             * value of A for the second letter of the grid square, based on
             * the grid pattern and set number of the utm zone.
             *
             *    zone            : Zone number             (input)
             *    ltr2_low_value  : 2nd letter low number   (output)
             *    ltr2_high_value : 2nd letter high number  (output)
             *    pattern_offset  : Pattern offset          (output)
             */

            long set_number;    /* Set number (1-6) based on UTM zone number */

            set_number = zone % 6;

            if ((set_number == 1) || (set_number == 4))
            {
                ltr2_low_value = LETTER_A;
                ltr2_high_value = LETTER_H;
            }
            else if ((set_number == 2) || (set_number == 5))
            {
                ltr2_low_value = LETTER_J;
                ltr2_high_value = LETTER_R;
            }
            else if ((set_number == 3) || (set_number == 0))
            {
                ltr2_low_value = LETTER_S;
                ltr2_high_value = LETTER_Z;
            }

            /* False northing at A for second letter of grid square */
            if (this.ellipsoidCode == CLARKE_1866 || this.ellipsoidCode == CLARKE_1880 || this.ellipsoidCode == BESSEL_1841 || this.ellipsoidCode == BESSEL_1841_NAMIBIA)
            {
                if ((set_number % 2) == 0)
                {
                    pattern_offset = 1500000.0;
                }
                else
                {
                    pattern_offset = 1000000.00;
                }
            }
            else
            {
                if ((set_number % 2) == 0)
                {
                    pattern_offset = 500000.0;
                }
                else
                {
                    pattern_offset = 0.0;
                }
            }
        }

        private void getLatitudeBandMinNorthing(long letter, ref double min_northing, ref double northing_offset)
        {
            /*
             * The function getLatitudeBandMinNorthing receives a latitude band letter
             * and uses the Latitude_Band_Table to determine the minimum northing and northing offset
             * for that latitude band letter.
             *
             *   letter          : Latitude band letter             (input)
             *   min_northing    : Minimum northing for that letter (output)
             *   northing_offset : Latitude band northing offset    (output)
             */

            if ((letter >= LETTER_C) && (letter <= LETTER_H))
            {
                min_northing = this.Latitude_Band_Table[letter - 2].min_northing;
                northing_offset = this.Latitude_Band_Table[letter - 2].northing_offset;
            }
            else if ((letter >= LETTER_J) && (letter <= LETTER_N))
            {
                min_northing = this.Latitude_Band_Table[letter - 3].min_northing;
                northing_offset = this.Latitude_Band_Table[letter - 3].northing_offset;
            }
            else if ((letter >= LETTER_P) && (letter <= LETTER_X))
            {
                min_northing = this.Latitude_Band_Table[letter - 4].min_northing;
                northing_offset = this.Latitude_Band_Table[letter - 4].northing_offset;
            }
            else
            {
                throw new ArgumentException(ErrorMessages.mgrsstaticString);
            }
        }

        private void getLatitudeRange(long letter, ref double north, ref double south)
        {
            /*
             * The function getLatitudeRange receives a latitude band letter
             * and uses the Latitude_Band_Table to determine the latitude band
             * boundaries for that latitude band letter.
             *
             *   letter   : Latitude band letter                        (input)
             *   north    : Northern latitude boundary for that letter  (output)
             *   north    : Southern latitude boundary for that letter  (output)
             */

            if ((letter >= LETTER_C) && (letter <= LETTER_H))
            {
                north = this.Latitude_Band_Table[letter - 2].north * PI_OVER_180;
                south = this.Latitude_Band_Table[letter - 2].south * PI_OVER_180;
            }
            else if ((letter >= LETTER_J) && (letter <= LETTER_N))
            {
                north = this.Latitude_Band_Table[letter - 3].north * PI_OVER_180;
                south = this.Latitude_Band_Table[letter - 3].south * PI_OVER_180;
            }
            else if ((letter >= LETTER_P) && (letter <= LETTER_X))
            {
                north = this.Latitude_Band_Table[letter - 4].north * PI_OVER_180;
                south = this.Latitude_Band_Table[letter - 4].south * PI_OVER_180;
            }
            else
            {
                throw new ArgumentException(ErrorMessages.mgrsstaticString);
            }
        }

        private void getLatitudeLetter(double latitude, ref int letter)
        {
            /*
             * The function getLatitudeLetter receives a latitude value
             * and uses the Latitude_Band_Table to determine the latitude band
             * letter for that latitude.
             *
             *   latitude   : Latitude              (input)
             *   letter     : Latitude band letter  (output)
             */

            long band = 0;

            if (latitude >= _72 && latitude < _84_5)
            {
                letter = LETTER_X;
            }
            else if (latitude > -_80_5 && latitude < _72)
            {
                band = (long)(((latitude + _80) / _8) + 1.0e-12);
                if (band < 0)
                {
                    band = 0;
                }
                letter = this.Latitude_Band_Table[band].letter;
            }
            else
            {
                throw new ArgumentException(ErrorMessages.latitude);
            }
        }
    }
}

// CLASSIFICATION: UNCLASSIFIED