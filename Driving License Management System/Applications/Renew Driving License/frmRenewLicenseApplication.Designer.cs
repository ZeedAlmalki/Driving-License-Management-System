namespace Driving_License_Management_System.License
{
    partial class frmRenewLicenseApplication
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
            this.lblShowLicenseInfo = new System.Windows.Forms.LinkLabel();
            this.lblShowLicenseHistory = new System.Windows.Forms.LinkLabel();
            this.btnClose = new Guna.UI2.WinForms.Guna2Button();
            this.btnRenew = new Guna.UI2.WinForms.Guna2Button();
            this.ctrllFindLocalDrivingLicense1 = new Driving_License_Management_System.License.Controls.ctrllFindLocalDrivingLicense();
            this.ctrlApplicationNewLicenseInfo1 = new Driving_License_Management_System.License.Controls.ctrlApplicationNewLicenseInfo();
            this.SuspendLayout();
            // 
            // lblShowLicenseInfo
            // 
            this.lblShowLicenseInfo.AutoSize = true;
            this.lblShowLicenseInfo.Enabled = false;
            this.lblShowLicenseInfo.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShowLicenseInfo.Location = new System.Drawing.Point(266, 973);
            this.lblShowLicenseInfo.Name = "lblShowLicenseInfo";
            this.lblShowLicenseInfo.Size = new System.Drawing.Size(138, 19);
            this.lblShowLicenseInfo.TabIndex = 153;
            this.lblShowLicenseInfo.TabStop = true;
            this.lblShowLicenseInfo.Text = "Show License Info";
            this.lblShowLicenseInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblShowLicenseInfo_LinkClicked);
            // 
            // lblShowLicenseHistory
            // 
            this.lblShowLicenseHistory.AutoSize = true;
            this.lblShowLicenseHistory.Enabled = false;
            this.lblShowLicenseHistory.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShowLicenseHistory.Location = new System.Drawing.Point(91, 973);
            this.lblShowLicenseHistory.Name = "lblShowLicenseHistory";
            this.lblShowLicenseHistory.Size = new System.Drawing.Size(159, 19);
            this.lblShowLicenseHistory.TabIndex = 152;
            this.lblShowLicenseHistory.TabStop = true;
            this.lblShowLicenseHistory.Text = "Show License History";
            this.lblShowLicenseHistory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblShowLicenseHistory_LinkClicked);
            // 
            // btnClose
            // 
            this.btnClose.Animated = true;
            this.btnClose.AnimatedGIF = true;
            this.btnClose.AutoRoundedCorners = true;
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(65)))));
            this.btnClose.BorderThickness = 1;
            this.btnClose.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnClose.CheckedState.ForeColor = System.Drawing.Color.White;
            this.btnClose.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnClose.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnClose.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnClose.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnClose.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(42)))), ((int)(((byte)(57)))));
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(56)))), ((int)(((byte)(76)))));
            this.btnClose.HoverState.ForeColor = System.Drawing.Color.White;
            this.btnClose.Image = global::Driving_License_Management_System.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnClose.ImageOffset = new System.Drawing.Point(1, 0);
            this.btnClose.ImageSize = new System.Drawing.Size(32, 32);
            this.btnClose.Location = new System.Drawing.Point(778, 973);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(115, 42);
            this.btnClose.TabIndex = 151;
            this.btnClose.Text = "Close";
            this.btnClose.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.btnClose.UseTransparentBackground = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnRenew
            // 
            this.btnRenew.Animated = true;
            this.btnRenew.AnimatedGIF = true;
            this.btnRenew.AutoRoundedCorners = true;
            this.btnRenew.BackColor = System.Drawing.Color.Transparent;
            this.btnRenew.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(65)))));
            this.btnRenew.BorderThickness = 1;
            this.btnRenew.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnRenew.CheckedState.ForeColor = System.Drawing.Color.White;
            this.btnRenew.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnRenew.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnRenew.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnRenew.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnRenew.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(42)))), ((int)(((byte)(57)))));
            this.btnRenew.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnRenew.ForeColor = System.Drawing.Color.White;
            this.btnRenew.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(56)))), ((int)(((byte)(76)))));
            this.btnRenew.HoverState.ForeColor = System.Drawing.Color.White;
            this.btnRenew.Image = global::Driving_License_Management_System.Properties.Resources.IssueDrivingLicense_32;
            this.btnRenew.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnRenew.ImageOffset = new System.Drawing.Point(1, 0);
            this.btnRenew.ImageSize = new System.Drawing.Size(32, 32);
            this.btnRenew.Location = new System.Drawing.Point(899, 973);
            this.btnRenew.Name = "btnRenew";
            this.btnRenew.Size = new System.Drawing.Size(115, 42);
            this.btnRenew.TabIndex = 150;
            this.btnRenew.Text = "Renew";
            this.btnRenew.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.btnRenew.UseTransparentBackground = true;
            this.btnRenew.Click += new System.EventHandler(this.btnRenew_Click);
            // 
            // ctrllFindLocalDrivingLicense1
            // 
            this.ctrllFindLocalDrivingLicense1.AsignLicenseID = "";
            this.ctrllFindLocalDrivingLicense1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(250)))));
            this.ctrllFindLocalDrivingLicense1.FilterEnabled = true;
            this.ctrllFindLocalDrivingLicense1.IsRenewMode = false;
            this.ctrllFindLocalDrivingLicense1.Location = new System.Drawing.Point(22, 12);
            this.ctrllFindLocalDrivingLicense1.Name = "ctrllFindLocalDrivingLicense1";
            this.ctrllFindLocalDrivingLicense1.Size = new System.Drawing.Size(1133, 518);
            this.ctrllFindLocalDrivingLicense1.TabIndex = 1;
            this.ctrllFindLocalDrivingLicense1.OnLicensenSelected += new System.Action<int, bool>(this.ctrllFindLocalDrivingLicense1_OnLicensenSelected);
            // 
            // ctrlApplicationNewLicenseInfo1
            // 
            this.ctrlApplicationNewLicenseInfo1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(250)))));
            this.ctrlApplicationNewLicenseInfo1.Location = new System.Drawing.Point(76, 520);
            this.ctrlApplicationNewLicenseInfo1.Name = "ctrlApplicationNewLicenseInfo1";
            this.ctrlApplicationNewLicenseInfo1.NewLicenseID = "???";
            this.ctrlApplicationNewLicenseInfo1.RenewApplicationID = "???";
            this.ctrlApplicationNewLicenseInfo1.Size = new System.Drawing.Size(966, 447);
            this.ctrlApplicationNewLicenseInfo1.TabIndex = 0;
            // 
            // frmRenewLicenseApplication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1180, 1060);
            this.Controls.Add(this.lblShowLicenseInfo);
            this.Controls.Add(this.lblShowLicenseHistory);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnRenew);
            this.Controls.Add(this.ctrllFindLocalDrivingLicense1);
            this.Controls.Add(this.ctrlApplicationNewLicenseInfo1);
            this.Name = "frmRenewLicenseApplication";
            this.Text = "Renew License Application";
            this.Load += new System.EventHandler(this.frmRenewLicenseApplication_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.ctrlApplicationNewLicenseInfo ctrlApplicationNewLicenseInfo1;
        private Controls.ctrllFindLocalDrivingLicense ctrllFindLocalDrivingLicense1;
        private System.Windows.Forms.LinkLabel lblShowLicenseInfo;
        private System.Windows.Forms.LinkLabel lblShowLicenseHistory;
        private Guna.UI2.WinForms.Guna2Button btnClose;
        private Guna.UI2.WinForms.Guna2Button btnRenew;
    }
}