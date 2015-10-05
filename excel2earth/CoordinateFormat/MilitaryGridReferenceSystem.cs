// CLASSIFICATION: UNCLASSIFIED

using MSP.CCS;
using System;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace excel2earth
{
    /// <summary>
    /// Military Grid Reference System coordinate format.
    /// </summary>
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class MilitaryGridReferenceSystem : CoordinateFormat
    {
        MGRSorUSNGCoordinates mgrsCoordinates;
        public string Grid { get; private set; }

        public MilitaryGridReferenceSystem(string Grid)
        {
            this.Grid = Grid;
            this.ValidateCoordinates();
            this.mgrsCoordinates = new MGRSorUSNGCoordinates(CoordinateType.Enum.militaryGridReferenceSystem, this.Grid);
        }

        public MilitaryGridReferenceSystem(MGRSorUSNGCoordinates mgrsCoordinates)
        {
            this.mgrsCoordinates = mgrsCoordinates;
            this.Grid = this.mgrsCoordinates.MGRSString;
            this.ValidateCoordinates();
        }

        private void ValidateCoordinates()
        {
            this.Grid = this.Grid.Replace(" ", "");
            if (new Regex("^(\\d{0,2}|\\s{2})[A-HJ-NP-Z]{3}(\\d{2})*$", RegexOptions.IgnoreCase).IsMatch(Grid))
            {
                // Fix Easting and Northing
                Regex enRegex = new Regex("(\\d{2})*$");
                string en = enRegex.Match(this.Grid).Value;
                int div = ((en.Length / 2 > 4) ? 5 : en.Length / 2);
                this.Grid = enRegex.Replace(this.Grid.ToUpper(), en.Substring(0, div).PadRight(5, '0') + en.Substring(en.Length / 2, div).PadRight(5, '0'), 1);
            }
            else
            {
                throw new ArgumentException("Invalid Grid.", "Grid");
            }
        }

        public void setGrid(string Grid)
        {
            this.Grid = Grid;
            this.ValidateCoordinates();
            this.mgrsCoordinates = new MGRSorUSNGCoordinates(CoordinateType.Enum.militaryGridReferenceSystem, this.Grid);
        }

        /// <summary>
        /// Converts the coordinates to Decimal Degrees.
        /// </summary>
        /// <returns>Coordintes in Decimal Degrees</returns>
        public override DecimalDegrees ToDecimalDegrees()
        {
            double _180_OVER_PI = 180d / Math.PI;
            GeodeticCoordinates geodeticCoordinates = new MGRS().convertToGeodetic(mgrsCoordinates);
            return new DecimalDegrees(Math.Round(geodeticCoordinates.longitude * _180_OVER_PI, 5), Math.Round(geodeticCoordinates.latitude * _180_OVER_PI, 5));
        }

        public override DegreesDecimalMinutes ToDegreesDecimalMinutes()
        {
            return this.ToDecimalDegrees().ToDegreesDecimalMinutes();
        }

        public override DegreesMinutesSeconds ToDegreesMinutesSeconds()
        {
            return this.ToDecimalDegrees().ToDegreesMinutesSeconds();
        }

        public override MilitaryGridReferenceSystem ToMilitaryGridReferenceSystem()
        {
            return this;
        }
    }
}

// CLASSIFICATION: UNCLASSIFIED