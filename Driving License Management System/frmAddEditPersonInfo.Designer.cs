namespace Driving_License_Management_System
{
    partial class frmAddEditPersonInfo
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
            this.ctrlAddNewEdit1 = new Driving_License_Management_System.ctrlAddNewEdit();
            this.SuspendLayout();
            // 
            // ctrlAddNewEdit1
            // 
            this.ctrlAddNewEdit1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(250)))));
            this.ctrlAddNewEdit1.Location = new System.Drawing.Point(105, 41);
            this.ctrlAddNewEdit1.Name = "ctrlAddNewEdit1";
            this.ctrlAddNewEdit1.Size = new System.Drawing.Size(1231, 565);
            this.ctrlAddNewEdit1.TabIndex = 0;
            // 
            // frmAddEditPersonInfo
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1475, 674);
            this.Controls.Add(this.ctrlAddNewEdit1);
            this.Name = "frmAddEditPersonInfo";
            this.ResumeLayout(false);

        }


        #endregion

        private ctrlAddNewEdit ctrlAddNewEdit1;
    }
}