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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Driving_License_Management_System
{
    public partial class frmLoginScreen : Form
    {
        public frmLoginScreen()
        {
            InitializeComponent();
        }

        void RestartSettings()
        {
            txtPassword.Text = string.Empty;
            txtUserName.Text = string.Empty;
            cbRememberMe.Checked = false;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Please fill in the requirements as required");
                return;
            }

            string UserName = txtUserName.Text;
            clsUser user = clsUser.Find(UserName);

            if (user == null)
            {
                MessageBox.Show("User is not in the system.", "Must be register", MessageBoxButtons.OK);
                RestartSettings();
                return;
            }

            if (txtUserName.Text == user.UserName && txtPassword.Text == user.Password)
            {
                if (!user.IsActive)
                {
                    MessageBox.Show("User is not Active.", "Must be active", MessageBoxButtons.OK);
                    return;
                }
                if (cbRememberMe.Checked)
                {
                    clsUtil.SaveUserLoginInformation(user.UserName, user.Password, cbRememberMe.Checked);
                }
                else
                {
                    txtUserName.Text = "";
                    txtPassword.Text = "";
                    cbRememberMe.Checked = false;
                    clsUtil.RemoveUserLoginInformation();
                }
                GlobalSettings.User = user;
                this.Hide();
                MainForm frm = new MainForm(this);
                frm.ShowDialog();
                RefreshSavedUserLoginInformation();
            }
            else
            {
                MessageBox.Show("Invalid UserName/Password.", "Wrong Credentials", MessageBoxButtons.OK);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtUserNameAndPassword_Validating(object sender, CancelEventArgs e)
        {
            clsValidation.txtIsNotNullOrWhiteSpaceValdiateHandling((Guna2TextBox)sender, e, errorProvider1);
        }

        private void frmLoginScreen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                btnLogin.PerformClick();
            }
        }

        private void RefreshSavedUserLoginInformation()
        {
            string UserName = "";
            string Password = "";
            bool RememberMe = false;
            if (clsUtil.GetSavedUserLoginInformation(ref UserName, ref Password))
            {
                txtUserName.Text = UserName;
                txtPassword.Text = Password;
                cbRememberMe.Checked = true;
            }
        }

        private void frmLoginScreen_Load(object sender, EventArgs e)
        {
            RefreshSavedUserLoginInformation();
        }

        private void cbRememberMe_CheckedChanged(object sender, EventArgs e)
        {
            clsUtil.IsRememberMe = cbRememberMe.Checked;
        }
    }
}
