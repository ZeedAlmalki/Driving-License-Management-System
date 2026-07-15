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
using Guna.UI2.WinForms;
using static BusinessLayer.clsManageTestType;

namespace Driving_License_Management_System.Applications.LocalDrivingLicenseApplication.Controls
{
    public partial class ctrlTestsCard : UserControl
    {

        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        private clsTestAppointment _TestAppointment;
        private clsManageTestType _TestType;
        public enum enMode { AddNew = 1, Update = 1, TakeTest = 2};
        private enMode _Mode = enMode.AddNew;

        public static void SetTestTypeSettings(int testTypeID, Guna2HtmlLabel lblTitle, PictureBox pbTestPicture)
        {
            if (lblTitle == null || pbTestPicture == null) return;

            switch (testTypeID)
            {
                case 1:
                    lblTitle.Text = "Vision Test Appointments";
                    pbTestPicture.Image = Properties.Resources.Vision_512;
                    break;
                case 2:
                    lblTitle.Text = "Written Test Appointments";
                    pbTestPicture.Image = Properties.Resources.Written_Test_512;
                    break;
                case 3:
                    lblTitle.Text = "Street Test Appointments";
                    pbTestPicture.Image = Properties.Resources.Schedule_Test_512;
                    break;
            }
        }

        void SetFormSettings()
        {
            SetTestTypeSettings((int)_TestType.TestTypeID, lblTitle, pbTestPicture);
            gbTest.Text = lblTitle.Text;
        }

        public ctrlTestsCard()
        {
            InitializeComponent();
        }

        private int _LocalDrivingLicenseApplicationID;
        private int _TestAppointmentID;


        public DateTime TestDate
        {
            get { return dtpTestDate.Value; }
            set { dtpTestDate.Value = value; }
        }

        public string PaidFees
        {
            set { __lblFees.Text = value; }

            get { return __lblFees.Text; }
        }

        public string RetakeTestFees
        {
            set { _lblReTakeApplicationFees.Text = value; }
            get { return _lblReTakeApplicationFees.Text; }
        }

        public string TotalFees
        {
            set { _lblTotalFees.Text = value; }
            get { return (_lblTotalFees.Text); }
        }

        public string ReTakeTestApplicationID
        {
            set { lblReTakeTestApplicatoinID.Text = value; }
            get { return lblReTakeTestApplicatoinID.Text; }
        }

        public string Title
        {
            get { return lblTitle.Text; }
        }


        private bool _ReTakeTestEnabled = false;
        private bool _ReTakeTestInvisible = false;


        private void _ResetTestsInfo()
        {
            _LocalDrivingLicenseApplicationID = -1;
            _TestAppointmentID = -1;
            _TestType = null;
            _LocalDrivingLicenseApplication = null;
            _TestAppointment = null;
            lblDLAppID.Text = string.Empty;
            lblDriverLicenseClass.Text = string.Empty;
            __lblFees.Text = string.Empty;
            lblName.Text = string.Empty;
            _lblReTakeApplicationFees.Text = "0";
            lblReTakeTestApplicatoinID.Text = "N/A";
            _lblTotalFees.Text = "0";   
            lblTrial.Text = string.Empty;
            dtpTestDate.Value = DateTime.Now;
            _ReTakeTestEnabled = false;
        }

 
        public bool ReTakeTestInvisible
        {
            get
            {
                return _ReTakeTestInvisible;
            }
            set
            {
                _ReTakeTestInvisible = value;
                gbRetakeTest.Visible = !(_ReTakeTestInvisible);
            }
        }

        public bool ReTakeTestEnabled
        {
            get
            {
                return _ReTakeTestEnabled;
            }
            set
            {
                _ReTakeTestEnabled = value;
                gbRetakeTest.Enabled = _ReTakeTestEnabled;
            }
        }

        public void ResetDateTimePickerToLabel()
        {
            lblDate.Visible = true;
            lblDate.Text = dtpTestDate.Text;
            dtpTestDate.Visible = false;
        }

