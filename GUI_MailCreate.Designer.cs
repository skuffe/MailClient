namespace MailClient
{
    partial class GUI_MailCreate
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUI_MailCreate));
            this.rtBody = new System.Windows.Forms.RichTextBox();
            this.buttonSend = new System.Windows.Forms.Button();
            this.tbRecipient = new System.Windows.Forms.TextBox();
            this.tbCc = new System.Windows.Forms.TextBox();
            this.buttonTo = new System.Windows.Forms.Button();
            this.buttonCc = new System.Windows.Forms.Button();
            this.Subject_Label = new System.Windows.Forms.Label();
            this.tbSubject = new System.Windows.Forms.TextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // rtBody
            // 
            this.rtBody.Location = new System.Drawing.Point(12, 93);
            this.rtBody.Name = "rtBody";
            this.rtBody.Size = new System.Drawing.Size(832, 396);
            this.rtBody.TabIndex = 0;
            this.rtBody.Text = "";
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(13, 14);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(65, 73);
            this.buttonSend.TabIndex = 2;
            this.buttonSend.Text = "Send";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // tbRecipient
            // 
            this.tbRecipient.Location = new System.Drawing.Point(155, 14);
            this.tbRecipient.Name = "tbRecipient";
            this.tbRecipient.Size = new System.Drawing.Size(689, 20);
            this.tbRecipient.TabIndex = 3;
            this.tbRecipient.Validated += new System.EventHandler(this.tbRecipient_Validated);
            // 
            // tbCc
            // 
            this.tbCc.Location = new System.Drawing.Point(155, 41);
            this.tbCc.Name = "tbCc";
            this.tbCc.Size = new System.Drawing.Size(689, 20);
            this.tbCc.TabIndex = 4;
            // 
            // buttonTo
            // 
            this.buttonTo.Location = new System.Drawing.Point(85, 14);
            this.buttonTo.Name = "buttonTo";
            this.buttonTo.Size = new System.Drawing.Size(64, 23);
            this.buttonTo.TabIndex = 5;
            this.buttonTo.Text = "Modtager";
            this.buttonTo.UseVisualStyleBackColor = true;
            // 
            // buttonCc
            // 
            this.buttonCc.Location = new System.Drawing.Point(85, 41);
            this.buttonCc.Name = "buttonCc";
            this.buttonCc.Size = new System.Drawing.Size(64, 23);
            this.buttonCc.TabIndex = 6;
            this.buttonCc.Text = "Cc";
            this.buttonCc.UseVisualStyleBackColor = true;
            // 
            // Subject_Label
            // 
            this.Subject_Label.AutoSize = true;
            this.Subject_Label.Location = new System.Drawing.Point(84, 70);
            this.Subject_Label.Name = "Subject_Label";
            this.Subject_Label.Size = new System.Drawing.Size(37, 13);
            this.Subject_Label.TabIndex = 7;
            this.Subject_Label.Text = "Emne:";
            // 
            // tbSubject
            // 
            this.tbSubject.Location = new System.Drawing.Point(155, 67);
            this.tbSubject.Name = "tbSubject";
            this.tbSubject.Size = new System.Drawing.Size(689, 20);
            this.tbSubject.TabIndex = 8;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // GUI_MailCreate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(872, 501);
            this.Controls.Add(this.tbSubject);
            this.Controls.Add(this.Subject_Label);
            this.Controls.Add(this.buttonCc);
            this.Controls.Add(this.buttonTo);
            this.Controls.Add(this.tbCc);
            this.Controls.Add(this.tbRecipient);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.rtBody);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GUI_MailCreate";
            this.Text = "New Email";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtBody;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.TextBox tbRecipient;
        private System.Windows.Forms.TextBox tbCc;
        private System.Windows.Forms.Button buttonTo;
        private System.Windows.Forms.Button buttonCc;
        private System.Windows.Forms.Label Subject_Label;
        private System.Windows.Forms.TextBox tbSubject;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}