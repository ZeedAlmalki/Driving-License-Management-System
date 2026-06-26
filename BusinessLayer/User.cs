using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DataAccessLayer;

namespace BusinessLayer
{
    public class clsUser
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public clsPerson Person;

        public clsUser()
        {
            this.UserID = -1;
            this.UserName = "";
            this.Password = "";
            this.IsActive = false;
        }

        private clsUser(int UserID, int PersonID, string UserName, string Password, bool IsActive)
        {
            this.UserID = UserID;
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = IsActive;
            this.PersonID = PersonID;
            this.Person = clsPerson.Find(PersonID);
            Mode = enMode.Update;
        }

        private bool _AddNewUser()
        {
            this.UserID = clsUserData.AddNewUser(this.PersonID, this.UserName, this.Password, this.IsActive);

            return (this.UserID != -1);
        }

        private bool _UpdateUser()
        {
            return clsUserData.UpdateUser(this.UserID, this.PersonID, this.UserName, this.Password, this.IsActive);
        }

        public static DataTable GetAllUsers()
        {
            return clsUserData.GetAllUsers();
        }

        public static bool DeleteUser(int UserID)
        {
            return clsUserData.DeleteUser(UserID);
        }

        public static bool IsUserExist(string UserName)
        {
            return clsUserData.IsUserExist(UserName);
        }

        public static bool IsUserExist(int UserID)
        {
            return clsUserData.IsUserExist(UserID);
        }

        public static int GetTotalUsers()
        {
            return clsUserData.GetTotalUsers();
        }

        public static clsUser Find(int UserID)
        {
            string UserName = "", Password = "";
            int PersonID = -1;
            bool IsActive = false;

            if (clsUserData.GetUserInfoByUserID(UserID, ref PersonID, ref UserName, ref Password, ref IsActive))
            {
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            }
            else
            {
                return null;
            }
        }

        public static clsUser FindByPersonID(int PersonID)
        {
            string UserName = "", Password = "";
            int UserID = -1;
            bool IsActive = false;

            if (clsUserData.GetUserInfoByPersonID(PersonID, ref UserID, ref UserName, ref Password, ref IsActive))
            {
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            }
            else
            {
                return null;
            }
        }

        public static clsUser Find(string UserName)
        {
            string Password = "";
            int PersonID = 0, UserID = 0;
            bool IsActive = false;

            if (clsUserData.GetUserInfoByUserName(UserName, ref PersonID, ref UserID, ref Password, ref IsActive))
            {
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            }
            else
            {
                return null;
            }
        }

        public static bool IsUserExistForPersonID(int PersonID)
        {
            return clsUserData.IsUserExistForPersonID(PersonID);
        }

        public static bool GetUserByUserNameAndPassword(string UserName, string Password, ref int PersonID, ref int UserID, ref bool IsActive)
        {
            return clsUserData.GetUserByUserNameAndPassword(UserName, Password, ref PersonID, ref UserID, ref IsActive);
        }

        public static bool ChangePassword(int UserID, string NewPassword)
        {
            return clsUserData.ChangePassword(UserID, NewPassword);
        }

        public bool Save()
        {

            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateUser();

            }
            return false;
        }
    }
}
