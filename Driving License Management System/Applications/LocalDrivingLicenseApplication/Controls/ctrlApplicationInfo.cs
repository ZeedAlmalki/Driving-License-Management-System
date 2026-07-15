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

namespace Driving_License_Management_System.Applications.LocalDrivingLicenseApplication.Controls
{
    public partial class ctrlApplicationInfo : UserControl
    {
        public ctrlApplicationInfo()
        {
            InitializeComponent();
        }

        private clsApplication _Application;
        private int _ApplicationID = -1;

        public int ApplicationID
        {
            get { return _ApplicationID; }
        }

        public void ResetpplicationInfo()
        {
            lblApplicationID.Text = "???";
            lblStatus.Text = "???";
            lblFees.Text = "$$$";
            lblApplicationType.Text = "???";
            lblApplicant.Text = "???";
            lblDate.Text = "???";
            lblStatusDate.Text = "???";
            lblCreatedBy.Text = "???";
        }

        private void _FillApplicationInfo()
        {
            lblApplicationID.Text = _Application.ApplicationID.ToString();
            lblStatus.Text = ((clsApplication.enApplicationSatus)_Application.ApplicationStatus).ToString();
            lblFees.Text = _Application.PaidFees.ToString();
            lblApplicationType.Text = _Application.ApplicationType.ApplicationTypeTitle;
            lblApplicant.Text = clsPerson.FindByPersonID(_Application.ApplicantPersonID).FullName();
            lblDate.Text = _Application.ApplicationDate.ToString();
            lblStatusDate.Text = _Application.LastStatusDate.ToString();
            lblCreatedBy.Text = clsUser.Find(_Application.CreatedByUserID).UserName;
        }

        public void LoadApplicationInfo(int ApplicationID)
        {
            _Application = clsApplication.FindApplicationByID(ApplicationID);
            if (_Application == null)
            {
                ResetpplicationInfo();
                //MessageBox.Show("No Local Driving License With ID Number = " + LocalDrivingLicenseApplicationID, "ERROR", MessageBoxButtons.OK);
                return;
            }
            _ApplicationID = _Application.ApplicationID;
            _FillApplicationInfo();
        }

        private void lblShowPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmPersonDetails frmPersonDetails = new frmPersonDetails(_Application.ApplicantPersonID);
            frmPersonDetails.ShowDialog();
            LoadApplicationInfo(ApplicationID);
        }
    }
}
