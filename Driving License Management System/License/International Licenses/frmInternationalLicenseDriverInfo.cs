using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driving_License_Management_System.License.International_Driving_License
{
    public partial class frmInternationalLicenseDriverInfo : Form
    {
        private int _InternationalLicenseID = -1;
        public frmInternationalLicenseDriverInfo(int InternationalLicenseID)
        {
            InitializeComponent();
            _InternationalLicenseID = InternationalLicenseID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmInternationalLicenseDriverInfo_Load(object sender, EventArgs e)
        {
            if (!ctrlInternationalLicenseDriverInfo1.LoadData(_InternationalLicenseID))
            {
                MessageBox.Show("Something went error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
