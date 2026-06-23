using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using BusinessLayer;
using Driving_License_Management_System.Properties;
using Guna.UI2.WinForms;
using TheArtOfDevHtmlRenderer.Adapters;
using static System.Net.Mime.MediaTypeNames;

namespace Driving_License_Management_System.People
{
    public partial class frmAddNewUser : Form
    {

        public delegate void DataBackEventHandler(object sender, int UserID);

        public event DataBackEventHandler DataBack;


        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;


        private int _UserID;
        clsUser _User;
        int _PersonID;

        public frmAddNewUser()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }

        public frmAddNewUser(int UserID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _UserID = UserID;
            ctrlPersonCardWithFilter1.FilterEnabled = false;
        }

        private void _ResetDefaultValues()
        {
            if (_Mode == enMode.AddNew)
            {
                lblMode.Text = "Add New User";
                _User = new clsUser();
            }
            else
            {
                lblMode.Text = "Update User";
            }
            txtUserName.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtConfirmPassword.Text = string.Empty;
            cbIsActive.Checked = false;
            lblUserID.Text = string.Empty;
            ctrlPersonCardWithFilter1.ResetPersonInfo();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            int PersonID = ctrlPersonCardWithFilter1.PersonID;

            
            if (clsPerson.IsPersonHasUser(PersonID) && _User.PersonID != PersonID)
            {
                tcUser.SelectedTab = tpPersonInfo;
                MessageBox.Show("Selected Person Already Has a User, Choose Another One.", "Select Another Perosn", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!clsPerson.IsPersonExist(PersonID))
            {
                tcUser.SelectedTab = tpPersonInfo;
                MessageBox.Show("No Person Exists, You must create one.", "Create a Perosn", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _PersonID = PersonID;
            tcUser.SelectedTab = tpUserInfo;
            txtUserName.Focus();
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            clsValidation.txtIsNotNullOrWhiteSpaceValdiateHandling((Guna2TextBox)sender, e, errorProvider1);
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            clsValidation.txtIsNotNullOrWhiteSpaceValdiateHandling(txtPassword, e, errorProvider1);
            tcUser.SelectedTab = tcUser.SelectedTab;

            if (!clsValidation.IsPasswordMatch(txtPassword.Text, txtConfirmPassword.Text))
            {
                txtConfirmPassword.Focus();
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Please Enter Correct Password");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtConfirmPassword, "");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Please fill in the requirements as required");
                return;
            }
            
            _User.UserName = txtUserName.Text.Trim();
            _User.Password = txtPassword.Text.Trim();
            _User.IsActive = cbIsActive.Checked;
            _User.PersonID = _PersonID;

            if (_User.Save())
            {
                lblUserID.Text = _User.UserID.ToString();
                _Mode = enMode.Update;
                lblMode.Text = "Update User";
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK);
                if (clsUtil.IsFileExistAndHasData(clsUtil.FilePath) && _User.UserID == GlobalSettings.User.UserID)
                {
                    clsUtil.SaveUserLoginInformation(_User.UserName, _User.Password, clsUtil.IsRememberMe);
                }
                DataBack?.Invoke(this, _User.UserID);
            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK);
            }

        }

        private void _LoadData()
        {

            _User = clsUser.Find(_UserID);

            if (_User == null)
            {
                MessageBox.Show("No User with ID = " + _UserID);
                this.Close();
                return;
            }

            txtUserName.Text = _User.UserName;
            txtPassword.Text = _User.Password;
            txtConfirmPassword.Text = _User.Password;
            cbIsActive.Checked = _User.IsActive;
            lblUserID.Text = _User.UserID.ToString();
            ctrlPersonCardWithFilter1.LoadPersonInfo(_User.PersonID);

        }

        private void frmAddNewUser_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();
            if (_Mode == enMode.Update)
                _LoadData();

        }

        private void tcUser_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage == tpUserInfo)
            {
                btnNext.PerformClick();
                return;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbIsActive_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbIsActive.Checked && GlobalSettings.User.UserID == _User.UserID)
            {
                cbIsActive.Checked = true;
                MessageBox.Show("The logged-in user cannot be deactivated.", "Cannot Deactivate", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}