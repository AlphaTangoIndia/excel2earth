// CLASSIFICATION: UNCLASSIFIED

namespace MSP.CCS
{
    /// <summary>
    /// Coordinate Tuples.
    /// 
    /// Originally from GEOTRANS.
    /// </summary>
    public class CoordinateTuple
    {
        public CoordinateType.Enum coordinateType;
        public string errorMessage;
        public string warningMessage;

        public CoordinateTuple()
        {
            this.coordinateType = CoordinateType.Enum.geodetic;
            this.errorMessage = "";
            this.warningMessage = "";
        }

        public CoordinateTuple(CoordinateType.Enum coordinateType)
        {
            this.coordinateType = coordinateType;
            this.errorMessage = "";
            this.warningMessage = "";
        }

        public CoordinateTuple(CoordinateType.Enum coordinateType, string warningMessage)
        {
            this.coordinateType = coordinateType;
            this.errorMessage = "";
            this.warningMessage = warningMessage;
        }

        public void setCoordinateType(CoordinateType.Enum coordinateType)
        {
            this.coordinateType = coordinateType;
        }

        public CoordinateType.Enum getCoordinateType()
        {
            return this.coordinateType;
        }

        public void set(CoordinateType.Enum coordinateType, string warningMessage, string errorMessage)
        {
            this.coordinateType = coordinateType;
            this.warningMessage = warningMessage;
            this.errorMessage = errorMessage;
        }

        public void setErrorMessage(string errorMessage)
        {
            this.errorMessage = errorMessage;
        }

        public string getErrorMessage()
        {
            return this.errorMessage;
        }

        public void setWarningMessage(string warningMessage)
        {
            this.warningMessage = warningMessage;
        }

        public string getWarningMessage()
        {
            return this.warningMessage;
        }
    }
}

// CLASSIFICATION: UNCLASSIFIED