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

namespace Driving_License_Management_System.Users
{
    public partial class frmChangeUserPassword : Form
    {
        string CurrentPassword = string.Empty;
        private int _UserID = 0;
        private clsUser _User;
        public frmChangeUserPassword(int UserID)
        {
            InitializeComponent();
            this._UserID = UserID;
        }

        private void frmChangeUserPassword_Load(object sender, EventArgs e)
        {
            _User = clsUser.Find(_UserID);

            if (_User != null)
            {
                CurrentPassword = _User.Password;
                ctrlUserCard1.LoadUserInfo(_User.UserID, _User.PersonID);
            }
        }

        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtCurrentPassword.Text != CurrentPassword)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "Current Password Is Wrong!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtCurrentPassword, "");
            }
        }



        private void txtNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (!clsValidation.IsPasswordMatch(txtNewPassword.Text, txtConfirmPassword.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Password Must Be Match");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtConfirmPassword, "");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren())
            {
                if (_User != null)
                {
                    _User.Password = txtNewPassword.Text;
                    _User.Save();
                    CurrentPassword = _User.Password;
                    if (clsUtil.IsFileExistAndHasData(clsUtil.FilePath) && _User.UserID == GlobalSettings.User.UserID)
                    {
                        clsUtil.SaveUserLoginInformation(_User.UserName, _User.Password, clsUtil.IsRememberMe);
                    }
                    MessageBox.Show("Password Changed Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Something Went Wrong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please Fill The Requirements as Required", "Requirements", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
