// CLASSIFICATION: UNCLASSIFIED

using Excel = Microsoft.Office.Interop.Excel;
using excel2earth.kml;
using excel2earth.FormSupport;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace excel2earth
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public partial class Main : Form
    {
        private string[] headers;
        private Excel.Range dataRange;
        private SortedList<string, int> headerPositions = new SortedList<string, int>();
        private Dictionary<string, int> iconSelection = new Dictionary<string, int>();
        private Dictionary<string, StyleMap> styleSelection = new Dictionary<string, StyleMap>();
        private ushort styleIncrement = 0;
        private GoogleEarthIconStyles googleEarthIconStyles = new GoogleEarthIconStyles();
        private Control[] ioControls;
        public Simple simpleForm = new Simple();
        private About aboutForm = new About();
        private const int iconImageSize = 32;
        private const int iconColumnAmount = 8;

        /// <summary>
        /// Constructs the Main form.
        /// </summary>
        public Main()
        {
            this.InitializeComponent();
            this.simpleForm.mainForm = this;
            this.Start_MultiType_comboBox.SelectedIndex = 0;
            this.Start_DataType_comboBox.SelectedIndex = 0;
            this.Location_Format_comboBox.SelectedIndex = 0;
            this.Location_AltitudeMode_comboBox.SelectedIndex = 1;
            this.Location_AltitudeUnit_comboBox.SelectedIndex = 0;
            this.DateTime_Format1_comboBox.SelectedIndex = 0;
            this.resetRangeToActiveSheet();   
            this.Start_Headers_checkBox_CheckedChanged(this, new EventArgs());

            // Event Handlers for Form Validation
            this.Start_Name_comboBox.SelectedIndexChanged += new EventHandler(this.checkForErrors);
            this.Start_Snippet_comboBox.EnabledChanged += new EventHandler(this.checkForErrors);
            this.Start_Snippet_comboBox.SelectedIndexChanged += new EventHandler(this.checkForErrors);
            this.Start_MultiGeometryGroup_comboBox.EnabledChanged += new EventHandler(this.checkForErrors);
            this.Start_MultiGeometryGroup_comboBox.SelectedIndexChanged += new EventHandler(this.checkForErrors);
            this.Start_MultiTypeGroup_comboBox.EnabledChanged += new EventHandler(this.checkForErrors);
            this.Start_MultiTypeGroup_comboBox.SelectedIndexChanged += new EventHandler(this.checkForErrors);
            this.Start_DataTypeGroup_comboBox.EnabledChanged += new EventHandler(this.checkForErrors);
            this.Start_DataTypeGroup_comboBox.SelectedIndexChanged += new EventHandler(this.checkForErrors);
            this.Start_Model_comboBox.EnabledChanged += new EventHandler(this.checkForErrors);
            this.Start_Model_comboBox.SelectedIndexChanged += new EventHandler(this.checkForErrors);
            this.Location_Field1_comboBox.SelectedIndexChanged += new EventHandler(this.checkForErrors);
            this.Location_Field2_comboBox.VisibleChanged += new EventHandler(this.checkForErrors);
            this.Location_Field2_comboBox.SelectedIndexChanged += new EventHandler(this.checkForErrors);
            this.Location_Altitude_comboBox.EnabledChanged += new EventHandler(this.checkForErrors);
            this.Location_Altitude_comboBox.SelectedIndexChanged += new EventHandler(this.checkForErrors);
            this.DateTime_Field1_comboBox.EnabledChanged += new EventHandler(this.checkForErrors);
            this.DateTime_Field1_comboBox.SelectedIndexChanged += new EventHandler(this.checkForErrors);
            this.DateTime_Field2_comboBox.EnabledChanged += new EventHandler(this.checkForErrors);
            this.DateTime_Field2_comboBox.VisibleChanged += new EventHandler(this.checkForErrors);
            this.DateTime_Field2_comboBox.SelectedIndexChanged += new EventHandler(this.checkForErrors);
            this.DateTime_Field3_comboBox.EnabledChanged += new EventHandler(this.checkForErrors);
            this.DateTime_Field3_comboBox.SelectedIndexChanged += new EventHandler(this.checkForErrors);
            this.DateTime_Field4_comboBox.EnabledChanged += new EventHandler(this.checkForErrors);
            this.DateTime_Field4_comboBox.VisibleChanged += new EventHandler(this.checkForErrors);
            this.DateTime_Field4_comboBox.SelectedIndexChanged += new EventHandler(this.checkForErrors);

            this.ioControls = new Control[]
            {
                this.Start_DataRange_checkBox,
                this.Start_DataRange_textBox,
                this.Start_Headers_checkBox,
                this.Start_Name_comboBox,
                this.Start_Snippet_checkBox,
                this.Start_Snippet_comboBox,
                this.Start_SnippetMaxLines_numericUpDown,
                this.Start_MultiGeometry_checkBox,
                this.Start_MultiGeometryGroup_comboBox,
                this.Start_MultiType_comboBox,
                this.Start_MultiTypeGroup_comboBox,
                this.Start_DataType_comboBox,
                this.Start_DataTypeGroup_comboBox,
                this.Start_Model_checkBox,
                this.Start_Model_comboBox,
                this.Location_Format_comboBox,
                this.Location_Field1_comboBox,
                this.Location_Field2_comboBox,
                this.Location_Altitude_checkBox,
                this.Location_Altitude_comboBox,
                this.Location_AltitudeMode_comboBox,
                this.Location_AltitudeUnit_comboBox,
                this.Location_AltitudeExtrude_checkBox,
                this.Location_AltitudeTessellate_checkBox,
                this.Location_TrackHeading_comboBox,
                this.Location_TrackTilt_comboBox,
                this.Location_TrackRoll_comboBox,
                this.Location_TrackInterpolate_checkBox,
                this.Location_ModelHeading_comboBox,
                this.Location_ModelTilt_comboBox,
                this.Location_ModelRoll_comboBox,
                this.Location_ModelXScale_comboBox,
                this.Location_ModelYScale_comboBox,
                this.Location_ModelZScale_comboBox,
                this.DateTime_None_radioButton,
                this.DateTime_TimeStamp_radioButton,
                this.DateTime_TimeSpan_radioButton,
                this.DateTime_Field1_comboBox,
                this.DateTime_Field2_comboBox,
                this.DateTime_Format1_comboBox,
                this.DateTime_Field3_comboBox,
                this.DateTime_Field4_comboBox,
                this.DateTime_Format2_comboBox,
                this.Description_Columns_listBox,
                this.Description_Table_listBox,
                this.Folders_Columns_listBox,
                this.Folders_Folders_treeView,
                this.Icons_Columns_comboBox,
                this.Icons_SymbolCategories_listBox,
                this.main_OpenAfterCreation_checkBox
            };

            // Add Icons
            for (int i = 0; i < this.Icons_imageList.Images.Count; i++)
            {
                this.addIcon(i);
            }
        }

        #region Main Form
        private void main_tabPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.main_Back_button.Enabled = (this.main_tabPage.SelectedIndex != 0);
            this.main_Next_button.Enabled = (this.main_tabPage.SelectedIndex != main_tabPage.TabPages.Count - 1);
        }

        private void main_Back_button_Click(object sender, EventArgs e)
        {
            if (this.main_tabPage.SelectedIndex > 0)
            {
                this.main_tabPage.SelectedIndex--;
            }
        }

        private void main_Next_button_Click(object sender, EventArgs e)
        {
            if (this.main_tabPage.SelectedIndex < this.main_tabPage.TabPages.Count - 1)
            {
                this.main_tabPage.SelectedIndex++;
            }
        }

        public void main_Finish_button_Click(object sender, EventArgs e)
        {
            if (this.KMZKML_saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                kmlGenerator kmlGen = new kmlGenerator();

                // Start
                kmlGen.range = this.dataRange;
                kmlGen.hasHeaders = this.Start_Headers_checkBox.Checked;
                kmlGen.nameColumn = this.Start_Name_comboBox.SelectedIndex + 1;
                kmlGen.hasSnippet = this.Start_Snippet_checkBox.Checked;
                kmlGen.snippetColumn = this.Start_Snippet_comboBox.SelectedIndex + 1;
                kmlGen.snippetMaxLines = (int)this.Start_SnippetMaxLines_numericUpDown.Value;
                kmlGen.hasMultiGeometry = this.Start_MultiGeometry_checkBox.Checked;
                kmlGen.multiGeometryColumn = this.Start_MultiGeometryGroup_comboBox.SelectedIndex + 1;
                kmlGen.multiType = this.Start_MultiType_comboBox.SelectedIndex;
                kmlGen.multiTypeGroupColumn = this.Start_MultiTypeGroup_comboBox.SelectedIndex + 1;
                kmlGen.dataType = this.Start_DataType_comboBox.SelectedIndex;
                kmlGen.dataTypeGroupColumn = this.Start_DataTypeGroup_comboBox.SelectedIndex + 1;
                kmlGen.hasModel = this.Start_Model_checkBox.Checked;
                kmlGen.modelColumn = this.Start_Model_comboBox.SelectedIndex + 1;
                // Location
                kmlGen.coordinatesFormat = this.Location_Format_comboBox.SelectedIndex;
                kmlGen.coordinatesField1Column = this.Location_Field1_comboBox.SelectedIndex + 1;
                kmlGen.coordinatesField2Column = this.Location_Field2_comboBox.SelectedIndex + 1;
                kmlGen.hasAltitude = this.Location_Altitude_checkBox.Checked;
                kmlGen.altitudeColumn = this.Location_Altitude_comboBox.SelectedIndex + 1;
                kmlGen.altitudeMode = this.Location_AltitudeMode_comboBox.SelectedIndex;
                kmlGen.altitudeUnit = this.Location_AltitudeUnit_comboBox.SelectedIndex;
                kmlGen.extrude = this.Location_AltitudeExtrude_checkBox.Checked;
                kmlGen.tesselllate = this.Location_AltitudeTessellate_checkBox.Checked;
                kmlGen.trackHeadingColumn = this.Location_TrackHeading_comboBox.SelectedIndex + 1;
                kmlGen.trackTiltColumn = this.Location_TrackTilt_comboBox.SelectedIndex + 1;
                kmlGen.trackRollColumn = this.Location_TrackRoll_comboBox.SelectedIndex + 1;
                kmlGen.trackInterpolate = this.Location_TrackInterpolate_checkBox.Checked;
                kmlGen.modelHeadingColumn = this.Location_ModelHeading_comboBox.SelectedIndex + 1;
                kmlGen.modelTiltColumn = this.Location_ModelTilt_comboBox.SelectedIndex + 1;
                kmlGen.modelRollColumn = this.Location_ModelRoll_comboBox.SelectedIndex + 1;
                kmlGen.modelXScaleColumn = this.Location_ModelXScale_comboBox.SelectedIndex + 1;
                kmlGen.modelYScaleColumn = this.Location_ModelYScale_comboBox.SelectedIndex + 1;
                kmlGen.modelZScaleColumn = this.Location_ModelZScale_comboBox.SelectedIndex + 1;
                // Date/Time
                kmlGen.hasNoDateTimeField = this.DateTime_None_radioButton.Checked;
                kmlGen.hasTimeStamp = this.DateTime_TimeStamp_radioButton.Checked;
                kmlGen.hasTimeSpan = this.DateTime_TimeSpan_radioButton.Checked;
                kmlGen.dateTimeField1Column = this.DateTime_Field1_comboBox.SelectedIndex + 1;
                kmlGen.dateTimeField2Column = this.DateTime_Field2_comboBox.SelectedIndex + 1;
                kmlGen.dateTimeFormat1 = this.DateTime_Format1_comboBox.SelectedIndex;
                kmlGen.dateTimeField3Column = this.DateTime_Field3_comboBox.SelectedIndex + 1;
                kmlGen.dateTimeField4Column = this.DateTime_Field4_comboBox.SelectedIndex + 1;
                kmlGen.dateTimeFormat2 = this.DateTime_Format2_comboBox.SelectedIndex;
                // Description
                List<int> descriptionColumns = new List<int>();

                for (int i = 0; i < this.Description_Table_listBox.Items.Count; i++)
                {
                    descriptionColumns.Add(this.headerPositions[this.Description_Table_listBox.Items[i].ToString()] + 1);
                }
                
                kmlGen.descriptionColumns = descriptionColumns.ToArray();
                // Folders
                List<int> folderColumns = new List<int>();
                TreeNodeCollection tnc = this.Folders_Folders_treeView.Nodes;
                
                while (tnc.Count > 0)
                {
                    folderColumns.Add(this.headerPositions[tnc[0].Text] + 1);
                    tnc = tnc[0].Nodes;
                }
                
                kmlGen.folderColumns = folderColumns.ToArray();
                // Icons
                kmlGen.iconColumn = this.Icons_Columns_comboBox.SelectedIndex + 1;
                kmlGen.styleSelection = this.styleSelection;

                if (this.KMZKML_saveFileDialog.FileName.EndsWith(".kmz"))
                {
                    using (ZipFile zip = new ZipFile())
                    {
                        zip.AddEntry("doc.kml", kmlGen.generateKML(ref this.toolStripProgressBar));
                        zip.Save(this.KMZKML_saveFileDialog.FileName);
                    }
                }
                else
                {
                    StreamWriter file = new StreamWriter(this.KMZKML_saveFileDialog.FileName);
                    file.Write(kmlGen.generateKML(ref this.toolStripProgressBar));
                    file.Close();
                }

                if (this.main_OpenAfterCreation_checkBox.Checked)
                {
                    Process.Start(this.KMZKML_saveFileDialog.FileName);
                }
            }
        }

        private void toolStripStatusLabel_Click(object sender, EventArgs e)
        {
            this.aboutForm.Show();
        }
        #endregion

        #region Tool Strip Menu
        private void loadSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.Settings_openFileDialog.ShowDialog() == DialogResult.OK && File.Exists(this.Settings_openFileDialog.FileName))
            {
                using (StreamReader file = new StreamReader(this.Settings_openFileDialog.FileName))
                {
                    string line;

                    foreach (Control control in this.ioControls)
                    {
                        if (file.Peek() < 0)
                        {
                            break;
                        }
                        else
                        {
                            line = file.ReadLine();

                            if (control is CheckBox)
                            {
                                ((CheckBox)control).Checked = Convert.ToBoolean(line);
                                
                                if (((CheckBox)control).Name == "Start_Headers_checkBox")
                                {
                                    this.Start_DataRange_checkBox_CheckedChanged(sender, e);
                                }
                            }
                            else if (control is ComboBox)
                            {
                                int index = Convert.ToInt32(line);

                                if (index < ((ComboBox)control).Items.Count)
                                {
                                    ((ComboBox)control).SelectedIndex = index;
                                }
                            }
                            else if (control is NumericUpDown)
                            {
                                ((NumericUpDown)control).Value = Convert.ToDecimal(line);
                            }
                            else if (control is RadioButton)
                            {
                                ((RadioButton)control).Checked = Convert.ToBoolean(line);
                            }
                            else if (control is ListBox)
                            {
                                string[] items = line.Split(' ');

                                if (control == this.Icons_SymbolCategories_listBox)
                                {
                                    if (items.Length == ((ListBox)control).Items.Count && !string.IsNullOrEmpty(items[0]))
                                    {
                                        this.styleSelection.Clear();

                                        

                                        for (int i = 0; i < ((ListBox)control).Items.Count; i++)
                                        {
                                            // need to convert load string to icon
                                            // check save settings function
                                            //
                                            //
                                            //
                                            //
                                            //
                                            //this.iconSelection.Add(((ListBox)control).Items[i].ToString(), new GoogleEarthIconParameters(Convert.ToInt32(items[i]), new Color(), 0, 0));
                                        }
                                    }
                                }
                                else
                                {
                                    ((ListBox)control).Items.Clear();

                                    if (!string.IsNullOrEmpty(items[0]))
                                    {
                                        foreach (string item in items)
                                        {
                                            if (Convert.ToInt32(item) < this.headers.Length)
                                            {
                                                ((ListBox)control).Items.Add(headers[Convert.ToInt32(item)]);
                                            }
                                        }
                                    }
                                }
                            }
                            else if (control is TextBox)
                            {
                                ((TextBox)control).Text = line;
                            }
                            else if (control is TreeView)
                            {
                                string[] items = line.Split(' ');
                                TreeNodeCollection tnc = ((TreeView)control).Nodes;
                                tnc.Clear();

                                if (!string.IsNullOrEmpty(items[0]))
                                {
                                    foreach (string item in items)
                                    {
                                        if (Convert.ToInt32(item) < this.headers.Length)
                                        {
                                            while (tnc.Count > 0) tnc = tnc[0].Nodes;
                                            tnc.Add(headers[Convert.ToInt32(item)]);
                                        }
                                    }
                                }

                                ((TreeView)control).ExpandAll();
                            }
                        }
                    }

                    file.Close();
                }
            }
        }

        private void saveSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.Settings_saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter file = new StreamWriter(this.Settings_saveFileDialog.FileName))
                {
                    foreach (Control control in this.ioControls)
                    {
                        if (control is CheckBox)
                        {
                            file.WriteLine(((CheckBox)control).Checked.ToString());
                        }
                        else if (control is ComboBox)
                        {
                            file.WriteLine(((ComboBox)control).SelectedIndex.ToString());
                        }
                        else if (control is NumericUpDown)
                        {
                            file.WriteLine(((NumericUpDown)control).Value.ToString());
                        }
                        else if (control is RadioButton)
                        {
                            file.WriteLine(((RadioButton)control).Checked.ToString());
                        }
                        else if (control is ListBox)
                        {
                            string line = "";

                            if (control == this.Icons_SymbolCategories_listBox)
                            {
                                foreach (string item in ((ListBox)control).Items)
                                {
                                    line += styleSelection[item].ToString() + " ";
                                }
                            }
                            else
                            {
                                foreach (string item in ((ListBox)control).Items)
                                {
                                    line += this.headerPositions[item].ToString() + " ";
                                }
                            }

                            file.WriteLine(line.TrimEnd());
                        }
                        else if (control is TextBox)
                        {
                            file.WriteLine(((TextBox)control).Text.Replace('\n', ' '));
                        }
                        else if (control is TreeView)
                        {
                            string line = "";
                            TreeNodeCollection tnc = ((TreeView)control).Nodes;
                            while (tnc.Count > 0)
                            {
                                line += this.headerPositions[tnc[0].Text].ToString() + " ";
                                tnc = tnc[0].Nodes;
                            }
                            file.WriteLine(line.TrimEnd());
                        }
                    }

                    file.Close();
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aboutExcel2earthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.aboutForm.Show();
        }

        private void simpleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.simpleForm.Show();
        }
        #endregion

        #region Start Tab
        private void Start_DataRange_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            this.Start_DataRange_textBox.Enabled = !this.Start_DataRange_checkBox.Checked;
            this.Start_DataRange_button.Enabled = !this.Start_DataRange_checkBox.Checked;

            if (this.Start_DataRange_checkBox.Checked)
            {
                this.resetRangeToActiveSheet();
            }
            else
            {
                this.parseToRange(this.Start_DataRange_textBox.Text);
            }

            this.Start_Headers_checkBox_CheckedChanged(sender, e);
        }

        private void Start_DataRange_button_Click(object sender, EventArgs e)
        {
            this.Hide();
            RefEditBox refEditBox = new RefEditBox();
            refEditBox.FormClosing += new FormClosingEventHandler(refEditBox_FormClosing);
            refEditBox.Show();
        }

        private void Start_Headers_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            int headersCount = (this.headers == null) ? 0 : this.headers.Length;

            if (this.dataRange == null)
            {
                this.headers = new string[] {};
                this.headerPositions.Clear();
            }
            else
            {
                this.headers = this.getHeaders(this.Start_Headers_checkBox.Checked, this.dataRange);
            }

            foreach (ComboBox comboBox in new Control[] {
                this.Start_Name_comboBox,
                this.simpleForm.Start_Name_comboBox,
                this.Start_Snippet_comboBox,
                this.Start_MultiGeometryGroup_comboBox,
                this.Start_MultiTypeGroup_comboBox,
                this.Start_DataTypeGroup_comboBox,
                this.Start_Model_comboBox,
                this.Location_Field1_comboBox,
                this.simpleForm.Location_Field1_comboBox,
                this.Location_Field2_comboBox,
                this.simpleForm.Location_Field2_comboBox,
                this.Location_Altitude_comboBox,
                this.Location_TrackHeading_comboBox,
                this.Location_TrackTilt_comboBox,
                this.Location_TrackRoll_comboBox,
                this.Location_ModelHeading_comboBox,
                this.Location_ModelTilt_comboBox,
                this.Location_ModelRoll_comboBox,
                this.Location_ModelXScale_comboBox,
                this.Location_ModelYScale_comboBox,
                this.Location_ModelZScale_comboBox,
                this.DateTime_Field1_comboBox,
                this.DateTime_Field2_comboBox,
                this.DateTime_Field3_comboBox,
                this.DateTime_Field4_comboBox,
                this.Icons_Columns_comboBox})
            {
                int tempSelectedIndex = comboBox.SelectedIndex;
                int tempItemsCount = comboBox.Items.Count;
                comboBox.Items.Clear();
                comboBox.Items.AddRange(this.headers);

                if (tempItemsCount == comboBox.Items.Count)
                {
                    comboBox.SelectedIndex = tempSelectedIndex;
                }
            }

            if (headerPositions.Count == 0 || headersCount != this.headers.Length)
            {
                this.Description_Columns_listBox.Items.Clear();
                this.Description_Table_listBox.Items.Clear();
                this.Folders_Columns_listBox.Items.Clear();
                this.Folders_Folders_treeView.Nodes.Clear();
                this.Description_Table_listBox.Items.AddRange(this.headers);
                this.Folders_Columns_listBox.Items.AddRange(this.headers);
            }
            else
            {
                foreach (ListBox listBox in new ListBox[] {
                    this.Description_Columns_listBox,
                    this.Description_Table_listBox,
                    this.Folders_Columns_listBox})
                {
                    for (int i = 0; i < listBox.Items.Count; i++)
                    {
                        if (this.headers.Length >= listBox.Items.Count)
                        {
                            listBox.Items[i] = this.headers[headerPositions[listBox.Items[i].ToString()]];
                        }
                    }
                }

                TreeNodeCollection tnc = this.Folders_Folders_treeView.Nodes;
                while (tnc.Count > 0)
                {
                    tnc[0].Text = this.headers[headerPositions[tnc[0].Text]];
                    tnc = tnc[0].Nodes;
                }
            }
            
            this.headerPositions.Clear();
            
            for (int i = 0; i < this.headers.Length; i++)
            {
                this.headerPositions.Add(this.headers[i], i);
            }
        }

        private void Start_Name_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.simpleForm.Visible)
            {
                this.simpleForm.Start_Name_comboBox.SelectedIndex = this.Start_Name_comboBox.SelectedIndex;
            }
        }

        private void Start_Snippet_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            this.Start_Snippet_comboBox.Enabled = this.Start_Snippet_checkBox.Checked;
            this.Start_SnippetMaxLines_label.Enabled = this.Start_Snippet_checkBox.Checked;
            this.Start_SnippetMaxLines_numericUpDown.Enabled = this.Start_Snippet_checkBox.Checked;
        }

        private void Start_MultiGeometry_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            this.Start_MultiGeometryGroup_label.Enabled = this.Start_MultiGeometry_checkBox.Checked;
            this.Start_MultiGeometryGroup_comboBox.Enabled = this.Start_MultiGeometry_checkBox.Checked;
        }

        private void Start_DataType_comboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            this.Start_MultiType_comboBox_SelectionChangeCommitted(sender, e);
        }

        private void Start_MultiType_comboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            switch (this.Start_MultiType_comboBox.Text)
            {
                case "None":
                    this.Start_MultiTypeGroup_label.Enabled = false;
                    this.Start_MultiTypeGroup_comboBox.Enabled = false;
                    this.Start_DataType_comboBox.Enabled = true;
                    this.Location_TrackInterpolate_checkBox.Enabled = false;
                    break;
                case "Polygon":
                    this.Start_MultiTypeGroup_label.Enabled = true;
                    this.Start_MultiTypeGroup_comboBox.Enabled = true;
                    this.Start_DataType_comboBox.Enabled = false;
                    this.Start_DataType_comboBox.SelectedItem = "Linear Ring";
                    this.Location_TrackInterpolate_checkBox.Enabled = false;
                    break;
                case "Multi Track":
                    this.Start_MultiTypeGroup_label.Enabled = true;
                    this.Start_MultiTypeGroup_comboBox.Enabled = true;
                    this.Start_DataType_comboBox.Enabled = false;
                    this.Start_DataType_comboBox.SelectedItem = "Track";
                    this.Location_TrackInterpolate_checkBox.Enabled = true;
                    break;
            }
        }

        private void Start_DataType_comboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            switch (Start_DataType_comboBox.Text)
            {
                case "Point":
                    this.Start_DataTypeGroup_label.Enabled = false;
                    this.Start_DataTypeGroup_comboBox.Enabled = false;
                    this.Start_Model_checkBox.Enabled = false;
                    this.Location_AltitudeExtrude_checkBox.Visible = true;
                    this.Location_AltitudeTessellate_checkBox.Visible = false;
                    this.Location_Track_groupBox.Enabled = false;
                    break;
                case "Line String":
                    this.Start_DataTypeGroup_label.Enabled = true;
                    this.Start_DataTypeGroup_comboBox.Enabled = true;
                    this.Start_Model_checkBox.Enabled = false;
                    this.Location_AltitudeExtrude_checkBox.Visible = true;
                    this.Location_AltitudeTessellate_checkBox.Visible = true;
                    this.Location_Track_groupBox.Enabled = false;
                    break;
                case "Linear Ring":
                    this.Start_DataTypeGroup_label.Enabled = true;
                    this.Start_DataTypeGroup_comboBox.Enabled = true;
                    this.Start_Model_checkBox.Enabled = false;
                    this.Location_AltitudeExtrude_checkBox.Visible = true;
                    this.Location_AltitudeTessellate_checkBox.Visible = true;
                    this.Location_Track_groupBox.Enabled = false;
                    break;
                case "Model":
                    this.Start_DataTypeGroup_label.Enabled = false;
                    this.Start_DataTypeGroup_comboBox.Enabled = false;
                    this.Start_Model_checkBox.Enabled = true;
                    this.Start_Model_checkBox.Checked = true;
                    this.Location_AltitudeExtrude_checkBox.Visible = false;
                    this.Location_AltitudeTessellate_checkBox.Visible = false;
                    this.Location_Track_groupBox.Enabled = false;
                    break;
                case "Track":
                    this.Start_DataTypeGroup_label.Enabled = true;
                    this.Start_DataTypeGroup_comboBox.Enabled = true;
                    this.Start_Model_checkBox.Enabled = true;
                    this.Location_AltitudeExtrude_checkBox.Visible = false;
                    this.Location_AltitudeTessellate_checkBox.Visible = false;
                    this.Location_Track_groupBox.Enabled = true;
                    break;
            }
        }

        private void Start_DataType_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Start_DataType_comboBox_SelectionChangeCommitted(sender, e);
        }

        private void Start_Model_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.Start_DataType_comboBox.Text == "Model")
            {
                this.Start_Model_checkBox.Checked = true;
            }

            this.Start_Model_label.Enabled = Start_Model_checkBox.Checked;
            this.Start_Model_comboBox.Enabled = Start_Model_checkBox.Checked;
            this.Location_Model_groupBox.Enabled = Start_Model_checkBox.Checked;
        }

        private void Start_Model_checkBox_EnabledChanged(object sender, EventArgs e)
        {
            this.Start_Model_comboBox.Enabled = (this.Start_Model_checkBox.Enabled && this.Start_Model_checkBox.Checked);
        }
        #endregion

        #region Location Tab
        public void Location_Format_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.simpleForm.Visible)
            {
                this.simpleForm.Location_Format_comboBox.SelectedIndex = this.Location_Format_comboBox.SelectedIndex;
            }

            if (this.Location_Format_comboBox.SelectedIndex > 2) // Military Grid Reference System
            {
                this.simpleForm.Location_Field1_label.Text = this.Location_Field1_label.Text = "Grid: ";
                this.simpleForm.Location_Field2_label.Visible = this.Location_Field2_label.Visible = false;
                this.simpleForm.Location_Field2_comboBox.Visible = this.Location_Field2_comboBox.Visible = false;
            }
            else
            {
                this.simpleForm.Location_Field1_label.Text = this.Location_Field1_label.Text = "Latitude (Y): ";
                this.simpleForm.Location_Field2_label.Visible = this.Location_Field2_label.Visible = true;
                this.simpleForm.Location_Field2_comboBox.Visible = this.Location_Field2_comboBox.Visible = true;
            }
        }

        private void Location_Field1_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.simpleForm.Visible)
            {
                this.simpleForm.Location_Field1_comboBox.SelectedIndex = this.Location_Field1_comboBox.SelectedIndex;
            }
        }

        private void Location_Field2_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.simpleForm.Visible)
            {
                this.simpleForm.Location_Field2_comboBox.SelectedIndex = this.Location_Field2_comboBox.SelectedIndex;
            }
        }

        private void Location_Altitude_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            this.Location_Altitude_comboBox.Enabled = this.Location_Altitude_checkBox.Checked;
            this.Location_AltitudeMode_label.Enabled = this.Location_Altitude_checkBox.Checked;
            this.Location_AltitudeMode_comboBox.Enabled = this.Location_Altitude_checkBox.Checked;
            this.Location_AltitudeUnit_label.Enabled = this.Location_Altitude_checkBox.Checked;
            this.Location_AltitudeUnit_comboBox.Enabled = this.Location_Altitude_checkBox.Checked;
            this.Location_AltitudeExtrude_checkBox.Enabled = this.Location_Altitude_checkBox.Checked;
            this.Location_AltitudeTessellate_checkBox.Enabled = this.Location_Altitude_checkBox.Checked;
            this.Location_AltitudeMode_comboBox_SelectedIndexChanged(sender, e);
        }

        private void Location_AltitudeMode_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.Location_AltitudeMode_comboBox.Text)
            {
                case "clampToGround":
                    this.Location_AltitudeExtrude_checkBox.Enabled = false;
                    this.Location_AltitudeTessellate_checkBox.Enabled = this.Location_Altitude_checkBox.Checked;
                    break;
                case "relativeToGround":
                    this.Location_AltitudeExtrude_checkBox.Enabled = this.Location_Altitude_checkBox.Checked;
                    this.Location_AltitudeTessellate_checkBox.Enabled = false;
                    break;
                case "absolute":
                    this.Location_AltitudeExtrude_checkBox.Enabled = this.Location_Altitude_checkBox.Checked;
                    this.Location_AltitudeTessellate_checkBox.Enabled = false;
                    break;
                case "clampToSeaFloor":
                    this.Location_AltitudeExtrude_checkBox.Enabled = false;
                    this.Location_AltitudeTessellate_checkBox.Enabled = this.Location_Altitude_checkBox.Checked;
                    break;
                case "relativeToSeaFloor":
                    this.Location_AltitudeExtrude_checkBox.Enabled = this.Location_Altitude_checkBox.Checked;
                    this.Location_AltitudeTessellate_checkBox.Enabled = false;
                    break;
            }
        }
        #endregion

        #region Date/Time Tab
        private void DateTime_None_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            this.DateTime_Fields_groupBox.Enabled = !this.DateTime_None_radioButton.Checked;
            this.DateTime_Field1_label.Enabled = !this.DateTime_None_radioButton.Checked;
            this.DateTime_Field1_comboBox.Enabled = !this.DateTime_None_radioButton.Checked;
            this.DateTime_Field2_label.Enabled = !this.DateTime_None_radioButton.Checked;
            this.DateTime_Field2_comboBox.Enabled = !this.DateTime_None_radioButton.Checked;
            this.DateTime_Format1_label.Enabled = !this.DateTime_None_radioButton.Checked;
            this.DateTime_Format1_comboBox.Enabled = !this.DateTime_None_radioButton.Checked;
        }

        private void DateTime_TimeSpan_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            this.DateTime_Begin_label.Visible = this.DateTime_TimeSpan_radioButton.Checked;
            this.DateTime_End_label.Visible = this.DateTime_TimeSpan_radioButton.Checked;
            this.DateTime_Field3_label.Enabled = this.DateTime_TimeSpan_radioButton.Checked;
            this.DateTime_Field3_comboBox.Enabled = this.DateTime_TimeSpan_radioButton.Checked;
            this.DateTime_Field4_label.Enabled = this.DateTime_TimeSpan_radioButton.Checked;
            this.DateTime_Field4_comboBox.Enabled = this.DateTime_TimeSpan_radioButton.Checked;
            this.DateTime_Format2_label.Enabled = this.DateTime_TimeSpan_radioButton.Checked;
            this.DateTime_Format2_comboBox.Enabled = this.DateTime_TimeSpan_radioButton.Checked;
        }

        private void DateTime_Format1_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.DateTime_Field2_label.Visible = (this.DateTime_Format1_comboBox.SelectedIndex == 1);
            this.DateTime_Field2_comboBox.Visible = (this.DateTime_Format1_comboBox.SelectedIndex == 1);
            this.DateTime_Format2_comboBox.SelectedIndex = this.DateTime_Format1_comboBox.SelectedIndex;
            
            if (this.DateTime_Format1_comboBox.SelectedIndex == 0)
            {
                this.DateTime_Field1_label.Text = "Date/Time:";
            }
            else
            {
                this.DateTime_Field1_label.Text = "Date:";
            }
        }

        private void DateTime_Format2_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.DateTime_Field4_label.Visible = (this.DateTime_Format2_comboBox.SelectedIndex == 1);
            this.DateTime_Field4_comboBox.Visible = (this.DateTime_Format2_comboBox.SelectedIndex == 1);
            
            if (this.DateTime_Format2_comboBox.SelectedIndex == 0)
            {
                this.DateTime_Field3_label.Text = "Date/Time:";
            }
            else
            {
                this.DateTime_Field3_label.Text = "Date:";
            }
        }
        #endregion

        #region Description Tab
        internal void Description_AddAll_button_Click(object sender, EventArgs e)
        {
            ListBoxExtensions.swapListBoxes(ref this.Description_Columns_listBox, ref this.Description_Table_listBox, false, false, ref headerPositions);
        }

        private void Description_SwapToColumns_button_Click(object sender, EventArgs e)
        {
            ListBoxExtensions.swapListBoxes(ref this.Description_Table_listBox, ref this.Description_Columns_listBox, true, true, ref headerPositions);
        }

        private void Description_SwapToTable_button_Click(object sender, EventArgs e)
        {
            ListBoxExtensions.swapListBoxes(ref this.Description_Columns_listBox, ref this.Description_Table_listBox, true, false, ref headerPositions);
        }

        private void Description_RemoveAll_button_Click(object sender, EventArgs e)
        {
            ListBoxExtensions.swapListBoxes(ref this.Description_Table_listBox, ref this.Description_Columns_listBox, false, true, ref headerPositions);
        }

        private void Description_MoveToTop_button_Click(object sender, EventArgs e)
        {
            ListBoxExtensions.moveToTop(ref this.Description_Table_listBox);
        }

        private void Description_MoveUp_button_Click(object sender, EventArgs e)
        {
            ListBoxExtensions.moveUp(ref this.Description_Table_listBox);
        }

        private void Description_MoveDown_button_Click(object sender, EventArgs e)
        {
            ListBoxExtensions.moveDown(ref this.Description_Table_listBox);
        }

        private void Description_MoveToBottom_button_Click(object sender, EventArgs e)
        {
            ListBoxExtensions.moveToBottom(ref this.Description_Table_listBox);
        }
        #endregion

        #region Folders Tab
        private void Folders_AddAll_button_Click(object sender, EventArgs e)
        {
            ListBoxExtensions.swapToTreeView(ref this.Folders_Columns_listBox, ref this.Folders_Folders_treeView, false);
        }

        private void Folders_SwapToColumns_button_Click(object sender, EventArgs e)
        {
            TreeViewExtensions.swapToListBox(ref this.Folders_Folders_treeView, ref this.Folders_Columns_listBox, true, ref headerPositions);
        }

        private void Folders_SwapToFolders_button_Click(object sender, EventArgs e)
        {
            ListBoxExtensions.swapToTreeView(ref this.Folders_Columns_listBox, ref this.Folders_Folders_treeView, true);
        }

        internal void Folders_RemoveAll_button_Click(object sender, EventArgs e)
        {
            TreeViewExtensions.swapToListBox(ref this.Folders_Folders_treeView, ref this.Folders_Columns_listBox, false, ref headerPositions);
        }

        private void Folders_MoveToTop_button_Click(object sender, EventArgs e)
        {
            TreeViewExtensions.moveToTop(ref this.Folders_Folders_treeView);
        }

        private void Folders_MoveUp_button_Click(object sender, EventArgs e)
        {
            TreeViewExtensions.moveUp(ref this.Folders_Folders_treeView);
        }

        private void Folders_MoveDown_button_Click(object sender, EventArgs e)
        {
            TreeViewExtensions.moveDown(ref this.Folders_Folders_treeView);
        }

        private void Folders_MoveToBottom_button_Click(object sender, EventArgs e)
        {
            TreeViewExtensions.moveToBottom(ref this.Folders_Folders_treeView);
        }
        #endregion

        #region Icons Tab
        private void Icons_Columns_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Icons_SymbolCategories_listBox.BeginUpdate();
            this.iconSelection.Clear();
            this.styleSelection.Clear();
            this.Icons_SymbolCategories_listBox.Items.Clear();
            this.Icons_Preview_pictureBox.Image = null;

            if (this.Icons_Columns_comboBox.SelectedIndex > -1)
            {
                int lastRow;

                if (this.dataRange.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing).Row < this.dataRange.Rows.Count)
                {
                    lastRow = this.dataRange.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing).Row;
                }
                else
                {
                    lastRow = this.dataRange.Rows.Count;
                }

                for (int i = Convert.ToInt32(this.Start_Headers_checkBox.Checked) + 1; i <= lastRow; i++)
                {
                    string val = ((Excel.Range)this.dataRange.Cells[i, this.Icons_Columns_comboBox.SelectedIndex + 1]).Text.ToString();
                    
                    if (!(this.iconSelection.ContainsKey(val) || this.styleSelection.ContainsKey(val)))
                    {
                        this.iconSelection.Add(val, 0);
                        this.styleSelection.Add(val, this.googleEarthIconStyles.getStyleMap(0));
                        this.Icons_SymbolCategories_listBox.Items.Add(val);
                    }
                }
            }

            this.Icons_SymbolCategories_listBox.EndUpdate();
        }

        private void Icons_SymbolCategories_listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Icons_Preview_pictureBox.Image = null;
            this.Icons_Preview_pictureBox.BackColor = Color.Transparent;
            
            if (this.Icons_SymbolCategories_listBox.SelectedItems.Count > 0)
            {
                bool differentIcons = false;
                bool differentColors = false;
                bool differentScales = false;
                bool differentOpacities = false;
                string selectedItemZero = this.Icons_SymbolCategories_listBox.SelectedItems[0].ToString();
                string firstItem;
                string secondItem;

                for (int i = 1; i < this.Icons_SymbolCategories_listBox.SelectedItems.Count; i++)
                {
                    firstItem = this.Icons_SymbolCategories_listBox.SelectedItems[i - 1].ToString();
                    secondItem = this.Icons_SymbolCategories_listBox.SelectedItems[i].ToString();

                    differentIcons |= (this.iconSelection[firstItem] != this.iconSelection[secondItem]);

                    if (this.styleSelection[firstItem] != null && this.styleSelection[secondItem] != null)
                    {
                        differentColors |= (this.styleSelection[firstItem].normal.style.iconStyle.color.R != this.styleSelection[secondItem].normal.style.iconStyle.color.R)
                                        || (this.styleSelection[firstItem].normal.style.iconStyle.color.G != this.styleSelection[secondItem].normal.style.iconStyle.color.G)
                                        || (this.styleSelection[firstItem].normal.style.iconStyle.color.B != this.styleSelection[secondItem].normal.style.iconStyle.color.B);
                        differentScales |= (this.styleSelection[firstItem].normal.style.iconStyle.scale != this.styleSelection[secondItem].normal.style.iconStyle.scale);
                        differentOpacities |= (this.styleSelection[firstItem].normal.style.iconStyle.color.A != this.styleSelection[secondItem].normal.style.iconStyle.color.A);
                    }
                }

                if (differentIcons)
                {
                    this.Icons_Preview_pictureBox.BackColor = SystemColors.InactiveCaption;
                }
                else
                {
                    if (this.iconSelection[selectedItemZero] > -1)
                    {
                        this.Icons_Preview_pictureBox.Image = this.Icons_imageList.Images[this.iconSelection[selectedItemZero]];
                    }
                    else
                    {
                        this.Icons_Preview_pictureBox.Image = null;
                    }
                }

                if (differentColors)
                {
                    this.Icons_IconColor_button.BackColor = SystemColors.InactiveCaption;
                }
                else
                {
                    this.Icons_IconColor_button.BackColor = this.styleSelection[selectedItemZero].normal.style.iconStyle.color;
                }

                if (differentScales)
                {
                    this.Icons_IconScale_numericUpDown.BackColor = SystemColors.InactiveCaption;
                }
                else
                {
                    this.Icons_IconScale_numericUpDown.BackColor = SystemColors.Window;
                    this.Icons_IconScale_numericUpDown.Value = (decimal)this.styleSelection[selectedItemZero].normal.style.iconStyle.scale;
                }

                if (differentOpacities)
                {
                    this.Icons_IconOpacity_textBox.BackColor = SystemColors.InactiveCaption;
                    this.Icons_IconOpacity_numericUpDown.BackColor = SystemColors.InactiveCaption;
                }
                else
                {
                    this.Icons_IconOpacity_textBox.BackColor = SystemColors.Window;
                    this.Icons_IconOpacity_numericUpDown.BackColor = SystemColors.Window;
                    this.Icons_IconOpacity_numericUpDown.Value = decimal.Round((decimal)(this.styleSelection[selectedItemZero].normal.style.iconStyle.color.A / 2.55m), 0, MidpointRounding.ToEven);
                }
            }

            //this.Icons_IconOpacity_numericUpDown_ValueChanged(sender, e);
        }

        private void Icons_NoIcon_button_Click(object sender, EventArgs e)
        {
            if (this.Icons_SymbolCategories_listBox.SelectedItems.Count > 0)
            {
                string selectedItem;
                Style noIconStyle = new Style("NoIcon", new IconStyle(0f, new excel2earth.kml.Icon(), new IconStyle.vec2(0d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction)));
                noIconStyle.iconStyle.color = Color.Transparent;
                StyleMap noIconStyleMap = new StyleMap("msn_NoIcon", new StyleMap.Pair(StyleMap.Pair.styleStateEnum.normal, noIconStyle), new StyleMap.Pair(StyleMap.Pair.styleStateEnum.normal, noIconStyle));
                noIconStyleMap.normal.style.id = "sn_" + noIconStyleMap.normal.style.id;
                noIconStyleMap.highlight.style.id = "sh_" + noIconStyleMap.highlight.style.id;

                for (int i = 0; i < this.Icons_SymbolCategories_listBox.SelectedItems.Count; i++)
                {
                    selectedItem = this.Icons_SymbolCategories_listBox.SelectedItems[i].ToString();
                    this.iconSelection[selectedItem] = -1;
                    this.styleSelection[selectedItem] = noIconStyleMap;
                }

                this.Icons_SymbolCategories_listBox_SelectedIndexChanged(sender, e);
            }
            else
            {
                MessageBox.Show("You must select a category first.");
            }
        }

        private void Icons_Icon_Click(object sender, EventArgs e)
        {
            if (this.Icons_SymbolCategories_listBox.SelectedItems.Count > 0)
            {
                int iconIndex = Convert.ToInt32(((Control)sender).Tag); //Convert.ToInt32(((PictureBox)sender).Name);

                for (int i = 0; i < this.Icons_SymbolCategories_listBox.SelectedItems.Count; i++)
                {
                    this.iconSelection[this.Icons_SymbolCategories_listBox.SelectedItems[i].ToString()] = iconIndex;

                    if (iconIndex < this.googleEarthIconStyles.styles.Count())
                    {
                        this.styleSelection[this.Icons_SymbolCategories_listBox.SelectedItems[i].ToString()] = this.googleEarthIconStyles.getStyleMap(iconIndex);
                    }
                    else
                    {
                        // Custom Icons
                    }
                }

                this.Icons_SymbolCategories_listBox_SelectedIndexChanged(sender, e);
            }
            else
            {
                MessageBox.Show("You must select a category first.");
            }
        }

        private void Icons_Icon_MouseEnter(object sender, EventArgs e)
        {
            ((Button)sender).FlatStyle = FlatStyle.Standard;
        }

        private void Icons_Icon_MouseLeave(object sender, EventArgs e)
        {
            ((Button)sender).FlatStyle = FlatStyle.Flat;
        }

        private void Icons_IconColor_button_Click(object sender, EventArgs e)
        {
            if (this.Icons_SymbolCategories_listBox.SelectedItems.Count > 0)
            {
                if (this.Icons_IconColor_colorDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedItem;
                    this.Icons_IconColor_button.BackColor = this.Icons_IconColor_colorDialog.Color;

                    for (int i = 0; i < this.Icons_SymbolCategories_listBox.SelectedItems.Count; i++)
                    {
                        selectedItem = this.Icons_SymbolCategories_listBox.SelectedItems[i].ToString();
                        this.incrementStyleMapId(ref this.styleSelection[selectedItem].id);
                        this.styleSelection[selectedItem].normal.style.iconStyle.color = this.Icons_IconColor_colorDialog.Color;
                        this.styleSelection[selectedItem].highlight.style.iconStyle.color = this.Icons_IconColor_colorDialog.Color;
                    }

                    this.Icons_SymbolCategories_listBox_SelectedIndexChanged(sender, e);
                }
            }
            else
            {
                MessageBox.Show("You must select a category first.");
            }
        }

        private void Icons_IconScale_numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (this.Icons_SymbolCategories_listBox.SelectedItems.Count > 0)
            {
                string selectedItem;
                float scaleHighlightDifference;

                for (int i = 0; i < this.Icons_SymbolCategories_listBox.SelectedItems.Count; i++)
                {
                    selectedItem = this.Icons_SymbolCategories_listBox.SelectedItems[i].ToString();
                    this.incrementStyleMapId(ref this.styleSelection[selectedItem].id);
                    scaleHighlightDifference = this.styleSelection[selectedItem].highlight.style.iconStyle.scale - this.styleSelection[selectedItem].normal.style.iconStyle.scale;
                    this.styleSelection[selectedItem].normal.style.iconStyle.scale = (float)this.Icons_IconScale_numericUpDown.Value;
                    this.styleSelection[selectedItem].highlight.style.iconStyle.scale = (float)this.Icons_IconScale_numericUpDown.Value + scaleHighlightDifference;
                }

                this.Icons_SymbolCategories_listBox_SelectedIndexChanged(sender, e);
            }
            else
            {
                MessageBox.Show("You must select a category first.");
            }
        }

        private void Icons_IconOpacity_numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (this.Icons_SymbolCategories_listBox.SelectedItems.Count > 0)
            {
                string selectedItem;

                for (int i = 0; i < this.Icons_SymbolCategories_listBox.SelectedItems.Count; i++)
                {
                    selectedItem = this.Icons_SymbolCategories_listBox.SelectedItems[i].ToString();
                    this.incrementStyleMapId(ref this.styleSelection[selectedItem].id);
                    this.styleSelection[selectedItem].normal.style.iconStyle.color = Color.FromArgb((int)(this.Icons_IconOpacity_numericUpDown.Value * 2.55m), this.styleSelection[selectedItem].normal.style.iconStyle.color);
                    this.styleSelection[selectedItem].highlight.style.iconStyle.color = this.styleSelection[selectedItem].normal.style.iconStyle.color;
                }

                this.Icons_IconOpacity_textBox.Text = this.Icons_IconOpacity_numericUpDown.Value.ToString() + "%";
                this.Icons_SymbolCategories_listBox_SelectedIndexChanged(sender, e);
            }
            else
            {
                MessageBox.Show("You must select a category first.");
            }
        }

        private void Icons_IconOpacity_textBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down && this.Icons_IconOpacity_numericUpDown.Value > this.Icons_IconOpacity_numericUpDown.Minimum)
                {
                    this.Icons_IconOpacity_numericUpDown.Value -= this.Icons_IconOpacity_numericUpDown.Increment;
                }
                else if (e.KeyCode == Keys.Up && this.Icons_IconOpacity_numericUpDown.Value < this.Icons_IconOpacity_numericUpDown.Maximum)
                {
                    this.Icons_IconOpacity_numericUpDown.Value += this.Icons_IconOpacity_numericUpDown.Increment;
                }
            }
            catch
            {
            }
        }

        private void Icons_IconOpacity_textBox_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Delta < 0 && this.Icons_IconOpacity_numericUpDown.Value > this.Icons_IconOpacity_numericUpDown.Minimum)
                {
                    this.Icons_IconOpacity_numericUpDown.Value -= this.Icons_IconOpacity_numericUpDown.Increment;
                }
                else if (e.Delta > 0 && this.Icons_IconOpacity_numericUpDown.Value < this.Icons_IconOpacity_numericUpDown.Maximum)
                {
                    this.Icons_IconOpacity_numericUpDown.Value += this.Icons_IconOpacity_numericUpDown.Increment;
                }
            }
            catch
            {
            }
        }

        private void Icons_AddCustomIcon_button_Click(object sender, EventArgs e)
        {
            if (this.Icons_AddCustomIcon_openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.addIcon(Image.FromFile(this.Icons_AddCustomIcon_openFileDialog.FileName), this.Icons_AddCustomIcon_openFileDialog.FileName);
                //this.Icons_Preview_pictureBox.Image = Image.FromFile(this.Icons_AddCustomIcon_openFileDialog.FileName);
                this.Icons_tableLayoutPanel.ScrollControlIntoView(this.Icons_tableLayoutPanel.Controls[this.Icons_tableLayoutPanel.Controls.Count - 1]);
                // Save to settings file
            }
        }

        private void Icons_ClearCustomIcons_button_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want clear all custom Icons?", "Clear Custom Icons", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                for (int i = this.Icons_tableLayoutPanel.Controls.Count - 1; i > this.googleEarthIconStyles.styles.Count(); i--)
                {
                    this.Icons_tableLayoutPanel.Controls.RemoveAt(i);
                }
            }
        }
        #endregion

        #region Miscellaneous Functions
        /// <summary>
        /// Resizes an Image to a specified size.
        /// </summary>
        /// <param name="imgToResize">The image to resize</param>
        /// <param name="size">The desired image size</param>
        /// <returns>The resized Image</returns>
        private static Image resizeImage(Image imgToResize, Size size)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)size.Width / (float)sourceWidth);
            nPercentH = ((float)size.Height / (float)sourceHeight);

            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
            }
            else
            {
                nPercent = nPercentW;
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();

            return (Image)b;
        }

        /// <summary>
        /// Adds and formats a new button to the Icons_tableLayoutPanel.
        /// </summary>
        /// <param name="i">The icon number</param>
        private void addIcon(int i)
        {
            Button button = new Button();
            button.Tag = i;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Size = new Size(iconImageSize, iconImageSize);
            button.Image = resizeImage(this.Icons_imageList.Images[i], new Size(iconImageSize, iconImageSize));
            button.ImageAlign = ContentAlignment.MiddleCenter;
            button.Click += new EventHandler(Icons_Icon_Click);
            button.MouseEnter += new EventHandler(Icons_Icon_MouseEnter);
            button.MouseLeave += new EventHandler(Icons_Icon_MouseLeave);
            this.Icons_tableLayoutPanel.Controls.Add(button);
        }

        /// <summary>
        /// Overload for addIcon for using an image. Adds the Image to the Icons_ImageList and calls addIcon.
        /// </summary>
        /// <param name="image">An image for the icon</param>
        private void addIcon(Image image, string URL)
        {
            URL = URL.Replace('\\', '/');
            string styleName = URL.Substring(URL.LastIndexOf('/'));
            styleName = styleName.Substring(0, styleName.IndexOf('.'));
            this.googleEarthIconStyles.styles.Concat(new Style[] { new Style(styleName, new IconStyle(1.1f, new excel2earth.kml.Icon(URL), new IconStyle.vec2(0.5d, 0.5d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))) });
            this.Icons_imageList.Images.Add(image);
            this.addIcon(this.Icons_imageList.Images.Count - 1);
        }
        
        private string[] getErrors()
        {
            List<string> errors = new List<string>();

            // Start
            if (this.Start_Name_comboBox.SelectedIndex < 0) errors.Add("Name");
            if (this.Start_Snippet_checkBox.Checked && this.Start_Snippet_comboBox.SelectedIndex < 0) errors.Add("Snippet");
            if (this.Start_MultiGeometryGroup_comboBox.Enabled && this.Start_MultiGeometryGroup_comboBox.SelectedIndex < 0) errors.Add("Multi Geometry Group");
            if (this.Start_MultiTypeGroup_comboBox.Enabled && this.Start_MultiTypeGroup_comboBox.SelectedIndex < 0) errors.Add("Multi Type Group");
            if (this.Start_DataTypeGroup_comboBox.Enabled && this.Start_DataTypeGroup_comboBox.SelectedIndex < 0) errors.Add("Data Type Group");
            if (this.Start_Model_comboBox.Enabled && this.Start_Model_comboBox.SelectedIndex < 0) errors.Add("Model Link");
            // Location
            if (this.Location_Format_comboBox.SelectedIndex < 0) errors.Add("Format");
            if (this.Location_Field1_comboBox.SelectedIndex < 0) errors.Add(this.minusColon(this.Location_Field1_label.Text));
            if (this.Location_Format_comboBox.SelectedIndex < 2 && this.Location_Field2_comboBox.SelectedIndex < 0) errors.Add(this.minusColon(this.Location_Field2_label.Text));
            if (this.Location_Altitude_comboBox.Enabled && this.Location_Altitude_comboBox.SelectedIndex < 0) errors.Add("Altitude");
            // Date/Time
            if (this.DateTime_Field1_comboBox.Enabled && this.DateTime_Field1_comboBox.SelectedIndex < 0) errors.Add(this.minusColon(this.DateTime_Field1_label.Text));
            if (this.DateTime_Field2_comboBox.Enabled && this.DateTime_Field2_comboBox.Visible && this.DateTime_Field2_comboBox.SelectedIndex < 0) errors.Add(this.minusColon(this.DateTime_Field2_label.Text));
            if (this.DateTime_Field3_comboBox.Enabled && this.DateTime_Field3_comboBox.SelectedIndex < 0) errors.Add(this.minusColon(this.DateTime_Field3_label.Text));
            if (this.DateTime_Field4_comboBox.Enabled && this.DateTime_Field4_comboBox.Visible && this.DateTime_Field4_comboBox.SelectedIndex < 0) errors.Add(this.minusColon(this.DateTime_Field4_label.Text));

            return errors.ToArray();
        }

        private string minusColon(string labelText)
        {
            if (labelText.Length > 1)
            {
                return labelText.Substring(0, labelText.Length - 2);
            }
            else
            {
                return labelText;
            }
        }

        private void checkForErrors(object sender, EventArgs e)
        {
            this.checkForErrors();
        }

        internal void checkForErrors()
        {
            string[] errors = this.getErrors();
            this.simpleForm.save_button.Enabled = this.main_Finish_button.Enabled = (errors.Length == 0);

            if (errors.Length > 0)
            {
                string err = "";
                foreach (string error in errors)
                {
                    err += error + '\n';
                }
                this.errors_toolTip.SetToolTip(this.main_Finish_button, err.Substring(0, err.Length - 2));
            }
            else
            {
                this.errors_toolTip.RemoveAll();
            }
        }

        private void refEditBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!((RefEditBox)sender).cancel)
            {
                this.Start_DataRange_textBox.Text = ((RefEditBox)sender).textBox.Text;
                this.dataRange = ((RefEditBox)sender).rng;
                this.Start_Headers_checkBox_CheckedChanged(sender, e);
            }
            this.Show();
        }

        private static Excel.Range Union(Excel.Range range1, Excel.Range range2)
        {
            if (range1 == null && range2 == null)
            {
                return null;
            }
            if (range1 == null)
            {
                return range2;
            }
            if (range2 == null)
            {
                return range1;
            }

            return Globals.ThisAddIn.Application.Union(range1, range2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
        }

        private void resetRangeToActiveSheet()
        {
            Excel.Worksheet CurrentSheet = (Excel.Worksheet)Globals.ThisAddIn.Application.ActiveSheet;
            this.dataRange = CurrentSheet.get_Range(CurrentSheet.Cells[1, 1], CurrentSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing));
        }

        private void parseToRange(string rangeString)
        {
            this.dataRange = null;

            if (!string.IsNullOrEmpty(rangeString))
            {
                try
                {
                    string[] SheetsAndCells = rangeString.Split(',');

                    foreach (string SheetAndCells in SheetsAndCells)
                    {
                        string[] sc = SheetAndCells.Split('!');
                        this.dataRange = Union(this.dataRange, ((Excel.Worksheet)Globals.ThisAddIn.Application.Sheets.get_Item(sc[0].Replace("'", ""))).get_Range(sc[1], Type.Missing));
                    }
                }
                catch
                {
                    MessageBox.Show("Range is invalid.");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheetHasHeaders"></param>
        /// <param name="rng"></param>
        /// <returns></returns>
        public string[] getHeaders(bool sheetHasHeaders, Excel.Range rng)
        {
            string[] headers = new string[rng.Columns.Count];

            for (int i = 1; i <= rng.Columns.Count; i++)
            {
                if (!string.IsNullOrEmpty(((Excel.Range)rng.Cells[1, i]).Text.ToString()) && sheetHasHeaders)
                {
                    headers[i - 1] = ((Excel.Range)rng.Cells[1, i]).Text.ToString();
                }
                else
                {
                    headers[i - 1] = "Column " + this.columnNumberToLetter(((Excel.Range)rng.Cells[1, i]).Column);
                }
            }

            return headers;
        }

        private string columnNumberToLetter(int number)
        {
            return new String((char)((int)'A' + ((number - 1) % 26)), (int)Math.Ceiling(number / 26.0));
        }

        private void incrementStyleMapId(ref string id)
        {
            Regex iRegex = new Regex(@"_\d+$");
            Match iMatch = iRegex.Match(id);
            
            if (iMatch.Success)
            {
                id = id.Substring(0, iMatch.Index);
            }

            id += "_" + (++this.styleIncrement).ToString();
        }
        #endregion
    }
}

// CLASSIFICATION: UNCLASSIFIED