using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer;
using Driving_License_Management_System.Properties;
using Guna.UI2.WinForms;

namespace Driving_License_Management_System
{
    public partial class frmAddEditPersonInfo : Form
    {

        public delegate void DataBackEventHandler(object sender, int PersonID);

        public event DataBackEventHandler DataBack;

        public enum enMode { AddNew = 0, Update = 1 };
        public enum enGendor { Male = 0, Female = 1 };
        private enMode _Mode;


        int _PersonID;
        clsPerson _Person;

        public frmAddEditPersonInfo()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
            //ctrlAddNewEdit1.LoadPersonData(PersonID);
        }

        public frmAddEditPersonInfo(int PersonID)
        {
            InitializeComponent();

            _Mode = enMode.Update;
            _PersonID = PersonID;
        }

        private void frmAddEditPersonInfo_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();

            if (_Mode == enMode.Update)
                _LoadData();
        }
        private void _FillCountiresInComboBox()
        {
            DataTable dtCountries = clsCountry.GetAllCountries();

            foreach (DataRow row in dtCountries.Rows)
            {
                cbCountry.Items.Add(row["CountryName"]);
            }
        }
        private void _ResetDefaultValues()
        {
            _FillCountiresInComboBox();
            if (_Mode == enMode.AddNew)
            {
                lblMode.Text = "Add New Person";
                _Person = new clsPerson();
            }
            else
            {
                lblMode.Text = "Update Person";
            }

            if (rbMale.Checked)
                pbPerson.Image = Resources.Male_512;
            else
                pbPerson.Image = Resources.Female_512;

            lblRemoveImage.Visible = (pbPerson.ImageLocation != null);
            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            dtpDateOfBirth.Value = dtpDateOfBirth.MaxDate; // default value of dttime picker

            dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-100);
            cbCountry.SelectedIndex = cbCountry.FindString("Saudi Arabia");

            txtFirstName.Text = string.Empty;
            txtSecondName.Text = string.Empty;
            txtThirdName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtNationalNumber.Text = string.Empty;
            rbMale.Checked = true;
            txtPhone.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtAddress.Text = string.Empty;

        }


        private void _LoadData()
        {

            _Person = clsPerson.Find(_PersonID);

            if (_Person == null)
            {
                MessageBox.Show("No Person with ID = " + _PersonID);
                this.Close();
                return;
            }

            lblPersonID.Text = _Person.PersonID.ToString();
            txtFirstName.Text = _Person.FirstName;
            txtSecondName.Text = _Person.SecondName;
            txtThirdName.Text = _Person.ThirdName;
            txtLastName.Text = _Person.LastName;
            txtNationalNumber.Text = _Person.NationalNo;
            txtEmail.Text = _Person.Email;
            txtAddress.Text = _Person.Address;
            dtpDateOfBirth.Value = _Person.DateOfBirth;
            txtPhone.Text = _Person.Phone;
            if (_Person.Gendor == 1)
            {
                rbFemale.Checked = true;
                //rbMale.Checked = false;
            }
            else
            {
                rbMale.Checked = true;
                //rbFemale.Checked = false;
            }

            //if (!string.IsNullOrWhiteSpace(_Person.ImagePath))
            //{
            //    _LoadPersonImage(_Person.ImagePath);
            //}

            if (_Person.ImagePath != "")
                pbPerson.ImageLocation = _Person.ImagePath;

            lblRemoveImage.Visible = (!string.IsNullOrWhiteSpace(_Person.ImagePath));
            //cbCountry.SelectedIndex = cbCountry.FindString(clsCountry.Find(_Person.NationalityCountryID).CountryName);
             cbCountry.SelectedIndex = cbCountry.FindString(_Person.CountryInfo.CountryName);

        }



        void GenderSettings()
        {
            if (rbFemale.Checked)
            {

                _Person.Gendor = (short)enGendor.Female;
            }
            else
            {
                _Person.Gendor = (short)enGendor.Male;
            }
        }


        bool IsPersonExist()
        {
            if ((clsPerson.IsPersonExist(txtNationalNumber.Text) && _Mode == enMode.Update && _Person.NationalNo != txtNationalNumber.Text)
                /*update*/ ||
                (clsPerson.IsPersonExist(txtNationalNumber.Text) && _Mode == enMode.AddNew))/*add*/
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool _HandlePersonImage()
        {
            if (_Person.ImagePath != pbPerson.ImageLocation)
            {
                if (_Person.ImagePath != "")
                {
                    try
                    {
                        File.Delete(_Person.ImagePath);
                    }
                    catch (IOException e)
                    {

                    }
                }
                if (pbPerson.ImageLocation != null)
                {
                    string SourceImageFile = pbPerson.ImageLocation.ToString();
                    if (string.IsNullOrWhiteSpace(SourceImageFile))
                        return true;

                    if (clsUtil.CopyImageProjectImageFolder(ref SourceImageFile))
                    {
                        pbPerson.ImageLocation = SourceImageFile;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK);
                        return false;
                    }
                }
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (!this.ValidateChildren())
            {
                MessageBox.Show("Please fill in the requirements as required");
            }

            if (!_HandlePersonImage())
                return;

            if (IsPersonExist())
            {
                MessageBox.Show("National Number is Used By another person");
                return;
            }
            int CountryID = clsCountry.Find(cbCountry.Text).ID;
            _Person.FirstName = txtFirstName.Text.Trim();
            _Person.SecondName = txtSecondName.Text.Trim();
            _Person.ThirdName = txtThirdName.Text.Trim();
            _Person.LastName = txtLastName.Text.Trim();
            _Person.NationalNo = txtNationalNumber.Text.Trim();
            _Person.Email = txtEmail.Text.Trim();
            _Person.Address = txtAddress.Text.Trim();
            _Person.Phone = txtPhone.Text.Trim();
            _Person.DateOfBirth = dtpDateOfBirth.Value;
            _Person.NationalityCountryID = CountryID;
            if (pbPerson.ImageLocation != null)
                _Person.ImagePath = pbPerson.ImageLocation;
            else
                _Person.ImagePath = "";
            GenderSettings();

            //if (_RemoveImage && !string.IsNullOrWhiteSpace(_Person.ImagePath))
            //{
            //    string imagePathToDelete = _Person.ImagePath;
            //    _Person.ImagePath = "";
            //    _ResetDefaultImage();

            //    if (File.Exists(imagePathToDelete))
            //    {
            //        File.Delete(imagePathToDelete);
            //        //File.Move(_Person.ImagePath, @"C:\Course 19 DVLD\DeletedImages\" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + Path.GetExtension(_Person.ImagePath) + Guid.NewGuid()) ;
            //    }
            //}





            //if (!string.IsNullOrWhiteSpace(_NewImagePath))
            //{
            //    string folderPath = clsImageSettings.FolderPath;
            //    if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

            //    string destinationPath = folderPath + Guid.NewGuid().ToString() + Path.GetExtension(_NewImagePath);


            //    File.Copy(_NewImagePath, destinationPath);
            //    _Person.ImagePath = destinationPath;
            //    _LoadPersonImage(_Person.ImagePath);
            //}


            //if ((string.IsNullOrWhiteSpace(_Person.FirstName) || string.IsNullOrWhiteSpace(_Person.SecondName) || string.IsNullOrWhiteSpace(_Person.ThirdName) ||
            //    string.IsNullOrWhiteSpace(_Person.LastName) || string.IsNullOrWhiteSpace(_Person.NationalNo) || isEmailValid() ||
            //    string.IsNullOrWhiteSpace(_Person.Address) || string.IsNullOrWhiteSpace(_Person.Phone) || dtpDateOfBirth == null || string.IsNullOrWhiteSpace(_Person.NationalityCountryID.ToString())))
            //{
            //    MessageBox.Show("Please fill in the requirements as required");
            //    return;
            //}



            if (_Person.Save())
            {
                lblPersonID.Text = _Person.PersonID.ToString();
                _Mode = enMode.Update;
                lblMode.Text = "Update Person";
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK);

                DataBack?.Invoke(this, _Person.PersonID);
            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK);
            }
        }

        private void _ResetDefaultImage()
        {
            if (rbMale.Checked)
                pbPerson.Image = Properties.Resources.Male_512;
            else
                pbPerson.Image = Properties.Resources.Female_512;
        }

        //private void _LoadPersonImage(string imagePath)
        //{
        //    if (File.Exists(imagePath))
        //    {
        //        using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
        //        {
        //            pbPerson.Image = Image.FromStream(fs);
        //        }
        //    }
        //    else
        //    {
        //        _ResetDefaultImage();
        //    }
        //}





 
    

        //public void LoadPersonData(int PersonID)
        //{
        //    _PersonID = PersonID;
        //    _Mode = (_PersonID == -1) ? enMode.AddNew : enMode.Update;
        //    _LoadData();
        //}




        void txtValidating(Guna2TextBox txt, CancelEventArgs e/*, int maxLength , you can use it with tags*/)
        {
            if (string.IsNullOrWhiteSpace(txt.Text))
            {
                e.Cancel = true;
                txt.Focus();
                errorProvider1.SetError(txt, $"{txt.Name} Field Should have a value");
            }
            //else if (txt.Text.Length > maxLength)
            //{
            //    e.Cancel = true;
            //    txt.Focus();
            //    errorProvider1.SetError(txt, $"{txt.Name} Field Should Be Less Than {maxLength} Letters");
            //}
            else
            {
                errorProvider1.SetError(txt, "");
                e.Cancel = false;
            }
        }



        private void txtInfo_Validating(object sender, CancelEventArgs e)
        {
            txtValidating((Guna2TextBox)sender, e);
        }

        private void NationalNo_Validating(object sender, CancelEventArgs e)
        {
            Guna2TextBox txt = (Guna2TextBox)sender;

            if (string.IsNullOrEmpty(txtNationalNumber.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNumber, "This field is required!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtNationalNumber, null);
            }

            if (IsPersonExist())
            {
                e.Cancel = true;
                errorProvider1.SetError(txt, "National Number is Used By another person");
            }
            else
            {
                errorProvider1.SetError(txt, null);
                e.Cancel = false;
            }
        }


        private void Gender_CheckedChanged(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(pbPerson.ImageLocation) /* && string.IsNullOrWhiteSpace(_NewImagePath) &&*/)
            {
                if (rbMale.Checked)
                    pbPerson.Image = Properties.Resources.Male_512;
                else

                    pbPerson.Image = Properties.Resources.Female_512;
            }
        }

        private string _NewImagePath = "";
        private bool _RemoveImage = false;

        private void lblSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ofdPicture.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            ofdPicture.FilterIndex = 1;
            ofdPicture.RestoreDirectory = true;
            ofdPicture.Title = "Select a Picture";
        //    if (ofdPicture.ShowDialog() == DialogResult.OK)
        //    {
        //        if (_Mode == enMode.Update && !string.IsNullOrWhiteSpace(_Person.ImagePath))
        //        {
        //            _RemoveImage = true;
        //        }
        //        else
        //            _RemoveImage = false;
        //        _NewImagePath = ofdPicture.FileName;
        //        _LoadPersonImage(ofdPicture.FileName);
        //        lblRemoveImage.Visible = true;
        //    }

            if (ofdPicture.ShowDialog() == DialogResult.OK)
            {
                string SelectedFilePath = ofdPicture.FileName;
                //pbPerson.Load(SelectedFilePath);
                pbPerson.ImageLocation = SelectedFilePath;
                lblRemoveImage.Visible = true;
            }
        }

        private void lblRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            //_RemoveImage = true;
            pbPerson.ImageLocation = "";
            _ResetDefaultImage();
            lblRemoveImage.Visible = false;
        }

        //private bool isEmailValid()
        //{
        //    if (txtEmail.TextLength > 0 && !txtEmail.Text.Contains("@"))
        //        return true;
        //    else
        //        return false;
        //}

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (!clsValidation.isEmailValid(txtEmail.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, $"Invalid Email Address Format!");
            }
            else
            {
                errorProvider1.SetError(txtEmail, "");
            }
        }


        private void btnClose(object sender, EventArgs e)
        {
            this.Close();
        }



      
    }
}
