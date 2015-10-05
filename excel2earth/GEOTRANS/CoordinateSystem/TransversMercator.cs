// CLASSIFICATION: UNCLASSIFIED

using System;

namespace MSP.CCS
{
    /// <summary>
    /// Transverse Mercator
    /// 
    /// Originally from GEOTRANS.
    /// </summary>
    public class TransverseMercator : CoordinateSystem
    {
        const double PI_OVER_2 = (Math.PI / 2.0);         // PI over 2
        const double MAX_LAT = (Math.PI * 89.99) / 180.0; // 89.99 degrees in radians
        const double MAX_DELTA_LONG = PI_OVER_2;          // 90 degrees in radians
        const double MIN_SCALE_FACTOR = 0.3;
        const double MAX_SCALE_FACTOR = 3.0;

        /* Ellipsoid Parameters */
        private double TranMerc_es;             /* Eccentricity squared */
        private double TranMerc_ebs;            /* Second Eccentricity squared */

        /* Transverse_Mercator projection Parameters */
        private double TranMerc_Origin_Lat;           /* Latitude of origin in radians */
        private double TranMerc_Origin_Long;          /* Longitude of origin in radians */
        private double TranMerc_False_Northing;       /* False northing in meters */
        private double TranMerc_False_Easting;        /* False easting in meters */
        private double TranMerc_Scale_Factor;         /* Scale factor  */

        /* Isometric to geodetic latitude parameters */
        private double TranMerc_ap;
        private double TranMerc_bp;
        private double TranMerc_cp;
        private double TranMerc_dp;
        private double TranMerc_ep;

        /* Maximum variance for easting and northing values */
        private double TranMerc_Delta_Easting;
        private double TranMerc_Delta_Northing;

        //private CoordinateSystem coordsys = new CoordinateSystem();
        //private double semiMajorAxis;
        //private double flattening;

