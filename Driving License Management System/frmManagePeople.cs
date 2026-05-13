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
using Guna.UI2.WinForms;
using static Driving_License_Management_System.ctrlPeopleGridView;

namespace Driving_License_Management_System
{
    public partial class frmManagePeople : Form
    {


        public frmManagePeople(DataTable dt)
        {
            InitializeComponent();
            ctrlPeopleGridView1.LoadDataToGrid(dt);
        }

        private void btnApplications_Click(object sender, EventArgs e)
        {
            frmAddEditPersonInfo frmAddPerson = new frmAddEditPersonInfo(-1);
            frmAddPerson.ShowDialog();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
