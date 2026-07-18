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

        public int LicenseID
        {
            get { return _License.LicenseID; }
        }

        private void _FillLicenseInformation()
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

        private void _FillLicenseInformationByLicenseIDOnly()
        {

            clsPerson Person = clsPerson.FindByPersonID(_License.PersonID);
            lblClass.Text = clsLicenseClass.FindLicenseClassByID(_License.LicenseClass).ClassName;
            lblName.Text = Person.FullName();
            lblLicenseID.Text = _License.LicenseID.ToString();
            lblNationalNo.Text = Person.NationalNo;

            lblIssueDate.Text = _License.IssueDate.ToString();
            lblIssueReason.Text = _License.IssueReason.ToString();

            switch (_License.IssueReason)
            {
                case clsLicense.enIssueReason.FirstTime:
                    lblIssueReason.Text = "First Time";
                    break;
                case clsLicense.enIssueReason.Renew:
                    lblIssueReason.Text = "Re New";
                    break;
                case clsLicense.enIssueReason.ReplacementForLost:
                    lblIssueReason.Text = "Replacement For Lost";
                    break;
                case clsLicense.enIssueReason.ReplacementForDamaged:
                    lblIssueReason.Text = "Replacement For Damaged";
                    break;
                default:
                    lblIssueReason.Text = "";
                    break;
            }

            lblDateOfBirth.Text = Person.DateOfBirth.ToString();


            lblDriverID.Text = _License.DriverID.ToString();
            lblExpirationDate.Text = _License.ExpirationDate.ToString();
            lblIsDetained.Text = "Edit it later";
            pbPersonPicture.ImageLocation = Person.ImagePath;

            if (Person.Gendor == 1)
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
            lblDriverID.Text = "???";
            lblExpirationDate.Text = "???";
            lblIsDetained.Text = "???";
            pbPersonPicture.ImageLocation = null;
        }


        public bool LoadLicenseInfoByLicenseID(int LicenseID)
        {

            _License = clsLicense.FindLicenseByID(LicenseID);

            if (_License == null)
            {
                _ResetLicenseInfo();
                return false;
            }
            else
            {
                _FillLicenseInformationByLicenseIDOnly();
                return true;
            }
        }


    }
}
