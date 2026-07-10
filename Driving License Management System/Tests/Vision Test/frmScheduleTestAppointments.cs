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
using Driving_License_Management_System.Applications.Applications_Types.Controls;

namespace Driving_License_Management_System.Applications.LocalDrivingLicenseApplication
{
    public partial class frmScheduleTestAppointments : Form
    {
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        private int _TestAppointmentID = -1;
        private clsManageTestType _TestType;
        private clsTestAppointment _Appointment;
        public enum enMode { Add = 1, Update = 2 };
        private enMode _Mode = enMode.Add;


        public frmScheduleTestAppointments(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseApplicationByID(LocalDrivingLicenseApplicationID);
            _Mode = enMode.Add;
        }

        public frmScheduleTestAppointments(int TestAppointmentID, bool IsUpdate)
        {
            InitializeComponent();
            _TestAppointmentID = TestAppointmentID;
            _Appointment = clsTestAppointment.FindTestAppointmentByID(_TestAppointmentID);
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseApplicationByID(_Appointment.LocalDrivingLicenseApplicationID);
            _Mode = enMode.Update;

        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            _Appointment.AppointmentDate = ctrlTestsCard1.TestDate;
            _Appointment.TestTypeID = (int)_TestType.TestTypeID;
            _Appointment.IsLocked = false;

            if (_Mode == enMode.Add)
            {
                _Appointment.LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationsID;
                _Appointment.PaidFees = _TestType.TestTypeFees;
                _Appointment.CreatedByUserID = GlobalSettings.User.UserID;
            }

            if (ctrlTestsCard1.ReTakeTestEnabled && _Mode == enMode.Add)
            {
                clsApplication ApplicationRetakeTest = new clsApplication();
                ApplicationRetakeTest.ApplicantPersonID = _LocalDrivingLicenseApplication.ApplicantPersonID;
                ApplicationRetakeTest.ApplicationDate = DateTime.Now;
                ApplicationRetakeTest.ApplicationTypeID = (int)clsManageApplicationTypes.enManageApplicationTypes.RetakeTest;
                ApplicationRetakeTest.ApplicationStatus = clsApplication.enApplicationSatus.New;
                ApplicationRetakeTest.LastStatusDate = DateTime.Now;
                ApplicationRetakeTest.PaidFees = clsManageApplicationTypes.FindApplicationType((int)clsManageApplicationTypes.enManageApplicationTypes.RetakeTest).ApplicationFees;
                ApplicationRetakeTest.CreatedByUserID = GlobalSettings.User.UserID;
                ApplicationRetakeTest.Save();
                _Appointment.RetakeTestApplicationID = ApplicationRetakeTest.ApplicationID;
                ctrlTestsCard1.ReTakeTestApplicationID = _Appointment.RetakeTestApplicationID.ToString();
            }
            if (_Appointment.Save())
            {
                _Mode = enMode.Update;
                MessageBox.Show("Appointment Successfully Added", "Appointment Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                return;
            }
            else
            {
                MessageBox.Show("Something went error while adding the appointment", "Appointment Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }


        private void _LoadData()
        {
            if (_Appointment == null)
            {
                MessageBox.Show("No Appointment With ID = " + _TestAppointmentID, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            ctrlTestsCard1.LoadData(_LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationsID, _TestType, _Appointment.TestAppointmentID);

        }


        private void _ResetDefaultValues()
        {

            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("Something went wrong.", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            _TestType = clsManageTestType.FindTestTypeByID(_LocalDrivingLicenseApplication.PassedTestLevel);

            if (_Mode == enMode.Add)
            {
                _Appointment = new clsTestAppointment();
                ctrlTestsCard1.TestDate = DateTime.Now;
                ctrlTestsCard1.PaidFees = _TestType.TestTypeFees.ToString();
            }
            if (_Mode == enMode.Update)
            {

                _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseApplicationByID(clsTestAppointment.FindTestAppointmentByID(_TestAppointmentID).LocalDrivingLicenseApplicationID);
                if (clsTestAppointment.FindTestAppointmentByID(_TestAppointmentID).IsLocked)
                {
                    btnSave.Enabled = false;
                    ctrlTestsCard1.ResetDateTimePickerToLabel();
                    lblCaution.Visible = true;
                }
            }

            if (_LocalDrivingLicenseApplication.GetTotalSameExamTrialsForLicense((int)_TestType.TestTypeID) >= 1 && _Mode != enMode.Update)
            {
                ctrlTestsCard1.ReTakeTestEnabled = true;
            }
        }

        private void frmScheduleTestAppointments_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();

            if (_Mode == enMode.Add)
            {
                ctrlTestsCard1.LoadData(_LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationsID, _TestType);
            }
            if (_Mode == enMode.Update)
            {
                _LoadData();
            }
            this.Text = ctrlTestsCard1.Title;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
