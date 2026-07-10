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
using Driving_License_Management_System.Users;

namespace Driving_License_Management_System.Applications.Applications_Types.Controls
{
    public partial class ctrlDrivingLicenseApplicationInfo : UserControl
    {

        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        private int _LocalDrivingLicenseApplicationID = -1;

        public int LocalDrivingLicenseApplicationID
        {
            get { return _LocalDrivingLicenseApplicationID; }
        }

        public clsLocalDrivingLicenseApplication SelectedLocalDrivingLicense
        {
            get { return _LocalDrivingLicenseApplication; }
        }


       

        public ctrlDrivingLicenseApplicationInfo()
        {
            InitializeComponent();
        }

        private void _FillLocalDrivingLicenseApplicationInfo()
        {
            lblDLappID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationsID.ToString();
            lblAppliedForLicense.Text = clsLicenseClass.FindLicenseClassByID(_LocalDrivingLicenseApplication.LicenseClassID).ClassName.ToString();
            lblPassedTests.Text = _LocalDrivingLicenseApplication.GetPassedTestCount() + "/3";
            // ^ Driving License Application Info.

            lblApplicationID.Text = _LocalDrivingLicenseApplication.ApplicationID.ToString();
            lblStatus.Text = ((clsApplication.enApplicationSatus)_LocalDrivingLicenseApplication.ApplicationStatus).ToString();
            lblFees.Text = _LocalDrivingLicenseApplication.PaidFees.ToString();
            lblApplicationType.Text = _LocalDrivingLicenseApplication.ApplicationType.ApplicationTypeTitle;
            lblApplicant.Text = clsPerson.Find(_LocalDrivingLicenseApplication.ApplicantPersonID).FullName();
            lblDate.Text = _LocalDrivingLicenseApplication.ApplicationDate.ToString();
            lblStatusDate.Text = _LocalDrivingLicenseApplication.LastStatusDate.ToString();
            lblCreatedBy.Text = clsUser.Find(_LocalDrivingLicenseApplication.CreatedByUserID).UserName;
        }

        private void _ResetLocalDrivingLicenseApplicationInfo()
        {
            lblDLappID.Text = "???";
            lblAppliedForLicense.Text = "???";
            lblPassedTests.Text = "???";
            // ^ Driving License Application Info.
            lblApplicationID.Text = "???";
            lblStatus.Text = "???";
            lblFees.Text = "???";
            lblApplicationType.Text = "???";
            lblApplicant.Text = "???";
            lblDate.Text = "???";
            lblStatusDate.Text = "???";
            lblCreatedBy.Text = "???";
        }

        public void LoadLocalDrivingLicenseApplicationInfo(int LocalDrivingLicenseApplicationID)
        {
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseApplicationByID(LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                _ResetLocalDrivingLicenseApplicationInfo();
                //MessageBox.Show("No Local Driving License With ID Number = " + LocalDrivingLicenseApplicationID, "ERROR", MessageBoxButtons.OK);
                return;
            }
            _FillLocalDrivingLicenseApplicationInfo();
        }



        private void lblEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmPersonDetails frmPersonDetails = new frmPersonDetails(_LocalDrivingLicenseApplication.ApplicantPersonID);
            frmPersonDetails.ShowDialog();
            LoadLocalDrivingLicenseApplicationInfo(_LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationsID);
        }
    }
}
