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

namespace Driving_License_Management_System.License.International_Driving_License.Controls
{
    public partial class ctrlInternationalLicenseDriverInfo : UserControl
    {

        public ctrlInternationalLicenseDriverInfo()
        {
            InitializeComponent();
        }

        clsInternationalLicense _InternationalLicense;

        private void _ResetInternationalLicenseInfo()
        {
            lblName.Text = "???";
            lblInternationalLicenseInfo.Text = "???";
            lblLicenseID.Text = "???";
            lblNationalNo.Text = "???";
            lblGendor.Text = "???";
            lblIssueDate.Text = "???";


            lblApplicationID.Text = "???";
            lblIsActive.Text = "???";
            lblDateOfBirth.Text = "???";
            lblDriverID.Text = "???";
            lblExpirationDate.Text = "???";
            pbPersonPicture.ImageLocation = null;
        }

        private void _FillInternationalLicenseInfo()
        {
            lblName.Text = _InternationalLicense.PersonInfo.FullName();
            lblInternationalLicenseInfo.Text = _InternationalLicense.InternationalLicenseID.ToString();
            lblLicenseID.Text = _InternationalLicense.IssuedUsingLocalLicenseID.ToString();
            lblNationalNo.Text = _InternationalLicense.PersonInfo.NationalNo.ToString();
            if (_InternationalLicense.PersonInfo.Gendor == 1)
                lblGendor.Text = "Female";
            else
                lblGendor.Text = "Male";


            lblIssueDate.Text = _InternationalLicense.IssueDate.ToString();

            lblApplicationID.Text = _InternationalLicense.ApplicationID.ToString();
            if (_InternationalLicense.IsActive)
                lblIsActive.Text = "Yes";
            else
                lblIsActive.Text = "No";


            lblDateOfBirth.Text = _InternationalLicense.PersonInfo.DateOfBirth.ToString();
            lblDriverID.Text = _InternationalLicense.DriverID.ToString();
            lblExpirationDate.Text = _InternationalLicense.ExpirationDate.ToString();
            pbPersonPicture.ImageLocation = _InternationalLicense.PersonInfo.ImagePath;
        }

        public bool LoadData(int InternationalLicenseID)
        {
            _InternationalLicense = clsInternationalLicense.FindInternationalLicenseByID(InternationalLicenseID);

            if (_InternationalLicense != null)
            {
                _FillInternationalLicenseInfo();
                return true;
            }
            else
            {
                _ResetInternationalLicenseInfo();
                return false;
            }

        }
    }
}
