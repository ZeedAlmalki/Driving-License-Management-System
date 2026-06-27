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

namespace Driving_License_Management_System.TestTypes
{
    public partial class frmUpdateTestType : Form
    {
        private clsManageTestType.enTestType _TestTypeID = clsManageTestType.enTestType.None;
        private clsManageTestType _TestType;
        public frmUpdateTestType(clsManageTestType.enTestType TestTypeID)
        {
            InitializeComponent();
            _TestTypeID = TestTypeID;
        }

        private void _LoadData()
        {
            _TestType = clsManageTestType.FindTestTypeByID(_TestTypeID);

            if (_TestType != null)
            {
                lblTestTypeID.Text = ((int)_TestType.TestTypeID).ToString();
                txtTestTypeDescription.Text = _TestType.TestTypeDescription;
                txtTestTypeTitle.Text = _TestType.TestTypeTitle;
                txtTestTypeFees.Text = Math.Floor(_TestType.TestTypeFees).ToString();
            }
            else
            {
                MessageBox.Show("ERROR: Something Went Error, Please Contact The Developer", "Application Type Doesn't Exist", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Please Fill In The Requirement As Required", "Fill Fields", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_TestType != null)
            {
                _TestType.TestTypeTitle = txtTestTypeTitle.Text.Trim();
                _TestType.TestTypeDescription = txtTestTypeDescription.Text.Trim();
                _TestType.TestTypeFees = Convert.ToDecimal(txtTestTypeFees.Text.Trim());

                if (_TestType.Save())
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

        private void frmUpdateTestType_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void frmUpdateTestType_Shown(object sender, EventArgs e)
        {
            txtTestTypeTitle.Focus();
        }

        private void frmUpdateTestType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                btnSave.PerformClick();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTestTypeTitle_Validating(object sender, CancelEventArgs e)
        {
            clsValidation.txtIsNotNullOrWhiteSpaceValdiateHandling(txtTestTypeTitle, e, errorProvider1);
        }

        private void txtTestTypeDescription_Validating(object sender, CancelEventArgs e)
        {
            clsValidation.txtIsNotNullOrWhiteSpaceValdiateHandling(txtTestTypeTitle, e, errorProvider1);

        }

        private void txtTestTypeFees_Validating(object sender, CancelEventArgs e)
        {
            clsValidation.txtIsNotNullOrWhiteSpaceValdiateHandling(txtTestTypeTitle, e, errorProvider1);
            if (!clsValidation.IsNumberValid(txtTestTypeFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTestTypeFees, "Must Be Numeric");
            }
            else
            {
                errorProvider1.SetError(txtTestTypeFees, null);
            }
        }
    }
}