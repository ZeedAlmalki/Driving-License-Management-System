using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessLayer
{



    public class clsDetainedLicenses
    {
        public enum enMode { Detain = 1, Release = 2, ReadOnly = 3};
        private enMode _Mode = enMode.Detain;

        public int DetainID { get; set; }
        public int LicenseID { get; set; }
        public DateTime DetainDate { get; set; }
        public decimal FineFees { get; set; }
        public int CreatedByUserID { get; set; }


        // ... ^ Detained 

        public DateTime? ReleaseDate { get; set; }
        public int ReleasedByUserID { get; set; }
        public int ReleaseApplicationID { get; set; }

        // ... ^ Release.
        public bool IsReleased { get; set; } // Automatically Seed


        public clsDetainedLicenses()
        {
            DetainID = -1;
            LicenseID = -1;
            DetainDate = DateTime.Now;
            FineFees = 0;
            CreatedByUserID = -1;


            ReleaseDate = null;
            ReleasedByUserID = -1;
            ReleaseApplicationID = -1;
            IsReleased = false;
            _Mode = enMode.Detain;
        }
        public clsLicense DetainedLicenseInfo { get; set; }
        public clsApplication ReleaseApplicationInfo { get; set; }
        private clsDetainedLicenses(int DetainID, int LicenseID, DateTime DetainDate, decimal FineFees, int CreatedByUserID, bool IsReleased, DateTime? ReleaseDate, int ReleasedByUserID, int ReleaseApplicationID)
        {
            this.DetainID = DetainID;
            this.LicenseID = LicenseID;
            DetainedLicenseInfo = clsLicense.FindLicenseByID(LicenseID);
            this.DetainDate = DetainDate;
            this.FineFees = FineFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsReleased = IsReleased;
            this.ReleaseDate = ReleaseDate;
            this.ReleasedByUserID = ReleasedByUserID;

            this.ReleaseApplicationID = ReleaseApplicationID;
            if (ReleaseApplicationID != -1)
            {
                ReleaseApplicationInfo = clsApplication.FindApplicationByID(ReleaseApplicationID);
            }


            if (IsReleased)
            {
                _Mode = enMode.ReadOnly;
            }
            else
            {
                _Mode = enMode.Release;
            }
        }

        public static DataTable GetAllDetainedLicenses()
        {
            return clsDetainedLicensesData.GetAllDetainedLicenses();
        }

        public static bool IsLicenseDetained(int LicenseID)
        {
            return clsDetainedLicensesData.IsLicenseDetained(LicenseID);
        }

        public static clsDetainedLicenses FindDetainedLicenseByID(int DetainID)
        {
            int LicenseID = -1;
            DateTime DetainDate = DateTime.Now;
            decimal FineFees = 0;
            int CreatedByUserID = -1;
            bool IsReleased = false;
            DateTime? ReleaseDate = null;
            int ReleasedByUserID = -1;
            int ReleaseApplicationID = -1;

            if (clsDetainedLicensesData.FindDetainedLicenseByID(DetainID, ref LicenseID, ref DetainDate, ref FineFees, ref CreatedByUserID, ref IsReleased, ref ReleaseDate, ref ReleasedByUserID, ref ReleaseApplicationID))
            {
                return new clsDetainedLicenses(DetainID, LicenseID, DetainDate, FineFees, CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID);
            }
            else
            {
                return null;
            }
        }

        public static clsDetainedLicenses FindDetainedLicenseByLicenseID(int LicenseID)
        {
            int DetainID = -1;
            DateTime DetainDate = DateTime.Now;
            decimal FineFees = 0;
            int CreatedByUserID = -1;
            bool IsReleased = false;
            DateTime? ReleaseDate = null;
            int ReleasedByUserID = -1;
            int ReleaseApplicationID = -1;

            if (clsDetainedLicensesData.FindDetainedLicenseByLicenseID(LicenseID, ref DetainID, ref DetainDate, ref FineFees, ref CreatedByUserID, ref IsReleased, ref ReleaseDate, ref ReleasedByUserID, ref ReleaseApplicationID))
            {
                return new clsDetainedLicenses(DetainID, LicenseID, DetainDate, FineFees, CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID);
            }
            else
            {
                return null;
            }
        }

        private bool AddNewDetainLicense()
        {
            this.DetainID = clsDetainedLicensesData.AddNewDetainLicense(this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID);

            return (this.DetainID != -1);
        }

        private bool ReleaseLicense()
        {
            return clsDetainedLicensesData.ReleaseLicense(this.DetainID, this.ReleaseDate, this.ReleasedByUserID, this.ReleaseApplicationID);
        }

        //private bool UpdateDetainLicense()
        //{
        //    return clsDetainedLicensesData.UpdateDetainLicense(this.DetainID, this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID);
        //}

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.Detain:
                    if (AddNewDetainLicense())
                    {
                        _Mode = enMode.Release;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Release:
                    return ReleaseLicense();
            }

            return false;
        }

    }
}