        /// <summary>
        /// The constructor receives the ellipsoid
        /// parameters and Tranverse Mercator projection parameters as inputs, and
        /// sets the corresponding state variables. If any errors occur, an exception 
        /// is thrown with a description of the error.
        /// </summary>
        /// <param name="ellipsoidSemiMajorAxis">Semi-major axis of ellipsoid, in meters</param>
        /// <param name="ellipsoidFlattening">Flattening of ellipsoid</param>
        /// <param name="centralMeridian">Longitude in radians at the center of the projection</param>
        /// <param name="latitudeOfTrueScale">Latitude in radians at the origin of the projection</param>
        /// <param name="falseEasting">Easting/X at the center of the projection</param>
        /// <param name="falseNorthing">Northing/Y at the center of the projection</param>
        /// <param name="scaleFactor">Projection scale factor</param>
        public TransverseMercator(double ellipsoidSemiMajorAxis, double ellipsoidFlattening, double centralMeridian, double latitudeOfTrueScale, double falseEasting, double falseNorthing, double scaleFactor)
        {
            this.TranMerc_es = 0.0066943799901413800;
            this.TranMerc_ebs = 0.0067394967565869;
            this.TranMerc_Origin_Long = 0.0;
            this.TranMerc_Origin_Lat = 0.0;
            this.TranMerc_False_Easting = 0.0;
            this.TranMerc_False_Northing = 0.0;
            this.TranMerc_Scale_Factor = 1.0;
            this.TranMerc_ap = 6367449.1458008;
            this.TranMerc_bp = 16038.508696861;
            this.TranMerc_cp = 16.832613334334;
            this.TranMerc_dp = 0.021984404273757;
            this.TranMerc_ep = 3.1148371319283e-005;
            this.TranMerc_Delta_Easting = 40000000.0;
            this.TranMerc_Delta_Northing = 40000000.0;

            double tn1;         /* True Meridianal distance constant  */
            double tn2;
            double tn3;
            double tn4;
            double tn5;
            double TranMerc_b; /* Semi-minor axis of ellipsoid, in meters */
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
            if ((latitudeOfTrueScale < -PI_OVER_2) || (latitudeOfTrueScale > PI_OVER_2))
            { /* latitudeOfTrueScale out of range */
                errorStatus += ErrorMessages.originLatitude;
            }
            if ((centralMeridian < -Math.PI) || (centralMeridian > (2 * Math.PI)))
            { /* centralMeridian out of range */
                errorStatus += ErrorMessages.centralMeridian;
            }
            if ((scaleFactor < MIN_SCALE_FACTOR) || (scaleFactor > MAX_SCALE_FACTOR))
            {
                errorStatus += ErrorMessages.scaleFactor;
            }

            if (errorStatus.Length > 0)
            {
                throw new ArgumentException(errorStatus);
            }

            //double semiMajorAxis = ellipsoidSemiMajorAxis;
            //double flattening = ellipsoidFlattening;

            //this.semiMajorAxis = coordsys.semiMajorAxis;
            //this.flattening = coordsys.flattening;

            TranMerc_Origin_Lat = latitudeOfTrueScale;
            if (centralMeridian > Math.PI)
            {
                centralMeridian -= (2 * Math.PI);
            }
            this.TranMerc_Origin_Long = centralMeridian;
            this.TranMerc_False_Northing = falseNorthing;
            this.TranMerc_False_Easting = falseEasting;
            this.TranMerc_Scale_Factor = scaleFactor;

            /* Eccentricity Squared */
            this.TranMerc_es = 2 * this.flattening - this.flattening * this.flattening;
            /* Second Eccentricity Squared */
            this.TranMerc_ebs = (1 / (1 - this.TranMerc_es)) - 1;

            TranMerc_b = this.semiMajorAxis * (1 - this.flattening);
            /*True meridianal constants  */
            tn1 = (this.semiMajorAxis - TranMerc_b) / (this.semiMajorAxis + TranMerc_b);
            tn2 = tn1 * tn1;
            tn3 = tn2 * tn1;
            tn4 = tn3 * tn1;
            tn5 = tn4 * tn1;

            TranMerc_ap = this.semiMajorAxis * (1.0 - tn1 + 5.0 * (tn2 - tn3) / 4.0 + 81.0 * (tn4 - tn5) / 64.0);
            TranMerc_bp = 3.0 * this.semiMajorAxis * (tn1 - tn2 + 7.0 * (tn3 - tn4) / 8.0 + 55.0 * tn5 / 64.0) / 2.0;
            TranMerc_cp = 15.0 * this.semiMajorAxis * (tn2 - tn3 + 3.0 * (tn4 - tn5) / 4.0) / 16.0;
            TranMerc_dp = 35.0 * this.semiMajorAxis * (tn3 - tn4 + 11.0 * tn5 / 16.0) / 48.0;
            TranMerc_ep = 315.0 * this.semiMajorAxis * (tn4 - tn5) / 512.0;
            
            this.TranMerc_Delta_Northing = convertFromGeodetic(new GeodeticCoordinates(CoordinateType.Enum.geodetic, MAX_DELTA_LONG + TranMerc_Origin_Long, MAX_LAT)).getNorthing() + 1;
            this.TranMerc_Delta_Easting = convertFromGeodetic(new GeodeticCoordinates(CoordinateType.Enum.geodetic, MAX_DELTA_LONG + TranMerc_Origin_Long, 0)).getEasting() + 1;
        }

        /// <summary>
        /// The function getParameters returns the current
        /// ellipsoid and Transverse Mercator projection parameters.
        /// </summary>
        /// <returns></returns>
        public MapProjection5Parameters getParameters()
        {
            /*
             *    ellipsoidSemiMajorAxis     : Semi-major axis of ellipsoid, in meters    (output)
             *    ellipsoidFlattening        : Flattening of ellipsoid						        (output)
             *    centralMeridian            : Longitude in radians at the center of the  (output)
             *                                 projection
             *    latitudeOfTrueScale        : Latitude in radians at the origin of the   (output)
             *                                 projection
             *    falseEasting               : Easting/X at the center of the projection  (output)
             *    falseNorthing              : Northing/Y at the center of the projection (output)
             *    scaleFactor                : Projection scale factor                )    (output) 
             */

            return new MapProjection5Parameters(CoordinateType.Enum.transverseMercator, TranMerc_Origin_Long, TranMerc_Origin_Lat, TranMerc_Scale_Factor, TranMerc_False_Easting, TranMerc_False_Northing);
        }

