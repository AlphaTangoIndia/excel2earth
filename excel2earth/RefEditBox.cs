// CLASSIFICATION: UNCLASSIFIED

using Excel = Microsoft.Office.Interop.Excel;
using excel2earth;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace excel2earth
{   
    public partial class RefEditBox : Form
    {
        public bool cancel;
        public Excel.Range rng;

        public RefEditBox()
        {
            this.InitializeComponent();
            this.cancel = true;

            if (Globals.ThisAddIn.Application.Selection is Excel.Range)
            {
                form_SheetSelectionChange(Globals.ThisAddIn.Application.Selection, (Excel.Range)Globals.ThisAddIn.Application.Selection);
            }
            
            Globals.ThisAddIn.Application.SheetSelectionChange += new Excel.AppEvents_SheetSelectionChangeEventHandler(form_SheetSelectionChange);
        }

        private void button_Click(object sender, EventArgs e)
        {
            this.cancel = false;
            this.Close();
        }

        void form_SheetSelectionChange(object Sh, Excel.Range Target)
        {
            this.textBox.Text = "";
            this.rng = Target;

            foreach (Excel.Range range in Target.Areas)
            {
                if (this.textBox.Text.Length > 0)
                {
                    this.textBox.Text += ",";
                }
                this.textBox.Text += "'" + range.Worksheet.Name + "'!" + range.get_Address(Type.Missing, Type.Missing, Excel.XlReferenceStyle.xlA1, Type.Missing, Type.Missing);
            }
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            if (this.validateRangeAddress(textBox.Text))
            {
                this.textBox.BackColor = SystemColors.Window;
            }
            else
            {
                this.textBox.BackColor = Color.Salmon;
            }
        }

        public bool validateRangeAddress(string address)
        {
            bool isValid = true;         
            string[] areas = address.Split(',');

            foreach (string addr in areas)
            {
                string cellsAddress;
                //bool cellsValid = false;

                if (addr.Contains("!")) // Includes Sheet Name
                {
                    bool sheetValid = false;
                    string[] sheetAndCells = addr.Split('!');
                    string sheetName = sheetAndCells[0].Replace("'", "");
                    cellsAddress = sheetAndCells[1];

                    foreach (Excel.Worksheet worksheet in Globals.ThisAddIn.Application.ActiveWorkbook.Worksheets)
                    {
                        sheetValid |= (sheetName == worksheet.Name);
                    }

                    isValid &= sheetValid;
                }
                else // Doesn't Include Sheet Name
                {
                    cellsAddress = addr;
                }

                if (cellsAddress.Split(':').Length > 2)
                {
                    return false;
                }
                else
                {
                    foreach (string cellAddr in cellsAddress.Split(':'))
                    {
                        string[] colsAndRows;
                        string col = "";
                        string row = "";

                        if (cellAddr.Contains("$"))
                        {
                            int startSubstr = Convert.ToInt32(cellAddr.Substring(0, 1) == "$");
                            colsAndRows = cellAddr.Substring(startSubstr, cellAddr.Length - startSubstr).Split('$');

                            if (colsAndRows.Length == 2)
                            {
                                if (IsAlpha(colsAndRows[0]) && IsNumeric(colsAndRows[1]))
                                {
                                    col = colsAndRows[0];
                                    row = colsAndRows[1];
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else if (colsAndRows.Length == 1)
                            {
                                if (IsAlpha(colsAndRows[0]))
                                {
                                    col = colsAndRows[0];
                                }
                                else if (IsNumeric(colsAndRows[0]))
                                {
                                    row = colsAndRows[0];
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {

                        }
                    }
                }               
            }

            return isValid;
        }

        public bool IsAlpha(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }

            foreach(char c in str.ToUpper().ToCharArray())
            {
                if (c < 'A' || c > 'Z')
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsNumeric(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }

            foreach (char c in str.ToCharArray())
            {
                if (c < '0' || c > '9')
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsAlphaNumeric(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }

            foreach (char c in str.ToUpper().ToCharArray())
            {
                if ((c < '0' || c > '9') && (c < 'A' || c > 'Z'))
                {
                    return false;
                }
            }

            return true;
        }
    }
}

// CLASSIFICATION: UNCLASSIFIED