using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MailClient
{
    public partial class GUI_Option : Form
    {
        public GUI_Option()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAuth.Checked)
            {
                tbUsername.Enabled = true;
                tbPassword.Enabled = true;
                labelUsername.Enabled = true;
                labelPassword.Enabled = true;
            }
            else
            {
                tbUsername.Enabled = false;
                tbPassword.Enabled = false;
                labelUsername.Enabled = false;
                labelPassword.Enabled = false;
            }
        }

        private void GUI_Option_Load(object sender, EventArgs e)
        {
            tbHost.Text = Properties.Settings.Default.smtpHost;
            tbPort.Text = Properties.Settings.Default.smtpPort.ToString();
            tbUsername.Text = Properties.Settings.Default.smtpUsername;
            tbPassword.Text = Properties.Settings.Default.smtpPassword;
            cbAuth.Checked = Properties.Settings.Default.smtpUseAuth;
            tbPopHost.Text = Properties.Settings.Default.popHost;
            tbPopPort.Text = Properties.Settings.Default.popPort.ToString();
            tbPopUsername.Text = Properties.Settings.Default.popUsername;
            tbPopPassword.Text = Properties.Settings.Default.popPassword;
            popUseSSL.Checked = Properties.Settings.Default.popUseSSL;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.smtpHost = tbHost.Text;
            Properties.Settings.Default.smtpPort = Convert.ToInt32(tbPort.Text);
            Properties.Settings.Default.smtpUsername = tbUsername.Text;
            Properties.Settings.Default.smtpPassword = tbPassword.Text;
            Properties.Settings.Default.smtpUseAuth = cbAuth.Checked;
            Properties.Settings.Default.popHost = tbPopHost.Text;
            Properties.Settings.Default.popPort = Convert.ToInt32(tbPopPort.Text);
            Properties.Settings.Default.popUsername = tbPopUsername.Text;
            Properties.Settings.Default.popPassword = tbPopPassword.Text;
            Properties.Settings.Default.popUseSSL = popUseSSL.Checked;

            Properties.Settings.Default.Save();
            this.Close();
        }
    }
}