        private void _FillLocalDrivingLicenseApplicationInfo()
        {
            if (_TestType == null || _LocalDrivingLicenseApplication == null)
            {
                return;
            }
            



            if (_TestAppointmentID != -1) // equal update
            {

                _TestAppointment = clsTestAppointment.FindTestAppointmentByID(_TestAppointmentID);
                _TestType = clsManageTestType.FindTestTypeByID((clsManageTestType.enTestType)clsTestAppointment.FindTestAppointmentByID(_TestAppointment.TestAppointmentID).TestTypeID); // if update, we return the right typeid, because if it is update.
                SetFormSettings();

                if (_TestAppointment != null)
                {
                    __lblFees.Text = _TestAppointment.PaidFees.ToString(); // got the paid fees from the real application (some system do discount so we have to got the paid from real application, not the static usually fees)
                    dtpTestDate.Value = _TestAppointment.AppointmentDate;

                    if (_Mode == enMode.TakeTest)
                    {
                        lblTitle.Text = "Take " + _TestType.TestTypeTitle;
                    }
                    else 
                    {
                        lblTitle.Text = "Edit " + _TestType.TestTypeTitle;
                    }

                    if (_TestAppointment.RetakeTestApplicationID != -1) // its update and its after failed , so we got the fees from the real application, and the retake id.
                    {
                        ReTakeTestApplicationID = _TestAppointment.RetakeTestApplicationID.ToString();
                        _lblReTakeApplicationFees.Text = clsApplication.FindApplicationByID(_TestAppointment.RetakeTestApplicationID).PaidFees.ToString();
                    }
                }
            }
            else
            {
                lblTitle.Text = "Schedule " + _TestType.TestTypeTitle;
                __lblFees.Text = _TestType.TestTypeFees.ToString();
                if (_LocalDrivingLicenseApplication.ItHasAppointmentTestBefore((int)_TestType.TestTypeID)) // its add, and its has appointment before (Failed) so we add the retaketest fees.
                {
                    _lblReTakeApplicationFees.Text = clsManageApplicationTypes.FindApplicationType((int)clsManageApplicationTypes.enManageApplicationTypes.RetakeTest).ApplicationFees.ToString();
                }
            }

            lblDLAppID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationsID.ToString();
            lblDriverLicenseClass.Text = clsLicenseClass.FindLicenseClassByID(_LocalDrivingLicenseApplication.LicenseClassID).ClassName;
            lblName.Text = clsPerson.FindByPersonID(_LocalDrivingLicenseApplication.ApplicantPersonID).FullName();
            int Trail = _LocalDrivingLicenseApplication.GetTotalSameExamTrialsForLicense((int)_TestType.TestTypeID);
            lblTrial.Text = Trail.ToString();


            if (!decimal.TryParse(__lblFees?.Text, out decimal fees))
            {
                fees = 0;
            }

            if (!decimal.TryParse(_lblReTakeApplicationFees?.Text, out decimal retakeFees))
            {
                retakeFees = 0;
            }

            if (_lblTotalFees != null)
            {
                _lblTotalFees.Text = (fees + retakeFees).ToString("G29"); 
            }
        }

        public void LoadData(int sendLocalDrivingLicenseApplicationID, clsManageTestType TestType, int TestAppointmentID = -1, enMode Mode = enMode.AddNew)
        {
            _LocalDrivingLicenseApplicationID = sendLocalDrivingLicenseApplicationID;
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseApplicationByID(_LocalDrivingLicenseApplicationID);

            _Mode = Mode;

            if (_TestAppointmentID != -1 && _Mode == enMode.AddNew)
            {
                _Mode = enMode.Update;
            }

            _TestAppointmentID = TestAppointmentID;

            _TestType = TestType;
            SetFormSettings();

            if (_LocalDrivingLicenseApplication == null )
            {
                MessageBox.Show("No LocalDrivingLicenseApplication With ID = " + sendLocalDrivingLicenseApplicationID, "ERROR", MessageBoxButtons.OK);
                _ResetTestsInfo();
                return;
            }
            _FillLocalDrivingLicenseApplicationInfo();
        }



    }
}
