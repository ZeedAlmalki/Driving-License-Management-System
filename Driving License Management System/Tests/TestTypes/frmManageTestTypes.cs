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

namespace Driving_License_Management_System.TestTypes
{
    public partial class frmManageTestTypes : Form
    {
        public frmManageTestTypes()
        {
            InitializeComponent();
        }
        private static DataTable _dtAllTestTypes;

        private void frmManageTestTypes_Load(object sender, EventArgs e)
        {
            _dtAllTestTypes = clsManageTestType.GetAllTestTypes();
            ManageTestTypessDataGridView.DataSource = _dtAllTestTypes;
            lblTotalRecords.Text = _dtAllTestTypes.Rows.Count.ToString();

            if (ManageTestTypessDataGridView.Rows.Count > 0)
            {
                ManageTestTypessDataGridView.Columns[0].HeaderText = "ID";
                ManageTestTypessDataGridView.Columns[0].Width = 30;


                ManageTestTypessDataGridView.Columns[1].HeaderText = "Title";
                ManageTestTypessDataGridView.Columns[1].Width = 50;


                ManageTestTypessDataGridView.Columns[2].HeaderText = "Description";
                ManageTestTypessDataGridView.Columns[2].Width = 250;


                ManageTestTypessDataGridView.Columns[3].HeaderText = "Fees";
                ManageTestTypessDataGridView.Columns[3].Width = 130;
            }
        }

        private void EditTestTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUpdateTestType frmUpdateTestType = new frmUpdateTestType((clsManageTestType.enTestType)ManageTestTypessDataGridView.CurrentRow.Cells[0].Value);
            frmUpdateTestType.ShowDialog();
            frmManageTestTypes_Load(null, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
