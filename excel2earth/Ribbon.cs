// CLASSIFICATION: UNCLASSIFIED

using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Tools.Ribbon;
using System;
//using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Win32;

namespace excel2earth
{
    public partial class excel2earth_Ribbon : OfficeRibbon
    {
        public excel2earth_Ribbon()
        {
            this.InitializeComponent();
        }

        private void excel2earth_Ribbon_Load(object sender, RibbonUIEventArgs e)
        {
            this.To_dropDown.SelectedItemIndex++;
        }

        private void Main_button_Click(object sender, RibbonControlEventArgs e)
        {
            new Main().Show();
        }

        private void Simple_button_Click(object sender, RibbonControlEventArgs e)
        {
            new Main().simpleForm.Show();
        }

        private void ConvertMGRS_button_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                MilitaryGridReferenceSystem militaryGridReferenceSystem = new MilitaryGridReferenceSystem(this.MGRS_editBox.Text);
                this.MGRS_editBox.Text = militaryGridReferenceSystem.Grid;

                switch (this.LatLonFromat_dropDown.SelectedItem.Label)
                {
                    case "Decimal Degrees":
                        DecimalDegrees decimalDegrees = militaryGridReferenceSystem.ToDecimalDegrees();
                        this.Latitude_editBox.Text = decimalDegrees.Latitude.ToString();
                        this.Longitude_editBox.Text = decimalDegrees.Longitude.ToString();
                        break;
                    case "Degrees Decimal Minutes":
                        DegreesDecimalMinutes degreesDecimalMinutes = militaryGridReferenceSystem.ToDegreesDecimalMinutes();
                        this.Latitude_editBox.Text = degreesDecimalMinutes.Latitude;
                        this.Longitude_editBox.Text = degreesDecimalMinutes.Longitude;
                        break;
                    case "Degrees Minutes Seconds":
                        DegreesMinutesSeconds degreesMinutesSeconds = militaryGridReferenceSystem.ToDegreesMinutesSeconds();
                        this.Latitude_editBox.Text = degreesMinutesSeconds.Latitude;
                        this.Longitude_editBox.Text = degreesMinutesSeconds.Longitude;
                        break;
                }
            }
            catch
            {
                MessageBox.Show("Invalid MGRS Input");
            }
        }

        private void ConvertLatLon_button_Click(object sender, RibbonControlEventArgs e)
        {
            DecimalDegrees decimalDegrees;
            DegreesDecimalMinutes degreesDecimalMinutes;
            DegreesMinutesSeconds degreesMinutesSeconds;

            try
            {
                switch (this.LatLonFromat_dropDown.SelectedItem.Label)
                {
                    case "Decimal Degrees":
                        decimalDegrees = new DecimalDegrees(this.Longitude_editBox.Text, this.Latitude_editBox.Text);
                        this.MGRS_editBox.Text = decimalDegrees.ToMilitaryGridReferenceSystem().Grid;
                        break;
                    case "Degrees Decimal Minutes":
                        degreesDecimalMinutes = new DegreesDecimalMinutes(this.Longitude_editBox.Text, this.Latitude_editBox.Text);
                        this.MGRS_editBox.Text = degreesDecimalMinutes.ToMilitaryGridReferenceSystem().Grid;
                        break;
                    case "Degrees Minutes Seconds":
                        degreesMinutesSeconds = new DegreesMinutesSeconds(this.Longitude_editBox.Text, this.Latitude_editBox.Text);
                        this.MGRS_editBox.Text = degreesMinutesSeconds.ToMilitaryGridReferenceSystem().Grid;
                        break;
                }
            }
            catch
            {
                MessageBox.Show("Invalid " + this.LatLonFromat_dropDown.SelectedItem.Label + " input.");
            }
        }

        private void LatLonFromat_dropDown_SelectionChanged(object sender, RibbonControlEventArgs e)
        {
            CoordinateFormat coordinateFormat = null;
            DecimalDegrees decimalDegrees = new DecimalDegrees();
            DegreesDecimalMinutes degreesDecimalMinutes = new DegreesDecimalMinutes();
            DegreesMinutesSeconds degreesMinutesSeconds = new DegreesMinutesSeconds();

            if (!(String.IsNullOrEmpty(this.Longitude_editBox.Text) || String.IsNullOrEmpty(this.Latitude_editBox.Text)))
            {
                if (decimalDegrees.TryParse(this.Longitude_editBox.Text, this.Latitude_editBox.Text, out decimalDegrees))
                {
                    coordinateFormat = decimalDegrees;
                }
                else if (degreesDecimalMinutes.TryParse(this.Longitude_editBox.Text, this.Latitude_editBox.Text, out degreesDecimalMinutes))
                {
                    coordinateFormat = degreesDecimalMinutes;
                }
                else if (degreesMinutesSeconds.TryParse(this.Longitude_editBox.Text, this.Latitude_editBox.Text, out degreesMinutesSeconds))
                {
                    coordinateFormat = degreesMinutesSeconds;
                }

                if (coordinateFormat != null)
                {
                    try
                    {
                        switch (this.LatLonFromat_dropDown.SelectedItem.Label)
                        {
                            case "Decimal Degrees":
                                decimalDegrees = coordinateFormat.ToDecimalDegrees();
                                this.Latitude_editBox.Text = decimalDegrees.Latitude.ToString("0.#####");
                                this.Longitude_editBox.Text = decimalDegrees.Longitude.ToString("0.#####");
                                break;
                            case "Degrees Decimal Minutes":
                                degreesDecimalMinutes = coordinateFormat.ToDegreesDecimalMinutes();
                                this.Latitude_editBox.Text = degreesDecimalMinutes.Latitude;
                                this.Longitude_editBox.Text = degreesDecimalMinutes.Longitude;
                                break;
                            case "Degrees Minutes Seconds":
                                degreesMinutesSeconds = coordinateFormat.ToDegreesMinutesSeconds();
                                this.Latitude_editBox.Text = degreesMinutesSeconds.Latitude;
                                this.Longitude_editBox.Text = degreesMinutesSeconds.Longitude;
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Invalid input. " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Invalid input.");
                }
            }
        }

        private void SelectAndConvertColumns_button_Click(object sender, RibbonControlEventArgs e)
        {
            if (Globals.ThisAddIn.Application.Selection is Excel.Range)
            {
                Excel.Range rng = (Excel.Range)Globals.ThisAddIn.Application.Selection;

                if ((this.From_dropDown.SelectedItemIndex < 1 && rng.Columns.Count == 1) || (this.From_dropDown.SelectedItemIndex > 0 && rng.Columns.Count == 2))
                {
                    // Shift Cells so that nothing is overwritten
                    if (this.To_dropDown.SelectedItemIndex < 1)
                    {
                        ((Excel.Range)rng.get_Item(1, rng.Columns.Count)).get_Offset(0, 1).EntireColumn.Insert(Excel.XlInsertShiftDirection.xlShiftToRight, Type.Missing);
                    }
                    else
                    {
                        rng.Worksheet.get_Range(((Excel.Range)rng.get_Item(1, rng.Columns.Count)).get_Offset(0, 1).EntireColumn, ((Excel.Range)rng.get_Item(1, rng.Columns.Count)).get_Offset(0, 2).EntireColumn).Insert(Excel.XlInsertShiftDirection.xlShiftToRight, Type.Missing);
                    }

                    //ManualResetEvent[] doneEvents = new ManualResetEvent[rng.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing).Row];
                    
                    int lastRow = rng.Worksheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing).Row;

                    for (int i = 1; i <= rng.Rows.Count && ((Excel.Range)rng.get_Item(i, 1)).Row <= lastRow; i++)
                    {
                        //doneEvents[i - 1] = new ManualResetEvent(false);
                        RowConverter rowConverter = new RowConverter(rng, i, this.From_dropDown.SelectedItem.Label, this.To_dropDown.SelectedItem.Label); //, doneEvents[i - 1]);
                        rowConverter.convertRow();
                        //new Thread(new ThreadStart(rowConverter.convertRow)).Start();
                        //ThreadPool.QueueUserWorkItem(rowConverter.ThreadPoolCallback, i);
                    }

                    //foreach (ManualResetEvent handle in doneEvents)
                    //{
                    //    handle.WaitOne();
                    //}
                    //WaitHandle.WaitAll(doneEvents);

                    // Autofit Cells
                    if (this.To_dropDown.SelectedItemIndex < 1)
                    {
                        ((Excel.Range)rng.get_Item(1, rng.Columns.Count)).get_Offset(0, 1).EntireColumn.AutoFit();
                    }
                    else
                    {
                        rng.Worksheet.get_Range(((Excel.Range)rng.get_Item(1, rng.Columns.Count)).get_Offset(0, 1).EntireColumn, ((Excel.Range)rng.get_Item(1, rng.Columns.Count)).get_Offset(0, 2).EntireColumn).AutoFit();
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Selection!");
                }
            }
            else
            {
                MessageBox.Show("You must select a worksheet range.");
            }
        }

        private void GoogleEarth_button_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                string gePath = Registry.ClassesRoot.OpenSubKey(@"Google Earth.kmlfile\\shell\\Open\\command").GetValue(null).ToString().Replace(" \"%1\"", "");
                Process.Start(gePath);
            }
            catch
            {
                MessageBox.Show("Google Earth is not correctly associated with KML files.");
            }
        }
    }
}

// CLASSIFICATION: UNCLASSIFIED