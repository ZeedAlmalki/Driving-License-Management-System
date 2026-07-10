using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessLayer
{
    public class clsApplication
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int ApplicationID {  get; set; } 
        public int ApplicantPersonID { get; set; }
        public DateTime ApplicationDate { get; set; }
        public int ApplicationTypeID { get; set; } 
        public clsManageApplicationTypes ApplicationType { get; set; }
        public enum enApplicationSatus { New = 1, Cancelled = 2, Completed = 3 };
        public clsApplication.enApplicationSatus ApplicationStatus { get; set; }

        public DateTime LastStatusDate { get; set; }
        public decimal PaidFees { get; set; }
        public int CreatedByUserID { get; set; }


        public clsApplication()
        {
            ApplicationID = -1; 
            ApplicantPersonID = -1;
            ApplicationDate = DateTime.MinValue;
            ApplicationTypeID = -1;
            ApplicationStatus = enApplicationSatus.New;
            LastStatusDate = DateTime.MinValue;
            PaidFees = 0;
            CreatedByUserID = -1;
            Mode = enMode.AddNew;
        }



        private clsApplication(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate,
            int ApplicationTypeID, enApplicationSatus ApplicationStatus, DateTime LastStatusDate,
            decimal PaidFees, int CreatedByUserID)
        {
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            ApplicationType = clsManageApplicationTypes.FindApplicationType(ApplicationTypeID);
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            Mode = enMode.Update;
        }

        public static DataTable GetAllApplication()
        {
            return clsApplicationsData.GetAllApplications();
        }

        public static bool DeleteApplicatoinByID(int ApplicationID)
        {
            return clsApplicationsData.DeleteApplicationByID(ApplicationID);
        }

        private bool _UpdateApplication()
        {
            return clsApplicationsData.UpdateApplication(this.ApplicationID, this.ApplicantPersonID, this.ApplicationDate,
                this.ApplicationTypeID, (byte)this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);
        }

        private bool _AddNewApplication()
        {
            this.ApplicationID = clsApplicationsData.AddNewApplication(this.ApplicantPersonID, this.ApplicationDate,
                this.ApplicationTypeID, (byte)this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);

            return (this.ApplicationID != -1);
        }

        public static clsApplication FindApplicationByID(int ApplicationID)
        {

            int ApplicantPersonID = -1;
            DateTime ApplicationDate = DateTime.MinValue;
            int ApplicationTypeID = -1;
            byte ApplicationStatus = 1;
            DateTime LastStatusDate = DateTime.MinValue;
            decimal PaidFees = 0;
            int CreatedByUserID = -1;

            if (clsApplicationsData.FindApplicationByID(ApplicationID, ref ApplicantPersonID, ref ApplicationDate, ref ApplicationTypeID,
                ref ApplicationStatus, ref LastStatusDate, ref PaidFees, ref CreatedByUserID))
            {
                return new clsApplication(ApplicationID, ApplicantPersonID, ApplicationDate, ApplicationTypeID,
                 (enApplicationSatus)ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID);
            }
            else
            {
                return null;
            }
        }

        //public static clsApplication FindApplicationByPersonID(int ApplicantPersonID)
        //{

        //    int ApplicationID = -1;
        //    DateTime ApplicationDate = DateTime.MinValue;
        //    int ApplicationTypeID = -1;
        //    byte ApplicationStatus = 1;
        //    DateTime LastStatusDate = DateTime.MinValue;
        //    decimal PaidFees = 0;
        //    int CreatedByUserID = -1;

        //    if (clsApplicationsData.FindApplicationByPersonID(ApplicantPersonID, ref ApplicationID, ref ApplicationDate, ref ApplicationTypeID,
        //        ref ApplicationStatus, ref LastStatusDate, ref PaidFees, ref CreatedByUserID))
        //    {
        //        return new clsApplication(ApplicationID, ApplicantPersonID, ApplicationDate, ApplicationTypeID,
        //         (enApplicationSatus)ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID);
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplication())
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
                        return _UpdateApplication();
                    }
            }
            return false;
        }

    }
}
