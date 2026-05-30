using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using BusinessLayer;

namespace Driving_License_Management_System
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private DataTable LoadPersonData()
        {
            return clsPerson.GetAllPeople();
        }
        private void btnApplications_Click(object sender, EventArgs e)
        {

           


        }

        private void btnPeople_Click(object sender, EventArgs e)
        {
            frmManagePeople frmManagePeople = new frmManagePeople(LoadPersonData());
            frmManagePeople.ShowDialog();
        }
    }
}
