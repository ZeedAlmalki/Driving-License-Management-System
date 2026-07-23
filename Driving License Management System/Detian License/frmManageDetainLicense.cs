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

namespace Driving_License_Management_System.Detian_License
{
    public partial class frmManageDetainLicense : Form
    {
        DataTable dtAllDetianLicenses;
        public frmManageDetainLicense()
        {
            InitializeComponent();
        }

        private void frmManageDetainLicense_Load(object sender, EventArgs e)
        {
            dtAllDetianLicenses = clsDetainedLicenses.GetAllDetainedLicenses();
            DetainedLicensesDataGridView.DataSource = dtAllDetianLicenses;
            lblTotalRecords.Text = DetainedLicensesDataGridView.Rows.Count.ToString();

            if (DetainedLicensesDataGridView.Rows.Count > 0)
            {
                DetainedLicensesDataGridView.Columns[0].HeaderText = "D.ID";
                DetainedLicensesDataGridView.Columns[0].Width = 50;

                DetainedLicensesDataGridView.Columns[1].HeaderText = "L.ID";
                DetainedLicensesDataGridView.Columns[1].Width = 50;

                DetainedLicensesDataGridView.Columns[2].HeaderText = "D.Date";
                DetainedLicensesDataGridView.Columns[2].Width = 63;

                DetainedLicensesDataGridView.Columns[3].HeaderText = "Is Released";
                DetainedLicensesDataGridView.Columns[3].Width = 35;
                DetainedLicensesDataGridView.Columns[3].ReadOnly = true;

                DetainedLicensesDataGridView.Columns[4].HeaderText = "Fine Fees";
                DetainedLicensesDataGridView.Columns[4].Width = 50;

                DetainedLicensesDataGridView.Columns[5].HeaderText = "Release Date";
                DetainedLicensesDataGridView.Columns[5].Width = 63;

                DetainedLicensesDataGridView.Columns[6].HeaderText = "N No.";
                DetainedLicensesDataGridView.Columns[6].Width = 30;

                DetainedLicensesDataGridView.Columns[7].HeaderText = "Full Name";
                DetainedLicensesDataGridView.Columns[7].Width = 70;

                DetainedLicensesDataGridView.Columns[8].HeaderText = "Release App.ID";
                DetainedLicensesDataGridView.Columns[8].Width = 60;
            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Text = "";


            if (cbFilterBy.SelectedItem == "None")
            {
                txtFilterValue.Visible = false;
                cbIsReleased.Visible = false;
            }
            else if (cbFilterBy.SelectedItem == "Is Released")
            {
                txtFilterValue.Visible = false;
                cbIsReleased.Visible = true;
            }
            else
            {
                txtFilterValue.Visible = true;
                cbIsReleased.Visible = false;
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            switch (cbFilterBy.Text)
            {
                case "None":
                    FilterColumn = "None";
                    break;
                case "Detain ID":
                    FilterColumn = "DetainID";
                    break;
                case "National No.":
                    FilterColumn = "NationalNo";
                    break;
                case "Full Name":
                    FilterColumn = "FullName";
                    break;
                case "Release Application ID":
                    FilterColumn = "ReleaseApplicationID";
                    break;
                default:
                    FilterColumn = "";
                    break;
            }

            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "" || FilterColumn == "None")
            {
                dtAllDetianLicenses.DefaultView.RowFilter = "";
                lblTotalRecords.Text = DetainedLicensesDataGridView.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "FullName" || FilterColumn == "NationalNo")
            {
                dtAllDetianLicenses.DefaultView.RowFilter = string.Format("[{0}] Like '{1}%'", FilterColumn, txtFilterValue.Text);
            }
            else
            {
                dtAllDetianLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text);
            }
            lblTotalRecords.Text = DetainedLicensesDataGridView.Rows.Count.ToString();
        }

        private void cbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsReleased";
            string Value = "";
            switch (cbIsReleased.SelectedItem.ToString())
            {
                case "All":
                    Value = "";
                    break;
                case "Yes":
                    Value = "1";
                    break;
                case "No":
                    Value = "0";
                    break;
            }

            if (Value == "")
            {
                dtAllDetianLicenses.DefaultView.RowFilter = "";
                lblTotalRecords.Text = DetainedLicensesDataGridView.Rows.Count.ToString();
                return;
            }

            dtAllDetianLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, Value);
            lblTotalRecords.Text = DetainedLicensesDataGridView.Rows.Count.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDetianLicense_Click(object sender, EventArgs e)
        {
            frmDetianLicense frmDetianLicense = new frmDetianLicense();
            frmDetianLicense.ShowDialog();
            frmManageDetainLicense_Load(null, null);
        }

        private void btnReleaseLicense_Click(object sender, EventArgs e)
        {
            frmReleaseLicense frmReleaseLicense = new frmReleaseLicense();
            frmReleaseLicense.ShowDialog();
            frmManageDetainLicense_Load(null, null);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmPersonDetails frmPersonDetails = new frmPersonDetails(DetainedLicensesDataGridView.CurrentRow.Cells[6].Value.ToString());
            frmPersonDetails.ShowDialog();
            frmManageDetainLicense_Load(null, null);
        }

        private void showPersonInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLicenseInfo frmLicenseInfo = new frmLicenseInfo((int)DetainedLicensesDataGridView.CurrentRow.Cells[1].Value);
            frmLicenseInfo.ShowDialog();
            frmManageDetainLicense_Load(null, null);
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowLicensesHistory frmShowLicensesHistory = new frmShowLicensesHistory((string)DetainedLicensesDataGridView.CurrentRow.Cells[6].Value);
            frmShowLicensesHistory.ShowDialog();
            frmManageDetainLicense_Load(null, null);
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseLicense frmReleaseLicense = new frmReleaseLicense((int)DetainedLicensesDataGridView.CurrentRow.Cells[1].Value);
            frmReleaseLicense.ShowDialog();
            frmManageDetainLicense_Load(null, null);

        }

        private void DetainedLicensesDataGridView_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            releaseDetainedLicenseToolStripMenuItem.Enabled = true;


            if ((bool)DetainedLicensesDataGridView.CurrentRow.Cells[3].Value == true) // equal IsReleased = true
            {
                releaseDetainedLicenseToolStripMenuItem.Enabled = false;
            }

        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Release Application ID" || cbFilterBy.Text == "Detain ID")
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}