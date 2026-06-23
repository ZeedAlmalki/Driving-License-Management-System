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
    public partial class frmShowUserInfo : Form
    {

        private int _UserID = 0;
        private clsUser _User;

        public frmShowUserInfo(int UserID)
        {

            InitializeComponent();

            this._UserID = UserID;
        }

        private void frmShowUserInfo_Load(object sender, EventArgs e)
        {
            clsUser User = clsUser.Find(_UserID);

            if (User != null)
            {
                ctrlUserCard1.LoadUserInfo(User.UserID, User.PersonID);
                lblCurrentUser.Visible = (GlobalSettings.User.UserID == User.UserID);
            }
            else
            {
                MessageBox.Show("Something went wrong, please contact the developer");
                this.Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {

            this.Close();
        }
    }
}