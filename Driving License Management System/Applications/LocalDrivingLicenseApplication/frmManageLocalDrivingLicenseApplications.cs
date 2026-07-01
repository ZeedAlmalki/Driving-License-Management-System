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
            clsApplication application = clsApplication.FindApplicationByID(clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseApplicationByID((int)LocalDrivingLicenseApplicationsGridView.CurrentRow.Cells[0].Value).ApplicationID);
            if (application.ApplicationStatus == clsApplication.enApplicationSatus.Cancelled)
            {
                MessageBox.Show("Application Is Already Cancelled, you can not Cancel a Cancalled Application");
                return;
            }
            application.ApplicationStatus = clsApplication.enApplicationSatus.Cancelled;
            application.LastStatusDate = DateTime.Now;
            if (application.Save())
            {
                MessageBox.Show("Cancalled Has Been Successuflly");
                frmManageLocalDrivingLicenseApplications_Load(null, null);
            }
            else
            {
                MessageBox.Show("Something went error");
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
            if (clsLocalDrivingLicenseApplication.DeleteLocalDrivingLicenseApplicationByID((int)LocalDrivingLicenseApplicationsGridView.CurrentRow.Cells[0].Value))
            {
                MessageBox.Show("Deleted Has Been Successuflly");
                frmManageLocalDrivingLicenseApplications_Load(null, null);
            }
            else
            {
                MessageBox.Show("Something Went Error");
            }
        }
    }
}
