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

        public clsUser SelectedUserInfo
        {
            get { return _User; }
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
            _UserID = _User.UserID;
            lblUserID.Text = _User.UserID.ToString();
            lblUserName.Text = _User.UserName.ToString();
            lblIsActive.Text = _User.IsActive.ToString();
        }

        public void LoadUserInfo(int UserID, int PersonID)
        {
            _User = clsUser.Find(UserID);
            
            if (_User == null)
            {
                ResetUserInfo();
                return;
            }
            ctrlPersonCard1.LoadPersonInfo(PersonID);
            _FillUserInfo();
        }

        public void LoadUserInfo(string UserName, int PersonID)
        {
            _User = clsUser.Find(UserName);

            if (_User == null)
            {
                ResetUserInfo();
                return;
            }
            ctrlPersonCard1.LoadPersonInfo(PersonID);
            _FillUserInfo();
        }


    }
}
