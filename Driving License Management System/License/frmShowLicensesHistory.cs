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

namespace Driving_License_Management_System.License
{
    public partial class frmShowLicensesHistory : Form
    {

        private int _PersonID = -1;
        public frmShowLicensesHistory(string NationalNo)
        {
            InitializeComponent();
            clsPerson person = clsPerson.Find(NationalNo);
            if (person != null)
            {
                _PersonID = person.PersonID;
            }
            else
            {
                MessageBox.Show("Person Is not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public frmShowLicensesHistory(int PersonID)
        {
            InitializeComponent();
            clsPerson person = clsPerson.FindByPersonID(PersonID);
            if (person != null)
            {
                _PersonID = person.PersonID;
            }
            else
            {
                MessageBox.Show("Person Is not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmShowLicensesHistory_Load(object sender, EventArgs e)
        {
            ctrlPersonCardWithFilter1.LoadPersonInfo(_PersonID);
            ctrlPersonCardWithFilter1.FilterEnabled = false;
            ctrlDriverLicenses1.GetAllLicensesForPersonByPersonID(_PersonID);
        }
    }
}
