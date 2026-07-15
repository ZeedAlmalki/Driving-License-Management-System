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
    public partial class ctrlInternationalDrivingLicenseApplicationInfo : UserControl
    {
        public ctrlInternationalDrivingLicenseApplicationInfo()
        {
            InitializeComponent();
        }
        private clsLicense _License;

        public int ILApplicationID
        {
            get
            {
                return Convert.ToInt32(lblInternationalLicenseApplicationID.Text);
            }
            set
            {
                lblInternationalLicenseApplicationID.Text = Convert.ToString(value);
            }
        }

        public int ILLicenseID
        {
            get
            {
                return Convert.ToInt32(lblInternationalLicenseID.Text);
            }
            set
            {
                lblInternationalLicenseID.Text = Convert.ToString(value);
            }
        }

        private void _ResetApplicationInfo()
        {
            lblInternationalLicenseApplicationID.Text = "???";
            lblApplicationDate.Text = "???";
            lblIssueDate.Text = "???";
            lblFees.Text = "???";
            

            lblInternationalLicenseID.Text = "???";
            lblLocalLicenseID.Text = "???";
            lblExpiriationDate.Text = "???";
            lblCreatedBy.Text = "???";
        }

        private void _ResetDefaultValue()
        {
            lblApplicationDate.Text = DateTime.Now.ToString();
            lblIssueDate.Text = DateTime.Now.ToString();
            lblFees.Text = clsManageApplicationTypes.FindApplicationType((int)clsManageApplicationTypes.enManageApplicationTypes.NewInternationalLicense).ApplicationFees.ToString();
            lblExpiriationDate.Text = (DateTime.Now.AddYears(1)).ToString();
            lblCreatedBy.Text = GlobalSettings.User.UserName;
        }

        private void _FillApplicationInfo()
        {
            // lblInternationalLicenseApplicationID.Text = _License.ApplicationID.ToString();
            // lblInternationalLicenseID.Text = _InternationalLicense.InternationalLicenseID.ToString();


            lblApplicationDate.Text = DateTime.Now.ToString();
            lblIssueDate.Text = _License.IssueDate.ToString();
            lblFees.Text = _License.PaidFees.ToString();

            lblLocalLicenseID.Text = _License.LicenseID.ToString();
            lblExpiriationDate.Text = _License.ExpirationDate.ToString();
        }

        public bool LoadData(int LicenseID)
        {
            _ResetDefaultValue();
            _License = clsLicense.FindLicenseByID(LicenseID);
            if (_License != null)
            {
                _FillApplicationInfo();
                return true;
            }
            else
            {
                _ResetApplicationInfo();
                return false;
            }
        }
    }
}
