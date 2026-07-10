

using System.Data;
using System.Runtime.CompilerServices;
using DataAccessLayer;

namespace BusinessLayer
{
    public class clsManageApplicationTypes
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int ApplicationTypeID { get; set; }

        public enum enManageApplicationTypes {NewLocalDrivingLicenseService = 1, RenewDrivingLicenseService = 2, ReplacementForaLostdDrivingLicense = 3, ReplacementForaDamageddDrivingLicense = 4, RelaseDetainedDrivingLicense = 5, NewInternationalLicense = 6, RetakeTest = 7};
        public  clsManageApplicationTypes.enManageApplicationTypes enApplicationTypeID { get; set; }
        public string ApplicationTypeTitle { get; set; }
        public decimal ApplicationFees { get; set; }

        public clsManageApplicationTypes()
        {
            ApplicationTypeID = -1;
            ApplicationTypeTitle = string.Empty;
            ApplicationFees = 0;
            Mode = enMode.AddNew;
        }

        private clsManageApplicationTypes(int ApplicationTypeID, string ApplicationTypeTitle, decimal ApplicationFees)
        {
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypeTitle = ApplicationTypeTitle;
            this.ApplicationFees = ApplicationFees;
            Mode = enMode.Update;
        }

        public static DataTable GetAllApplicationTypes()
        {
            return clsManageApplicationTypesData.GetAllApplicationTypes();
        }


        private bool _UpdateApplicationType()
        {
            return clsManageApplicationTypesData.UpdateApplicationType(this.ApplicationTypeID, this.ApplicationTypeTitle, this.ApplicationFees);
        }

        private bool _AddNewApplicationType()
        {
            this.ApplicationTypeID = clsManageApplicationTypesData.AddNewApplicationType(this.ApplicationTypeTitle, this.ApplicationFees);

            return (this.ApplicationTypeID != -1);
        }

        public static clsManageApplicationTypes FindApplicationType(int ApplicationTypeID)
        {

            string ApplicationTypeTitle = "";
            decimal ApplicationFees = 0;

            if (clsManageApplicationTypesData.FindApplicationTypeByID(ApplicationTypeID, ref ApplicationTypeTitle, ref ApplicationFees))
            {
                return new clsManageApplicationTypes(ApplicationTypeID, ApplicationTypeTitle, ApplicationFees);
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
                    if (_AddNewApplicationType())
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
                        return _UpdateApplicationType();
                    }
            }
            return false;
        }

    }
}
