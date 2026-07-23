using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using BusinessLayer;
using Driving_License_Management_System.Users;
using Driving_License_Management_System.TestTypes;
using Driving_License_Management_System.Drivers;
using Driving_License_Management_System.License;
using Driving_License_Management_System.License.International_Driving_License;
using Driving_License_Management_System.Detian_License;
using Driving_License_Management_System.Applications.LocalDrivingLicenseApplication;

namespace Driving_License_Management_System
{
    public partial class MainForm : Form
    {
        frmLoginScreen _frmLogin;
        public MainForm(frmLoginScreen frm)
        {
            InitializeComponent();
            _frmLogin = frm;
        }

        private void btnPeople_Click(object sender, EventArgs e)
        {
            frmManagePeople frmManagePeople = new frmManagePeople();
            frmManagePeople.ShowDialog();
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            frmManageUsers frmManageUsers = new frmManageUsers();
            frmManageUsers.ShowDialog();
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowUserInfo frmShowUserInfo = new frmShowUserInfo(GlobalSettings.User.UserID);
            frmShowUserInfo.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangeUserPassword frmChangeUserPassword = new frmChangeUserPassword(GlobalSettings.User.UserID);
            frmChangeUserPassword.ShowDialog();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GlobalSettings.User = null;
            _frmLogin.Show();
            this.Close();
        }
        private void ManageApplicationstoolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageApplicationsTypes frm = new frmManageApplicationsTypes();
            frm.ShowDialog();
        }

        private void ManageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageTestTypes frm = new frmManageTestTypes();
            frm.ShowDialog();
        }

        private void localDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageLocalDrivingLicenseApplications frmManageLocalDrivingLicenseApplications = new frmManageLocalDrivingLicenseApplications();
            frmManageLocalDrivingLicenseApplications.ShowDialog();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            frmListDrivers listDrivers = new frmListDrivers();
            listDrivers.ShowDialog();
        }

        private void detianLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddNewInternationalDrivingLicense addNewInternationalDrivingLicenseApplication = new frmAddNewInternationalDrivingLicense();
            addNewInternationalDrivingLicenseApplication.ShowDialog();
        }

        private void internationalDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListInternationalLicense frmListInternationalLicense = new frmListInternationalLicense();
            frmListInternationalLicense.ShowDialog();
        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddNewLocalDrivingLicenseApplication frmAddNewLocalDrivingLicenseApplication = new frmAddNewLocalDrivingLicenseApplication();
            frmAddNewLocalDrivingLicenseApplication.ShowDialog();
        }

        private void internationalDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddNewInternationalDrivingLicense frmAddNewInternationalDrivingLicense = new frmAddNewInternationalDrivingLicense();
            frmAddNewInternationalDrivingLicense.ShowDialog();
        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRenewLicenseApplication frmRenewLicenseApplication = new frmRenewLicenseApplication();
            frmRenewLicenseApplication.ShowDialog();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            frmReplacementForDamagedLicense frmReplacementForDamagedLicense = new frmReplacementForDamagedLicense();
            frmReplacementForDamagedLicense.ShowDialog();
        }

        private void detianLicenseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmDetianLicense frmDetianLicense = new frmDetianLicense();
            frmDetianLicense.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseLicense frmReleaseLicense = new frmReleaseLicense();
            frmReleaseLicense.ShowDialog();
        }

        private void manageDetainedLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageDetainLicense frmManageDetain = new frmManageDetainLicense();
            frmManageDetain.ShowDialog();
        }

        private void releaseDetainedDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseLicense frmReleaseLicense = new frmReleaseLicense();
            frmReleaseLicense.ShowDialog();
        }

        private void retakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageLocalDrivingLicenseApplications frmManageLocalDrivingLicenseApplications = new frmManageLocalDrivingLicenseApplications();
            frmManageLocalDrivingLicenseApplications.ShowDialog();
        }
    }
}
