using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer;

namespace Driving_License_Management_System.Users
{
    public partial class frmChangeUserPassword : Form
    {
        private int _UserID = 0;
        private clsUser _User;
        public frmChangeUserPassword(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
        }

        private void _ResetDefaultValues()
        {
            txtCurrentPassword.Text = "";
            txtNewPassword.Text = "";
            txtConfirmPassword.Text = "";
            txtCurrentPassword.Focus();
        }

        private void frmChangeUserPassword_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();
            _User = clsUser.Find(_UserID);

            if (_User == null)
            {
                MessageBox.Show("Can not find this user with id = " + _UserID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            ctrlUserCard1.LoadUserInfo(_User.UserID);
        }

        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrEmpty(txtCurrentPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "Password Cannot Be Blank");
                return;
            }
            else
            {
                errorProvider1.SetError(txtCurrentPassword, null);
            }

            if (_User.Password != txtCurrentPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "Current Password Is Wrong!");
                return;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtCurrentPassword, null);
            }
        }

        private void txtNewPassword_Validating(object sender, CancelEventArgs e)
        {
            clsValidation.txtIsNotNullOrWhiteSpaceValdiateHandling(txtNewPassword, e, errorProvider1);
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (!clsValidation.IsPasswordMatch(txtNewPassword.Text, txtConfirmPassword.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Password Confirmation Does Not Match New Password");
            }
            else
            {
             // e.Cancel = false; by default because of the constructor, var e = new CancelEventArgs();
                errorProvider1.SetError(txtConfirmPassword, null);
            }
        }



        //private void txtNewPassword_Validating(object sender, CancelEventArgs e)
        //{
        //    clsValidation.txtIsNotNullOrWhiteSpaceValdiateHandling(txtNewPassword, e, errorProvider1);

        //    if (!clsValidation.IsPasswordMatch(txtNewPassword.Text, txtConfirmPassword.Text))
        //    {
        //        e.Cancel = true;
        //        errorProvider1.SetError(txtConfirmPassword, "Password Confirmation Does Not Match New Password");
        //    }
        //    else
        //    {
        //        e.Cancel = false;
        //        errorProvider1.SetError(txtConfirmPassword, "");
        //    }
        //}

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Please Fill The Requirements as Required", "Requirements", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_User == null)
            {
                MessageBox.Show("Something Went Wrong, User is not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            _User.Password = txtNewPassword.Text;

            if (_User.Save())
            {
                MessageBox.Show("Password Changed Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _ResetDefaultValues();
            }
            else
            {
                MessageBox.Show("Password Can Not Change Successfully", "Can't Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            if (clsUtil.IsFileExistAndHasData(clsUtil.FilePath) && _User.UserID == GlobalSettings.User.UserID)
            {
                clsUtil.SaveUserLoginInformation(_User.UserName, _User.Password, clsUtil.IsRememberMe);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
