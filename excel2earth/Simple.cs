// CLASSIFICATION: UNCLASSIFIED

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace excel2earth
{
    public partial class Simple : Form
    {
        public Main mainForm;

        /// <summary>
        /// Constructs the Simple form.
        /// </summary>
        public Simple()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Closes the Main form when this form is closing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simple_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.mainForm.Close();
        }

        #region Validation
        private void simple_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                this.mainForm.Start_DataRange_checkBox.Checked = true;
                this.mainForm.Start_Snippet_checkBox.Checked = false;
                this.mainForm.Start_MultiGeometry_checkBox.Checked = false;
                this.mainForm.Start_MultiType_comboBox.SelectedIndex = 0;
                this.mainForm.Start_DataType_comboBox.SelectedIndex = 0;
                this.mainForm.Location_Altitude_checkBox.Checked = false;
                this.mainForm.DateTime_None_radioButton.Checked = true;
            }
        }

        private void Start_Name_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                this.mainForm.Start_Name_comboBox.SelectedIndex = this.Start_Name_comboBox.SelectedIndex;
            }
        }

        private void Location_Format_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                this.mainForm.Location_Format_comboBox.SelectedIndex = this.Location_Format_comboBox.SelectedIndex;
            }
        }

        private void Location_Field1_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                this.mainForm.Location_Field1_comboBox.SelectedIndex = this.Location_Field1_comboBox.SelectedIndex;
            }
        }

        private void Location_Field2_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                this.mainForm.Location_Field2_comboBox.SelectedIndex = this.Location_Field2_comboBox.SelectedIndex;
            }
        }
        #endregion

        private void showMain_button_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.mainForm.Show();
        }

        private void save_button_Click(object sender, EventArgs e)
        {
            this.mainForm.Description_AddAll_button_Click(sender, e);
            this.mainForm.Folders_RemoveAll_button_Click(sender, e);
            this.mainForm.Icons_Columns_comboBox.SelectedIndex = -1;
            this.mainForm.main_Finish_button_Click(sender, e);
        }
    }
}

// CLASSIFICATION: UNCLASSIFIED