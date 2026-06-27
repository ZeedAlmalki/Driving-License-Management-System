using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessLayer
{
    public class clsManageTestType
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        
        public enum enTestType { None = -1, VisionTest = 1, WrittenTest = 2, StreetTest = 3};
        public clsManageTestType.enTestType TestTypeID { get; set; }
        public string TestTypeTitle { get; set; }
        public decimal TestTypeFees { get; set; }
        public string TestTypeDescription { get; set; }



        public clsManageTestType()
        {
            TestTypeTitle = string.Empty;
            TestTypeDescription = string.Empty;
            TestTypeID = clsManageTestType.enTestType.VisionTest;
            TestTypeFees = 0;
            Mode = enMode.AddNew;
        }

        private clsManageTestType(clsManageTestType.enTestType TestTypeID, string TestTypeTitle, string TestTypeDescription, decimal TestTypeFees)
        {
            this.TestTypeID = TestTypeID;
            this.TestTypeTitle = TestTypeTitle;
            this.TestTypeDescription = TestTypeDescription;
            this.TestTypeFees = TestTypeFees;
            Mode = enMode.Update;
        }

        public static DataTable GetAllTestTypes()
        {
            return clsManageTestTypeData.GetAllTestTypes();
        }


        private bool _UpdateTestType()
        {
            return clsManageTestTypeData.UpdateTestType((int)this.TestTypeID, this.TestTypeTitle, this.TestTypeDescription, TestTypeFees);
        }

        private bool _AddNewTestType()
        {
            this.TestTypeID = (enTestType)clsManageTestTypeData.AddNewTestType(this.TestTypeTitle, this.TestTypeDescription, this.TestTypeFees);

            return (this.TestTypeID != enTestType.None);
        }

        public static clsManageTestType FindTestTypeByID(clsManageTestType.enTestType TestTypeID)
        {

            string TestTypeTitle = string.Empty;
            string TestTypeDescription = string.Empty;
            decimal TestTypeFees = 0;

            if (clsManageTestTypeData.FindTestTypeByID((int)TestTypeID, ref TestTypeTitle, ref TestTypeDescription, ref TestTypeFees))
            {
                return new clsManageTestType(TestTypeID, TestTypeTitle, TestTypeDescription, TestTypeFees);
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
                    if (_AddNewTestType())
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
                        return _UpdateTestType();
                    }
            }
            return false;
        }

    }
}