        /// <summary>
        /// The function convertFromGeodetic converts geodetic
        /// (latitude and longitude) coordinates to Transverse Mercator projection
        /// (easting and northing) coordinates, according to the current ellipsoid
        /// and Transverse Mercator projection coordinates.  If any errors occur, 
        /// an exception is thrown with a description of the error.
        /// </summary>
        /// <param name="geodeticCoordinates"></param>
        /// <returns></returns>
        public MapProjectionCoordinates convertFromGeodetic(GeodeticCoordinates geodeticCoordinates)
        {
            /*
             *    longitude     : Longitude in radians                        (input)
             *    latitude      : Latitude in radians                         (input)
             *    easting       : Easting/X in meters                         (output)
             *    northing      : Northing/Y in meters                        (output)
             */

            double c1;       /* Cosine of latitude                          */
            double c2;
            double c3;
            double c5;
            double c7;
            double dlam;    /* Delta longitude - Difference in Longitude       */
            double eta1;     /* constant - TranMerc_ebs *c *c                   */
            double eta2;
            double eta3;
            double eta4;
            double s;       /* Sine of latitude                        */
            double sn;      /* Radius of curvature in the prime vertical       */
            double tan1;       /* Tangent of latitude                             */
            double tan2;
            double tan3;
            double tan4;
            double tan5;
            double tan6;
            double t1;      /* Term in coordinate conversion formula - GP to Y */
            double t2;      /* Term in coordinate conversion formula - GP to Y */
            double t3;      /* Term in coordinate conversion formula - GP to Y */
            double t4;      /* Term in coordinate conversion formula - GP to Y */
            double t5;      /* Term in coordinate conversion formula - GP to Y */
            double t6;      /* Term in coordinate conversion formula - GP to Y */
            double t7;      /* Term in coordinate conversion formula - GP to Y */
            double t8;      /* Term in coordinate conversion formula - GP to Y */
            double t9;      /* Term in coordinate conversion formula - GP to Y */
            double temp_Origin;
            double temp_Long;
            string errorStatus = "";

            double longitude = geodeticCoordinates.longitude;
            double latitude = geodeticCoordinates.latitude;

            if ((latitude < -MAX_LAT) || (latitude > MAX_LAT))
            {  /* Latitude out of range */
                errorStatus += ErrorMessages.latitude;
            }
            if (longitude > Math.PI)
            {
                longitude -= (2 * Math.PI);
            }
            if ((longitude < (this.TranMerc_Origin_Long - MAX_DELTA_LONG)) || (longitude > (this.TranMerc_Origin_Long + MAX_DELTA_LONG)))
            {
                if (longitude < 0)
                {
                    temp_Long = longitude + 2 * Math.PI;
                }
                else
                {
                    temp_Long = longitude;
                }
                if (this.TranMerc_Origin_Long < 0)
                {
                    temp_Origin = this.TranMerc_Origin_Long + 2 * Math.PI;
                }
                else
                {
                    temp_Origin = this.TranMerc_Origin_Long;
                }
                //if ((temp_Long < (temp_Origin - MAX_DELTA_LONG)) || (temp_Long > (temp_Origin + MAX_DELTA_LONG)))
                //{
                //    errorStatus += ErrorMessages.longitude;
                //}
            }

            if (errorStatus.Length > 0)
            {
                
                //throw new ArgumentException(errorStatus);
            }

            /* 
             *  Delta Longitude
             */
            dlam = longitude - this.TranMerc_Origin_Long;

            if (Math.Abs(dlam) > Math.PI / 20.0)
            { /* Distortion will result if Longitude is more than 9 degrees from the Central Meridian */
                errorStatus += WarningMessages.longitude;
            }

            if (dlam > Math.PI)
            {
                dlam -= (2 * Math.PI);
            }
            if (dlam < -Math.PI)
            {
                dlam += (2 * Math.PI);
            }
            if (Math.Abs(dlam) < 2.0e-10)
            {
                dlam = 0.0;
            }

            s = Math.Sin(latitude);
            c1 = Math.Cos(latitude);
            c2 = c1 * c1;
            c3 = c2 * c1;
            c5 = c3 * c2;
            c7 = c5 * c2;
            tan1 = Math.Tan(latitude);
            tan2 = tan1 * tan1;
            tan3 = tan2 * tan1;
            tan4 = tan3 * tan1;
            tan5 = tan4 * tan1;
            tan6 = tan5 * tan1;
            eta1 = TranMerc_ebs * c2;
            eta2 = eta1 * eta1;
            eta3 = eta2 * eta1;
            eta4 = eta3 * eta1;

            // radius of curvature in prime vertical
            sn = sphsn(latitude);

            // northing
            t1 = (sphtmd(latitude) - sphtmd(this.TranMerc_Origin_Lat)) * this.TranMerc_Scale_Factor; // True Meridinial Distances, Origin
            t2 = sn * s * c1 * this.TranMerc_Scale_Factor / 2.0;
            t3 = sn * s * c3 * this.TranMerc_Scale_Factor * (5.0 - tan2 + 9.0 * eta1 + 4.0 * eta2) / 24.0;
            t4 = sn * s * c5 * this.TranMerc_Scale_Factor * (61.0 - 58.0 * tan2 + tan4 + 270.0 * eta1 - 330.0 * tan2 * eta1 + 445.0 * eta2 + 324.0 * eta3 - 680.0 * tan2 * eta2 + 88.0 * eta4 - 600.0 * tan2 * eta3 - 192.0 * tan2 * eta4) / 720.0;
            t5 = sn * s * c7 * this.TranMerc_Scale_Factor * (1385.0 - 3111.0 * tan2 + 543.0 * tan4 - tan6) / 40320.0;
            double northing = this.TranMerc_False_Northing + t1 + Math.Pow(dlam, 2.0) * t2 + Math.Pow(dlam, 4.0) * t3 + Math.Pow(dlam, 6.0) * t4 + Math.Pow(dlam, 8.0) * t5;

            // Easting
            t6 = sn * c1 * this.TranMerc_Scale_Factor;
            t7 = sn * c3 * this.TranMerc_Scale_Factor * (1.0 - tan2 + eta1) / 6.0;
            t8 = sn * c5 * this.TranMerc_Scale_Factor * (5.0 - 18.0 * tan2 + tan4 + 14.0 * eta1 - 58.0 * tan2 * eta1 + 13.0 * eta2 + 4.0 * eta3 - 64.0 * tan2 * eta2 - 24.0 * tan2 * eta3) / 120.0;
            t9 = sn * c7 * this.TranMerc_Scale_Factor * (61.0 - 479.0 * tan2 + 179.0 * tan4 - tan6) / 5040.0;
            double easting = this.TranMerc_False_Easting + dlam * t6 + Math.Pow(dlam, 3.0) * t7 + Math.Pow(dlam, 5.0) * t8 + Math.Pow(dlam, 7.0) * t9;

            return new MapProjectionCoordinates(CoordinateType.Enum.transverseMercator, errorStatus, easting, northing);
        }

