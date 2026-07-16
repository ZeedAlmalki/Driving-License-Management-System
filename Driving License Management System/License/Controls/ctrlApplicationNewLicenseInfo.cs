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

namespace Driving_License_Management_System.License.Controls
{
    public partial class ctrlApplicationNewLicenseInfo : UserControl
    {
        public ctrlApplicationNewLicenseInfo()
        {
            InitializeComponent();
        }

        public string RenewApplicationID
        {
            get
            {
                return lblInternationalLicenseRenewApplicationID.Text;
            }
            set
            {
                lblInternationalLicenseRenewApplicationID.Text = value;
            }
        }

        public string NewLicenseID
        {
            get
            {
                return lblRenewdLocalLicenseID.Text;
            }
            set
            {
                lblRenewdLocalLicenseID.Text = value;
            }
        }

        public string Notes
        {
            get
            {
                return txtNotes.Text;
            }
        }

        public void LoadControlData(int LicenseID)
        {
            clsLicense License = clsLicense.FindLicenseByID(LicenseID);

            if (License == null)
                return;

            lblExpiriationDate.Text = License.ExpirationDate.ToString();
            lblLicenseFees.Text = clsLicenseClass.FindLicenseClassByID((License.LicenseClass)).ClassFees.ToString();
            lblTotalFees.Text = (Convert.ToDecimal(lblApplicationFees.Text) + Convert.ToDecimal(lblLicenseFees.Text)).ToString();
            lblOldLicenseID.Text = License.LicenseID.ToString();
        }

        public void ResetDefaultValues()
        {
            lblApplicationDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            lblIssueDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            lblApplicationFees.Text = clsManageApplicationTypes.FindApplicationType((int)clsManageApplicationTypes.enManageApplicationTypes.RenewDrivingLicenseService).ApplicationFees.ToString();
            lblCreatedBy.Text = GlobalSettings.User.UserName;
        }

    }
}
