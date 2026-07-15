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
using Driving_License_Management_System.Applications.LocalDrivingLicenseApplication.Controls;

namespace Driving_License_Management_System.Tests.Tests
{
    public partial class frmTakeTest : Form
    {
        private int _TestAppointmendID;
        private int _Trial = 0;
        private clsTestAppointment _TestAppointment;
        private clsApplication _Application;
        clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;

        public frmTakeTest(int TestAppointmentID)
        {
            InitializeComponent();
            _TestAppointmendID = TestAppointmentID;
            _TestAppointment = clsTestAppointment.FindTestAppointmentByID(TestAppointmentID);
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseApplicationByID(_TestAppointment.LocalDrivingLicenseApplicationID);
            clsManageTestType TestType = clsManageTestType.FindTestTypeByID((clsManageTestType.enTestType)_TestAppointment.TestTypeID);
            ctrlTestsCard1.LoadData(_TestAppointment.LocalDrivingLicenseApplicationID, TestType, _TestAppointmendID, ctrlTestsCard.enMode.TakeTest);
            this.Text = ctrlTestsCard1.Title;
        }

        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            ctrlTestsCard1.ReTakeTestInvisible = true;
            ctrlTestsCard1.ResetDateTimePickerToLabel();
            rbPass.Checked = true;
            _Trial = _LocalDrivingLicenseApplication.GetTotalSameExamTrialsForLicense(_TestAppointment.TestTypeID);
            if (_Trial >= 1)
            {
                ctrlTestsCard1.PaidFees = ctrlTestsCard1.TotalFees;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            clsTests Test = new clsTests();
            Test.Notes = txtNotes.Text;
            Test.TestAppointmentID = _TestAppointmendID;
            Test.CreatedByUserID = GlobalSettings.User.UserID;
            Test.TestResult = rbPass.Checked;



            if (MessageBox.Show("Are you sure you want to save? After you save you cannot Change the Pass/Fail Results", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                if (_Trial >= 1)
                {
                    _Application = clsApplication.FindApplicationByID(_TestAppointment.RetakeTestApplicationID);
                    if (_Application != null)
                    {
                        _Application.ApplicationStatus = clsApplication.enApplicationSatus.Completed;
                        _Application.LastStatusDate = DateTime.Now;
                        _Application.Save();
                    }
                }

                if (Test.Save())
                {
                    _TestAppointment.LockTestAppointment(); 
                    // ^ we can use query to lock test appointment, in the Save expression the clsTestData.
                    // that will be better.

                    
                    lblTestID.Text = Test.TestID.ToString();
                    btnSave.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Something went wrong while saving data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }
    }
}
