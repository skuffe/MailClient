using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;

namespace MailClient
{
    public partial class GUI_MailCreate : Form
    {
        public GUI_MailCreate()
        {
            InitializeComponent();
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            string errorMsg;
            if (!ValidEmailAddress(tbRecipient.Text, out errorMsg))
            {
                tbRecipient.Select(0, tbRecipient.Text.Length);

                // Set the ErrorProvider error with the text to display. 
                this.errorProvider1.SetError(tbRecipient, errorMsg);
            }
            //if (!ValidEmailAddress(tbCc.Text, out errorMsg))
            //{
            //    tbCc.Select(0, tbCc.Text.Length);

            //    // Set the ErrorProvider error with the text to display. 
            //    this.errorProvider1.SetError(tbCc, errorMsg);
            //}
            else
            {
                // Create SMTP Client.
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = Properties.Settings.Default.smtpHost;
                smtpClient.Port = Properties.Settings.Default.smtpPort;
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new System.Net.NetworkCredential(Properties.Settings.Default.smtpUsername, Properties.Settings.Default.smtpPassword);

                // Create Mailadresses and construct message
                MailAddress to = new MailAddress(tbRecipient.Text);
                MailAddress from = new MailAddress(Properties.Settings.Default.smtpUsername);
                string subject = tbSubject.Text;
                string body = rtBody.Text;

                MailMessage message = new MailMessage(from, to);
                message.Body = body;
                message.Subject = subject;

                // Add cc address only if input
                if (tbCc.TextLength != 0)
                {
                    MailAddress cc = new MailAddress(tbCc.Text);
                    message.CC.Add(cc);
                }

                try
                {
                    smtpClient.Send(message);
                    this.Close();
                }

                catch (Exception ex)
                {
                    MessageBox.Show("Exception caught in CreateTestMessage1(): {0}",
                          ex.ToString());
                }    
            }
        }

        private void tbRecipient_Validated(object sender, System.EventArgs e)
        {
            // If all conditions have been met, clear the ErrorProvider of errors.
            errorProvider1.SetError(tbRecipient, "");
        }
        public bool ValidEmailAddress(string emailAddress, out string errorMessage)
        {
            // Confirm that the e-mail address string is not empty.
            //if (emailAddress.Length == 0)
            //{
            //    errorMessage = "e-mail address is required.";
            //    return false;
            //}

            // Confirm that there is an "@" and a "." in the e-mail address, and in the correct order.
            if (emailAddress.IndexOf("@") > -1)
            {
                if (emailAddress.IndexOf(".", emailAddress.IndexOf("@")) > emailAddress.IndexOf("@"))
                {
                    errorMessage = "";
                    return true;
                }
            }

            errorMessage = "e-mail address must be valid e-mail address format.\n" +
               "For example 'someone@example.com' ";
            return false;
        }
    }
}
