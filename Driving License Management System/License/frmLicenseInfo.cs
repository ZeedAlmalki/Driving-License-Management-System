using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driving_License_Management_System.License
{
    public partial class frmLicenseInfo : Form
    {
        private int _LocalDrivingLicenseApplicationID = -1;
        public frmLicenseInfo(int LocalDrivingLicenseApplcationID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplcationID;
        }

        private void frmLicenseInfo_Load(object sender, EventArgs e)
        {
            if (!ctrlLicenseInfo1.LoadLicenseInfo(_LocalDrivingLicenseApplicationID))
            {
                MessageBox.Show("Something Went error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
    }
}
