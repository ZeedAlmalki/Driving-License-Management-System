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
using static BusinessLayer.clsApplication;

namespace Driving_License_Management_System.License.International_Driving_License
{
    public partial class frmAddNewInternationalDrivingLicense : Form
    {
        public frmAddNewInternationalDrivingLicense()
        {
            InitializeComponent();
        }

        private int _LicenseID = -1;
        private clsLicense _License;
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to issued this driver license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;

            _License = clsLicense.FindLicenseByID(_LicenseID);

           clsInternationalLicense InternationalLicense = new clsInternationalLicense(_LicenseID, GlobalSettings.User.UserID);

            if (InternationalLicense.Save())
            {
                ctrllFindLocalDrivingLicense1.FilterEnabled = false;
                ctrllFindLocalDrivingLicense1.InternationalLicenseApplicationID = InternationalLicense.ApplicationID;
                ctrllFindLocalDrivingLicense1.InternationalLicenseID = InternationalLicense.InternationalLicenseID;

                btnIssue.Enabled = false;
                lblShowLicenseInfo.Enabled = true;
                lblShowLicenseHistory.Enabled = true;
                MessageBox.Show("International Saved Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                MessageBox.Show("Something went error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void ctrllFindLocalDrivingLicense1_OnLicensenSelected(int obj)
        {
            _LicenseID = obj;
            if (_LicenseID != -1)
            {
                btnIssue.Enabled = true;
            }

        }

        private void frmAddNewInternationalDrivingLicense_Load(object sender, EventArgs e)
        {
            btnIssue.Enabled = false;
            lblShowLicenseHistory.Enabled = false;
            lblShowLicenseInfo.Enabled = false;
        }

        private void lblShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicensesHistory frmShowLicensesHistory = new frmShowLicensesHistory(clsLicense.FindLicenseByID(_LicenseID).PersonID);
            frmShowLicensesHistory.ShowDialog();
        }

        private void lblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmInternationalLicenseDriverInfo frmInternationalLicenseDriverInfo = new frmInternationalLicenseDriverInfo(ctrllFindLocalDrivingLicense1.InternationalLicenseID);
            frmInternationalLicenseDriverInfo.ShowDialog();
        }
    }
}
