using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataAccessLayer.InternationalLicensesData;

namespace BusinessLayer
{
    public class clsInternationalLicense : clsApplication
    {

        public enum enMode { Add = 1, Update = 2 }
        private enMode _Mode = enMode.Add;

        public int InternationalLicenseID { get; set; }
        public int DriverID { get; set; }
        public int IssuedUsingLocalLicenseID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }
        public clsPerson PersonInfo { get; }
        public clsLocalDrivingLicenseApplication LocalDrivingLicense { get; }

        public clsUser User { get;}

        public clsInternationalLicense()
        {
            InternationalLicenseID = -1;
            ApplicationID = -1;
            DriverID = -1;
            IssuedUsingLocalLicenseID = -1;
            IssueDate = DateTime.MinValue;
            ExpirationDate = DateTime.MinValue;
            IsActive = false;
            CreatedByUserID = -1;

            _Mode = enMode.Add;
        }

        public clsInternationalLicense(int localLicenseID, int createdByUserID)
        {
            clsLicense localLicense = clsLicense.FindLicenseByID(localLicenseID);
            if (localLicense == null) return;
            this.IssuedUsingLocalLicenseID = localLicenseID;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now.AddYears(1);
            this.DriverID = localLicense.DriverID;
            this.IsActive = true;
            this.CreatedByUserID = createdByUserID;

            base.ApplicantPersonID = localLicense.PersonID;
            base.ApplicationDate = DateTime.Now;
            base.LastStatusDate = DateTime.Now;
            base.ApplicationStatus = clsApplication.enApplicationSatus.Completed;
            base.CreatedByUserID = createdByUserID;
            base.ApplicantPersonID = localLicense.PersonID;
            base.ApplicationTypeID = (int)clsManageApplicationTypes.enManageApplicationTypes.NewInternationalLicense;

            clsManageApplicationTypes testType = clsManageApplicationTypes.FindApplicationType(this.ApplicationTypeID);
            this.PaidFees = (testType != null) ? testType.ApplicationFees : 0;

            _Mode = enMode.Update;
        }

        private clsInternationalLicense(int InternationalLicenseID, int ApplicationID, int DriverID,
            int IssuedUsingLocalLicenseID, DateTime IssueDate, DateTime ExpirationDate,
            bool IsActive, int CreatedByUserID)
        {
            this.InternationalLicenseID = InternationalLicenseID;
            this.ApplicationID = ApplicationID;
            clsApplication BaseApplication = clsApplication.FindApplicationByID(ApplicationID);
            PersonInfo = clsPerson.FindByApplicationID(ApplicationID);
            this.DriverID = DriverID;
            this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
            LocalDrivingLicense = clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseApplicationByID(IssuedUsingLocalLicenseID);
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;
            this.CreatedByUserID = CreatedByUserID;
            User = clsUser.Find(CreatedByUserID);

            // We here use 4 object created it by ID. its not very good to do if we don't need to it
            // Because if we Search for 4 object every time, so if we create 100 Object in IL
            // we will search 400 times. and its bad.
            _Mode = enMode.Update;
        }


        public static DataTable GetAllInternationalLicenses()
        {
            return clsInternationalLicensesData.GetAllInternationalLicenses();
        }

        public static DataTable GetAllInternationalLicensesByPersonID(int PersonID)
        {
            return clsInternationalLicensesData.GetAllInternationalLicensesByPersonID(PersonID);
        }

        public static clsInternationalLicense FindInternationalLicenseByID(int InternationalLicenseID)
        {
            int ApplicationID = -1;
            int DriverID = -1;
            int IssuedUsingLocalLicenseID = -1;
            DateTime IssueDate = DateTime.MinValue;
            DateTime ExpirationDate = DateTime.MinValue;
            bool IsActive = false;
            int CreatedByUserID = -1;

            if (clsInternationalLicensesData.FindInternationalLicenseByID(InternationalLicenseID, ref ApplicationID,
                ref DriverID, ref IssuedUsingLocalLicenseID, ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedByUserID))
            {
                return new clsInternationalLicense(InternationalLicenseID, ApplicationID, DriverID,
                    IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID);
            }
            else
            {
                return null;
            }
        }

        public static bool ItHasInternationalDrivingLicense(int LocalLicenseID)
        {
            return clsInternationalLicensesData.ItHasInternationalDrivingLicense(LocalLicenseID);
        }

        public static clsInternationalLicense FindInternationalLicenseByLocalLicenseID(int IssuedUsingLocalLicenseID)
        {
            int InternationalLicenseID = -1;
            int DriverID = -1;
            int ApplicationID = -1;
            DateTime IssueDate = DateTime.MinValue;
            DateTime ExpirationDate = DateTime.MinValue;
            bool IsActive = false;
            int CreatedByUserID = -1;

            if (clsInternationalLicensesData.FindInternationalLicenseByLocalLicenseID(IssuedUsingLocalLicenseID, ref InternationalLicenseID,
                ref ApplicationID, ref DriverID, ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedByUserID))
            {
                return new clsInternationalLicense(InternationalLicenseID, ApplicationID, DriverID,
                    IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID);
            }
            else
            {
                return null;
            }
        }

        public static clsInternationalLicense FindInternationalLicenseByApplicationID(int ApplicationID)
        {
            int InternationalLicenseID = -1;
            int DriverID = -1;
            int IssuedUsingLocalLicenseID = -1;
            DateTime IssueDate = DateTime.MinValue;
            DateTime ExpirationDate = DateTime.MinValue;
            bool IsActive = false;
            int CreatedByUserID = -1;

            if (clsInternationalLicensesData.FindInternationalLicenseByApplicationID(ApplicationID, ref InternationalLicenseID,
                ref DriverID, ref IssuedUsingLocalLicenseID, ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedByUserID))
            {
                return new clsInternationalLicense(InternationalLicenseID, ApplicationID, DriverID,
                    IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID);
            }
            else
            {
                return null;
            }
        }

        private bool _AddNewInternationalLicense()
        {
            if (!base.Save())
            {
                return false;
            }


            this.InternationalLicenseID = clsInternationalLicensesData.AddNewInternationalLicense(
                this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID,
                this.IssueDate, this.ExpirationDate, this.IsActive, this.CreatedByUserID);

            return (this.InternationalLicenseID != -1);
        }

        private bool _UpdateInternationalLicense()
        {

            if (!base.Save())
            {
                return false;
            }

            return clsInternationalLicensesData.UpdateInternationalLicense(
                this.InternationalLicenseID, this.ApplicationID, this.DriverID,
                this.IssuedUsingLocalLicenseID, this.IssueDate, this.ExpirationDate,
                this.IsActive, this.CreatedByUserID);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.Add:
                    if (_AddNewInternationalLicense())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateInternationalLicense();
            }
            return false;
        }
    }
}
