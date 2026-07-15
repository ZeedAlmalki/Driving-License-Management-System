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
using Driving_License_Management_System.Applications.LocalDrivingLicenseApplication;
using Driving_License_Management_System.License;
using static System.Net.Mime.MediaTypeNames;

namespace Driving_License_Management_System
{
    public partial class frmManageLocalDrivingLicenseApplications : Form
    {
        public frmManageLocalDrivingLicenseApplications()
        {
            InitializeComponent();
        }
        private static DataTable _dtLocalDrivingLicenseApplicationsView;

        private void frmManageLocalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            _dtLocalDrivingLicenseApplicationsView = clsLocalDrivingLicenseApplication.GetLocalDrivingLicenseApplicationsView();
            LocalDrivingLicenseApplicationsGridView.DataSource = _dtLocalDrivingLicenseApplicationsView;
            lblTotalRecords.Text = LocalDrivingLicenseApplicationsGridView.Rows.Count.ToString();
            cbFilterBy.SelectedIndex = 0;

            if (_dtLocalDrivingLicenseApplicationsView.Rows.Count > 0)
            {
                LocalDrivingLicenseApplicationsGridView.Columns[0].HeaderText = "L.D.L.AppID";
                LocalDrivingLicenseApplicationsGridView.Columns[0].Width = 70;

                LocalDrivingLicenseApplicationsGridView.Columns[1].HeaderText = "Driving License";
                LocalDrivingLicenseApplicationsGridView.Columns[1].Width = 120;

                LocalDrivingLicenseApplicationsGridView.Columns[2].HeaderText = "National No.";
                LocalDrivingLicenseApplicationsGridView.Columns[2].Width = 70;

                LocalDrivingLicenseApplicationsGridView.Columns[3].HeaderText = "Full Name";
                LocalDrivingLicenseApplicationsGridView.Columns[3].Width = 170;

                LocalDrivingLicenseApplicationsGridView.Columns[4].HeaderText = "Application Date";
                LocalDrivingLicenseApplicationsGridView.Columns[4].Width = 70;

                LocalDrivingLicenseApplicationsGridView.Columns[5].HeaderText = "Passed Tests";
                LocalDrivingLicenseApplicationsGridView.Columns[5].Width = 50;

                LocalDrivingLicenseApplicationsGridView.Columns[6].HeaderText = "Status";
                LocalDrivingLicenseApplicationsGridView.Columns[6].Width = 100;
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            switch (cbFilterBy.Text)
            {
                case "L.D.LAppID":
                    FilterColumn = "LocalDrivingLicenseApplicationID";
                    break;
                case "National No.":
                    FilterColumn = "NationalNo";
                    break;
                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtLocalDrivingLicenseApplicationsView.DefaultView.RowFilter = "";
                lblTotalRecords.Text = LocalDrivingLicenseApplicationsGridView.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "LocalDrivingLicenseApplicationID")
                _dtLocalDrivingLicenseApplicationsView.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text);
            else
                _dtLocalDrivingLicenseApplicationsView.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text);

