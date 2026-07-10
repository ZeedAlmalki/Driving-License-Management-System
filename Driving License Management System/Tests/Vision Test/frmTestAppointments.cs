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
using Driving_License_Management_System.Tests.Tests;
using static BusinessLayer.clsManageTestType;

namespace Driving_License_Management_System.Applications.LocalDrivingLicenseApplication
{
    public partial class frmTestAppointments : Form
    {
        private clsLocalDrivingLicenseApplication _clsLocalDrivingLicenseApplication;
        int TotalPassedTests = 0;
        private int _TestTypeID = -1;
        private DataTable _dtAllTestAppointments;

        void SetFormSettings()
        {
            ctrlTestsCard.SetTestTypeSettings(TotalPassedTests, lblTestTitle, pbTestPicture);


            this.Text = lblTestTitle.Text;
        }

        private void frmTestAppointments_Load(object sender, EventArgs e)
        {
            TotalPassedTests = _clsLocalDrivingLicenseApplication.GetPassedTestCount();
            if (TotalPassedTests < 3)
            {
                TotalPassedTests++;
            }

            _dtAllTestAppointments = clsTestAppointment.GetAllTestAppointments(_clsLocalDrivingLicenseApplication.LocalDrivingLicenseApplicationsID, TotalPassedTests);
            SetFormSettings();
            if (_clsLocalDrivingLicenseApplication != null)
            {
                ctrlDrivingLicenseApplicationInfo1.LoadLocalDrivingLicenseApplicationInfo(_clsLocalDrivingLicenseApplication.LocalDrivingLicenseApplicationsID);
            }
            else
            {
                MessageBox.Show("No Local Driving License With ID " + _clsLocalDrivingLicenseApplication.LocalDrivingLicenseApplicationsID);
                this.Close();
                return;
            }

            if (_dtAllTestAppointments == null)
            {
                MessageBox.Show("Table was Null, Please Contact the Admin", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            if (_dtAllTestAppointments.Rows.Count == 0)
            {
                TestAppointmentsDataGridView.DataSource = null;
                lblTotalRecords.Text = "0";
                return;
            }


            _dtAllTestAppointments = _dtAllTestAppointments.DefaultView.ToTable(false, "TestAppointmentID", "AppointmentDate",
    "PaidFees", "IsLocked");
            TestAppointmentsDataGridView.DataSource = _dtAllTestAppointments;
            lblTotalRecords.Text = TestAppointmentsDataGridView.Rows.Count.ToString();


            if (TestAppointmentsDataGridView.Rows.Count > 0)
            {
                TestAppointmentsDataGridView.Columns[0].HeaderText = "Appointment ID";
                TestAppointmentsDataGridView.Columns[0].Width = 70;

                TestAppointmentsDataGridView.Columns[1].HeaderText = "Appointment Date";
                TestAppointmentsDataGridView.Columns[1].Width = 100;

                TestAppointmentsDataGridView.Columns[2].HeaderText = "Paid Fees";
                TestAppointmentsDataGridView.Columns[2].Width = 80;


                TestAppointmentsDataGridView.Columns[3].HeaderText = "Is Locked";
                TestAppointmentsDataGridView.Columns[3].Width = 35;
                TestAppointmentsDataGridView.Columns[3].ReadOnly = true;
            }


        }
        public frmTestAppointments(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _clsLocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseApplicationByID(LocalDrivingLicenseApplicationID);
            if (_clsLocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("Something went error");
                this.Close();
                return;
            }

            _TestTypeID = (int)_clsLocalDrivingLicenseApplication.PassedTestLevel;
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            //foreach (DataRow row in _dtAllTestAppointments.Rows)
            //{
            //    bool IsLocked = Convert.ToBoolean(row["IsLocked"]);

            //    if (!IsLocked)
            //    {
            //        MessageBox.Show("Person Is Already Has An Active Appointment For This Test, You Cannot Add New Appointment", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return;
            //    }

            if (_clsLocalDrivingLicenseApplication.IsAnActiveAppointmentExist(TotalPassedTests))
            {
                MessageBox.Show("Person Is Already Has An Active Appointment For This Test, You Cannot Add New Appointment", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_clsLocalDrivingLicenseApplication.IsPassedAppointmentTestBefore(TotalPassedTests))
            {
                MessageBox.Show("Person Is Passed This Test, You Cannot Add New Appointment", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            

            frmScheduleTestAppointments frmScheduleTestAppointments = new frmScheduleTestAppointments(_clsLocalDrivingLicenseApplication.LocalDrivingLicenseApplicationsID);
            frmScheduleTestAppointments.ShowDialog();
            frmTestAppointments_Load(null, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmScheduleTestAppointments frmScheduleTestAppointments = new frmScheduleTestAppointments((int)TestAppointmentsDataGridView.CurrentRow.Cells[0].Value, true);
            frmScheduleTestAppointments.ShowDialog();
            frmTestAppointments_Load(null, null);
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (clsTestAppointment.FindTestAppointmentByID((int)TestAppointmentsDataGridView.CurrentRow.Cells[0].Value).IsLocked)
            {
                MessageBox.Show("Person Has ALready Takes his Test", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmTakeTest frmTakeTest = new frmTakeTest((int)TestAppointmentsDataGridView.CurrentRow.Cells[0].Value);
            frmTakeTest.ShowDialog();
            frmTestAppointments_Load(null, null);
        }
    }
}
