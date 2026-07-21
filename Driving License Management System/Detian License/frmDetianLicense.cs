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
using Driving_License_Management_System.License;

namespace Driving_License_Management_System.Detian_License
{
    public partial class frmDetianLicense : Form
    {
        public frmDetianLicense()
        {
            InitializeComponent();
        }
        private int _LicenseID;
        private clsLicense _License;
        private void ctrllFindLocalDrivingLicense1_OnLicensenSelected(int LicenseID, bool arg2)
        {
            lblShowLicenseHistory.Enabled = false;
            btnDetianLicense.Enabled = false;


            _License = clsLicense.FindLicenseByID(LicenseID);

            if (!_License.IsActive)
            {
                clsLicense ActiveLicense = clsLicense.FindActiveLicenseByLicenseClassIDAndPersonID(_License.PersonID, _License.LicenseClass);
                if (ActiveLicense != null)
                {
                    if (MessageBox.Show("Selected License Is Not Active, The System Found The Active License it have same Class for the same Person with License ID " + ActiveLicense.LicenseID + " do you want to select it?", "Detian Active License for same person", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        _License = ActiveLicense;
                    }
                    else
                    {
                        return;
                    }
                }
                // if the deactived license is the only one for the person we detained it without any question, because he will pay for active it then he pay for release it.
            }

            _LicenseID = _License.LicenseID;
            ctrllFindLocalDrivingLicense1.AsignLicenseID = _LicenseID.ToString();
            ctrllFindLocalDrivingLicense1.LoadDataByLicenseID(_LicenseID);
            lblShowLicenseHistory.Enabled = true;

            if (clsDetainedLicenses.IsLicenseDetained(_LicenseID))
            {
                MessageBox.Show("License Is Already Detained.", "Detained", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            btnDetianLicense.Enabled = true;
        }

        private void frmDetianLicense_Load(object sender, EventArgs e)
        {
            btnDetianLicense.Enabled = false;
            lblDetainDate.Text = DateTime.Now.ToString();
            lblCreatedBy.Text = GlobalSettings.User.UserName;
        }

        private void btnDetianLicense_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Fill the requierments as requiered", "Requierments Needed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            clsDetainedLicenses DetainLicense = new clsDetainedLicenses();
            DetainLicense.LicenseID = _LicenseID;
            DetainLicense.DetainDate = DateTime.Now;
            DetainLicense.FineFees = decimal.Parse(txtFineFees.Text);
            DetainLicense.CreatedByUserID = GlobalSettings.User.UserID;
            // IsRelased column is handled in the query.

            if (DetainLicense.Save())
            {
                lblDetainID.Text = DetainLicense.DetainID.ToString();
                lblLicenseID.Text = _LicenseID.ToString();
                txtFineFees.Enabled = false;
                lblShowLicenseInfo.Enabled = true;
                btnDetianLicense.Enabled = false;
                ctrllFindLocalDrivingLicense1.FilterEnabled = false;
                MessageBox.Show("License Detained Successfully.", "Detained", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ctrllFindLocalDrivingLicense1.LoadDataByLicenseID(_LicenseID);
            }
            else
            {
                MessageBox.Show("Something went error while Detained License.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void txtFineFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFineFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFineFees, "Please Enter Fine Fees");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtFineFees, null);
            }
        }

        private void lblShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicensesHistory ShowLicensesHistory = new frmShowLicensesHistory(clsLicense.FindLicenseByID(_LicenseID).PersonID);
            ShowLicensesHistory.ShowDialog();
        }

        private void lblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseInfo frmLicenseInfo = new frmLicenseInfo(_LicenseID);
            frmLicenseInfo.ShowDialog();
        }
        private void txtFineFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.';

            TextBox txt = sender as TextBox;
            if (e.KeyChar == '.' && txt.Text.Contains('.'))
            {
                e.Handled = true;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
