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
using Driving_License_Management_System.License.International_Driving_License.Controls;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace Driving_License_Management_System.License.Controls
{
    public partial class ctrllFindLocalDrivingLicense : UserControl
    {
        public ctrllFindLocalDrivingLicense()
        {
            InitializeComponent();
        }
        private clsLicense _License;
        public event Action<int, bool> OnLicensenSelected;

        protected virtual void LicenseSelected(int LicenseID, bool AllowEdit = false)
        {
            Action<int, bool> handler = OnLicensenSelected;
            if (handler != null)
            {
                handler(LicenseID, AllowEdit);
            }
        }

        private bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            get
            {
                return _FilterEnabled;
            }
            set
            {
                _FilterEnabled = value;
                gbFilter.Enabled = _FilterEnabled;
            }
        }
        private bool RenewMode = false;

        public bool IsRenewMode
        {
            get
            {
                return RenewMode;
            }
            set
            {
                RenewMode = value;
            }
        }


        private int _LicenseID;
        public int LicenseID
        {
            get { return ctrlLicenseInfo1.LicenseID; }
        }

        public bool LoadDataByApplicationID(int ApplicationID)
        {
            return ctrlLicenseInfo1.LoadLicenseInfoByApplicatoinID(ApplicationID);
        }

        public bool LoadDataByLicenseID(int LicenseID)
        {
            return ctrlLicenseInfo1.LoadLicenseInfoByLicenseID(LicenseID);
        }
        public bool LoadLicenseInfoByLocalDrivingLicenseApplication(int LocalDrivingLicenseApplication)
        {
            return ctrlLicenseInfo1.LoadLicenseInfoByLocalDrivingLicenseApplication(LocalDrivingLicenseApplication);
        }

        public string AsignLicenseID
        {
            set
            {
                txtLicenseID.Text = value;
            }
            get
            {
                return txtLicenseID.Text;
            }
        }

        private void btnFindPerson_Click(object sender, EventArgs e)
        {

            if (!int.TryParse(txtLicenseID.Text, out int LicenseID)) 
            {
                MessageBox.Show("Please Enter a Correct License ID In Numbers", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _LicenseID = LicenseID;

            _License = clsLicense.FindLicenseByID(LicenseID);

            if (_License == null)
            {
                MessageBox.Show("License not found, please enter correct license", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            LicenseSelected(_LicenseID, true);

        }

        private void txtLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
