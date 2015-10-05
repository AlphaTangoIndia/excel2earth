// CLASSIFICATION: UNCLASSIFIED

namespace MSP.CCS
{
    /// <summary>
    /// Military Grid Reference System or United States Navigation Grid Coordinates.
    /// 
    /// Originally from GEOTRANS.
    /// </summary>
    public class MGRSorUSNGCoordinates : CoordinateTuple
    {
        public Precision.Enum precision;
        public string MGRSString;

        public MGRSorUSNGCoordinates()
        {
            this.coordinateType = CoordinateType.Enum.militaryGridReferenceSystem;
            this.precision = Precision.Enum.tenthOfSecond;
            this.MGRSString = "31NEA0000000000";
        }

        public MGRSorUSNGCoordinates(CoordinateType.Enum coordinateType)
        {
            this.coordinateType = coordinateType;
            this.precision = Precision.Enum.tenthOfSecond;
            this.MGRSString = "31NEA0000000000";
        }

        public MGRSorUSNGCoordinates(CoordinateType.Enum coordinateType, string MGRSString)
        {
            this.coordinateType = coordinateType;
            this.precision = Precision.Enum.tenthOfSecond;
            this.MGRSString = MGRSString;
        }

        public MGRSorUSNGCoordinates(CoordinateType.Enum coordinateType, string MGRSString, Precision.Enum precision)
        {
            this.coordinateType = coordinateType;
            this.precision = precision;
            this.MGRSString = MGRSString;
        }

        public MGRSorUSNGCoordinates(CoordinateType.Enum coordinateType, string warningMessage, string MGRSString, Precision.Enum precision)
        {
            this.coordinateType = coordinateType;
            this.precision = precision;
            this.MGRSString = MGRSString;
            this.warningMessage = warningMessage;
        }

        public MGRSorUSNGCoordinates(MGRSorUSNGCoordinates c)
        {
            this.coordinateType = c.coordinateType;
            this.MGRSString = c.MGRSString;
            this.precision = c.precision;
            this.warningMessage = c.warningMessage;
        }

        ~MGRSorUSNGCoordinates()
        {
        }

        //MGRSorUSNGCoordinates& operator=( const MGRSorUSNGCoordinates &c )
        //{
        //  if( this != &c )
        //  {
        //    coordinateType = c.coordinateType;

        //    strncpy( MGRSString, c.MGRSString, 20 );
        //    MGRSString[20] = '\0';

        //    precision = c.precision;

        //    int length = strlen( c.warningMessage );
        //    strncpy( warningMessage, c.warningMessage, length );
        //    warningMessage[ length ] = '\0';
        //  }

        //  return *this;
        //}

        //public void set(string MGRSString)
        //{
        //    this.MGRSString = MGRSString;
        //}

        //public string MGRSString()
        //{
        //    return this.MGRSString;
        //}

        //public Precision.Enum precision()
        //{
        //    return this.precision;
        //}
    }
}

// CLASSIFICATION: UNCLASSIFIED