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

namespace Driving_License_Management_System
{
    public partial class frmManageApplicationsTypes : Form
    {
        public frmManageApplicationsTypes()
        {
            InitializeComponent();
        }
        private static DataTable _dtAllApplicationTypes;


        private void frmManageApplicationsTypes_Load(object sender, EventArgs e)
        {
            _dtAllApplicationTypes = clsManageApplicationTypes.GetAllApplicationTypes();
            ApplicationTypesDataGridView.DataSource = _dtAllApplicationTypes;
            lblTotalRecords.Text = _dtAllApplicationTypes.Rows.Count.ToString();

            if (ApplicationTypesDataGridView.Rows.Count > 0)
            {
                ApplicationTypesDataGridView.Columns[0].HeaderText = "ID";
                ApplicationTypesDataGridView.Columns[0].Width = 30;

                ApplicationTypesDataGridView.Columns[1].HeaderText = "Title";
                ApplicationTypesDataGridView.Columns[1].Width = 50;

                ApplicationTypesDataGridView.Columns[2].HeaderText = "Fees";
                ApplicationTypesDataGridView.Columns[2].Width = 80;
            }
        }

        private void EditApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUpdateApplicationType frmUpdateApplication = new frmUpdateApplicationType((int)ApplicationTypesDataGridView.CurrentRow.Cells[0].Value);
            frmUpdateApplication.ShowDialog();
            frmManageApplicationsTypes_Load(null, null);
        }

      

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
