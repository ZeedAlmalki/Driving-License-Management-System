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


        private int _UserID = -1;
        clsUser _User;
        // int _PersonID;

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
        }

        private void _ResetDefaultValues()
        {
            if (_Mode == enMode.AddNew)
            {
                lblMode.Text = "Add New User";
                this.Text = "Add New User";
                _User = new clsUser();

                tpUserInfo.Enabled = false;
                ctrlPersonCardWithFilter1.FilterFocus();
            }

            else
            {
                lblMode.Text = "Update User";
                this.Text = "Update User";
                tpUserInfo.Enabled = true;
                btnSave.Enabled = true;
                //ctrlPersonCardWithFilter1.FilterEnabled = false;
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

            if (_Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tpUserInfo.Enabled = true;
                tcUser.SelectedTab = tcUser.TabPages["tpUserInfo"];
                return;
            }

            if (ctrlPersonCardWithFilter1.PersonID != -1)
            {

                if (clsUser.IsUserExistForPersonID(PersonID) && _User.PersonID != PersonID)
                {
                    tcUser.SelectedTab = tpPersonInfo;
                    MessageBox.Show("Selected Person Already Has a User, Choose Another One.", "Select Another Perosn", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ctrlPersonCardWithFilter1.FilterFocus();
                    return;
                }
                else
                {
                    btnSave.Enabled = true;
                    tpUserInfo.Enabled = true;
                    tcUser.SelectedTab = tcUser.TabPages["tpUserInfo"];
                }
            }
            else
            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ctrlPersonCardWithFilter1.FilterFocus();
            }
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            clsValidation.txtIsNotNullOrWhiteSpaceValdiateHandling((Guna2TextBox)sender, e, errorProvider1);
            if ((clsUser.IsUserExist(txtUserName.Text) && _Mode == enMode.Update && _User.UserName != txtUserName.Text)
                 /*update*/ ||
                 (clsUser.IsUserExist(txtUserName.Text) && _Mode == enMode.AddNew))/*add*/
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUserName, "Already Has a User With this UsreName, Choose Another One");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtUserName, "");
            }

        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            clsValidation.txtIsNotNullOrWhiteSpaceValdiateHandling(txtPassword, e, errorProvider1);

            if (!clsValidation.IsPasswordMatch(txtPassword.Text, txtConfirmPassword.Text))
            {
                txtConfirmPassword.Focus();
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Please Enter Correct Password");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtConfirmPassword, null);
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
            _User.PersonID = ctrlPersonCardWithFilter1.PersonID;

            if (_User.Save())
            {
                lblUserID.Text = _User.UserID.ToString();
                _Mode = enMode.Update;
                lblMode.Text = "Update User";
                this.Text = "Update User";
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
            ctrlPersonCardWithFilter1.FilterEnabled = false;
            if (_User == null)
            {
                MessageBox.Show("No User with ID = " + _UserID, "User Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            //if (e.TabPage == tpUserInfo)
            //{
            //    btnNext.PerformClick();
            //    return;
            //}
            if (e.TabPage == tpUserInfo && clsUser.IsUserExistForPersonID(ctrlPersonCardWithFilter1.PersonID) && !(_Mode == enMode.Update))
            {
                tpUserInfo.Enabled = false;
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