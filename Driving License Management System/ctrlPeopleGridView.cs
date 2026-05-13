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

namespace Driving_License_Management_System
{
    public partial class ctrlPeopleGridView : UserControl
    {
        public ctrlPeopleGridView()
        {
            InitializeComponent();
        }
        DataTable dtPersons;


        private void _RefreshPersonList()
        {
            dtPersons = clsPerson.GetAllPersons();
            PeopleDataGridView.DataSource = dtPersons;
            _UpdateTotalRecords();
        }

        public void LoadDataToGrid(DataTable dt)
        {
            dtPersons = dt;
            PeopleDataGridView.DataSource = dt;
            PeopleDataGridView.Columns["FirstName"].HeaderText = "First Name";
            PeopleDataGridView.Columns["SecondName"].HeaderText = "Second Name";
            PeopleDataGridView.Columns["ThirdName"].HeaderText = "Third Name";
            PeopleDataGridView.Columns["LastName"].HeaderText = "Last Name";
            PeopleDataGridView.Columns["DateOfBirth"].HeaderText = "Date Of Birth";
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditPersonInfo frm = new frmAddEditPersonInfo(-1);
            frm.ShowDialog();
            _RefreshPersonList();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditPersonInfo frm = new frmAddEditPersonInfo((int)PeopleDataGridView.CurrentRow.Cells[0].Value);

            frm.ShowDialog();

            _RefreshPersonList();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete Person [" + PeopleDataGridView.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel) == DialogResult.OK)

            {
                //Perform Delele and refresh
                if (clsPerson.DeletePerson((int)PeopleDataGridView.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Person Deleted Successfully.");
                    _RefreshPersonList();
                }
                else
                    MessageBox.Show("Person was not deleted because it has data linked to it.");

            }
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is not implemented yet.", "Caution", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is not implemented yet.", "Caution", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ctrlPeopleGridView_Load(object sender, EventArgs e)
        {
            _UpdateTotalRecords();
            //PeopleDataGridView.Columns["ImagePath"].Visible = true;
            //PeopleDataGridView.Columns["NationalityCountryID"].HeaderText = "Nationality";
            //PeopleDataGridView.Columns["NationalNo"].HeaderText = "National No.";
        }





        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            string selectedFilter = cbFilterBy.Text;

            if (selectedFilter == "Person ID" || selectedFilter == "Phone")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }

            else if (selectedFilter == "None")
            {
                e.Handled = true;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

            string selectedFilter = cbFilterBy.Text;
            _UpdateTotalRecords();

            string searchValue = txtSearch.Text.Trim().Replace("'", "''"); // حماية من علامات الاقتباس
            DataView dt = dtPersons.DefaultView;

            if (string.IsNullOrWhiteSpace(searchValue) || selectedFilter == "None")
            {
                dt.RowFilter = "";
                return;
            }

            switch (selectedFilter)
            {
                case "Person ID":
                    if (int.TryParse(searchValue, out int id))
                        dt.RowFilter = $"PersonID = {id}";
                    break;

                case "National No.":
                    dt.RowFilter = $"NationalNo LIKE '{searchValue}%'";
                    break;

                case "First Name":
                    dt.RowFilter = $"FirstName LIKE '{searchValue}%'";
                    break;

                case "Second Name":
                    dt.RowFilter = $"SecondName LIKE '{searchValue}%'";
                    break;

                case "Third Name":
                    dt.RowFilter = $"ThirdName LIKE '{searchValue}%'";
                    break;

                case "Last Name":
                    dt.RowFilter = $"LastName LIKE '{searchValue}%'";
                    break;

                case "Nationality":
                    dt.RowFilter = $"Nationality LIKE '{searchValue}%'";
                    break;


                case "Phone":
                    dt.RowFilter = $"Phone LIKE '{searchValue}%'";
                    break;

                case "Email":
                    dt.RowFilter = $"Email LIKE '{searchValue}%'";
                    break;

                default:
                    dt.RowFilter = "";
                    break;
            }
            _UpdateTotalRecords();
            PeopleDataGridView.DataSource = dt;
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            DataView dt = dtPersons.DefaultView;

            if (cbFilterBy.SelectedItem == "None")
            {
                txtSearch.Visible = false;
                cbGendor.Visible = false;
                dt.RowFilter = null;
                _UpdateTotalRecords();

                return;
            }
            if (cbFilterBy.SelectedItem == "Gendor")
            {
                txtSearch.Visible = false;
                cbGendor.Visible = true;
            }
            else
            {
                cbGendor.SelectedIndex = -1;
                dt.RowFilter = null;
                _UpdateTotalRecords();
                txtSearch.Visible = true;
                cbGendor.Visible = false;
            }
        }

        private void _UpdateTotalRecords(DataView dt)
        {
            lblTotalRecords.Text = dt.Count.ToString();
        }

        private void _UpdateTotalRecords()
        {
            lblTotalRecords.Text = clsPerson.GetTotalPersons().ToString();
        }

        private void cbGendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataView dt = dtPersons.DefaultView;

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

            _UpdateTotalRecords(dt);

        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPersonDetails frmDetails = new frmPersonDetails((int)PeopleDataGridView.CurrentRow.Cells[0].Value);
            frmDetails.ShowDialog();
        }
    }

    //public enum GenderDisplayMode
    //{ Raw, Text };


    //private void ApplyGenderTextMode()
    //{

    //    PeopleDataGridView.Columns["GendorText"].HeaderText = "Gender";
    //    PeopleDataGridView.Columns["GendorText"].DisplayIndex = PeopleDataGridView.Columns["Gendor"].Index;

    //    PeopleDataGridView.Columns["Gendor"].Visible = false;
    //    PeopleDataGridView.Columns["GendorText"].Visible = true;
    //}

    //private void ApplyGenderRawMode()
    //{

    //    PeopleDataGridView.Columns["Gendor"].Visible = true;

    //    PeopleDataGridView.Columns["GendorText"].Visible = false;
    //}

    //public void SetGenderDisplayMode(GenderDisplayMode mode)
    //{
    //    switch (mode)
    //    {
    //        case GenderDisplayMode.Text:
    //            ApplyGenderTextMode();
    //            break;

    //        case GenderDisplayMode.Raw:
    //            ApplyGenderRawMode();
    //            break;
    //    }
    //}

    //public void HideImagePath(bool Hide)
    //{
    //    if (Hide)
    //    {
    //        PeopleDataGridView.Columns["ImagePath"].Visible = false;
    //    }
    //    else
    //    {
    //        PeopleDataGridView.Columns["ImagePath"].Visible = true;
    //    }
    //}
}