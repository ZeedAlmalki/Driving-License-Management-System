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

namespace Driving_License_Management_System.Users.Controls
{
    public partial class ctrlUserCard : UserControl
    {

        private clsUser _User;
        private int _UserID = -1;

        public int UserID
        {
            get { return _UserID; }
        }

        public ctrlUserCard()
        {
            InitializeComponent();
        }

        private void ResetUserInfo()
        {
            _UserID = -1;
            lblUserID.Text = "";
            lblUserName.Text = "";
            lblIsActive.Text = "";
        }

        private void _FillUserInfo()
        {
            lblUserID.Text = _User.UserID.ToString();
            lblUserName.Text = _User.UserName.ToString();
            ctrlPersonCard1.LoadPersonInfo(_User.PersonID);

            if (_User.IsActive)
            {
                lblIsActive.Text = "Yes";
            }
            else
            {
                lblIsActive.Text = "No";
            }
        }

        public void LoadUserInfo(int UserID)
        {
            _User = clsUser.Find(UserID);
            _UserID = UserID;
            if (_User == null)
            {
                ResetUserInfo();
                MessageBox.Show("No User with UserID = " + UserID.ToString());
                return;
            }
            _FillUserInfo();
        }

        public void LoadUserInfo(string UserName)
        {
            _User = clsUser.Find(UserName);
            _UserID = UserID;
            if (_User == null)
            {
                ResetUserInfo();
                return;
            }
            _FillUserInfo();
        }
    }
}
