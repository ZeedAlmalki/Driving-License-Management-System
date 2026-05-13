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
    public partial class ctrlPeopleDetails : UserControl
    {
        public ctrlPeopleDetails()
        {
            InitializeComponent();
        }
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
                if (Person.Gendor)
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
                this.lblCountry.Text = clsCountry.Find(Person.NationalityCountryID).CountryName.ToString();
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
        }
    }
}