        /// <summary>
        /// The function convertToGeodetic converts Transverse
        /// Mercator projection (easting and northing) coordinates to geodetic
        /// (latitude and longitude) coordinates, according to the current ellipsoid
        /// and Transverse Mercator projection parameters.  If any errors occur, 
        /// an exception is thrown with a description of the error.
        /// </summary>
        /// <param name="mapProjectionCoordinates"></param>
        /// <returns></returns>
        public GeodeticCoordinates convertToGeodetic(MapProjectionCoordinates mapProjectionCoordinates)
        {   /*
             *    easting       : Easting/X in meters                         (input)
             *    northing      : Northing/Y in meters                        (input)
             *    longitude     : Longitude in radians                        (output)
             *    latitude      : Latitude in radians                         (output)
             */

            double c;       /* Cosine of latitude                          */
            double de;      /* Delta easting - Difference in Easting (easting-Fe)    */
            double dlam;    /* Delta longitude - Difference in Longitude       */
            double eta1;    /* constant - TranMerc_ebs *c *c                   */
            double eta2;
            double eta3;
            double eta4;
            double ftphi;   /* Footpoint latitude                              */
            int i;          /* Loop iterator                   */
            double s;       /* Sine of latitude                        */
            double sn;      /* Radius of curvature in the prime vertical       */
            double sr;      /* Radius of curvature in the meridian             */
            double tan1;    /* Tangent of latitude                             */
            double tan2;
            double tan4;
            double t10;     /* Term in coordinate conversion formula - GP to Y */
            double t11;     /* Term in coordinate conversion formula - GP to Y */
            double t12;     /* Term in coordinate conversion formula - GP to Y */
            double t13;     /* Term in coordinate conversion formula - GP to Y */
            double t14;     /* Term in coordinate conversion formula - GP to Y */
            double t15;     /* Term in coordinate conversion formula - GP to Y */
            double t16;     /* Term in coordinate conversion formula - GP to Y */
            double t17;     /* Term in coordinate conversion formula - GP to Y */
            double tmd;     /* True Meridianal distance                        */
            double tmdo;    /* True Meridianal distance for latitude of origin */
            string errorStatus = "";

            double latitude;
            double longitude;

            double easting = mapProjectionCoordinates.getEasting();
            double northing = mapProjectionCoordinates.getNorthing();

            if ((easting < (TranMerc_False_Easting - TranMerc_Delta_Easting)) || (easting > (TranMerc_False_Easting + TranMerc_Delta_Easting)))
            { /* easting out of range  */
                errorStatus += ErrorMessages.easting;
            }
            if ((northing < (TranMerc_False_Northing - TranMerc_Delta_Northing)) || (northing > (TranMerc_False_Northing + TranMerc_Delta_Northing)))
            { /* northing out of range */
                errorStatus += ErrorMessages.northing;
            }

            if (errorStatus.Length > 0)
            {
                throw new ArgumentException(errorStatus);
            }

            /* True Meridianal Distances for latitude of origin */
            tmdo = sphtmd(TranMerc_Origin_Lat);

            /*  Origin  */
            tmd = tmdo + (northing - TranMerc_False_Northing) / TranMerc_Scale_Factor;

            /* First Estimate */
            sr = sphsr(0.0);
            ftphi = tmd / sr;

            for (i = 0; i < 5; i++)
            {
                t10 = sphtmd(ftphi);
                sr = sphsr(ftphi);
                ftphi += (tmd - t10) / sr;
            }

            /* Radius of Curvature in the meridian */
            sr = sphsr(ftphi);

            /* Radius of Curvature in the meridian */
            sn = sphsn(ftphi);

            /* Sine CoMath.Sine terms */
            s = Math.Sin(ftphi);
            c = Math.Cos(ftphi);

            /* Tangent Value  */
            tan1 = Math.Tan(ftphi);
            tan2 = tan1 * tan1;
            tan4 = tan2 * tan2;
            eta1 = TranMerc_ebs * Math.Pow(c, 2.0);
            eta2 = eta1 * eta1;
            eta3 = eta2 * eta1;
            eta4 = eta3 * eta1;
            de = easting - TranMerc_False_Easting;
            if (Math.Abs(de) < 0.0001)
            {
                de = 0.0;
            }
            
            /* Latitude */
            t10 = tan1 / (2.0 * sr * sn * Math.Pow(TranMerc_Scale_Factor, 2.0));
            t11 = tan1 * (5.0 + 3.0 * tan2 + eta1 - 4.0 * Math.Pow(eta1, 2.0) - 9.0 * tan2 * eta1) / (24.0 * sr * Math.Pow(sn, 3.0) * Math.Pow(this.TranMerc_Scale_Factor, 4.0));
            t12 = tan1 * (61.0 + 90.0 * tan2 + 46.0 * eta1 + 45.0 * tan4 - 252.0 * tan2 * eta1 - 3.0 * eta2 + 100.0 * eta3 - 66.0 * tan2 * eta2 - 90.0 * tan4 * eta1 + 88.0 * eta4 + 225.0 * tan4 * eta2 + 84.0 * tan2 * eta3 - 192.0 * tan2 * eta4) / (720.0 * sr * Math.Pow(sn, 5.0) * Math.Pow(this.TranMerc_Scale_Factor, 6.0));
            t13 = tan1 * (1385.0 + 3633.0 * tan2 + 4095.0 * tan4 + 1575.0 * Math.Pow(tan1, 6.0)) / (40320.0 * sr * Math.Pow(sn, 7.0) * Math.Pow(TranMerc_Scale_Factor, 8.0));
            
            latitude = ftphi - Math.Pow(de, 2.0) * t10 + Math.Pow(de, 4.0) * t11 - Math.Pow(de, 6.0) * t12 + Math.Pow(de, 8.0) * t13;
            
            t14 = 1.0 / (sn * c * this.TranMerc_Scale_Factor);
            t15 = (1.0 + 2.0 * tan2 + eta1) / (6.0 * Math.Pow(sn, 3.0) * c * Math.Pow(this.TranMerc_Scale_Factor, 3.0));
            t16 = (5.0 + 6.0 * eta1 + 28.0 * tan2 - 3.0 * eta2 + 8.0 * tan2 * eta1 + 24.0 * tan4 - 4.0 * eta3 + 4.0 * tan2 * eta2 + 24.0 * tan2 * eta3) / (120.0 * Math.Pow(sn, 5.0) * c * Math.Pow(this.TranMerc_Scale_Factor, 5.0));
            t17 = (61.0 + 662.0 * tan2 + 1320.0 * tan4 + 720.0 * Math.Pow(tan1, 6.0)) / (5040.0 * Math.Pow(sn, 7.0) * c * Math.Pow(this.TranMerc_Scale_Factor, 7.0));

            /* Difference in Longitude */
            dlam = de * t14 - Math.Pow(de, 3.0) * t15 + Math.Pow(de, 5.0) * t16 - Math.Pow(de, 7.0) * t17;

            /* Longitude */
            longitude = this.TranMerc_Origin_Long + dlam;

            if (Math.Abs(latitude) > PI_OVER_2)
            {
                throw new ArgumentException(ErrorMessages.northing);
            }

            if (longitude > Math.PI)
            {
                longitude -= (2 * Math.PI);
                if (Math.Abs(longitude) > Math.PI)
                {
                    throw new ArgumentException(ErrorMessages.easting);
                }
            }
            else if (longitude < -Math.PI)
            {
                longitude += (2 * Math.PI);
                if (Math.Abs(longitude) > Math.PI)
                {
                    throw new ArgumentException(ErrorMessages.easting);
                }
            }

            if (Math.Abs(dlam) > Math.PI / 20 * Math.Cos(latitude))
            { /* Distortion will result if longitude is more than 9 degrees from the Central Meridian at the equator */
                /* and decreases to 0 degrees at the poles */
                /* As you move towards the poles, distortion will become more significant */
                errorStatus += WarningMessages.longitude;
            }

            return new GeodeticCoordinates(CoordinateType.Enum.geodetic, longitude, latitude);
        }

        private double sphtmd(double latitude)
        {
            return this.TranMerc_ap * latitude - this.TranMerc_bp * Math.Sin(2.0 * latitude) + this.TranMerc_cp * Math.Sin(4.0 * latitude) - this.TranMerc_dp * Math.Sin(6.0 * latitude) + this.TranMerc_ep * Math.Sin(8.0 * latitude);
        }

        private double sphsn(double latitude)
        {
            return this.semiMajorAxis / Math.Sqrt(1.0 - this.TranMerc_es * Math.Pow(Math.Sin(latitude), 2.0));
        }

        private double sphsr(double latitude)
        {
            return this.semiMajorAxis * (1.0 - this.TranMerc_es) / Math.Pow(Math.Sqrt(1.0 - this.TranMerc_es * Math.Pow(Math.Sin(latitude), 2.0)), 3.0);
        }
    }
}

// CLASSIFICATION: UNCLASSIFIED