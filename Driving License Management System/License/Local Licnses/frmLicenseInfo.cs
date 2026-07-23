using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Driving_License_Management_System.License
{
    public partial class frmLicenseInfo : Form
    {
        private int _LicenseID = -1;
        private int _ApplicationID = -1;


        public frmLicenseInfo(int LicenseID) 
        {
            InitializeComponent();
            _LicenseID = LicenseID;
        }

        private void frmLicenseInfo_Load(object sender, EventArgs e)
        {

            if (_LicenseID != -1)
            {
                if (!ctrlLicenseInfo1.LoadLicenseInfoByLicenseID(_LicenseID))
                {
                    MessageBox.Show("Something Went error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
        }

 

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
