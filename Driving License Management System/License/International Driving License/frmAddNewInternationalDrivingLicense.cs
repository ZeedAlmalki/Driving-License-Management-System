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
using static BusinessLayer.clsApplication;

namespace Driving_License_Management_System.License.International_Driving_License
{
    public partial class frmAddNewInternationalDrivingLicense : Form
    {
        public frmAddNewInternationalDrivingLicense()
        {
            InitializeComponent();
        }

        private int _LicenseID = -1;
        private clsLicense _License;
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to issued this driver license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;

            _License = clsLicense.FindLicenseByID(_LicenseID);

            clsInternationalLicense InternationalLicense = new clsInternationalLicense(_LicenseID, GlobalSettings.User.UserID);

            if (InternationalLicense.Save())
            {
                ctrlInternationalDrivingLicenseApplicationInfo1.ILApplicationID = InternationalLicense.ApplicationID;
                ctrlInternationalDrivingLicenseApplicationInfo1.ILLicenseID = InternationalLicense.InternationalLicenseID;

                btnIssue.Enabled = false;
                lblShowLicenseInfo.Enabled = true;
                lblShowLicenseHistory.Enabled = true;
                ctrllFindLocalDrivingLicense1.FilterEnabled = false;
                MessageBox.Show("International Saved Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                MessageBox.Show("Something went error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void frmAddNewInternationalDrivingLicense_Load(object sender, EventArgs e)
        {
            ctrlInternationalDrivingLicenseApplicationInfo1.ResetDefaultValue();
            btnIssue.Enabled = false;
            lblShowLicenseHistory.Enabled = false;
            lblShowLicenseInfo.Enabled = false;
        }

        private void lblShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicensesHistory frmShowLicensesHistory = new frmShowLicensesHistory(clsLicense.FindLicenseByID(_LicenseID).PersonID);
            frmShowLicensesHistory.ShowDialog();
        }

        private void lblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmInternationalLicenseDriverInfo frmInternationalLicenseDriverInfo = new frmInternationalLicenseDriverInfo(ctrlInternationalDrivingLicenseApplicationInfo1.ILLicenseID);
            frmInternationalLicenseDriverInfo.ShowDialog();
        }


        private void HandleLicenseSelected(int obj, bool AllowUpdate)
        {
            _LicenseID = obj;
            if (_LicenseID == -1)
            {
                return;
            }


            _License = clsLicense.FindLicenseByID(_LicenseID);

            int outLicenseID = -1;
            if (!clsLocalDrivingLicenseApplication.ItHasLocalDrivingLicenseClassBefore((int)clsLicenseClass.LicenseClass.OrdinaryDrivingLicense, _License.PersonID, ref outLicenseID))
            {
                MessageBox.Show("You Must have an Ordiranry License class before you apply in International license..", "Must be Have correct license", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (outLicenseID != -1)
            {
                _LicenseID = outLicenseID;
                _License = clsLicense.FindLicenseByID(_LicenseID);
                ctrllFindLocalDrivingLicense1.AsignLicenseID = outLicenseID.ToString();
            }

            if (!ctrllFindLocalDrivingLicense1.LoadDataByLicenseID(_License.LicenseID))
            {
                MessageBox.Show("Something went error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ctrlInternationalDrivingLicenseApplicationInfo1.LoadData(_LicenseID);

            if (!_License.IsActive)
            {
                MessageBox.Show("The License You use is not active, please active it", "Must be active", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (DateTime.Now > _License.ExpirationDate)
            {
                MessageBox.Show("The License You use is Expired", "Expired", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (clsInternationalLicense.ItHasInternationalDrivingLicense(_LicenseID))
            {
                MessageBox.Show("You already have an active internatinoal license.", "You already have an internatnioal license.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            btnIssue.Enabled = true;


        }
    }
}
