namespace Driving_License_Management_System.Detian_License
{
    partial class frmDetianLicense
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
            this.ctrllFindLocalDrivingLicense1 = new Driving_License_Management_System.License.Controls.ctrllFindLocalDrivingLicense();
            this.gbDetianLicensenfo = new System.Windows.Forms.GroupBox();
            this.txtFineFees = new System.Windows.Forms.TextBox();
            this.lblLicenseID = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCreatedBy = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblDetainDate = new System.Windows.Forms.Label();
            this.lblDetainID = new System.Windows.Forms.Label();
            this.label109 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblShowLicenseInfo = new System.Windows.Forms.LinkLabel();
            this.lblShowLicenseHistory = new System.Windows.Forms.LinkLabel();
            this.btnClose = new Guna.UI2.WinForms.Guna2Button();
            this.btnDetianLicense = new Guna.UI2.WinForms.Guna2Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.lblTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.gbDetianLicensenfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // ctrllFindLocalDrivingLicense1
            // 
            this.ctrllFindLocalDrivingLicense1.AsignLicenseID = "";
            this.ctrllFindLocalDrivingLicense1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(250)))));
            this.ctrllFindLocalDrivingLicense1.FilterEnabled = true;
            this.ctrllFindLocalDrivingLicense1.IsRenewMode = false;
            this.ctrllFindLocalDrivingLicense1.Location = new System.Drawing.Point(12, 88);
            this.ctrllFindLocalDrivingLicense1.Name = "ctrllFindLocalDrivingLicense1";
            this.ctrllFindLocalDrivingLicense1.Size = new System.Drawing.Size(1143, 514);
            this.ctrllFindLocalDrivingLicense1.TabIndex = 0;
            this.ctrllFindLocalDrivingLicense1.OnLicensenSelected += new System.Action<int, bool>(this.ctrllFindLocalDrivingLicense1_OnLicensenSelected);
            // 
            // gbDetianLicensenfo
            // 
            this.gbDetianLicensenfo.Controls.Add(this.txtFineFees);
            this.gbDetianLicensenfo.Controls.Add(this.lblLicenseID);
            this.gbDetianLicensenfo.Controls.Add(this.label8);
            this.gbDetianLicensenfo.Controls.Add(this.label3);
            this.gbDetianLicensenfo.Controls.Add(this.lblCreatedBy);
            this.gbDetianLicensenfo.Controls.Add(this.label12);
            this.gbDetianLicensenfo.Controls.Add(this.lblDetainDate);
            this.gbDetianLicensenfo.Controls.Add(this.lblDetainID);
            this.gbDetianLicensenfo.Controls.Add(this.label109);
            this.gbDetianLicensenfo.Controls.Add(this.label2);
            this.gbDetianLicensenfo.Location = new System.Drawing.Point(74, 608);
            this.gbDetianLicensenfo.Name = "gbDetianLicensenfo";
            this.gbDetianLicensenfo.Size = new System.Drawing.Size(992, 172);
            this.gbDetianLicensenfo.TabIndex = 95;
            this.gbDetianLicensenfo.TabStop = false;
            this.gbDetianLicensenfo.Text = "Detian Info";
            // 
            // txtFineFees
            // 
            this.txtFineFees.Location = new System.Drawing.Point(255, 132);
            this.txtFineFees.Name = "txtFineFees";
            this.txtFineFees.Size = new System.Drawing.Size(68, 20);
            this.txtFineFees.TabIndex = 79;
            this.txtFineFees.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFineFees_KeyPress);
            this.txtFineFees.Validating += new System.ComponentModel.CancelEventHandler(this.txtFineFees_Validating);
            // 
            // lblLicenseID
            // 
            this.lblLicenseID.AutoSize = true;
            this.lblLicenseID.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLicenseID.Location = new System.Drawing.Point(819, 56);
            this.lblLicenseID.Name = "lblLicenseID";
            this.lblLicenseID.Size = new System.Drawing.Size(33, 19);
            this.lblLicenseID.TabIndex = 78;
            this.lblLicenseID.Text = "???";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Image = global::Driving_License_Management_System.Properties.Resources.License_View_32;
            this.label8.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label8.Location = new System.Drawing.Point(613, 52);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(186, 26);
            this.label8.TabIndex = 77;
            this.label8.Text = "License ID:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Image = global::Driving_License_Management_System.Properties.Resources.money_32;
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label3.Location = new System.Drawing.Point(23, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(217, 31);
            this.label3.TabIndex = 71;
            this.label3.Text = "Fine Fees:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCreatedBy
            // 
            this.lblCreatedBy.AutoSize = true;
            this.lblCreatedBy.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreatedBy.Location = new System.Drawing.Point(819, 100);
            this.lblCreatedBy.Name = "lblCreatedBy";
            this.lblCreatedBy.Size = new System.Drawing.Size(33, 19);
            this.lblCreatedBy.TabIndex = 68;
            this.lblCreatedBy.Text = "???";
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Image = global::Driving_License_Management_System.Properties.Resources.User_32__2;
            this.label12.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label12.Location = new System.Drawing.Point(613, 96);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(186, 26);
            this.label12.TabIndex = 64;
            this.label12.Text = "Created By:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDetainDate
            // 
            this.lblDetainDate.AutoSize = true;
            this.lblDetainDate.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetainDate.Location = new System.Drawing.Point(267, 93);
            this.lblDetainDate.Name = "lblDetainDate";
            this.lblDetainDate.Size = new System.Drawing.Size(33, 19);
            this.lblDetainDate.TabIndex = 58;
            this.lblDetainDate.Text = "???";
            // 
            // lblDetainID
            // 
            this.lblDetainID.AutoSize = true;
            this.lblDetainID.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetainID.Location = new System.Drawing.Point(267, 56);
            this.lblDetainID.Name = "lblDetainID";
            this.lblDetainID.Size = new System.Drawing.Size(33, 19);
            this.lblDetainID.TabIndex = 56;
            this.lblDetainID.Text = "???";
            // 
            // label109
            // 
            this.label109.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label109.ForeColor = System.Drawing.Color.Black;
            this.label109.Image = global::Driving_License_Management_System.Properties.Resources.Number_32;
            this.label109.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label109.Location = new System.Drawing.Point(22, 44);
            this.label109.Name = "label109";
            this.label109.Size = new System.Drawing.Size(218, 42);
            this.label109.TabIndex = 55;
            this.label109.Text = "Detain ID";
            this.label109.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Image = global::Driving_License_Management_System.Properties.Resources.Calendar_32;
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label2.Location = new System.Drawing.Point(23, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(217, 26);
            this.label2.TabIndex = 51;
            this.label2.Text = "Detain Date";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblShowLicenseInfo
            // 
            this.lblShowLicenseInfo.AutoSize = true;
            this.lblShowLicenseInfo.Enabled = false;
            this.lblShowLicenseInfo.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShowLicenseInfo.Location = new System.Drawing.Point(245, 786);
            this.lblShowLicenseInfo.Name = "lblShowLicenseInfo";
            this.lblShowLicenseInfo.Size = new System.Drawing.Size(138, 19);
            this.lblShowLicenseInfo.TabIndex = 161;
            this.lblShowLicenseInfo.TabStop = true;
            this.lblShowLicenseInfo.Text = "Show License Info";
            this.lblShowLicenseInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblShowLicenseInfo_LinkClicked);
            // 
            // lblShowLicenseHistory
            // 
            this.lblShowLicenseHistory.AutoSize = true;
            this.lblShowLicenseHistory.Enabled = false;
            this.lblShowLicenseHistory.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShowLicenseHistory.Location = new System.Drawing.Point(70, 786);
            this.lblShowLicenseHistory.Name = "lblShowLicenseHistory";
            this.lblShowLicenseHistory.Size = new System.Drawing.Size(159, 19);
            this.lblShowLicenseHistory.TabIndex = 160;
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
            this.btnClose.Location = new System.Drawing.Point(795, 786);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(101, 42);
            this.btnClose.TabIndex = 159;
            this.btnClose.Text = "Close";
            this.btnClose.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.btnClose.UseTransparentBackground = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnDetianLicense
            // 
            this.btnDetianLicense.Animated = true;
            this.btnDetianLicense.AnimatedGIF = true;
            this.btnDetianLicense.AutoRoundedCorners = true;
            this.btnDetianLicense.BackColor = System.Drawing.Color.Transparent;
            this.btnDetianLicense.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(65)))));
            this.btnDetianLicense.BorderThickness = 1;
            this.btnDetianLicense.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnDetianLicense.CheckedState.ForeColor = System.Drawing.Color.White;
            this.btnDetianLicense.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDetianLicense.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDetianLicense.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDetianLicense.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnDetianLicense.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(42)))), ((int)(((byte)(57)))));
            this.btnDetianLicense.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnDetianLicense.ForeColor = System.Drawing.Color.White;
            this.btnDetianLicense.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(56)))), ((int)(((byte)(76)))));
            this.btnDetianLicense.HoverState.ForeColor = System.Drawing.Color.White;
            this.btnDetianLicense.Image = global::Driving_License_Management_System.Properties.Resources.Detain_32;
            this.btnDetianLicense.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnDetianLicense.ImageOffset = new System.Drawing.Point(1, 0);
            this.btnDetianLicense.ImageSize = new System.Drawing.Size(32, 32);
            this.btnDetianLicense.Location = new System.Drawing.Point(902, 786);
            this.btnDetianLicense.Name = "btnDetianLicense";
            this.btnDetianLicense.Size = new System.Drawing.Size(164, 42);
            this.btnDetianLicense.TabIndex = 158;
            this.btnDetianLicense.Text = "Detian License";
            this.btnDetianLicense.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.btnDetianLicense.UseTransparentBackground = true;
            this.btnDetianLicense.Click += new System.EventHandler(this.btnDetianLicense_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(442, 35);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(238, 47);
            this.lblTitle.TabIndex = 162;
            this.lblTitle.Text = "Detain License";
            // 
            // frmDetianLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1197, 859);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblShowLicenseInfo);
            this.Controls.Add(this.lblShowLicenseHistory);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnDetianLicense);
            this.Controls.Add(this.gbDetianLicensenfo);
            this.Controls.Add(this.ctrllFindLocalDrivingLicense1);
            this.Name = "frmDetianLicense";
            this.Text = "Detian License";
            this.Load += new System.EventHandler(this.frmDetianLicense_Load);
            this.gbDetianLicensenfo.ResumeLayout(false);
            this.gbDetianLicensenfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private License.Controls.ctrllFindLocalDrivingLicense ctrllFindLocalDrivingLicense1;
        private System.Windows.Forms.GroupBox gbDetianLicensenfo;
        private System.Windows.Forms.TextBox txtFineFees;
        private System.Windows.Forms.Label lblLicenseID;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblCreatedBy;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblDetainDate;
        private System.Windows.Forms.Label lblDetainID;
        private System.Windows.Forms.Label label109;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel lblShowLicenseInfo;
        private System.Windows.Forms.LinkLabel lblShowLicenseHistory;
        private Guna.UI2.WinForms.Guna2Button btnClose;
        private Guna.UI2.WinForms.Guna2Button btnDetianLicense;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTitle;
    }
}