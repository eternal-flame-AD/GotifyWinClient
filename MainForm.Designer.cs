namespace GotifyWinClient
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.NotificationIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.UrlInput = new System.Windows.Forms.TextBox();
            this.ConnectBtn = new System.Windows.Forms.Button();
            this.UrlInputLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // NotificationIcon
            // 
            this.NotificationIcon.Text = "Gotify";
            this.NotificationIcon.Visible = true;
            // 
            // UrlInput
            // 
            this.UrlInput.Location = new System.Drawing.Point(75, 168);
            this.UrlInput.Name = "UrlInput";
            this.UrlInput.Size = new System.Drawing.Size(545, 31);
            this.UrlInput.TabIndex = 0;
            // 
            // ConnectBtn
            // 
            this.ConnectBtn.Location = new System.Drawing.Point(216, 266);
            this.ConnectBtn.Name = "ConnectBtn";
            this.ConnectBtn.Size = new System.Drawing.Size(215, 79);
            this.ConnectBtn.TabIndex = 1;
            this.ConnectBtn.Text = "Connect";
            this.ConnectBtn.UseVisualStyleBackColor = true;
            // 
            // UrlInputLabel
            // 
            this.UrlInputLabel.AutoSize = true;
            this.UrlInputLabel.Location = new System.Drawing.Point(266, 111);
            this.UrlInputLabel.Name = "UrlInputLabel";
            this.UrlInputLabel.Size = new System.Drawing.Size(128, 25);
            this.UrlInputLabel.TabIndex = 2;
            this.UrlInputLabel.Text = "Stream URL";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 473);
            this.Controls.Add(this.UrlInputLabel);
            this.Controls.Add(this.ConnectBtn);
            this.Controls.Add(this.UrlInput);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Gotify";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon NotificationIcon;
        private System.Windows.Forms.TextBox UrlInput;
        private System.Windows.Forms.Button ConnectBtn;
        private System.Windows.Forms.Label UrlInputLabel;
    }
}

