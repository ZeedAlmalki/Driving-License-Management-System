using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using BusinessLayer;
using Guna.UI2.WinForms;

namespace Driving_License_Management_System
{
    public partial class ctrlAddNewEdit : UserControl
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;

        int _PersonID;
        clsPerson _Person;

        public ctrlAddNewEdit()
        {
            InitializeComponent();
        }


        private void _ResetDefaultImage()
        {
            if (rbMale.Checked)
                pbPerson.Image = Properties.Resources.Male_512;
            else
                pbPerson.Image = Properties.Resources.Female_512;
        }

        private void _LoadPersonImage(string imagePath)
        {
            if (File.Exists(imagePath))
            {
                using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                {
                    pbPerson.Image = Image.FromStream(fs);
                }
            }
            else
            {
                _ResetDefaultImage();
            }
        }

        private void _FillCountiresInComboBox()
        {
            DataTable dtCountries = clsCountry.GetAllCountries();

            foreach (DataRow row in dtCountries.Rows)
            {
                cbCountry.Items.Add(row["CountryName"]);
            }
        }

        private void _LoadData()
        {
            _FillCountiresInComboBox();
            cbCountry.SelectedIndex = 0;

            if (_Mode == enMode.AddNew)
            {
                lblMode.Text = "Add New Person";
                _Person = new clsPerson();
                return;
            }

            _Person = clsPerson.Find(_PersonID);

            if (_Person == null)
            {
                MessageBox.Show("No Person with ID = " + _PersonID);
                this.Parent.Controls.Remove(this);
                this.Dispose();
                return;
            }

            lblMode.Text = "Edit Person ID = " + _Person.PersonID.ToString();
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
            if (_Person.Gendor == true)
            { 
                rbFemale.Checked = true;
                rbMale.Checked = false;
            }
            else
            { 
                rbMale.Checked = true;
                rbFemale.Checked = false;
            }

            if (!string.IsNullOrWhiteSpace(_Person.ImagePath))
            {
                _LoadPersonImage(_Person.ImagePath);
            }

            lblRemoveImage.Visible = (!string.IsNullOrWhiteSpace(_Person.ImagePath));
            cbCountry.SelectedIndex = cbCountry.FindString(clsCountry.Find(_Person.NationalityCountryID).CountryName);


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


        void GenderSettings()
        {
            if (rbFemale.Checked)
            {

                _Person.Gendor = true;
            }
            else 
            {
                _Person.Gendor = false;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {

            if (IsPersonExist())
            {
                MessageBox.Show("National Number is Used By another person");
                return;
            }
            int CountryID = clsCountry.Find(cbCountry.Text).ID;
            _Person.FirstName = txtFirstName.Text;
            _Person.SecondName = txtSecondName.Text;
            _Person.ThirdName = txtThirdName.Text;
            _Person.LastName = txtLastName.Text;
            _Person.NationalNo = txtNationalNumber.Text;
            _Person.Email = txtEmail.Text;
            _Person.Address = txtAddress.Text;
            _Person.Phone = txtPhone.Text;
            _Person.DateOfBirth = dtpDateOfBirth.Value;
            _Person.NationalityCountryID = CountryID;
            GenderSettings();


            if (_RemoveImage && !string.IsNullOrWhiteSpace(_Person.ImagePath))
            {
                string imagePathToDelete = _Person.ImagePath;
                _Person.ImagePath = "";
                _ResetDefaultImage();

                if (File.Exists(imagePathToDelete))
                {
                    File.Delete(imagePathToDelete);
                    //File.Move(_Person.ImagePath, @"C:\Course 19 DVLD\DeletedImages\" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + Path.GetExtension(_Person.ImagePath) + Guid.NewGuid()) ;
                }
            }




            if (!string.IsNullOrWhiteSpace(_NewImagePath))
            {
                string folderPath = clsImageSettings.FolderPath;
                if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

                string destinationPath = folderPath + Guid.NewGuid().ToString() + Path.GetExtension(_NewImagePath);


                File.Copy(_NewImagePath, destinationPath);
                _Person.ImagePath = destinationPath;
                _LoadPersonImage(_Person.ImagePath);
            }


            if ((string.IsNullOrWhiteSpace(_Person.FirstName) || string.IsNullOrWhiteSpace(_Person.SecondName) || string.IsNullOrWhiteSpace(_Person.ThirdName) ||
                string.IsNullOrWhiteSpace(_Person.LastName) || string.IsNullOrWhiteSpace(_Person.NationalNo) || isEmailValid() ||
                string.IsNullOrWhiteSpace(_Person.Address) || string.IsNullOrWhiteSpace(_Person.Phone) || dtpDateOfBirth == null || string.IsNullOrWhiteSpace(_Person.NationalityCountryID.ToString())))
            {
                MessageBox.Show("Please fill in the requirements as required");
                return;
            }



            if (_Person.Save())
            {

                MessageBox.Show("Data Saved Successfully.");
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.");

            _Mode = enMode.Update;
            lblMode.Text = "Edit Person ID = " + _Person.PersonID;
            lblPersonID.Text = _Person.PersonID.ToString();

        }

        public void LoadPersonData(int PersonID)
        {
            _PersonID = PersonID;
            _Mode = (_PersonID == -1) ? enMode.AddNew : enMode.Update;
            _LoadData();
        }

        private void ctrlAddNewEdit_Load(object sender, EventArgs e)
        {
            //_LoadData();
            dtpDateOfBirth.MaxDate = DateTime.Today.AddYears(-18);
        }

      

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
            if (IsPersonExist())
            {
                e.Cancel = true;
                errorProvider1.SetError(txt, $"{txt.Name} National Number is Used By another person");
            }
            else
            {
                errorProvider1.SetError(txt, "");
                e.Cancel = false;
            }
        }


        private void Gender_CheckedChanged(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(_NewImagePath) && string.IsNullOrWhiteSpace(pbPerson.ImageLocation))
            {
                if (rbMale.Checked)
                    pbPerson.Image = Properties.Resources.Male_512;
                else
                {
                    pbPerson.Image = Properties.Resources.Female_512;
                }
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
            if (ofdPicture.ShowDialog() == DialogResult.OK)
            {
                if (_Mode == enMode.Update && !string.IsNullOrWhiteSpace(_Person.ImagePath))
                {
                    _RemoveImage = true;
                }
                else
                    _RemoveImage = false;
                _NewImagePath = ofdPicture.FileName;
                _LoadPersonImage(ofdPicture.FileName);
                lblRemoveImage.Visible = true;
            }
        }

        private void lblRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            _RemoveImage = true;
            pbPerson.ImageLocation = "";
            _ResetDefaultImage();
            lblRemoveImage.Visible = false;
        }

        private bool isEmailValid()
        {
            if (txtEmail.TextLength > 0 && !txtEmail.Text.Contains("@"))
                return true;
            else
                return false;
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (isEmailValid())
            {
                e.Cancel = true;
                txtEmail.Focus();
                errorProvider1.SetError(txtEmail, $"{txtEmail.Name} Email Must Contain @");
                return;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtEmail, "");
            }
        }
    }
}