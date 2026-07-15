using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer;
using Driving_License_Management_System.Applications.LocalDrivingLicenseApplication;
using static System.Net.Mime.MediaTypeNames;

namespace Driving_License_Management_System.License.International_Driving_License
{
    public partial class frmListInternationalLicense : Form
    {
        DataTable _dtAllInternationalLicenses;
        public frmListInternationalLicense()
        {
            InitializeComponent();
        }

        private void frmListInternationalLicense_Load(object sender, EventArgs e)
        {
            _dtAllInternationalLicenses = clsInternationalLicense.GetAllInternationalLicenses();



            if (_dtAllInternationalLicenses != null && _dtAllInternationalLicenses.Rows.Count > 0)
            {
                _dtAllInternationalLicenses = _dtAllInternationalLicenses.DefaultView.ToTable(false, "InternationalLicenseID", "ApplicationID", "DriverID", "IssuedUsingLocalLicenseID", "IssueDate", "ExpirationDate", "IsActive");

                InternationalDriverLicenseDataGridView.DataSource = _dtAllInternationalLicenses;
            }

            if (InternationalDriverLicenseDataGridView.Rows.Count > 0)
            {
                InternationalDriverLicenseDataGridView.Columns[0].HeaderText = "Inter.License ID";
                InternationalDriverLicenseDataGridView.Columns[0].Width = 80;


                InternationalDriverLicenseDataGridView.Columns[1].HeaderText = "App.ID";
                InternationalDriverLicenseDataGridView.Columns[1].Width = 70;

                InternationalDriverLicenseDataGridView.Columns[2].HeaderText = "Driver ID";
                InternationalDriverLicenseDataGridView.Columns[2].Width = 70;

                InternationalDriverLicenseDataGridView.Columns[3].HeaderText = "L.License ID";
                InternationalDriverLicenseDataGridView.Columns[3].Width = 150;


                InternationalDriverLicenseDataGridView.Columns[4].HeaderText = "Issue Date";
                InternationalDriverLicenseDataGridView.Columns[4].Width = 100;


                InternationalDriverLicenseDataGridView.Columns[5].HeaderText = "Expiration Date";
                InternationalDriverLicenseDataGridView.Columns[5].Width = 100;


                InternationalDriverLicenseDataGridView.Columns[6].HeaderText = "Is Active";
                InternationalDriverLicenseDataGridView.Columns[6].Width = 80;
                InternationalDriverLicenseDataGridView.Columns[6].ReadOnly = true;
            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "None")
            {
                txtFilterValue.Text = "";
                txtFilterValue.Visible = false;
                cbIsActive.Visible = false;
                return;
            }
            else if (cbFilterBy.Text == "Is Active")
            {
                txtFilterValue.Text = "";
                txtFilterValue.Visible = false;
                cbIsActive.Visible = true;
                return;
            }
            else
            {
                cbIsActive.Visible = false;
                txtFilterValue.Text = "";
                txtFilterValue.Visible = true;
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            switch (cbFilterBy.Text)
            {
                case "Local License ID":
                    FilterColumn = "IssuedUsingLocalLicenseID";
                    break;

                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;

                case "International License ID":
                    FilterColumn = "InternationalLicenseID";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }

            if (string.IsNullOrWhiteSpace(txtFilterValue.Text) || FilterColumn == "None")
            {
                _dtAllInternationalLicenses.DefaultView.RowFilter = "";
                lblTotalRecords.Text = InternationalDriverLicenseDataGridView.Rows.Count.ToString();
                return; 
            }

            _dtAllInternationalLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text);
            lblTotalRecords.Text = InternationalDriverLicenseDataGridView.Rows.Count.ToString();
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsActive";
            string FilterValue = cbIsActive.Text;
            switch (FilterValue)
            {
                case "All":
                    break;
                case "Active":
                    FilterValue = "1";
                    break;
                case "Deactive":
                    FilterValue = "0";
                    break;
            }
            if (FilterValue == "All")
                _dtAllInternationalLicenses.DefaultView.RowFilter = "";
            else
                _dtAllInternationalLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);
            lblTotalRecords.Text = _dtAllInternationalLicenses.DefaultView.Count.ToString();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            string SelectedFilter = cbFilterBy.Text;
            if (SelectedFilter == "None")
            {
                e.Handled = true;
                return;
            }
            // if, every thing is digit in the filter, so it's not important right now.

            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            

        }

        private void btnApplications_Click(object sender, EventArgs e)
        {
            frmAddNewInternationalDrivingLicense addNewInternationalDrivingLicense = new frmAddNewInternationalDrivingLicense();
            addNewInternationalDrivingLicense.ShowDialog();
            frmListInternationalLicense_Load(null, null);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmPersonDetails frmPersonDetails = new frmPersonDetails(clsApplication.FindApplicationByID((int)InternationalDriverLicenseDataGridView.CurrentRow.Cells[1].Value).ApplicantPersonID);
            frmPersonDetails.ShowDialog();
            frmListInternationalLicense_Load(null, null);
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInternationalLicenseDriverInfo frmShowLocalDriving = new frmInternationalLicenseDriverInfo((int)InternationalDriverLicenseDataGridView.CurrentRow.Cells[0].Value);
            frmShowLocalDriving.ShowDialog();
            frmListInternationalLicense_Load(null, null);
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowLicensesHistory frmShowLicensesHistory = new frmShowLicensesHistory(clsApplication.FindApplicationByID((int)InternationalDriverLicenseDataGridView.CurrentRow.Cells[1].Value).ApplicantPersonID);
            frmShowLicensesHistory.ShowDialog();
            frmListInternationalLicense_Load(null, null);
        }
    }
}
