using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Guna.UI2.WinForms;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Driving_License_Management_System
{
    public class clsValidation
    {

        public static bool isEmailValid(string Email)
        {
            string pattern = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$";

            var regex = new Regex(pattern);

            return regex.IsMatch(Email);
        }

        public static bool IsNumberValid(string Number)
        {
            string pattern = @"^\d+(\.\d+)?$";

            return Regex.IsMatch(Number, pattern);
        }


        public static void txtIsNotNullOrWhiteSpaceValdiateHandling(Guna2TextBox txt, CancelEventArgs e, ErrorProvider er/*, int maxLength , you can use it with tags*/)
        {

            if (string.IsNullOrWhiteSpace(txt.Text))
            {
                e.Cancel = true;
                txt.Focus();
                er.SetError(txt, $"{txt.Name} Field Should have a value");
            }
            else
            {
                e.Cancel = false;
                er.SetError(txt, null);
            }
            //if (string.IsNullOrWhiteSpace(txt.Text))
            //{
            //    e.Cancel = true;
            //    txt.Focus();
            //    errorProvider1.SetError(txt, $"{txt.Name} Field Should have a value");
            //}
            ////else if (txt.Text.Length > maxLength)
            ////{
            ////    e.Cancel = true;
            ////    txt.Focus();
            ////    errorProvider1.SetError(txt, $"{txt.Name} Field Should Be Less Than {maxLength} Letters");
            ////}
            //else
            //{
            //    errorProvider1.SetError(txt, "");
            //    e.Cancel = false;
            //}
        }
        public static bool IsPasswordMatch(string Password, string ConfirmPassword)
        {
            return (Password.Trim() == ConfirmPassword.Trim());
        }

    }
}
