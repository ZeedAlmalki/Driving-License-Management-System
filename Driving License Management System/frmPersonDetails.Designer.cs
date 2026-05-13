namespace Driving_License_Management_System
{
    partial class frmPersonDetails
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
            this.ctrlPeopleDetails1 = new Driving_License_Management_System.ctrlPeopleDetails();
            this.SuspendLayout();
            // 
            // ctrlPeopleDetails1
            // 
            this.ctrlPeopleDetails1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(250)))));
            this.ctrlPeopleDetails1.Location = new System.Drawing.Point(86, 49);
            this.ctrlPeopleDetails1.Name = "ctrlPeopleDetails1";
            this.ctrlPeopleDetails1.Size = new System.Drawing.Size(1029, 424);
            this.ctrlPeopleDetails1.TabIndex = 0;
            // 
            // frmPersonDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1252, 524);
            this.Controls.Add(this.ctrlPeopleDetails1);
            this.Name = "frmPersonDetails";
            this.Text = "frmPersonDetails";
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlPeopleDetails ctrlPeopleDetails1;
    }
}