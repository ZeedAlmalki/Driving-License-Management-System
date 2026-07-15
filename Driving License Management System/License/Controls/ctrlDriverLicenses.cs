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

namespace Driving_License_Management_System.License.Controls
{
    public partial class ctrlDriverLicenses : UserControl
    {

        private int _PersonID = -1;
        DataTable _dtAllLicenses;
        DataTable _dtAllInternationalLicenses;


        public ctrlDriverLicenses()
        {
            InitializeComponent();
        }

        private void _LocalDrivingLicenseExecution()
        {
            if (_dtAllLicenses.Rows.Count == 0)
            {
                InternationalDriverLicenseDataGridView.DataSource = null;
                return;
            }

            _dtAllLicenses = _dtAllLicenses.DefaultView.ToTable(false, "LicenseID", "ApplicationID", "ClassName", "IssueDate", "ExpirationDate", "IsActive");

            LocalDriverLicenseDataGridView.DataSource = _dtAllLicenses;

            if (LocalDriverLicenseDataGridView.Rows.Count > 0)
            {
                LocalDriverLicenseDataGridView.Columns[0].HeaderText = "Lic.ID";
                LocalDriverLicenseDataGridView.Columns[0].Width = 50;


                LocalDriverLicenseDataGridView.Columns[1].HeaderText = "App.ID";
                LocalDriverLicenseDataGridView.Columns[1].Width = 70;


                LocalDriverLicenseDataGridView.Columns[2].HeaderText = "Class Name";
                LocalDriverLicenseDataGridView.Columns[2].Width = 150;


                LocalDriverLicenseDataGridView.Columns[3].HeaderText = "Issue Date";
                LocalDriverLicenseDataGridView.Columns[3].Width = 100;


                LocalDriverLicenseDataGridView.Columns[4].HeaderText = "Expiration Date";
                LocalDriverLicenseDataGridView.Columns[4].Width = 100;


                LocalDriverLicenseDataGridView.Columns[5].HeaderText = "Is Active";
                LocalDriverLicenseDataGridView.Columns[5].Width = 80;
            }
        }

        private void _InternatioanlDrivingLicenseExecution()
        {
            if (_dtAllInternationalLicenses.Rows.Count == 0)
            {
                InternationalDriverLicenseDataGridView.DataSource = null;
                return;
            }

            _dtAllInternationalLicenses = _dtAllInternationalLicenses.DefaultView.ToTable(false, "InternationalLicenseID", "ApplicationID", "IssuedUsingLocalLicenseID", "IssueDate", "ExpirationDate", "IsActive");

            InternationalDriverLicenseDataGridView.DataSource = _dtAllInternationalLicenses;


            if (InternationalDriverLicenseDataGridView.Rows.Count > 0)
            {
                InternationalDriverLicenseDataGridView.Columns[0].HeaderText = "Inter.License ID";
                InternationalDriverLicenseDataGridView.Columns[0].Width = 80;


                InternationalDriverLicenseDataGridView.Columns[1].HeaderText = "App.ID";
                InternationalDriverLicenseDataGridView.Columns[1].Width = 70;


                InternationalDriverLicenseDataGridView.Columns[2].HeaderText = "L.License ID";
                InternationalDriverLicenseDataGridView.Columns[2].Width = 150;


                InternationalDriverLicenseDataGridView.Columns[3].HeaderText = "Issue Date";
                InternationalDriverLicenseDataGridView.Columns[3].Width = 100;


                InternationalDriverLicenseDataGridView.Columns[4].HeaderText = "Expiration Date";
                InternationalDriverLicenseDataGridView.Columns[4].Width = 100;


                InternationalDriverLicenseDataGridView.Columns[5].HeaderText = "Is Active";
                InternationalDriverLicenseDataGridView.Columns[5].Width = 80;
                InternationalDriverLicenseDataGridView.Columns[5].ReadOnly = true;
            }
        }

        public void GetAllLicensesForPersonByPersonID(int PersonID)
        {
            _dtAllLicenses = clsLicense.GetLicensesByPersonID(PersonID);
            _dtAllInternationalLicenses = clsInternationalLicense.GetAllInternationalLicensesByPersonID(PersonID);
            if (_dtAllLicenses == null || _dtAllInternationalLicenses == null)
            {
                MessageBox.Show("Table was Null, Please Contact the Admin", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _LocalDrivingLicenseExecution();
            _InternatioanlDrivingLicenseExecution();
        }

        private void tcDriverLicenses_Selected(object sender, TabControlEventArgs e)
        {
            if (tcDriverLicenses.SelectedTab == tpLocal)
            {
                lblTotalRecords.Text = _dtAllLicenses.Rows.Count.ToString();
                return;
            }

            if (tcDriverLicenses.SelectedTab == tpInternational)
            {
                lblTotalRecords.Text = _dtAllInternationalLicenses.Rows.Count.ToString();
                return;
            }
        }
    }
}
