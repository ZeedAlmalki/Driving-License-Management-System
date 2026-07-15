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

namespace Driving_License_Management_System.License.Controls
{
    public partial class ctrllFindLocalDrivingLicense : UserControl
    {
        public ctrllFindLocalDrivingLicense()
        {
            InitializeComponent();
        }
        private clsLicense _License;
        public event Action<int> OnLicensenSelected;

        protected virtual void LicenseSelected(int LicenseID)
        {
            Action<int> handler = OnLicensenSelected;
            if (handler != null)
            {
                handler(LicenseID);
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

        public int InternationalLicenseApplicationID
        {
            get
            {
                return ctrlInternationalDrivingLicenseApplicationInfo1.ILApplicationID;
            }
            set
            {
                ctrlInternationalDrivingLicenseApplicationInfo1.ILApplicationID = value;
            }
        }

        public int InternationalLicenseID
        {
            get
            {
                return ctrlInternationalDrivingLicenseApplicationInfo1.ILLicenseID;
            }
            set
            {
                ctrlInternationalDrivingLicenseApplicationInfo1.ILLicenseID = value;
            }
        }

        private int _LicenseID;
        public int LicenseID
        {
            get { return ctrlLicenseInfo1.LicenseID; }
        }


        private void btnFindPerson_Click(object sender, EventArgs e)
        {

            if (!int.TryParse(txtLicenseID.Text, out int LicenseID)) // we re check and continue all the things to orindary driving licenese for the same person.
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

            int outLicenseID = -1;
            if (!clsLocalDrivingLicenseApplication.ItHasLocalDrivingLicenseClassBefore((int)clsLicenseClass.LicenseClass.OrdinaryDrivingLicense, _License.PersonID, ref outLicenseID))
            {
                MessageBox.Show("You Must have an Ordiranry License class before you apply in International license..", "Must be Have correct license", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_License.LicenseClass != 3 && outLicenseID != -1)
            {
                txtLicenseID.Text = outLicenseID.ToString(); // we asign the txtLicenseID before we back in the function
                btnFindPerson.PerformClick();
                return;
            }

            if (!_License.IsActive)
            {
                MessageBox.Show("The License You use is not active, please active it", "Must be active", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (DateTime.Now > _License.ExpirationDate)
            {
                MessageBox.Show("The License You use is Expired", "Expired", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (clsInternationalLicense.ItHasInternationalDrivingLicense(_LicenseID))
            {
                MessageBox.Show("You already have an active internatinoal license.", "You already have an internatnioal license.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ctrlInternationalDrivingLicenseApplicationInfo1.LoadData(_LicenseID);

            if (!ctrlLicenseInfo1.LoadLicenseInfoByApplicatoinID(_License.ApplicationID))
            {
                MessageBox.Show("Something went error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlInternationalDrivingLicenseApplicationInfo1.LoadData(-1);
                return;
            }
            LicenseSelected(_LicenseID);
        }

        private void txtLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
