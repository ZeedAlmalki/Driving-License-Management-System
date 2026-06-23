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

namespace Driving_License_Management_System
{
    public partial class ctrlPersonCard : UserControl
    {
       

        /*    class MyPastCode
           { 
                   int globalPersonID = -1;

           public void LoadData(int PersonID)
           {
               clsPerson Person = clsPerson.Find(PersonID);
               if (Person != null)
               {
                   globalPersonID = PersonID;
                   this.lblPersonID.Text = Person.PersonID.ToString();
                   this.lblName.Text = (Person.FirstName + " " + Person.SecondName + " " + Person.ThirdName + " " + Person.LastName).ToString();
                   this.lblNationalNo.Text = Person.NationalNo.ToString();
                   if (Person.Gendor == 1)
                   {
                       lblGender.Text = "Female";
                       pbPerson.Image = Properties.Resources.Female_512;
                       lblGendorImage.Image = Properties.Resources.Woman_32;
                   }
                   else
                   {
                       lblGender.Text = "Male";
                       pbPerson.Image = Properties.Resources.Male_512;
                       lblGendorImage.Image = Properties.Resources.Man_32;
                   }
                   this.lblEmail.Text = Person.Email.ToString();
                   this.lblAddress.Text = Person.Address.ToString();
                   this.lblDateOfBirth.Text = Person.DateOfBirth.ToString("yyyy/MM/d");
                   this.lblPhone.Text = Person.Phone.ToString();
                   this.lblCountry.Text = Person.CountryInfo.CountryName.ToString();
                   if (!string.IsNullOrWhiteSpace(Person.ImagePath))
                   this.pbPerson.ImageLocation = Person.ImagePath;
               }
               else
               {
                   MessageBox.Show("Something went wrong.");
               }
           }

           private void lblEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
           {
               frmAddEditPersonInfo frmaddedit = new frmAddEditPersonInfo(globalPersonID);
               frmaddedit.ShowDialog();
               LoadData(globalPersonID);
           }
       }*/


        private clsPerson _Person;
        private int _PersonID = -1;

        public int PersonID
        {
            get { return _PersonID; }
        }

        public clsPerson SelectedPersonInfo
        {
            get { return _Person; }
        }
        public ctrlPersonCard()
        {
            InitializeComponent();
        }

        public void LoadPersonInfo(int PerosnID)
        {
            _Person = clsPerson.Find(PerosnID);

            if (_Person == null)
            {
                ResetPersonInfo();
                //MessageBox.Show("No Person With PersonID = " + PerosnID.ToString(), "ERROR", MessageBoxButtons.OK);
                return;
            }

            _FillPersonInfo();
        }

        public void LoadPersonInfo(string NatinoalNo)
        {
            _Person = clsPerson.Find(NatinoalNo);

            if (_Person == null)
            {
                ResetPersonInfo();
                MessageBox.Show("No Person With National Number = " + NatinoalNo, "ERROR", MessageBoxButtons.OK);
                return;
            }

            _FillPersonInfo();
        }
        private void _LoadPersonImage()
        {
            if (_Person.Gendor == 0)
                pbPerson.Image = Resources.Male_512;
            else
                pbPerson.Image = Resources.Female_512;


            string ImagePath = _Person.ImagePath;
            if (ImagePath != "")
            {
                if (File.Exists(ImagePath))
                    pbPerson.ImageLocation = ImagePath;
                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK);
            }
        }


        private void _FillPersonInfo()
        {
            _PersonID = _Person.PersonID;
            lblPersonID.Text = _Person.PersonID.ToString();
            lblNationalNo.Text = _Person.NationalNo.ToString();
            lblName.Text = _Person.FullName();
            lblGender.Text = _Person.Gendor == 0 ? "Male" : "Female";
            lblEmail.Text = _Person.Email;
            lblPhone.Text = _Person.Phone;
            lblDateOfBirth.Text = _Person.DateOfBirth.ToShortDateString();
            lblCountry.Text = clsCountry.Find(_Person.NationalityCountryID).CountryName;
            lblAddress.Text = _Person.Address;
            _LoadPersonImage();
        }


        private void lblEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddEditPersonInfo frmaddedit = new frmAddEditPersonInfo(_PersonID);
            frmaddedit.ShowDialog();
            LoadPersonInfo(_PersonID);
        }

        public void ResetPersonInfo()
        {
            _PersonID = -1;
            lblPersonID.Text = "";
            lblNationalNo.Text = "";
            lblName.Text = "";
            lblGender.Text = "";
            lblEmail.Text = "";
            lblPhone.Text = "";
            lblDateOfBirth.Text = "";
            lblCountry.Text = "";
            lblAddress.Text = "";

            if (pbPerson.ImageLocation != null)
            pbPerson.ImageLocation = null;
        }
    }
}
