using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer;
using Driving_License_Management_System;

namespace Driving_License_Management_System
{
    public partial class frmAddNewLocalDrivingLicenseApplication : Form
    {

        private int _LocalDrivingLicenseApplicationID = -1;
        private clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication;

        private const int ApplicationTypeID = 1;
        private readonly decimal ApplicationFees = clsManageApplicationTypes.FindApplicationType(ApplicationTypeID).ApplicationFees;

        public enum enMode { Add = 0, Update = 1 };
        private enMode _Mode;

        public frmAddNewLocalDrivingLicenseApplication()
        {
            InitializeComponent();
            _Mode = enMode.Add;
        }

        public frmAddNewLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            this._LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
        }
        private void _LoadData()
        {
            ctrlPersonCardWithFilter1.FilterEnabled = false;
            LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseApplicationByID(_LocalDrivingLicenseApplicationID);
            if (_Mode == enMode.Update && (LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationSatus.Completed || LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationSatus.Cancelled))
            {
                MessageBox.Show("You cannot update a canceled or completed license.", "Update Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                return;
            }

            if (LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("No Local Driving License Application with ID = " + _LocalDrivingLicenseApplicationID, "User Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            ctrlPersonCardWithFilter1.LoadPersonInfo(LocalDrivingLicenseApplication.ApplicantPersonID);
            cbLicenseClass.SelectedIndex = cbLicenseClass.FindString(clsLicenseClass.FindLicenseClassByID(LocalDrivingLicenseApplication.LicenseClassID).ClassName);
            lblDLApplicationID.Text = LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationsID.ToString();
            lblApplicatoinDate.Text = LocalDrivingLicenseApplication.ApplicationDate.ToString();
            lblApplicationFees.Text = LocalDrivingLicenseApplication.PaidFees.ToString();
            lblCreatedBy.Text = GlobalSettings.User.UserName;


            // the code before i explore to use find with the base class.
            //ctrlPersonCardWithFilter1.LoadPersonInfo(clsApplication.FindApplicationByID(LocalDrivingLicenseApplication.ApplicationID).ApplicantPersonID);
            //cbLicenseClass.SelectedIndex = cbLicenseClass.FindString(clsLicenseClass.FindLicenseClassByID(LocalDrivingLicenseApplication.LicenseClassID).ClassName);


            //lblDLApplicationID.Text = LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationsID.ToString();
            //lblApplicatoinDate.Text = LocalDrivingLicenseApplication.ApplicationDate.ToString();
            //lblApplicationFees.Text = clsManageApplicationTypes.FindApplicationType(clsApplication.FindApplicationByID(LocalDrivingLicenseApplication.ApplicationID).ApplicationTypeID).ApplicationFees.ToString();
        }




        private void _FillLicenseClassesInComboBox()
        {
            DataTable dtLicenseClasses = clsLicenseClass.GetAllLicenseClasses();

            foreach (DataRow row in dtLicenseClasses.Rows)
            {
                cbLicenseClass.Items.Add(row["ClassName"]);
            }
        }


        private void _ResetDefaultValues()
        {
            if (_Mode == enMode.Add)
            {
                lblMode.Text = "New Local Driver License Application";
                this.Text = "New Local Driver License Application";
                LocalDrivingLicenseApplication = new clsLocalDrivingLicenseApplication();
                tpApplicationInfo.Enabled = false;
            }
            else
            {
                lblMode.Text = "Update Local Driver License Application";
                this.Text = "Update Local Driver License Application";
            }

            _FillLicenseClassesInComboBox();
            lblApplicationFees.Text = ApplicationFees.ToString();
            lblCreatedBy.Text = GlobalSettings.User.UserName;
            lblDLApplicationID.Text = "???";
            ctrlPersonCardWithFilter1.ResetPersonInfo();
        }




        private void frmAddNewLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();
            if (_Mode == enMode.Update)
                _LoadData();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {


            if (ctrlPersonCardWithFilter1.PersonID != -1)
            {
                btnSave.Enabled = true;
                tpApplicationInfo.Enabled = true;
                if (_Mode == enMode.Add)
                {
                    lblApplicatoinDate.Text = DateTime.Now.ToString();
                }
                tcLocalDrivingLicense.SelectedTab = tcLocalDrivingLicense.TabPages["tpApplicationInfo"];
                return;
            }
            else
            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {

            if (!this.ValidateChildren())
            {
                MessageBox.Show("Please fill in the requirements as required");
                return;
            }

            clsLicenseClass LicenseClass = clsLicenseClass.FindLicenseClassByClassName(cbLicenseClass.Text);
            int LicenseClassID = 0;
            if (LicenseClass != null)
            {
                LicenseClassID = LicenseClass.LicenseClassID;
            }
            int ApplicationID = 0;

            if (clsLocalDrivingLicenseApplication.IsPersonHasActiveLicenseClass(ctrlPersonCardWithFilter1.PersonID, LicenseClassID, ref ApplicationID))
            {
                MessageBox.Show("Choose Another License Class, the selected Person Already Has an active application for the selected class with id = " + ApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (_Mode == enMode.Add)
            {
                LocalDrivingLicenseApplication.ApplicationDate = Convert.ToDateTime(lblApplicatoinDate.Text);
                LocalDrivingLicenseApplication.ApplicationStatus = clsApplication.enApplicationSatus.New;
            }
            LocalDrivingLicenseApplication.LicenseClassID = LicenseClassID;
            LocalDrivingLicenseApplication.ApplicantPersonID = ctrlPersonCardWithFilter1.PersonID;
            LocalDrivingLicenseApplication.LastStatusDate = DateTime.Now;
            LocalDrivingLicenseApplication.ApplicationTypeID = ApplicationTypeID;
            LocalDrivingLicenseApplication.PaidFees = ApplicationFees;
            LocalDrivingLicenseApplication.CreatedByUserID = GlobalSettings.User.UserID;

            if (LocalDrivingLicenseApplication.Save())
            {
                MessageBox.Show("Data Saved Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _Mode = enMode.Update;
                lblMode.Text = "Update Local Driver License Application";
                this.Text = "Update Local Driver License Application";
                lblDLApplicationID.Text = LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationsID.ToString();
            }
            else
            {
                MessageBox.Show("Something went error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        

        private void cbLicenseClass_Validating(object sender, CancelEventArgs e)
        {
            if  (cbLicenseClass.SelectedItem == null)
            {
                errorProvider1.SetError(cbLicenseClass, "Please Select a License Class From ComboBox");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(cbLicenseClass, null);
                e.Cancel = false;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}







//private void MyPastSaveClickCode()
//{
//    clsApplication applicatoin = new clsApplication();
//    if (_Mode == enMode.Add)
//    {
//        applicatoin.ApplicationDate = DateTime.Now;
//        applicatoin.ApplicationStatus = clsApplication.enApplicationSatus.New;
//    }
//    applicatoin.ApplicantPersonID = ctrlPersonCardWithFilter1.PersonID;
//    applicatoin.LastStatusDate = DateTime.Now;
//    // after save.
//    applicatoin.ApplicationTypeID = ApplicationTypeID;
//    applicatoin.PaidFees = ApplicationFees;
//    applicatoin.CreatedByUserID = GlobalSettings.User.UserID;
//    if (applicatoin.Save())
//    {
//        LocalDrivingLicenseApplication.ApplicationID = applicatoin.ApplicationID;
//        LocalDrivingLicenseApplication.LicenseClassID = LicenseClassID;
//        if (LocalDrivingLicenseApplication.Save())
//        {
//            MessageBox.Show("Data Saved Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
//            _Mode = enMode.Update;
//        }
//        else
//        {
//            MessageBox.Show("Something went error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//        }
//    }
//}