            lblTotalRecords.Text = LocalDrivingLicenseApplicationsGridView.Rows.Count.ToString();
        }
        
        private void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "Status";
            string FilterValue = cbStatus.Text;

            //switch (FilterValue)
            //{
            //    case "New":
            //        FilterValue = "New";
            //        break;
            //    case "Cancelled":
            //         FilterValue = "Cancelled";
            //        break;
            //    case "Completed":
            //        FilterValue = "Completed";
            //        break;
            //}

            _dtLocalDrivingLicenseApplicationsView.DefaultView.RowFilter = string.Format($"[{FilterColumn}] LIKE '{FilterValue}%'");
            lblTotalRecords.Text = _dtLocalDrivingLicenseApplicationsView.DefaultView.Count.ToString();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            _dtLocalDrivingLicenseApplicationsView.DefaultView.RowFilter = "";
            lblTotalRecords.Text = _dtLocalDrivingLicenseApplicationsView.DefaultView.Count.ToString();
            if (cbFilterBy.Text == "Status")
            {
                txtFilterValue.Visible = false;
                cbStatus.Visible = true;
                cbStatus.Focus();
                cbStatus.SelectedIndex = -1;
            }
            else
            {
                txtFilterValue.Visible = (cbFilterBy.Text != "None");
                cbStatus.Visible = false;
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            string selectedFilter = cbFilterBy.Text;

            if (selectedFilter == "L.D.LAppID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
            else if (selectedFilter == "None")
            {
                e.Handled = true;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void canceloolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do you want to cancel this Application", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                return;

            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseApplicationByID((int)LocalDrivingLicenseApplicationsGridView.CurrentRow.Cells[0].Value);

            if (LocalDrivingLicenseApplication.Cancel())
            {
                MessageBox.Show("Cancalled Has Been Successuflly");
                frmManageLocalDrivingLicenseApplications_Load(null, null);
            }
            else
            {
                MessageBox.Show("Something went error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddNewLocalDrivingLicenseApplication frmAddNewLocalDrivingLicenseApplication = new frmAddNewLocalDrivingLicenseApplication();
            frmAddNewLocalDrivingLicenseApplication.ShowDialog();
            frmManageLocalDrivingLicenseApplications_Load(null, null);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddNewLocalDrivingLicenseApplication frmAddNewLocalDrivingLicenseApplication = new frmAddNewLocalDrivingLicenseApplication((int)LocalDrivingLicenseApplicationsGridView.CurrentRow.Cells[0].Value);
            frmAddNewLocalDrivingLicenseApplication.ShowDialog();
            frmManageLocalDrivingLicenseApplications_Load(null, null);
        }

        private void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do you want to delete this Application", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                return;

            if (clsLocalDrivingLicenseApplication.DeleteLocalDrivingLicenseApplicationByID((int)LocalDrivingLicenseApplicationsGridView.CurrentRow.Cells[0].Value))
            {
                MessageBox.Show("Deleted Has Been Successuflly", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmManageLocalDrivingLicenseApplications_Load(null, null);
            }
            else
            {
                MessageBox.Show("Something Went Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowLocalDrivingLicenseApplicationInfo frmShowLocalDriving = new frmShowLocalDrivingLicenseApplicationInfo((int)LocalDrivingLicenseApplicationsGridView.CurrentRow.Cells[0].Value);
            frmShowLocalDriving.ShowDialog();
            frmManageLocalDrivingLicenseApplications_Load(null, null);
        }

        private void cmsLocalDrivingLicenseApplications_Opening(object sender, CancelEventArgs e)
        {
            //clsLocalDrivingLicenseApplication drv = clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseApplicationByID((int)LocalDrivingLicenseApplicationsGridView.CurrentRow.Cells[0].Value);
            //issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
            //showLicenseToolStripMenuItem.Enabled = false;


            //if (drv.GetPassedTestCount() == 1)
            //{
            //    scheduleVisionTestToolStripMenuItem.Enabled = false;
            //    scheduleWrittenTestToolStripMenuItem.Enabled = true;
            //}
            //else if (drv.GetPassedTestCount() == 2)
            //{
            //    scheduleWrittenTestToolStripMenuItem.Enabled= false;
            //    scheduleStreetTestToolStripMenuItem.Enabled = true;
            //}
            //// you have to write something here to see if license has been issued to turn it off and turn on the 'show license'
            //else if (drv.GetPassedTestCount() == 3)
            //{
            //    scheduleTestsToolStripMenuItem.Enabled = false;
            //    editToolStripMenuItem.Enabled = false;
            //    deleteApplicationToolStripMenuItem.Enabled = false;
            //    canceloolStripMenuItem.Enabled = false;
            //    scheduleStreetTestToolStripMenuItem.Enabled = false;
            //    issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = true;

            //    if (!(drv.ApplicationStatus == clsApplication.enApplicationSatus.Cancelled))
            //        showLicenseToolStripMenuItem.Enabled = true;
            //}

        }

        private void ApplyColorsToAllItems(ToolStripItemCollection items)
        {
            foreach (ToolStripItem item in items)
            {
                if (item is ToolStripMenuItem menuItem)
                {
                    if (!menuItem.Enabled)
                    {
                        menuItem.BackColor = Color.FromArgb(160, 160, 160);
                    }
                    else
                    {
                        menuItem.BackColor = Color.FromArgb(244, 246, 250);
                    }
                    if (menuItem.HasDropDownItems)
                    {
                        ApplyColorsToAllItems(menuItem.DropDownItems);
                    }
                }
            }
        }


        private void cmsManageLocalDrivingLicenseDefaultValue()
        {
            scheduleTestsToolStripMenuItem.Enabled = true;
            editToolStripMenuItem.Enabled = true;
            deleteApplicationToolStripMenuItem.Enabled = true;
            canceloolStripMenuItem.Enabled = true;
            scheduleStreetTestToolStripMenuItem.Enabled = false;
            scheduleVisionTestToolStripMenuItem.Enabled = false;
            scheduleWrittenTestToolStripMenuItem.Enabled = false;
            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
            showLicenseToolStripMenuItem.Enabled = false;
            scheduleWrittenTestToolStripMenuItem.Enabled = false;
        }
        private void LocalDrivingLicenseApplicationsGridView_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            int localDrivingLicenseApplicationID = (int)LocalDrivingLicenseApplicationsGridView.Rows[e.RowIndex].Cells[0].Value;

            clsLocalDrivingLicenseApplication drv = clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseApplicationByID(localDrivingLicenseApplicationID);

            if (drv == null)
            {
                return;
            }

            cmsManageLocalDrivingLicenseDefaultValue();

            int PassedTestCount = drv.GetPassedTestCount();

            if (PassedTestCount == 0)
            {
                scheduleVisionTestToolStripMenuItem.Enabled = true;
            }

            if (PassedTestCount == 1)
            {

                scheduleWrittenTestToolStripMenuItem.Enabled = true;
            }

            if (PassedTestCount == 2)
            {
                scheduleStreetTestToolStripMenuItem.Enabled = true;
            }

            if (PassedTestCount == 3 || (drv.ApplicationStatus == clsApplication.enApplicationSatus.Cancelled))
            {
                if (drv.ApplicationStatus != clsApplication.enApplicationSatus.Cancelled)
                {
                    issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = (!drv.ItHasLocalDrivingLicenseClassBefore());
                    showLicenseToolStripMenuItem.Enabled = (drv.ItHasLocalDrivingLicenseClassBefore()); // if we paramataraze the license id and person id to frmLDLAPInfo we can use it up,
                    // and that will be more fleixble because if license has issued for the reason who failed we can know.
                }


                scheduleTestsToolStripMenuItem.Enabled = false;
                editToolStripMenuItem.Enabled = false;
                deleteApplicationToolStripMenuItem.Enabled = false;
                canceloolStripMenuItem.Enabled = false;
                scheduleStreetTestToolStripMenuItem.Enabled = false;
            }

            ApplyColorsToAllItems(cmsLocalDrivingLicenseApplications.Items);
        }

        private void scheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTestAppointments frmTestAppointments = new frmTestAppointments((int)LocalDrivingLicenseApplicationsGridView.CurrentRow.Cells[0].Value);
            frmTestAppointments.ShowDialog();
            frmManageLocalDrivingLicenseApplications_Load(null, null);
        }

        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmIssueDriverLicenseForTheFirstTime IssueDrivingLicenseFirstTime = new frmIssueDriverLicenseForTheFirstTime((int)LocalDrivingLicenseApplicationsGridView.CurrentRow.Cells[0].Value);
            IssueDrivingLicenseFirstTime.ShowDialog();
            frmManageLocalDrivingLicenseApplications_Load(null, null);
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLicenseInfo LicenseInfo = new frmLicenseInfo((int)LocalDrivingLicenseApplicationsGridView.CurrentRow.Cells[0].Value);
            LicenseInfo.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowLicensesHistory licensesHistory = new frmShowLicensesHistory((string)LocalDrivingLicenseApplicationsGridView.CurrentRow.Cells[2].Value);
            licensesHistory.ShowDialog();
        }
    }
}
