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

namespace Driving_License_Management_System.License
{
    public partial class frmIssueDriverLicenseForTheFirstTime : Form
    {

        private clsLocalDrivingLicenseApplication _LDLApplication;
        public frmIssueDriverLicenseForTheFirstTime(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _LDLApplication = clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseApplicationByID(LocalDrivingLicenseApplicationID);
        }

        private void frmIssueDriverLicenseForTheFirstTime_Load(object sender, EventArgs e)
        {

            if (_LDLApplication != null && _LDLApplication.GetPassedTestCount() == 3)
            {
                if (_LDLApplication.ItHasLocalDrivingLicenseClassBefore())
                {
                    MessageBox.Show("The Person Already Has This Driving License ", "Already Taken it", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }
                ctrlDrivingLicenseApplicationInfo1.LoadLocalDrivingLicenseApplicationInfo(_LDLApplication.LocalDrivingLicenseApplicationsID);
            }
            else
            {
                MessageBox.Show("Something went wrong", "Wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (_LDLApplication == null)
            {
                MessageBox.Show("Error");
                this.Close();
                return;
            }
            clsLicenseClass LicenseClass = clsLicenseClass.FindLicenseClassByID(_LDLApplication.LicenseClassID);


            int DefaultValidityLengthInYears = LicenseClass.DefaultValidityLength;

            clsLicense License = new clsLicense();
            License.ApplicationID = _LDLApplication.ApplicationID;
            License.LicenseClass = _LDLApplication.LicenseClassID;
            License.IssueDate = DateTime.Now;
            License.ExpirationDate = License.IssueDate.AddYears(DefaultValidityLengthInYears);
            License.Notes = txtNotes.Text;
            License.PaidFees = LicenseClass.ClassFees;
            License.IsActive = true;
            License.IssueReason = clsLicense.enIssueReason.FirstTime;
            License.CreatedByUserID = GlobalSettings.User.UserID;

            clsDriver driver = clsDriver.FindDriversByPersonID(_LDLApplication.ApplicantPersonID);


            //clsDriver Driver = clsDriver.FindDriversByPersonID(_LDLApplication.ApplicantPersonID);
            //if (Driver != null)
            //    License.DriverID = clsDriver.FindDriversByPersonID(_LDLApplication.ApplicantPersonID).DriverID;
            //else
            //{
            //    MessageBox.Show("Something went error");
            //    this.Close();
            //    return;
            //}

            if (driver != null)
            {
                License.DriverID = driver.DriverID;
            }
            else
            {
                driver = new clsDriver();
                driver.PersonID = _LDLApplication.ApplicantPersonID;
                driver.CreatedByUserID = GlobalSettings.User.UserID;
                driver.CreatedDate = DateTime.Now;

                if (!driver.Save())
                {
                    MessageBox.Show("Something went error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }
            }


            License.DriverID = driver.DriverID;
            if (License.Save())
            {
                clsApplication application = clsApplication.FindApplicationByID(License.ApplicationID);
                application.ApplicationStatus = clsApplication.enApplicationSatus.Completed;
                application.Save();
                MessageBox.Show("License Issued Successfully With License ID = " + License.LicenseID, "Saved Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Something went error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
