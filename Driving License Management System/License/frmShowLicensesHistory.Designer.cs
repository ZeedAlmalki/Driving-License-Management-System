namespace Driving_License_Management_System.License
{
    partial class frmShowLicensesHistory
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.gbDriverLicenses = new System.Windows.Forms.GroupBox();
            this.lblTotalRecords = new System.Windows.Forms.Label();
            this.tcDriverLicenses = new System.Windows.Forms.TabControl();
            this.tpLocal = new System.Windows.Forms.TabPage();
            this.LocalDriverLicenseDataGridView = new Guna.UI2.WinForms.Guna2DataGridView();
            this.label109 = new System.Windows.Forms.Label();
            this.tpInternational = new System.Windows.Forms.TabPage();
            this.InternationalDriverLicenseDataGridView = new Guna.UI2.WinForms.Guna2DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.ctrlPersonCardWithFilter1 = new Driving_License_Management_System.People.ctrlPersonCardWithFilter();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).BeginInit();
            this.gbDriverLicenses.SuspendLayout();
            this.tcDriverLicenses.SuspendLayout();
            this.tpLocal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LocalDriverLicenseDataGridView)).BeginInit();
            this.tpInternational.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.InternationalDriverLicenseDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(606, 14);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(247, 47);
            this.lblTitle.TabIndex = 9;
            this.lblTitle.Text = "License History";
            // 
            // guna2PictureBox1
            // 
            this.guna2PictureBox1.BorderRadius = 2;
            this.guna2PictureBox1.Image = global::Driving_License_Management_System.Properties.Resources.PersonLicenseHistory_512;
            this.guna2PictureBox1.ImageRotate = 0F;
            this.guna2PictureBox1.Location = new System.Drawing.Point(12, 187);
            this.guna2PictureBox1.Name = "guna2PictureBox1";
            this.guna2PictureBox1.Size = new System.Drawing.Size(200, 173);
            this.guna2PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2PictureBox1.TabIndex = 8;
            this.guna2PictureBox1.TabStop = false;
            // 
            // gbDriverLicenses
            // 
            this.gbDriverLicenses.Controls.Add(this.lblTotalRecords);
            this.gbDriverLicenses.Controls.Add(this.tcDriverLicenses);
            this.gbDriverLicenses.Controls.Add(this.label13);
            this.gbDriverLicenses.Font = new System.Drawing.Font("Tahoma", 10F);
            this.gbDriverLicenses.Location = new System.Drawing.Point(12, 493);
            this.gbDriverLicenses.Name = "gbDriverLicenses";
            this.gbDriverLicenses.Size = new System.Drawing.Size(1289, 306);
            this.gbDriverLicenses.TabIndex = 81;
            this.gbDriverLicenses.TabStop = false;
            this.gbDriverLicenses.Text = "Driver Licenses";
            // 
            // lblTotalRecords
            // 
            this.lblTotalRecords.AutoSize = true;
            this.lblTotalRecords.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalRecords.ForeColor = System.Drawing.Color.Black;
            this.lblTotalRecords.Location = new System.Drawing.Point(141, 277);
            this.lblTotalRecords.Name = "lblTotalRecords";
            this.lblTotalRecords.Size = new System.Drawing.Size(18, 19);
            this.lblTotalRecords.TabIndex = 79;
            this.lblTotalRecords.Text = "?";
            // 
            // tcDriverLicenses
            // 
            this.tcDriverLicenses.Controls.Add(this.tpLocal);
            this.tcDriverLicenses.Controls.Add(this.tpInternational);
            this.tcDriverLicenses.Location = new System.Drawing.Point(18, 23);
            this.tcDriverLicenses.Name = "tcDriverLicenses";
            this.tcDriverLicenses.SelectedIndex = 0;
            this.tcDriverLicenses.Size = new System.Drawing.Size(1265, 244);
            this.tcDriverLicenses.TabIndex = 81;
            this.tcDriverLicenses.Selected += new System.Windows.Forms.TabControlEventHandler(this.tcDriverLicenses_Selected);
            // 
            // tpLocal
            // 
            this.tpLocal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(250)))));
            this.tpLocal.Controls.Add(this.LocalDriverLicenseDataGridView);
            this.tpLocal.Controls.Add(this.label109);
            this.tpLocal.Location = new System.Drawing.Point(4, 25);
            this.tpLocal.Name = "tpLocal";
            this.tpLocal.Padding = new System.Windows.Forms.Padding(3);
            this.tpLocal.Size = new System.Drawing.Size(1257, 215);
            this.tpLocal.TabIndex = 0;
            this.tpLocal.Text = "Local";
            // 
            // LocalDriverLicenseDataGridView
            // 
            this.LocalDriverLicenseDataGridView.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(58)))), ((int)(((byte)(82)))));
            this.LocalDriverLicenseDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.LocalDriverLicenseDataGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(42)))), ((int)(((byte)(57)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.LocalDriverLicenseDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.LocalDriverLicenseDataGridView.ColumnHeadersHeight = 40;
            this.LocalDriverLicenseDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(54)))), ((int)(((byte)(75)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 10F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.LocalDriverLicenseDataGridView.DefaultCellStyle = dataGridViewCellStyle3;
            this.LocalDriverLicenseDataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(60)))), ((int)(((byte)(80)))));
            this.LocalDriverLicenseDataGridView.Location = new System.Drawing.Point(10, 42);
            this.LocalDriverLicenseDataGridView.Margin = new System.Windows.Forms.Padding(10);
            this.LocalDriverLicenseDataGridView.Name = "LocalDriverLicenseDataGridView";
            this.LocalDriverLicenseDataGridView.ReadOnly = true;
            this.LocalDriverLicenseDataGridView.RowHeadersVisible = false;
            this.LocalDriverLicenseDataGridView.RowTemplate.Height = 35;
            this.LocalDriverLicenseDataGridView.Size = new System.Drawing.Size(1234, 160);
            this.LocalDriverLicenseDataGridView.TabIndex = 78;
            this.LocalDriverLicenseDataGridView.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.LocalDriverLicenseDataGridView.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.LocalDriverLicenseDataGridView.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.LocalDriverLicenseDataGridView.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.LocalDriverLicenseDataGridView.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.LocalDriverLicenseDataGridView.ThemeStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.LocalDriverLicenseDataGridView.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(60)))), ((int)(((byte)(80)))));
            this.LocalDriverLicenseDataGridView.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.LocalDriverLicenseDataGridView.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.LocalDriverLicenseDataGridView.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Tahoma", 8F);
            this.LocalDriverLicenseDataGridView.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.LocalDriverLicenseDataGridView.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.LocalDriverLicenseDataGridView.ThemeStyle.HeaderStyle.Height = 40;
            this.LocalDriverLicenseDataGridView.ThemeStyle.ReadOnly = true;
            this.LocalDriverLicenseDataGridView.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.LocalDriverLicenseDataGridView.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.LocalDriverLicenseDataGridView.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.LocalDriverLicenseDataGridView.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.White;
            this.LocalDriverLicenseDataGridView.ThemeStyle.RowsStyle.Height = 35;
            this.LocalDriverLicenseDataGridView.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.LocalDriverLicenseDataGridView.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.White;
            // 
            // label109
            // 
            this.label109.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label109.ForeColor = System.Drawing.Color.Black;
            this.label109.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label109.Location = new System.Drawing.Point(6, 3);
            this.label109.Name = "label109";
            this.label109.Size = new System.Drawing.Size(198, 42);
            this.label109.TabIndex = 59;
            this.label109.Text = "Local Licenses History:";
            this.label109.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tpInternational
            // 
            this.tpInternational.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(250)))));
            this.tpInternational.Controls.Add(this.InternationalDriverLicenseDataGridView);
            this.tpInternational.Controls.Add(this.label1);
            this.tpInternational.Location = new System.Drawing.Point(4, 25);
            this.tpInternational.Name = "tpInternational";
            this.tpInternational.Padding = new System.Windows.Forms.Padding(3);
            this.tpInternational.Size = new System.Drawing.Size(1257, 215);
            this.tpInternational.TabIndex = 1;
            this.tpInternational.Text = "International";
            // 
            // InternationalDriverLicenseDataGridView
            // 
            this.InternationalDriverLicenseDataGridView.AllowUserToAddRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(58)))), ((int)(((byte)(82)))));
            this.InternationalDriverLicenseDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.InternationalDriverLicenseDataGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(42)))), ((int)(((byte)(57)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 10F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.InternationalDriverLicenseDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.InternationalDriverLicenseDataGridView.ColumnHeadersHeight = 40;
            this.InternationalDriverLicenseDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(54)))), ((int)(((byte)(75)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Tahoma", 10F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.InternationalDriverLicenseDataGridView.DefaultCellStyle = dataGridViewCellStyle6;
            this.InternationalDriverLicenseDataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(60)))), ((int)(((byte)(80)))));
            this.InternationalDriverLicenseDataGridView.Location = new System.Drawing.Point(10, 42);
            this.InternationalDriverLicenseDataGridView.Margin = new System.Windows.Forms.Padding(10);
            this.InternationalDriverLicenseDataGridView.Name = "InternationalDriverLicenseDataGridView";
            this.InternationalDriverLicenseDataGridView.ReadOnly = true;
            this.InternationalDriverLicenseDataGridView.RowHeadersVisible = false;
            this.InternationalDriverLicenseDataGridView.RowTemplate.Height = 35;
            this.InternationalDriverLicenseDataGridView.Size = new System.Drawing.Size(1234, 160);
            this.InternationalDriverLicenseDataGridView.TabIndex = 81;
            this.InternationalDriverLicenseDataGridView.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.InternationalDriverLicenseDataGridView.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.InternationalDriverLicenseDataGridView.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.InternationalDriverLicenseDataGridView.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.InternationalDriverLicenseDataGridView.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.InternationalDriverLicenseDataGridView.ThemeStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.InternationalDriverLicenseDataGridView.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(60)))), ((int)(((byte)(80)))));
            this.InternationalDriverLicenseDataGridView.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.InternationalDriverLicenseDataGridView.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.InternationalDriverLicenseDataGridView.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Tahoma", 8F);
            this.InternationalDriverLicenseDataGridView.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.InternationalDriverLicenseDataGridView.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.InternationalDriverLicenseDataGridView.ThemeStyle.HeaderStyle.Height = 40;
            this.InternationalDriverLicenseDataGridView.ThemeStyle.ReadOnly = true;
            this.InternationalDriverLicenseDataGridView.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.InternationalDriverLicenseDataGridView.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.InternationalDriverLicenseDataGridView.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.InternationalDriverLicenseDataGridView.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.White;
            this.InternationalDriverLicenseDataGridView.ThemeStyle.RowsStyle.Height = 35;
            this.InternationalDriverLicenseDataGridView.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.InternationalDriverLicenseDataGridView.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.White;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.Location = new System.Drawing.Point(6, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(278, 42);
            this.label1.TabIndex = 60;
            this.label1.Text = "International Licenses History:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label13.Location = new System.Drawing.Point(18, 277);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(117, 19);
            this.label13.TabIndex = 76;
            this.label13.Text = "Total Records: ";
            // 
            // ctrlPersonCardWithFilter1
            // 
            this.ctrlPersonCardWithFilter1.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ctrlPersonCardWithFilter1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(250)))));
            this.ctrlPersonCardWithFilter1.FilterEnabled = true;
            this.ctrlPersonCardWithFilter1.Location = new System.Drawing.Point(218, 67);
            this.ctrlPersonCardWithFilter1.Name = "ctrlPersonCardWithFilter1";
            this.ctrlPersonCardWithFilter1.ShowAddPerson = true;
            this.ctrlPersonCardWithFilter1.Size = new System.Drawing.Size(1060, 420);
            this.ctrlPersonCardWithFilter1.TabIndex = 7;
            // 
            // frmShowLicensesHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1333, 804);
            this.Controls.Add(this.gbDriverLicenses);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.guna2PictureBox1);
            this.Controls.Add(this.ctrlPersonCardWithFilter1);
            this.Name = "frmShowLicensesHistory";
            this.Text = "frmShowLicensesHistory";
            this.Load += new System.EventHandler(this.frmShowLicensesHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).EndInit();
            this.gbDriverLicenses.ResumeLayout(false);
            this.gbDriverLicenses.PerformLayout();
            this.tcDriverLicenses.ResumeLayout(false);
            this.tpLocal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LocalDriverLicenseDataGridView)).EndInit();
            this.tpInternational.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.InternationalDriverLicenseDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2HtmlLabel lblTitle;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
        private People.ctrlPersonCardWithFilter ctrlPersonCardWithFilter1;
        private System.Windows.Forms.GroupBox gbDriverLicenses;
        private System.Windows.Forms.Label lblTotalRecords;
        private System.Windows.Forms.TabControl tcDriverLicenses;
        private System.Windows.Forms.TabPage tpLocal;
        private Guna.UI2.WinForms.Guna2DataGridView LocalDriverLicenseDataGridView;
        private System.Windows.Forms.Label label109;
        private System.Windows.Forms.TabPage tpInternational;
        private Guna.UI2.WinForms.Guna2DataGridView InternationalDriverLicenseDataGridView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label13;
    }
}