using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer;
using Guna.UI2.WinForms;
using Guna.UI2.WinForms.Enums;

namespace Driving_License_Management_System
{
    public partial class frmUpdateApplicationType : Form
    {
        private int _ApplicationTypeID = -1;
        private clsManageApplicationTypes _ApplicationType;
        public frmUpdateApplicationType(int ApplicationTypeID)
        {
            InitializeComponent();
            _ApplicationTypeID = ApplicationTypeID;
            //_LoadData();
        }

        private void _LoadData()
        {
            _ApplicationType = clsManageApplicationTypes.FindApplicationType(_ApplicationTypeID);

            if (_ApplicationType != null)
            {
                lblApplicationID.Text = _ApplicationType.ApplicationTypeID.ToString();
                txtApplicationTitle.Text = _ApplicationType.ApplicationTypeTitle;
                txtApplicationFees.Text = Math.Floor(_ApplicationType.ApplicationFees).ToString();
            }
            else
            {
                MessageBox.Show("ERROR: Something Went Error, Please Contact The Developer", "Application Type Doesn't Exist", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Please Fill In The Requirement As Required", "Fill Fields", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            
            if (_ApplicationType != null)
            {
                _ApplicationType.ApplicationTypeTitle = txtApplicationTitle.Text.Trim();
                _ApplicationType.ApplicationFees = Convert.ToDecimal(txtApplicationFees.Text.Trim());
                if (_ApplicationType.Save())
                {
                    MessageBox.Show("Updated Has Been Saved Successfully", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("ERROR: Updated Has not Been Saved Successfully", "Hasn't Updated", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("ERROR: Something Went Error, Please Contact The Developer", "Application Type Doesn't Exist", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }

        }

        private void frmUpdateApplicationType_Shown(object sender, EventArgs e)
        {
            txtApplicationTitle.Focus();
        }

        private void frmUpdateApplicationType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                btnSave.PerformClick();
            }
        }

        private void txtApplicationTitle_Validating(object sender, CancelEventArgs e)
        {
            clsValidation.txtIsNotNullOrWhiteSpaceValdiateHandling((Guna2TextBox)sender, e, errorProvider1);
        }

        private void txtApplicationFees_Validating(object sender, CancelEventArgs e)
        {
            clsValidation.txtIsNotNullOrWhiteSpaceValdiateHandling((Guna2TextBox)sender, e, errorProvider1);

            if (!clsValidation.IsNumberValid(txtApplicationFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtApplicationFees, "Must Be Numeric");
            }
           else
            {
                errorProvider1.SetError(txtApplicationFees, null);
            }
        }

        private void frmUpdateApplicationType_Load(object sender, EventArgs e)
        {
            _LoadData();
        }
    }
}
