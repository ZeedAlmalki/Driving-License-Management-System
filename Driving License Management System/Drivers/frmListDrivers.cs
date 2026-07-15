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

namespace Driving_License_Management_System.Drivers
{
    public partial class frmListDrivers : Form
    {
        public frmListDrivers()
        {
            InitializeComponent();
        }
        private static DataTable _dtAllDrivers;

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListDrivers_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            _dtAllDrivers = clsDriver.GetAllDrivers();

            _dtAllDrivers = _dtAllDrivers.DefaultView.ToTable(false, "DriverID", "PersonID", "NationalNo", "FullName",
                "CreatedDate", "NumberOfActiveLicenses");
            DriversDataGridView.DataSource = _dtAllDrivers;
            lblTotalRecords.Text = DriversDataGridView.Rows.Count.ToString();
            if (_dtAllDrivers.Rows.Count > 0)
            {
                DriversDataGridView.Columns[0].HeaderText = "Driver ID";
                DriversDataGridView.Columns[0].Width = 50;

                DriversDataGridView.Columns[1].HeaderText = "Person ID";
                DriversDataGridView.Columns[1].Width = 50;

                DriversDataGridView.Columns[2].HeaderText = "National No.";
                DriversDataGridView.Columns[2].Width = 70;

                DriversDataGridView.Columns[3].HeaderText = "Full Name";
                DriversDataGridView.Columns[3].Width = 160;

                DriversDataGridView.Columns[4].HeaderText = "Date";
                DriversDataGridView.Columns[4].Width = 80;

                DriversDataGridView.Columns[5].HeaderText = "Active Licenses";
                DriversDataGridView.Columns[5].Width = 100;
            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "None")
            {
                txtFilterValue.Text = string.Empty;
                txtFilterValue.Visible = false;
            }
            else
            {
                txtFilterValue.Visible = true;
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cbFilterBy.Text)
            {
                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;
                case "Full Name":
                    FilterColumn = "FullName";
                    break;
                case "National No.":
                    FilterColumn = "NationalNo";
                    break;
            }

            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllDrivers.DefaultView.RowFilter = "";
                lblTotalRecords.Text = DriversDataGridView.Rows.Count.ToString();
                return;
            } // Because if we continue to do RowFilters with empty string we will shock with bug.

            if (FilterColumn == "DriverID" || FilterColumn == "PersonID")
            {
                _dtAllDrivers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text);
            }
            else
            {
                _dtAllDrivers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text);
            }

            lblTotalRecords.Text = DriversDataGridView.Rows.Count.ToString();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            string SelectedFilter = cbFilterBy.Text;
            if (SelectedFilter == "Driver ID" || SelectedFilter == "Person ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
            else if (SelectedFilter == "None")
            {
                e.Handled = true;
            }

        }

        private void showPersonInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPersonDetails personDetails = new frmPersonDetails((string)DriversDataGridView.CurrentRow.Cells[2].Value);
            personDetails.ShowDialog();
            frmListDrivers_Load(null, null);
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowLicensesHistory personLicensesHistory = new frmShowLicensesHistory((string)DriversDataGridView.CurrentRow.Cells[2].Value);
            personLicensesHistory.ShowDialog();
            frmListDrivers_Load(null, null);
        }
    }
}
