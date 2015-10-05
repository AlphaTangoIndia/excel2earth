// CLASSIFICATION: UNCLASSIFIED

using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace excel2earth
{
    public partial class About : Form
    {
        public About()
        {
            this.InitializeComponent();
        }

        private void Close_button_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Intelink_U_linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(this.Intelink_U_linkLabel.Text);
        }

        private void Intelink_S_linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(this.Intelink_S_linkLabel.Text);
        }

        private void Intelink_TS_linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(this.Intelink_TS_linkLabel.Text);
        }
    }
}

// CLASSIFICATION: UNCLASSIFIED