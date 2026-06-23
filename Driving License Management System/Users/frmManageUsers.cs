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
using Driving_License_Management_System.People;
using Driving_License_Management_System.Users;
using static Driving_License_Management_System.frmAddEditPersonInfo;

namespace Driving_License_Management_System
{
    public partial class frmManageUsers : Form
    {

        private static DataTable _dtAllUsers = clsUser.GetAllUsers();

        private DataTable _dtUsers = _dtAllUsers.DefaultView.ToTable(false, "UserID", "PersonID", "UserName", "FullName", "IsActive");

        public frmManageUsers()
        {
            InitializeComponent();
        }

        private void frmManageUsers_Load(object sender, EventArgs e)
        {
            UsersDataGridView.DataSource = _dtUsers;
            lblTotalRecords.Text = _dtUsers.Rows.Count.ToString();
            cbFilterBy.SelectedIndex = 0;

            if (UsersDataGridView.Rows.Count > 0)
            {
                UsersDataGridView.Columns[0].HeaderText = "User ID";
                UsersDataGridView.Columns[0].Width = 50;

                UsersDataGridView.Columns[1].HeaderText = "Person ID.";
                UsersDataGridView.Columns[1].Width = 50;

                UsersDataGridView.Columns[2].HeaderText = "UserName";
                UsersDataGridView.Columns[2].Width = 70;

                UsersDataGridView.Columns[3].HeaderText = "Full Name";
                UsersDataGridView.Columns[3].Width = 100;

                UsersDataGridView.Columns[4].HeaderText = "Is Active";
                UsersDataGridView.Columns[4].Width = 100;
                UsersDataGridView.Columns[4].ReadOnly = true;
            }
        }

        private void _UpdateTotalRecords()
        {
            lblTotalRecords.Text = clsUser.GetTotalUsers().ToString();
        }

        public void _RefreshUsersList()
        {
            _dtAllUsers = clsUser.GetAllUsers();
            _dtUsers = _dtAllUsers.DefaultView.ToTable(false, "UserID", "PersonID", "UserName", "FullName", "IsActive");
            UsersDataGridView.DataSource = _dtUsers;
            _UpdateTotalRecords();
            // lblTotalRecords.Text = _dtUsers.Rows.Count.ToString();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowUserInfo frmShowUserInfo = new frmShowUserInfo((int)UsersDataGridView.CurrentRow.Cells[0].Value);
            frmShowUserInfo.ShowDialog();
        }

        private void btnAdd_Click(object sender, EventArgs e) // 2 reference with tool strip menu 
        {
            frmAddNewUser frmAddNewUser = new frmAddNewUser();
            frmAddNewUser.ShowDialog();
            _RefreshUsersList();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddNewUser frmAddNewUser = new frmAddNewUser((int)UsersDataGridView.CurrentRow.Cells[0].Value);
            frmAddNewUser.ShowDialog();
            _RefreshUsersList();
        }

        private void ChangePasswordStripMenuItem3_Click(object sender, EventArgs e)
        {
            frmChangeUserPassword frmChangeUserPassword = new frmChangeUserPassword((int)UsersDataGridView.CurrentRow.Cells[0].Value);
            frmChangeUserPassword.ShowDialog();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete User [" + UsersDataGridView.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                
                //Perform Delele and refresh

                if (GlobalSettings.User.UserID == (int)UsersDataGridView.CurrentRow.Cells[0].Value)
                {
                    MessageBox.Show("You Can not delete Logged In User.", "Logged In User", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (clsUser.DeleteUser((int)UsersDataGridView.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("User Deleted Successfully.");
                    _RefreshUsersList();
                }
                else
                    MessageBox.Show("User was not deleted because it has data linked to it.");
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

                case "User ID":
                    FilterColumn = "UserID";
                    break;

                case "UserName":
                    FilterColumn = "UserName";
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
                _dtUsers.DefaultView.RowFilter = "";
                lblTotalRecords.Text = UsersDataGridView.Rows.Count.ToString();
                return;
            }



            if (FilterColumn == "PersonID" || FilterColumn == "UserID")
                _dtUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text);
            else
                _dtUsers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text);

            lblTotalRecords.Text = UsersDataGridView.Rows.Count.ToString();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            string selectedFilter = cbFilterBy.Text;

            if (selectedFilter == "Person ID" || selectedFilter == "User ID")
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
                cbIsActive.SelectedIndex = -1;
                cbIsActive.Visible = false;
                _dtUsers.DefaultView.RowFilter = null;
                _UpdateTotalRecords();
                return;
            }
            else
            {
                txtFilterValue.Visible = true;
            }
            if (cbFilterBy.Text == "Is Active")
            {
                txtFilterValue.Visible = false;
                cbIsActive.Visible = true;
                _dtUsers.DefaultView.RowFilter = null;
                _UpdateTotalRecords();
            }
            else
            {
                cbIsActive.SelectedIndex = -1;
                _dtUsers.DefaultView.RowFilter = null;
                _UpdateTotalRecords();
                txtFilterValue.Visible = true;
                txtFilterValue.Text = string.Empty;
                cbIsActive.Visible = false;
            }
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataView dt = _dtUsers.DefaultView;

            if (cbIsActive.SelectedItem == "All")
            {
                dt.RowFilter = ("IsActive = true OR IsActive = false");
            }
            else if (cbIsActive.SelectedItem == "Yes")
            {
                dt.RowFilter = ("IsActive = true");
            }
            else if (cbIsActive.SelectedItem == "No")
            {
                dt.RowFilter = ("IsActive = false");
            }

            lblTotalRecords.Text = dt.Count.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}