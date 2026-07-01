using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessLayer
{
    public class clsLocalDrivingLicenseApplication : clsApplication
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int LocalDrivingLicenseApplicationsID { get; set; }
        public int LicenseClassID { get; set; }

        public clsLocalDrivingLicenseApplication()
        {
            LocalDrivingLicenseApplicationsID = -1;
            LicenseClassID = 0;
            Mode = enMode.AddNew;
        }

        private clsLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationsID, int ApplicationID, int LicenseClassID) 
        {
            this.LocalDrivingLicenseApplicationsID = LocalDrivingLicenseApplicationsID;
            this.ApplicationID = ApplicationID;
            this.LicenseClassID = LicenseClassID;
            clsApplication BaseApplication = clsApplication.FindApplicationByID(ApplicationID);
            this.ApplicationStatus = BaseApplication.ApplicationStatus;
            this.ApplicationDate = BaseApplication.ApplicationDate;
            this.LastStatusDate = BaseApplication.LastStatusDate;
            this.ApplicantPersonID = BaseApplication.ApplicantPersonID;
            this.ApplicationTypeID = BaseApplication.ApplicationTypeID;
            this.CreatedByUserID = BaseApplication.CreatedByUserID;
            this.PaidFees = BaseApplication.PaidFees;
            this.Mode = enMode.Update;
        }

        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            return clsLocalDrivingLicenseApplicationData.GetAllLocalDrivingLicenseApplications();
        }

        public static DataTable GetLocalDrivingLicenseApplicationsView()
        {
            return clsLocalDrivingLicenseApplicationData.GetLocalDrivingLicenseApplicationsView();
        }

        public static bool IsPersonHasActiveLicenseClass(int PersonID, int LicenseClassID, ref int ApplicationID)
        {
            return clsLocalDrivingLicenseApplicationData.IsPersonHasActiveLicenseClass(PersonID, LicenseClassID, ref ApplicationID);
        }
        private bool _UpdateLocalDrivingLicenseApplication()
        {
            if (!base.Save())
            {
                return false;
            }

            return clsLocalDrivingLicenseApplicationData.UpdateLocalDrivingLicenseApplication(this.LocalDrivingLicenseApplicationsID, this.ApplicationID, this.LicenseClassID);
        }

        public static bool DeleteLocalDrivingLicenseApplicationByID(int LocalDriverLicenseApplicationID)
        {
            int ApplicationID = clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseApplicationByID(LocalDriverLicenseApplicationID).ApplicationID;

            if (!clsLocalDrivingLicenseApplicationData.DeleteLocalDrivingLicenseApplicationByID(LocalDriverLicenseApplicationID))
            {
                return false;
            }

            return clsApplication.DeleteApplicatoinByID(ApplicationID);

        }

        private bool _AddNewdLocalDrivingLicenseApplication()
        {

            if (!base.Save())
            {
                return false; 
            }

            this.LocalDrivingLicenseApplicationsID = clsLocalDrivingLicenseApplicationData.AddNewdLocalDrivingLicenseApplication(this.ApplicationID, this.LicenseClassID);

            return (this.LocalDrivingLicenseApplicationsID != -1);
        }

        public static clsLocalDrivingLicenseApplication FindLocalDrivingLicenseApplicationByID(int LocalDrivingLicenseApplicationsID)
        {

            int _ApplicationID = -1;
            int _LicenseClassID = -1;

            if (clsLocalDrivingLicenseApplicationData.FindLocalDrivingLicenseApplicationByID(LocalDrivingLicenseApplicationsID, ref _ApplicationID, ref _LicenseClassID))
            {
                return new clsLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationsID, _ApplicationID, _LicenseClassID);
            }
            else
            {
                return null;
            }

        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewdLocalDrivingLicenseApplication())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    {
                        return _UpdateLocalDrivingLicenseApplication();
                    }
            }
            return false;
        }

    }
}
