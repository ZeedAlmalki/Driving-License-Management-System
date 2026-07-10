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
    public partial class ctrlLicenseInfo : UserControl
    {
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        private clsLicense _License;
        private clsDriver _Driver;
        private clsLicenseClass _LicenseClass;
        private clsPerson _Person;
        public ctrlLicenseInfo()
        {
            InitializeComponent();
        }

        private void _FillLicenseClassInformation()
        {
            

            lblClass.Text = _LicenseClass.ClassName;
            lblName.Text = _Person.FullName();
            lblLicenseID.Text = _License.LicenseID.ToString();
            lblNationalNo.Text = _Person.NationalNo;


            lblIssueDate.Text = _License.IssueDate.ToString();
            lblIssueReason.Text = _License.IssueReason.ToString();
            lblDateOfBirth.Text = _Person.DateOfBirth.ToString();


            lblDriverID.Text = _Driver.DriverID.ToString();
            lblExpirationDate.Text = _License.ExpirationDate.ToString();
            lblIsDetained.Text = "Edit it later";
            pbPersonPicture.ImageLocation = _Person.ImagePath;

            if (_Person.Gendor == 1)
                lblGendor.Text = "Female";
            else
                lblGendor.Text = "Male";

            if (_License.IsActive)
                lblIsActive.Text = "Yes";
            else
                lblIsActive.Text = "No";

            if (string.IsNullOrWhiteSpace(_License.Notes))
                lblNotes.Text = "No Notes";
            else
                lblNotes.Text = _License.Notes;

        }

        private void _ResetLicenseInfo()
        {
            _LocalDrivingLicenseApplication = null;
            _License = null;
            _Driver = null;
            _LicenseClass = null;
            _Person = null;
            lblClass.Text = "???";
            lblName.Text = "???";
            lblLicenseID.Text = "???";
            lblNationalNo.Text = "???";
            lblIssueDate.Text = "???";
            lblIssueReason.Text = "???";
            lblGendor.Text = "???";
            lblNotes.Text = "???";
            lblIsActive.Text = "???";
            lblDateOfBirth.Text = "???";
            lblExpirationDate.Text = "???";
            lblIsDetained.Text = "???";
            pbPersonPicture.ImageLocation = null;
        }


        public bool LoadLicenseInfo(int LocalDrivingLicenseApplication)
        {
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseApplicationByID(LocalDrivingLicenseApplication);
            if (_LocalDrivingLicenseApplication == null || _LocalDrivingLicenseApplication.ApplicationStatus != clsApplication.enApplicationSatus.Completed)
            {
                _ResetLicenseInfo();
                return false;
            }

            _License = clsLicense.FindLicenseByApplicationID(_LocalDrivingLicenseApplication.ApplicationID);
            _Driver = clsDriver.FindDriversByPersonID(_LocalDrivingLicenseApplication.ApplicantPersonID);
            _LicenseClass = clsLicenseClass.FindLicenseClassByID(_LocalDrivingLicenseApplication.LicenseClassID);
            _Person = clsPerson.Find(_LocalDrivingLicenseApplication.ApplicantPersonID);
            if (_License == null && _Driver == null && _LicenseClass == null && _Person == null)
            {
                _ResetLicenseInfo();
                return false;
            }
            _FillLicenseClassInformation();
            return true;
        }
    }
}
