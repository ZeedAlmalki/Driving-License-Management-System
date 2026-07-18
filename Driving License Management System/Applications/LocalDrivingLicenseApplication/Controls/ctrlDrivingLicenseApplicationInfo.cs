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
using Driving_License_Management_System.License;
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
            int PassedTestCount = _LocalDrivingLicenseApplication.GetPassedTestCount();

            lblDLappID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationsID.ToString();
            lblAppliedForLicense.Text = clsLicenseClass.FindLicenseClassByID(_LocalDrivingLicenseApplication.LicenseClassID).ClassName.ToString();
            lblPassedTests.Text = PassedTestCount + "/3";


            // ^ Driving License Application Info.
            lblShowLicenseInfo.Enabled = (PassedTestCount == 3 && _LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationSatus.Completed);
            ctrlApplicationInfo1.LoadApplicationInfo(_LocalDrivingLicenseApplication.ApplicationID);
        }

        private void _ResetLocalDrivingLicenseApplicationInfo()
        {
            lblDLappID.Text = "???";
            lblAppliedForLicense.Text = "???";
            lblPassedTests.Text = "???";
            ctrlApplicationInfo1.ResetpplicationInfo();
            // ^ Driving License Application Info.

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
            _LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationsID;
            _FillLocalDrivingLicenseApplicationInfo();
        }



        private void lblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clsLocalDrivingLicenseApplication clsLocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseApplicationByID(LocalDrivingLicenseApplicationID);
            clsLicense License = clsLicense.FindActiveLicenseByLicenseClassIDAndPersonID(clsLocalDrivingLicenseApplication.ApplicantPersonID, clsLocalDrivingLicenseApplication.LicenseClassID);

            frmLicenseInfo LicenseInfo = new frmLicenseInfo(LocalDrivingLicenseApplicationID);
            LicenseInfo.ShowDialog();
        }
    }
}
