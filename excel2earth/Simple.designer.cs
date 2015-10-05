// CLASSIFICATION: UNCLASSIFIED

namespace excel2earth
{
    partial class Simple
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Simple));
            this.Start_Name_label = new System.Windows.Forms.Label();
            this.Start_Name_comboBox = new System.Windows.Forms.ComboBox();
            this.Location_Field2_label = new System.Windows.Forms.Label();
            this.Location_Field1_label = new System.Windows.Forms.Label();
            this.Location_Format_label = new System.Windows.Forms.Label();
            this.Location_Field2_comboBox = new System.Windows.Forms.ComboBox();
            this.Location_Field1_comboBox = new System.Windows.Forms.ComboBox();
            this.Location_Format_comboBox = new System.Windows.Forms.ComboBox();
            this.save_button = new System.Windows.Forms.Button();
            this.showMain_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Start_Name_label
            // 
            this.Start_Name_label.AutoSize = true;
            this.Start_Name_label.Location = new System.Drawing.Point(10, 15);
            this.Start_Name_label.Name = "Start_Name_label";
            this.Start_Name_label.Size = new System.Drawing.Size(38, 13);
            this.Start_Name_label.TabIndex = 0;
            this.Start_Name_label.Text = "Name:";
            // 
            // Start_Name_comboBox
            // 
            this.Start_Name_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Start_Name_comboBox.FormattingEnabled = true;
            this.Start_Name_comboBox.Items.AddRange(new object[] {
            "Decimal Degrees",
            "Degrees Minutes Seconds",
            "Military Grid Reference System"});
            this.Start_Name_comboBox.Location = new System.Drawing.Point(89, 12);
            this.Start_Name_comboBox.Name = "Start_Name_comboBox";
            this.Start_Name_comboBox.Size = new System.Drawing.Size(200, 21);
            this.Start_Name_comboBox.TabIndex = 1;
            this.Start_Name_comboBox.SelectedIndexChanged += new System.EventHandler(this.Start_Name_comboBox_SelectedIndexChanged);
            // 
            // Location_Field2_label
            // 
            this.Location_Field2_label.AutoSize = true;
            this.Location_Field2_label.Location = new System.Drawing.Point(10, 95);
            this.Location_Field2_label.Name = "Location_Field2_label";
            this.Location_Field2_label.Size = new System.Drawing.Size(73, 13);
            this.Location_Field2_label.TabIndex = 0;
            this.Location_Field2_label.Text = "Longitude (X):";
            // 
            // Location_Field1_label
            // 
            this.Location_Field1_label.AutoSize = true;
            this.Location_Field1_label.Location = new System.Drawing.Point(10, 68);
            this.Location_Field1_label.Name = "Location_Field1_label";
            this.Location_Field1_label.Size = new System.Drawing.Size(64, 13);
            this.Location_Field1_label.TabIndex = 0;
            this.Location_Field1_label.Text = "Latitude (Y):";
            // 
            // Location_Format_label
            // 
            this.Location_Format_label.AutoSize = true;
            this.Location_Format_label.Location = new System.Drawing.Point(10, 41);
            this.Location_Format_label.Name = "Location_Format_label";
            this.Location_Format_label.Size = new System.Drawing.Size(42, 13);
            this.Location_Format_label.TabIndex = 0;
            this.Location_Format_label.Text = "Format:";
            // 
            // Location_Field2_comboBox
            // 
            this.Location_Field2_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Location_Field2_comboBox.FormattingEnabled = true;
            this.Location_Field2_comboBox.Location = new System.Drawing.Point(89, 92);
            this.Location_Field2_comboBox.Name = "Location_Field2_comboBox";
            this.Location_Field2_comboBox.Size = new System.Drawing.Size(200, 21);
            this.Location_Field2_comboBox.TabIndex = 4;
            this.Location_Field2_comboBox.SelectedIndexChanged += new System.EventHandler(this.Location_Field2_comboBox_SelectedIndexChanged);
            // 
            // Location_Field1_comboBox
            // 
            this.Location_Field1_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Location_Field1_comboBox.FormattingEnabled = true;
            this.Location_Field1_comboBox.Location = new System.Drawing.Point(89, 65);
            this.Location_Field1_comboBox.Name = "Location_Field1_comboBox";
            this.Location_Field1_comboBox.Size = new System.Drawing.Size(200, 21);
            this.Location_Field1_comboBox.TabIndex = 3;
            this.Location_Field1_comboBox.SelectedIndexChanged += new System.EventHandler(this.Location_Field1_comboBox_SelectedIndexChanged);
            // 
            // Location_Format_comboBox
            // 
            this.Location_Format_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Location_Format_comboBox.FormattingEnabled = true;
            this.Location_Format_comboBox.Items.AddRange(new object[] {
            "Decimal Degrees",
            "Degrees Decimal Minutes",
            "Degrees Minutes Seconds",
            "Military Grid Reference System"});
            this.Location_Format_comboBox.Location = new System.Drawing.Point(89, 38);
            this.Location_Format_comboBox.Name = "Location_Format_comboBox";
            this.Location_Format_comboBox.Size = new System.Drawing.Size(200, 21);
            this.Location_Format_comboBox.TabIndex = 2;
            this.Location_Format_comboBox.SelectedIndexChanged += new System.EventHandler(this.Location_Format_comboBox_SelectedIndexChanged);
            // 
            // save_button
            // 
            this.save_button.Enabled = false;
            this.save_button.Location = new System.Drawing.Point(214, 119);
            this.save_button.Name = "save_button";
            this.save_button.Size = new System.Drawing.Size(75, 23);
            this.save_button.TabIndex = 5;
            this.save_button.Text = "Save";
            this.save_button.UseVisualStyleBackColor = true;
            this.save_button.Click += new System.EventHandler(this.save_button_Click);
            // 
            // showMain_button
            // 
            this.showMain_button.Location = new System.Drawing.Point(89, 119);
            this.showMain_button.Name = "showMain_button";
            this.showMain_button.Size = new System.Drawing.Size(119, 23);
            this.showMain_button.TabIndex = 6;
            this.showMain_button.Text = "Show All Options";
            this.showMain_button.UseVisualStyleBackColor = true;
            this.showMain_button.Click += new System.EventHandler(this.showMain_button_Click);
            // 
            // simple
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(302, 147);
            this.Controls.Add(this.showMain_button);
            this.Controls.Add(this.save_button);
            this.Controls.Add(this.Location_Field2_label);
            this.Controls.Add(this.Location_Field1_label);
            this.Controls.Add(this.Location_Format_label);
            this.Controls.Add(this.Location_Field2_comboBox);
            this.Controls.Add(this.Location_Field1_comboBox);
            this.Controls.Add(this.Location_Format_comboBox);
            this.Controls.Add(this.Start_Name_label);
            this.Controls.Add(this.Start_Name_comboBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "simple";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "excel2earth - Simple  Point Tool";
            this.VisibleChanged += new System.EventHandler(this.simple_VisibleChanged);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.simple_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label Start_Name_label;
        internal System.Windows.Forms.Label Location_Format_label;
        internal System.Windows.Forms.ComboBox Start_Name_comboBox;
        internal System.Windows.Forms.ComboBox Location_Field2_comboBox;
        internal System.Windows.Forms.ComboBox Location_Field1_comboBox;
        internal System.Windows.Forms.ComboBox Location_Format_comboBox;
        internal System.Windows.Forms.Button save_button;
        internal System.Windows.Forms.Label Location_Field1_label;
        internal System.Windows.Forms.Label Location_Field2_label;
        private System.Windows.Forms.Button showMain_button;

    }
}

// CLASSIFICATION: UNCLASSIFIED