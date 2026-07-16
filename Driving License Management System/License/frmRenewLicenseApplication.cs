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
using Driving_License_Management_System.License.International_Driving_License;
using Driving_License_Management_System.License.International_Driving_License.Controls;

namespace Driving_License_Management_System.License
{
    public partial class frmRenewLicenseApplication : Form
    {
        public frmRenewLicenseApplication()
        {
            InitializeComponent();
        }
        private int _LicenseID;
        private clsLicense _License;

        private void DefaultSetteigns()
        {
            ctrlApplicationNewLicenseInfo1.ResetDefaultValues();
            btnRenew.Enabled = false;
            ctrllFindLocalDrivingLicense1.IsRenewMode = true;
        }

        private void frmRenewLicenseApplication_Load(object sender, EventArgs e)
        {
            DefaultSetteigns();
        }

        private void ctrllFindLocalDrivingLicense1_OnLicensenSelected(int LicenseID, bool arg2)
        {
            btnRenew.Enabled = false;
            _LicenseID = LicenseID;
            _License = clsLicense.FindLicenseByID(_LicenseID);

            if (_License == null)
                return;
            ctrlApplicationNewLicenseInfo1.LoadControlData(_LicenseID);
            ctrllFindLocalDrivingLicense1.LoadDataByLicenseID(_License.LicenseID);

            if (_License.ExpirationDate > DateTime.Now)
            {
                MessageBox.Show("Selected License Is Not Expired yet, it will expired in " + _License.ExpirationDate, "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!_License.IsActive)
            {
                MessageBox.Show("Selected License Is Not Active , You must to actived it ", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            btnRenew.Enabled = true;
        }

        private void btnRenew_Click(object sender, EventArgs e)
        {
            clsLicense License = new clsLicense();
            clsApplication application = new clsApplication();
            application.ApplicantPersonID = _License.PersonID;
            application.ApplicationDate = DateTime.Now;
            application.ApplicationTypeID = (int)clsManageApplicationTypes.enManageApplicationTypes.RenewDrivingLicenseService;
            application.ApplicationStatus = clsApplication.enApplicationSatus.Completed; // it should be new until we sure renew license saved successfully. but its method.
            application.LastStatusDate  = DateTime.Now;
            application.PaidFees = clsManageApplicationTypes.FindApplicationType((int)clsManageApplicationTypes.enManageApplicationTypes.RenewDrivingLicenseService).ApplicationFees;
            application.CreatedByUserID = _License.CreatedByUserID;

            if (!application.Save())
            {
                MessageBox.Show("Application Cant saved, please contact with the admin", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            License.ApplicationID = application.ApplicationID;
            License.DriverID = _License.DriverID;
            License.LicenseClass = _License.LicenseClass;
            clsLicenseClass LicenseClass = clsLicenseClass.FindLicenseClassByID(License.LicenseClass);
            License.IssueDate = DateTime.Now;
            License.ExpirationDate = (DateTime.Now.AddYears(LicenseClass.DefaultValidityLength));
            License.Notes = ctrlApplicationNewLicenseInfo1.Notes;
            License.PaidFees = LicenseClass.ClassFees;
            License.IsActive = true;
            License.IssueReason = clsLicense.enIssueReason.Renew;
            License.CreatedByUserID = GlobalSettings.User.UserID;

            if (License.Save())
            {
                _License.IsActive = false;
                _License.Save();

                ctrllFindLocalDrivingLicense1.FilterEnabled = false;
                btnRenew.Enabled = false;
                lblShowLicenseInfo.Enabled = true;
                ctrlApplicationNewLicenseInfo1.RenewApplicationID = License.ApplicationID.ToString();
                ctrlApplicationNewLicenseInfo1.NewLicenseID = License.LicenseID.ToString();
                MessageBox.Show("License Renewed Successfully", "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void lblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseInfo frmLicenseInfo = new frmLicenseInfo(-1);
            frmLicenseInfo.LoadLicenseInfoByApplicationID(_License.ApplicationID);
        }

        private void lblShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicensesHistory frmShowLicensesHistory = new frmShowLicensesHistory(clsLicense.FindLicenseByID(_LicenseID).PersonID);
            frmShowLicensesHistory.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
