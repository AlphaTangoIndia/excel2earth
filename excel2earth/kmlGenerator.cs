// CLASSIFICATION: UNCLASSIFIED

using Excel = Microsoft.Office.Interop.Excel;
using excel2earth.kml;
using MSP.CCS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace excel2earth
{
    /// <summary>
    /// Takes options from the Main form and applies them, pulling values from the Excel Workbook and generates the KML.
    /// </summary>
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class kmlGenerator
    {
        public Excel.Range range;
        public bool hasHeaders;
        public int nameColumn;
        public bool hasSnippet;
        public int snippetColumn;
        public int snippetMaxLines;
        public bool hasMultiGeometry;
        public int multiGeometryColumn;
        public int multiType;
        public int multiTypeGroupColumn;
        public int dataType;
        public int dataTypeGroupColumn;
        public bool hasModel;
        public int modelColumn;
        public int coordinatesFormat;
        public int coordinatesField1Column;
        public int coordinatesField2Column;
        public bool hasAltitude;
        public int altitudeColumn;
        public int altitudeMode;
        public int altitudeUnit;
        public bool extrude;
        public bool tesselllate;
        public int trackHeadingColumn;
        public int trackTiltColumn;
        public int trackRollColumn;
        public bool trackInterpolate;
        public int modelHeadingColumn;
        public int modelTiltColumn;
        public int modelRollColumn;
        public int modelXScaleColumn;
        public int modelYScaleColumn;
        public int modelZScaleColumn;
        public bool hasNoDateTimeField;
        public bool hasTimeStamp;
        public bool hasTimeSpan;
        public int dateTimeField1Column;
        public int dateTimeField2Column;
        public int dateTimeFormat1;
        public int dateTimeField3Column;
        public int dateTimeField4Column;
        public int dateTimeFormat2;
        public int[] descriptionColumns;
        public int[] folderColumns;
        public int iconColumn;
        public Dictionary<string, StyleMap> styleSelection;

        /// <summary>
        /// The order of Altitude modes.
        /// </summary>
        public Geometry.altitudeModeEnum[] altitudeModeIntToEnum = new Geometry.altitudeModeEnum[]
        {
            Geometry.altitudeModeEnum.clampToGround,
            Geometry.altitudeModeEnum.relativeToGround,
            Geometry.altitudeModeEnum.absolute,
            Geometry.altitudeModeEnum.clampToSeaFloor,
            Geometry.altitudeModeEnum.relativeToSeaFloor
        };

        /// <summary>
        /// An array of altitude unit multipliers to convert to meters.
        /// </summary>
        public double[] altitudeUnitMultipliers = new double[] 
        {
            1d,
            1000d,
            1200d / 3937d,
            3600d / 3937d,
            6336000d / 3937d,
            1852d,
            7200d / 3937d,
            792000d / 3937d,
            19008000d / 3937d
        };

        /// <summary>
        /// The date/time formats.
        /// </summary>
        public TimePrimitive.dateTimeFormatEnum[] dateTimeFormats = new TimePrimitive.dateTimeFormatEnum[]
        {
            TimePrimitive.dateTimeFormatEnum.dateTime,
            TimePrimitive.dateTimeFormatEnum.dateTime,
            TimePrimitive.dateTimeFormatEnum.date,
            TimePrimitive.dateTimeFormatEnum.gYearMonth,
            TimePrimitive.dateTimeFormatEnum.gYear
        };

        /// <summary>
        /// Constructor for the kmlGenerator class.
        /// </summary>
        public kmlGenerator()
        {
        }

        /// <summary>
        /// Generates the KML string.
        /// </summary>
        /// <param name="toolStripProgressBar">A windows Forms ToolStripProgressBar</param>
        /// <returns>The KML string.</returns>
        public string generateKML(ref System.Windows.Forms.ToolStripProgressBar toolStripProgressBar)
        {
            XNamespace gx = "http://www.google.com/kml/ext/2.2";
            XElement document = (XElement)(new Document().ToXNode());

            XDocument kml = new XDocument(
                new XComment("Created with excel2earth"),
                new XElement("kml",
                    new XAttribute(XNamespace.Xmlns + "kml", "http://www.opengis.net/kml/2.2"),
                    new XAttribute(XNamespace.Xmlns + "gx", "http://www.google.com/kml/ext/2.2"),
                    document
                )
            );

            int lastRow;

            if (this.range.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing).Row < this.range.Rows.Count)
            {
                lastRow = this.range.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing).Row;
            }
            else
            {
                lastRow = this.range.Rows.Count;
            }

            toolStripProgressBar.Minimum = this.range.Cells.Row + Convert.ToInt32(this.hasHeaders);
            toolStripProgressBar.Maximum = lastRow;
            toolStripProgressBar.Value = toolStripProgressBar.Minimum;
            toolStripProgressBar.Step = 1;

            foreach (StyleMap styleMap in this.styleSelection.Values.Where(StyleMap => StyleMap != null).Distinct())
            {
                if (!document.Descendants("StyleMap").Where(StyleMap => StyleMap.Attribute("id").Value == styleMap.id).Any())
                {
                    document.Add((XElement)styleMap.ToXNode());
                }
            }

            for (int row = 1 + Convert.ToInt32(this.hasHeaders); row <= lastRow; row++)
            {
                XElement parentNode = document;
                Placemark placemark = new Placemark(((Excel.Range)this.range.Cells[row, this.nameColumn]).Value2.ToString());
                string id = "";
                DecimalDegrees decimalDegrees;
                double altitude = 0.0;
                XElement[] childElements;
                XName childType = "Placemark";
                DateTime dateTime1;
                DateTime dateTime2;
                Geometry geometry = null;
                bool addElements = true;

                // XY Coordinates
                try
                {
                    switch (this.coordinatesFormat)
                    {
                        case 0: // Decimal Degrees
                            decimalDegrees = new DecimalDegrees(((Excel.Range)this.range.Cells[row, this.coordinatesField2Column]).Value2.ToString(), ((Excel.Range)this.range.Cells[row, this.coordinatesField1Column]).Value2.ToString());
                            break;
                        case 1: // Degrees, Decimal Minutes
                            decimalDegrees = new DegreesDecimalMinutes(((Excel.Range)this.range.Cells[row, this.coordinatesField2Column]).Value2.ToString(), ((Excel.Range)this.range.Cells[row, this.coordinatesField1Column]).Value2.ToString()).ToDecimalDegrees();
                            break;
                        case 2: // Degrees, Minutes, Seconds
                            decimalDegrees = new DegreesMinutesSeconds(((Excel.Range)this.range.Cells[row, this.coordinatesField2Column]).Value2.ToString(), ((Excel.Range)this.range.Cells[row, this.coordinatesField1Column]).Value2.ToString()).ToDecimalDegrees();
                            break;
                        case 3: // Military Grid Reference System
                            decimalDegrees = new MilitaryGridReferenceSystem(((Excel.Range)this.range.Cells[row, this.coordinatesField1Column]).Value2.ToString()).ToDecimalDegrees();
                            break;
                        //case 4: // Universal Transverse Mercator
                        //    throw new NotImplementedException();
                        //    break;
                        default:
                            decimalDegrees = new DecimalDegrees(0d, 0d);
                            break;
                    }
                }
                catch
                {
                    toolStripProgressBar.PerformStep();
                    continue;
                }

                // Z Coodinate
                if (this.hasAltitude)
                {
                    if (!double.TryParse(((Excel.Range)this.range.Cells[row, this.altitudeColumn]).Value2.ToString(), out altitude))
                    {
                        continue;
                    }

                    altitude *= altitudeUnitMultipliers[this.altitudeUnit];
                }

                Geometry.CoordinateSet coordinateSet = new Geometry.CoordinateSet(decimalDegrees.Latitude, decimalDegrees.Longitude, altitude);

                // Folders
                for (int i = 0; i < this.folderColumns.Length; i++)
                {
                    XElement[] folders = (from folder in parentNode.Descendants("Folder")
                                          where folder.Element("name").Value == ((Excel.Range)this.range.Cells[row, this.folderColumns[i]]).Value2.ToString()
                                          select folder).ToArray<XElement>();

                    if (folders.Length < 1) // If the folder doesn't exist, create it
                    {
                        parentNode.Add(new Folder(((Excel.Range)this.range.Cells[row, this.folderColumns[i]]).Value2.ToString()).ToXNode());
                        i--;
                    }
                    else // If the folder exists, use it
                    {
                        parentNode = folders[0];
                    }
                }

                // Snippet
                if (this.hasSnippet)
                {
                    placemark.snippet = ((Excel.Range)this.range.Cells[row, this.snippetColumn]).Value2.ToString();
                    placemark.snippetMaxLines = this.snippetMaxLines;
                }

                // Description
                if (this.descriptionColumns.Length > 0)
                {
                    XElement descriptionTable = new XElement("table");

                    for (int i = 0; i < this.descriptionColumns.Length; i++)
                    {
                        descriptionTable.Add
                        (
                            new XElement("tr",
                                new XAttribute("style", "background-color: #" + (i % 2 == 0 ? "FFFFFF" : "DDDDDD") + ";"),
                                new XElement("td",
                                    new XAttribute("style", "font-weight: bold;"),
                                    new XText(((Excel.Range)this.range.Cells[1, this.descriptionColumns[i]]).Value2.ToString())
                                ),
                                new XElement("td",
                                    new XText(((Excel.Range)this.range.Cells[row, this.descriptionColumns[i]]).Value2.ToString())
                                )
                            )
                        );
                    }

                    placemark.description = descriptionTable.ToString();
                }

                // Style
                if (this.iconColumn > 0)
                {
                    string styleKey = ((Excel.Range)this.range.Cells[row, this.iconColumn]).Value2.ToString();

                    if (this.styleSelection.ContainsKey(styleKey) && this.styleSelection[styleKey].id != null)
                    {
                        placemark.styleUrl = "#" + this.styleSelection[styleKey].id;
                    }
                }

                // Date/Time
                if (this.hasTimeStamp || this.hasTimeSpan)
                {
                    if (this.dateTimeFormat1 == 1)
                    {
                        DateTime.TryParse(((Excel.Range)this.range.Cells[row, this.dateTimeField1Column]).Value2.ToString() + " " + ((Excel.Range)this.range.Cells[row, this.dateTimeField2Column]).Value2.ToString(), out dateTime1);
                    }
                    else
                    {
                        DateTime.TryParse(((Excel.Range)this.range.Cells[row, this.dateTimeField1Column]).Value2.ToString(), new System.Globalization.CultureInfo("en-us", false).DateTimeFormat, System.Globalization.DateTimeStyles.AssumeUniversal, out dateTime1);
                    }

                    if (this.hasTimeStamp)
                    {
                        placemark.timePrimitive = new TimeStamp(dateTime1, dateTimeFormats[this.dateTimeFormat1]);
                    }

                    if (this.hasTimeSpan)
                    {
                        if (this.dateTimeFormat2 == 1)
                        {
                            DateTime.TryParse(((Excel.Range)this.range.Cells[row, this.dateTimeField3Column]).Value2.ToString() + " " + ((Excel.Range)this.range.Cells[row, this.dateTimeField4Column]).Value2.ToString(), out dateTime2);
                        }
                        else
                        {
                            DateTime.TryParse(((Excel.Range)this.range.Cells[row, this.dateTimeField3Column]).Value2.ToString(), out dateTime2);
                        }

                        placemark.timePrimitive = new excel2earth.kml.TimeSpan(dateTime1, dateTimeFormats[dateTimeFormat1], dateTime2, dateTimeFormats[dateTimeFormat2]);
                    }
                }

                // Model
                Orientation modelOrientation = null;
                Scale modelScale = null;
                Link modelLink = null;

                if (this.hasModel)
                {
                    // Orientation
                    if (this.modelHeadingColumn > 0 || this.modelTiltColumn > 0 || this.modelRollColumn > 0)
                    {
                        double modelHeading = 0.0;
                        double modelTilt = 0.0;
                        double modelRoll = 0.0;

                        if (this.modelHeadingColumn > 0)
                        {
                            double.TryParse(((Excel.Range)this.range.Cells[row, this.modelHeadingColumn]).Value2.ToString(), out modelHeading);
                        }

                        if (this.modelTiltColumn > 0)
                        {
                            double.TryParse(((Excel.Range)this.range.Cells[row, this.modelTiltColumn]).Value2.ToString(), out modelTilt);
                        }

                        if (this.modelRollColumn > 0)
                        {
                            double.TryParse(((Excel.Range)this.range.Cells[row, this.modelRollColumn]).Value2.ToString(), out modelRoll);
                        }

                        modelOrientation = new Orientation(modelHeading, modelTilt, modelRoll);
                    }

                    // Scale
                    if (this.modelXScaleColumn > 0 || this.modelYScaleColumn > 0 || this.modelZScaleColumn > 0)
                    {
                        double modelXScale = 1.0;
                        double modelYScale = 1.0;
                        double modelZScale = 1.0;

                        if (this.modelXScaleColumn > 0)
                        {
                            double.TryParse(((Excel.Range)this.range.Cells[row, this.modelXScaleColumn]).Value2.ToString(), out modelXScale);
                        }

                        if (this.modelYScaleColumn > 0)
                        {
                            double.TryParse(((Excel.Range)this.range.Cells[row, this.modelYScaleColumn]).Value2.ToString(), out modelYScale);
                        }

                        if (this.modelZScaleColumn > 0)
                        {
                            double.TryParse(((Excel.Range)this.range.Cells[row, this.modelZScaleColumn]).Value2.ToString(), out modelZScale);
                        }

                        modelScale = new Scale(modelXScale, modelYScale, modelZScale);
                    }

                    if (this.modelColumn > 0)
                    {
                        modelLink = new Link(((Excel.Range)this.range.Cells[row, this.modelColumn]).Value2.ToString());
                    }
                }

                // Geometry
                if (this.hasMultiGeometry)
                {
                    id = ((Excel.Range)this.range.Cells[row, this.multiGeometryColumn]).Value2.ToString();
                    childElements = (from children in parentNode.Descendants(childType)
                                     where children.Element("MultiGeometry").Attribute("id").Value == id
                                     select children).ToArray<XElement>();
                    childType = "MultiGeometry";

                    if (childElements.Length < 1) // If the Placemark doesn't exist, create it
                    {
                        placemark.geometry = new MultiGeometry(id);
                        parentNode.Add(placemark.ToXNode());
                        parentNode = (from elements in parentNode.Descendants("Placemark").Descendants(childType)
                                      where elements.Attribute("id").Value == id
                                      select elements).First();
                    }
                    else // If the Placemark exists, use it
                    {
                        parentNode = childElements.First();
                    }
                }

                #region MultiType
                switch (this.multiType)
                {
                    case 0: // None
                        break;
                    case 1: // Polygon
                        id = ((Excel.Range)this.range.Cells[row, this.multiTypeGroupColumn]).Value2.ToString();
                        childElements = (from children in parentNode.Descendants(childType).Descendants("Polygon")
                                         where children.Attribute("id").Value == id
                                         select children).ToArray<XElement>();
                        if (childElements.Length < 1) // If the Placemark doesn't exist, create it
                        {
                            Polygon polygon = new Polygon(id);

                            if (this.hasAltitude)
                            {
                                polygon.hasAltitude = true;
                                polygon.altitudeMode = this.altitudeModeIntToEnum[this.altitudeMode];
                                polygon.extrude = this.extrude;
                                polygon.tessellate = this.tesselllate;
                            }

                            if (childType == "Placemark")
                            {
                                XElement xPlacemark = (XElement)placemark.ToXNode();
                                parentNode.Add(xPlacemark);
                                parentNode = xPlacemark;
                            }

                            XElement xPolygon = (XElement)polygon.ToXNode();
                            parentNode.Add(xPolygon);
                            parentNode = xPolygon;
                        }
                        else // If the Placemark exists, use it
                        {
                            parentNode = childElements[0];
                            System.Windows.Forms.MessageBox.Show(parentNode.ToString());
                        }

                        childType = "Polygon";
                        break;
                    case 2: // MultiTrack
                        id = ((Excel.Range)this.range.Cells[row, this.multiTypeGroupColumn]).Value2.ToString();
                        childElements = (from children in parentNode.Descendants(childType).Descendants(gx + "MultiTrack")
                                         where children.Attribute("id").Value == id
                                         select children).ToArray<XElement>();

                        if (childElements.Length < 1) // If the Placemark doesn't exist, create it
                        {
                            MultiTrack multiTrack = new MultiTrack(id, this.trackInterpolate);

                            if (this.hasAltitude)
                            {
                                multiTrack.hasAltitude = true;
                                multiTrack.altitudeMode = this.altitudeModeIntToEnum[this.altitudeMode];
                            }

                            if (childType == "Placemark")
                            {
                                if (this.hasModel)
                                {
                                    placemark.styleUrl = null;
                                }

                                XElement xPlacemark = (XElement)placemark.ToXNode();
                                xPlacemark.Descendants("TimeStamp").Remove();
                                parentNode.Add(xPlacemark);
                                parentNode = xPlacemark;
                            }

                            XElement xMultiTrack = (XElement)multiTrack.ToXNode();
                            parentNode.Add(xMultiTrack);
                            parentNode = xMultiTrack;
                        }
                        else // If the placemark exists, use it
                        {
                            parentNode = childElements.First();
                        }

                        childType = gx + "MultiTrack";
                        break;
                }
                #endregion

                switch (this.dataType)
                {
                    case 0: // Point
                        geometry = new Point(new Geometry.Coordinates(coordinateSet));

                        if (this.hasAltitude)
                        {
                            ((Point)geometry).extrude = this.extrude;
                        }

                        break;
                    case 1: // Line String
                        id = ((Excel.Range)this.range.Cells[row, this.dataTypeGroupColumn]).Value2.ToString();
                        childElements = (from children in parentNode.Descendants(childType).Descendants("LineString")
                                         where children.Attribute("id").Value.ToString() == id
                                         select children).ToArray<XElement>();

                        if (childElements.Length < 1) // LineString does not exist
                        {
                            geometry = new LineString(id, new Geometry.Coordinates(coordinateSet));

                            if (this.hasAltitude)
                            {
                                ((LineString)geometry).extrude = this.extrude;
                                ((LineString)geometry).tessellate = this.tesselllate;
                            }
                        }
                        else // LineString exists, add coordinates
                        {
                            XElement coordinates = childElements[0].Element("coordinates");
                            coordinates.SetValue(coordinates.Value + " " + coordinateSet.getString());
                            updateDescriptionTable(ref this.range, ref row, ref document, (XElement)childElements[0].Parent);
                            addElements = false;
                        }

                        break;
                    case 2: // Linear Ring
                        id = ((Excel.Range)this.range.Cells[row, this.dataTypeGroupColumn]).Value2.ToString();

                        if (childType != "Polygon")
                        {
                            childElements = (from children in parentNode.Descendants(childType).Descendants("LinearRing")
                                             where children.Attribute("id").Value == id
                                             select children).ToArray<XElement>();
                        }
                        else
                        {
                            childElements = (from children in parentNode.Descendants().Descendants("LinearRing")
                                             where children.Attribute("id").Value == id
                                             select children).ToArray<XElement>();
                        }

                        if (childElements.Length < 1) // LinearRing does not exist
                        {
                            geometry = new LinearRing(id, new Geometry.Coordinates(coordinateSet));

                            if (this.hasAltitude && childType != "Polygon")
                            {
                                ((LinearRing)geometry).extrude = this.extrude;
                                ((LinearRing)geometry).tessellate = this.tesselllate;
                            }

                            if (childType == "Polygon")
                            {
                                if (!parentNode.Descendants("outerBoundaryIs").Any())
                                {
                                    parentNode.Add(new XElement("outerBoundaryIs", geometry.ToXNode()));
                                }
                                else
                                {
                                    parentNode.Add(new XElement("innerBoundaryIs", geometry.ToXNode()));
                                    updateDescriptionTable(ref this.range, ref row, ref document, parentNode.Parent);
                                }

                                addElements = false;
                            }
                        }
                        else // LinearRing exists, add coordinates
                        {
                            XElement coordinates = childElements[0].Element("coordinates");
                            coordinates.SetValue(coordinates.Value + " " + coordinateSet.getString());
                            updateDescriptionTable(ref this.range, ref row, ref document, childElements[0].Parent);
                            addElements = false;
                        }
                        break;
                    case 3: // Model
                        geometry = new Model(new Location(coordinateSet), modelOrientation, modelScale, modelLink);
                        break;
                    case 4: // Track
                        Orientation trackOrientation = null;
                        if (this.trackHeadingColumn > 0 || this.trackTiltColumn > 0 || this.trackRollColumn > 0)
                        {
                            double trackHeading = 0.0;
                            double trackTilt = 0.0;
                            double trackRoll = 0.0;

                            if (this.trackHeadingColumn > 0)
                            {
                                double.TryParse(((Excel.Range)this.range.Cells[row, this.trackHeadingColumn]).Value2.ToString(), out trackHeading);
                            }

                            if (this.trackTiltColumn > 0)
                            {
                                double.TryParse(((Excel.Range)this.range.Cells[row, this.trackTiltColumn]).Value2.ToString(), out trackTilt);
                            }

                            if (this.trackRollColumn > 0)
                            {
                                double.TryParse(((Excel.Range)this.range.Cells[row, this.trackRollColumn]).Value2.ToString(), out trackRoll);
                            }

                            trackOrientation = new Orientation(trackHeading, trackTilt, trackRoll);
                        }

                        id = ((Excel.Range)this.range.Cells[row, this.dataTypeGroupColumn]).Value2.ToString();

                        if (childType != gx + "MultiTrack")
                        {
                            childElements = (from children in parentNode.Descendants(childType).Descendants(gx + "Track")
                                             where children.Attribute("id").Value == id
                                             select children).ToArray<XElement>();
                        }
                        else
                        {
                            childElements = (from children in parentNode.Descendants(gx + "Track")
                                             where children.Attribute("id").Value == id
                                             select children).ToArray<XElement>();
                        }
                        if (childElements.Length < 1) // If the placemark doesn't exist, create it
                        {
                            geometry = new Track(id, new Geometry.Coordinates(coordinateSet));

                            if (this.hasTimeStamp)
                            {
                                ((Track)geometry).whens = new TimeStamp[] { (TimeStamp)placemark.timePrimitive };
                            }

                            if (trackOrientation != null)
                            {
                                ((Track)geometry).angles = new Orientation[] { trackOrientation };
                            }

                            if (this.hasModel)
                            {
                                ((Track)geometry).model = new Model(modelOrientation, modelScale, modelLink);
                            }

                            if (childType == gx + "MultiTrack" && parentNode.Descendants(gx + "Track").Any())
                            {
                                updateDescriptionTable(ref this.range, ref row, ref document, parentNode.Parent);
                            }
                        }
                        else
                        {
                            XElement track = childElements[0];

                            if (this.hasTimeStamp)
                            {
                                (from whens in track.Descendants("when") select whens).Last().AddAfterSelf(((XElement)placemark.timePrimitive.ToXNode()).Descendants("when"));
                            }

                            (from coords in track.Descendants(gx + "coord") select coords).Last().AddAfterSelf(new XElement(gx + "coord", coordinateSet.getString().Replace(',', ' ')));

                            if (trackOrientation != null)
                            {
                                (from angles in track.Descendants(gx + "angles") select angles).Last().AddAfterSelf(new XElement(gx + "angles", trackOrientation.getString()));
                            }

                            updateDescriptionTable(ref this.range, ref row, ref document, childElements[0].Parent);
                            addElements = false;
                        }
                        break;
                }

                if (addElements)
                {
                    if (this.hasAltitude && this.multiType != 1)
                    {
                        geometry.hasAltitude = true;
                        geometry.altitudeMode = this.altitudeModeIntToEnum[this.altitudeMode];
                    }

                    if (childType == "Placemark")
                    {
                        if (this.dataType == 4)
                        {
                            placemark.timePrimitive = null;
                        }

                        placemark.geometry = geometry;
                        parentNode.Add(placemark.ToXNode());
                    }
                    else
                    {
                        parentNode.Add(geometry.ToXNode());
                    }
                }
                toolStripProgressBar.PerformStep();
            }

            return kml.ToString();
        }

        /// <summary>
        /// Updates a description table in the KML balloon.
        /// </summary>
        /// <param name="range">The Excel Range for the values of the table</param>
        /// <param name="row">The row number of the Excel Range</param>
        /// <param name="document">The Document</param>
        /// <param name="xPlacemark">The Placemark to update</param>
        private void updateDescriptionTable(ref Excel.Range range, ref int row, ref XElement document, XElement xPlacemark)
        {
            XElement descriptionTable;

            while (!xPlacemark.Descendants("description").Any() && xPlacemark.Parent != document)
            {
                xPlacemark = xPlacemark.Parent;
            }

            if (xPlacemark.Descendants("description").Any())
            {
                descriptionTable = XElement.Parse(xPlacemark.Descendants("description").First().Value);

                for (int i = 0; i < this.descriptionColumns.Length && i < descriptionTable.Descendants("tr").Count(); i++)
                {
                    descriptionTable.Elements("tr").ElementAt(i).Add
                    (
                        new XElement("td",
                            new XText(((Excel.Range)range.Cells[row, this.descriptionColumns[i]]).Value2.ToString())
                        )
                    );
                }

                xPlacemark.Descendants("description").First().RemoveAll();
                xPlacemark.Descendants("description").First().Add(new XCData(descriptionTable.ToString()));
            }
        }
    }
}

// CLASSIFICATION: UNCLASSIFIED