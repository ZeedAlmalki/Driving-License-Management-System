using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer;
using Guna.UI2.WinForms;

namespace Driving_License_Management_System
{
    public partial class frmManagePeople : Form
    {


        private static DataTable _dtAllPeople = clsPerson.GetAllPeople();

        private DataTable _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo", "FirstName", "SecondName", "ThirdName", "LastName",
            "Gendor", "DateOfBirth", "Nationality", "Phone", "Email");


        public frmManagePeople()
        {
            InitializeComponent();
            //ctrlPeopleGridView1.LoadDataToGrid(dt);
        }

        private void btnApplications_Click(object sender, EventArgs e)
        {
            //ctrlPeopleGridView1.OpenAddNewPersonInfo();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmManagePeople_Load(object sender, EventArgs e)
        {
            PeopleDataGridView.DataSource = _dtPeople;
            lblTotalRecords.Text = _dtPeople.Rows.Count.ToString();
            cbFilterBy.SelectedIndex = 0;

            if (PeopleDataGridView.Rows.Count > 0)
            {
                PeopleDataGridView.Columns[0].HeaderText = "Person ID";
                PeopleDataGridView.Columns[0].Width = 110;

                PeopleDataGridView.Columns[1].HeaderText = "National No.";
                PeopleDataGridView.Columns[1].Width = 120;

                PeopleDataGridView.Columns[2].HeaderText = "First Name";
                PeopleDataGridView.Columns[2].Width = 120;

                PeopleDataGridView.Columns[3].HeaderText = "Second Name";
                PeopleDataGridView.Columns[3].Width = 140;

                PeopleDataGridView.Columns[4].HeaderText = "Third Name";
                PeopleDataGridView.Columns[4].Width = 120;

                PeopleDataGridView.Columns[5].HeaderText = "Last Name";
                PeopleDataGridView.Columns[5].Width = 120;

                PeopleDataGridView.Columns[6].HeaderText = "Gendor";
                PeopleDataGridView.Columns[6].Width = 120;

                PeopleDataGridView.Columns[7].HeaderText = "Date Of Birth";
                PeopleDataGridView.Columns[7].Width = 140;

                PeopleDataGridView.Columns[8].HeaderText = "Country Name";
                PeopleDataGridView.Columns[8].Width = 120;

                PeopleDataGridView.Columns[9].HeaderText = "Phone";
                PeopleDataGridView.Columns[9].Width = 120;

                PeopleDataGridView.Columns[10].HeaderText = "Email";
                PeopleDataGridView.Columns[10].Width = 170;

            }
        }

        private void _UpdateTotalRecords()
        {
            lblTotalRecords.Text = clsPerson.GetTotalPeople().ToString();
        }
        public void _RefreshPeopleList()
        {
            _dtAllPeople = clsPerson.GetAllPeople();
            _dtPeople = _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo", "FirstName", "SecondName", "ThirdName", "LastName",
            "Gendor", "DateOfBirth", "Nationality", "Phone", "Email");
            PeopleDataGridView.DataSource = _dtPeople;
            _UpdateTotalRecords();
            // lblTotalRecords.Text = _dtPeople.Rows.Count.ToString();
        }
        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPersonDetails frmDetails = new frmPersonDetails((int)PeopleDataGridView.CurrentRow.Cells[0].Value);
            frmDetails.ShowDialog();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditPersonInfo frm = new frmAddEditPersonInfo((int)PeopleDataGridView.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshPeopleList();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete Person [" + PeopleDataGridView.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel) == DialogResult.OK)

            {
                string ImagePath = (clsPerson.Find((int)PeopleDataGridView.CurrentRow.Cells[0].Value).ImagePath);

                //Perform Delele and refresh
                if (clsPerson.DeletePerson((int)PeopleDataGridView.CurrentRow.Cells[0].Value))
                {
                    if (!string.IsNullOrWhiteSpace(ImagePath))
                        File.Delete(ImagePath);

                    MessageBox.Show("Person Deleted Successfully.");
                    _RefreshPeopleList();
                }
                else
                    MessageBox.Show("Person was not deleted because it has data linked to it.");
            }
        }


        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            switch (cbFilterBy.Text)
            {
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;

                case "First Name":
                    FilterColumn = "FirstName";
                    break;

                case "Second Name":
                    FilterColumn = "SecondName";
                    break;

                case "Third Name":
                    FilterColumn = "ThirdName";
                    break;

                case "Last Name":
                    FilterColumn = "LastName";
                    break;

                case "Gendor":
                    FilterColumn = "GendorCaption";
                    break;

                case "Date Of Birth":
                    FilterColumn = "DateOfBirth";
                    break;

                case "Nationality":
                    FilterColumn = "Nationality";
                    break;

                case "Phone":
                    FilterColumn = "Phone";
                    break;

                case "Email":
                    FilterColumn = "Email";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtPeople.DefaultView.RowFilter = "";
                lblTotalRecords.Text = PeopleDataGridView.Rows.Count.ToString();
                return;
            }



            if (FilterColumn == "PersonID")
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text);
            else
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text);

            lblTotalRecords.Text = PeopleDataGridView.Rows.Count.ToString();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            string selectedFilter = cbFilterBy.Text;

            if (selectedFilter == "Person ID" || selectedFilter == "Phone")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
            else if (selectedFilter == "None")
            {
                e.Handled = true;
                // layer of saftey
            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isNone = cbFilterBy.Text == "None";
            if (isNone)
            {
                txtFilterValue.Visible = false;
                cbGendor.SelectedIndex = -1;
                cbGendor.Visible = false;
                _dtPeople.DefaultView.RowFilter = null;
                _UpdateTotalRecords();
                return;
            }
            else
            {
                txtFilterValue.Visible = true;
            }
            if (cbFilterBy.Text == "Gendor")
            {
                txtFilterValue.Visible = false;
                cbGendor.Visible = true;
                _dtPeople.DefaultView.RowFilter = null;
                _UpdateTotalRecords();
            }
            else
            {
                cbGendor.SelectedIndex = -1;
                _dtPeople.DefaultView.RowFilter = null;
                _UpdateTotalRecords();
                txtFilterValue.Visible = true;
                txtFilterValue.Text = string.Empty;
                cbGendor.Visible = false;
            }
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditPersonInfo frm = new frmAddEditPersonInfo();
            frm.ShowDialog();
            _RefreshPeopleList();
        }

        private void cbGendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataView dt = _dtPeople.DefaultView;

            if (cbGendor.SelectedItem == "Male")
            {
                dt.RowFilter = ("Gendor = 'Male'");
            }
            else if (cbGendor.SelectedItem == "Female")
            {
                dt.RowFilter = ("Gendor = 'Female'");
            }
            else if (cbGendor.SelectedItem == "Both")
            {
                dt.RowFilter = "Gendor IN ('Male', 'Female')";
            }

            lblTotalRecords.Text = dt.Count.ToString();
        }


    }
}
