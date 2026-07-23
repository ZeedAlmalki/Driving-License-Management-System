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
using Driving_License_Management_System.License.Controls;

namespace Driving_License_Management_System.License
{
    public partial class frmReplacementForDamagedLicense : Form
    {
        public frmReplacementForDamagedLicense()
        {
            InitializeComponent();
        }
        private static decimal ReplacementForaDamageddDrivingLicenseFees = clsManageApplicationTypes.FindApplicationType((int) clsManageApplicationTypes.enManageApplicationTypes.ReplacementForaDamageddDrivingLicense).ApplicationFees;
        private static decimal ReplacementForaLostdDrivingLicense = clsManageApplicationTypes.FindApplicationType((int)clsManageApplicationTypes.enManageApplicationTypes.ReplacementForaLostdDrivingLicense).ApplicationFees;
        private decimal ReplacementFor;
        private int _LicenseID = -1;
        private int NewLicenseID = -1;
        private clsLicense _License;
        private void frmReplacementForDamagedLicense_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = DateTime.Now.ToString();
            lblCreatedBy.Text = GlobalSettings.User.UserName;
            btnIssueReplacement.Enabled = false;
            rbDamagedLicense.Checked = true;
        }

        private void FillApplicationInfoForLicenseReplacement()
        {

        }

        private void rbReplacemntFor(object sender, EventArgs e)
        {
            if (rbDamagedLicense.Checked)
            {
                ReplacementFor = ReplacementForaDamageddDrivingLicenseFees;
                lblTitle.Text = "Replacment For Damaged License";
                lblApplicationFees.Text = ReplacementForaDamageddDrivingLicenseFees.ToString();
            }
            else if (rbLostLicense.Checked) 
            {
                ReplacementFor = ReplacementForaLostdDrivingLicense;
                lblTitle.Text = "Replacement For Lost License";
                lblApplicationFees.Text = ReplacementForaLostdDrivingLicense.ToString();
            }

            this.Text = lblTitle.Text;
        }

        private void ctrllFindLocalDrivingLicense1_OnLicensenSelected(int LicenseID, bool arg2)
        {
            btnIssueReplacement.Enabled = false;
            _LicenseID = LicenseID;
            _License = clsLicense.FindLicenseByID(_LicenseID);

            if (_License == null)
                return;

            lblShowLicenseHistory.Enabled = true;

            //FillApplicationInfoForLicenseReplacement();

            ctrllFindLocalDrivingLicense1.LoadDataByLicenseID(_License.LicenseID);
            lblOldLicenseID.Text = _License.LicenseID.ToString();

            if (_License.ExpirationDate < DateTime.Now)
            {
                MessageBox.Show("Selected License Is Expired, Please Renewed it Digitally", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!_License.IsActive)
            {
                MessageBox.Show("Selected License Is Not Active, You must to actived it ", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            btnIssueReplacement.Enabled = true;
        }

        private void btnIssueReplacement_Click(object sender, EventArgs e)
        {
            clsLicense License = new clsLicense();
            clsApplication application = new clsApplication();

            if (rbDamagedLicense.Checked)
            {
                application.ApplicationTypeID = (int)clsManageApplicationTypes.enManageApplicationTypes.ReplacementForaDamageddDrivingLicense;
                License.IssueReason = clsLicense.enIssueReason.ReplacementForDamaged;
            }
            else if (rbLostLicense.Checked)
            {
                application.ApplicationTypeID = (int)clsManageApplicationTypes.enManageApplicationTypes.ReplacementForaLostdDrivingLicense;
                License.IssueReason = clsLicense.enIssueReason.ReplacementForLost;
            }
            else
            {
                MessageBox.Show("Please Chose a Choise in raido button", "Chose Your Service.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            application.ApplicantPersonID = _License.PersonID;
            application.PaidFees = ReplacementFor;
            application.ApplicationDate = DateTime.Now;
            application.ApplicationStatus = clsApplication.enApplicationSatus.Completed; // it should be new until we sure renew license saved successfully. but its method.
            application.LastStatusDate = DateTime.Now;
            application.CreatedByUserID = _License.CreatedByUserID;

            if (!application.Save())
            {
                MessageBox.Show("Application Can not saved, please contact with the admin", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            License.ApplicationID = application.ApplicationID;
            License.DriverID = _License.DriverID;
            License.LicenseClass = _License.LicenseClass;
            clsLicenseClass LicenseClass = clsLicenseClass.FindLicenseClassByID(License.LicenseClass);
            License.IssueDate = DateTime.Now;
            License.ExpirationDate = _License.ExpirationDate;
            License.PaidFees = 0; // We don't pay for a new license, we just pay for the application.
            License.IsActive = true;
            License.CreatedByUserID = GlobalSettings.User.UserID;

            if (License.Save())
            {
                _License.IsActive = false;
                _License.Save();

                ctrllFindLocalDrivingLicense1.FilterEnabled = false;
                btnIssueReplacement.Enabled = false;
                lblShowLicenseInfo.Enabled = true;
                lblApplicationID.Text = License.ApplicationID.ToString();
                NewLicenseID = License.LicenseID;
                lblReplacedLocalLicenseID.Text = NewLicenseID.ToString();
                MessageBox.Show("License Replacement Successfully", "License Replacement", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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

        private void lblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseInfo frmLicenseInfo = new frmLicenseInfo(NewLicenseID);
            frmLicenseInfo.ShowDialog();
        }
    }
}
