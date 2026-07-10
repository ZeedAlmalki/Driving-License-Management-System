using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Driving_License_Management_System.People;

namespace Driving_License_Management_System.Applications.LocalDrivingLicenseApplication
{
    public partial class frmShowLocalDrivingLicenseApplicationInfo : Form
    {
        public frmShowLocalDrivingLicenseApplicationInfo(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            ctrlDrivingLicenseApplicationInfo1.LoadLocalDrivingLicenseApplicationInfo(LocalDrivingLicenseApplicationID);
        }
    }
}
