// CLASSIFICATION: UNCLASSIFIED

namespace MSP.CCS
{
    /// <summary>
    /// Coordinate Conversion Exception.
    /// 
    /// Originally from GEOTRANS.
    /// </summary>
    class CoordinateConversionException
    {
        private string message;

        public CoordinateConversionException(string message)
        {
            this.message = message;
        }

        public CoordinateConversionException(string directionStr, string coordinateSystemName, string separatorStr, string message)
        {
            this.message = directionStr + coordinateSystemName + separatorStr + message;
        }

        public string getMessage()
        {
            return this.message;
        }
    }
}

// CLASSIFICATION: UNCLASSIFIED