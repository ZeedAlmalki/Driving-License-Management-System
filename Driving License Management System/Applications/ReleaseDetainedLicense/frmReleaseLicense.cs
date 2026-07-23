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
    public partial class frmReleaseLicense : Form
    {
        enum enMode { Grid = 1, Manual = 0}
        enMode _Mode;
        private clsManageApplicationTypes ReleaseApplicationType = clsManageApplicationTypes.FindApplicationType((int)clsManageApplicationTypes.enManageApplicationTypes.RelaseDetainedDrivingLicense);
        private int _LicenseID = -1;
        private clsDetainedLicenses DetainedLicense;
        public frmReleaseLicense()
        {
            InitializeComponent();
            _Mode = enMode.Manual;
        }

        public frmReleaseLicense(int LicenseID)
        {
            InitializeComponent();
            _LicenseID = LicenseID;
            _Mode = enMode.Grid;
        }

        private void frmReleaseLicense_Load(object sender, EventArgs e)
        {
            lblApplicationFees.Text = ReleaseApplicationType.ApplicationFees.ToString();
            lblShowLicenseInfo.Enabled = false;


            if (_Mode == enMode.Manual)
            {
                btnReleaseLicense.Enabled = false;
                lblShowLicenseHistory.Enabled = false;
            }
            else if (_Mode == enMode.Grid)
            {
                btnReleaseLicense.Enabled = true;
                lblShowLicenseHistory.Enabled = true;
                ctrllFindLocalDrivingLicense1.FilterEnabled = false;
                ctrllFindLocalDrivingLicense1.AsignLicenseID = _LicenseID.ToString();
                FillReleaseLicenseWithInformation();
            }
        }

        private void _ResetDefaultValue()
        {
            lblLicenseID.Text = "???";
            lblFineFees.Text = "???";
            lblDetainDate.Text = "???";
            lblTotalFees.Text = "???";
            lblDetainID.Text = "???";
            lblCreatedBy.Text = "???";
        }

        private void FillReleaseLicenseWithInformation()
        {
            if (!ctrllFindLocalDrivingLicense1.LoadDataByLicenseID(_LicenseID))
            {
                MessageBox.Show("Something went error, license is not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            lblShowLicenseHistory.Enabled = true;
            btnReleaseLicense.Enabled = false;
            _ResetDefaultValue();
            if (!clsDetainedLicenses.IsLicenseDetained(_LicenseID))
            {
                MessageBox.Show("Selected License is Not Detained", "Not Detained", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!clsLicense.FindLicenseByID(_LicenseID).IsActive)
            {
                MessageBox.Show("You Must to renew your license before release it", "License Must be active", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DetainedLicense = clsDetainedLicenses.FindDetainedLicenseByLicenseID(_LicenseID);

            if (DetainedLicense == null)
            {
                MessageBox.Show("Something went error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            lblLicenseID.Text = DetainedLicense.LicenseID.ToString();
            lblFineFees.Text = DetainedLicense.FineFees.ToString();
            lblDetainDate.Text = DetainedLicense.DetainDate.ToString();
            lblDetainID.Text = DetainedLicense.DetainID.ToString();
            lblTotalFees.Text = (DetainedLicense.FineFees + ReleaseApplicationType.ApplicationFees).ToString();
            lblCreatedBy.Text = DetainedLicense.CreatedByUserID.ToString();

            btnReleaseLicense.Enabled = true;
            lblShowLicenseHistory.Enabled = true;
        }

        private void ctrllFindLocalDrivingLicense1_OnLicensenSelected(int LicenseID, bool arg2)
        {
            _LicenseID = LicenseID;
            FillReleaseLicenseWithInformation();
        }

        private void btnReleaseLicense_Click(object sender, EventArgs e)
        {
            clsApplication ReleaseApplication = new clsApplication();

            ReleaseApplication.ApplicantPersonID = DetainedLicense.DetainedLicenseInfo.PersonID;
            ReleaseApplication.ApplicationDate = DateTime.Now;
            ReleaseApplication.ApplicationTypeID = ReleaseApplicationType.ApplicationTypeID;
            ReleaseApplication.ApplicationStatus = clsApplication.enApplicationSatus.Completed;
            ReleaseApplication.LastStatusDate = DateTime.Now;
            ReleaseApplication.PaidFees = ReleaseApplicationType.ApplicationFees;
            ReleaseApplication.CreatedByUserID = GlobalSettings.User.UserID;

            if (!ReleaseApplication.Save())
            {
                MessageBox.Show("Application was not saved successfully", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // if we in real system we will stop in the application and the process will go sequential, so for this we will send a new time and a new user id by global setteings ( The applicatino requester not always the same user who release the license).
            DetainedLicense.ReleaseDate = DateTime.Now;
            DetainedLicense.ReleasedByUserID = GlobalSettings.User.UserID;
            DetainedLicense.ReleaseApplicationID = ReleaseApplication.ApplicationID;

            if (DetainedLicense.Save())
            {
                btnReleaseLicense.Enabled = false;
                lblApplicationID.Text = ReleaseApplication.ApplicationID.ToString();
                lblShowLicenseInfo.Enabled = true;
                ctrllFindLocalDrivingLicense1.FilterEnabled = false;
                MessageBox.Show("Released Has Been Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ctrllFindLocalDrivingLicense1.LoadDataByLicenseID(_LicenseID);
            }
            else
            {
                MessageBox.Show("Something went error while releasing", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseInfo frmLicenseInfo = new frmLicenseInfo(_LicenseID);
            frmLicenseInfo.ShowDialog();
        }

        private void lblShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicensesHistory ShowLicensesHistory = new frmShowLicensesHistory(clsLicense.FindLicenseByID(_LicenseID).PersonID);
            ShowLicensesHistory.ShowDialog();
        }
    }
}
