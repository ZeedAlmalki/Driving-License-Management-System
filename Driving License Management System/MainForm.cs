using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using BusinessLayer;
using Driving_License_Management_System.Users;

namespace Driving_License_Management_System
{
    public partial class MainForm : Form
    {
        frmLoginScreen _frmLogin;
        public MainForm(frmLoginScreen frm)
        {
            InitializeComponent();
            _frmLogin = frm;
        }

        private void btnPeople_Click(object sender, EventArgs e)
        {
            frmManagePeople frmManagePeople = new frmManagePeople();
            frmManagePeople.ShowDialog();
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            frmManageUsers frmManageUsers = new frmManageUsers();
            frmManageUsers.ShowDialog();
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowUserInfo frmShowUserInfo = new frmShowUserInfo(GlobalSettings.User.UserID);
            frmShowUserInfo.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangeUserPassword frmChangeUserPassword = new frmChangeUserPassword(GlobalSettings.User.UserID);
            frmChangeUserPassword.ShowDialog();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GlobalSettings.User = null;
            _frmLogin.Show();
            this.Close();
        }

        private void ManageApplicationstoolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmManageApplicationsTypes frm = new frmManageApplicationsTypes();
            frm.ShowDialog();
        }
    }
}
