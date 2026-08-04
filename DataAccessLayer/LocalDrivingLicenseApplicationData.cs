using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using static System.Net.Mime.MediaTypeNames;

namespace DataAccessLayer
{
    public class clsLocalDrivingLicenseApplicationData
    {
        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            DataTable dt = new DataTable();
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM LocalDrivingLicenseApplications";

            SqlCommand Command = new SqlCommand(query, Connection);

            try
            {
                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                SharedDVLD.RegisterLogInEventHandler(ex);

            }
            finally
            {
                Connection.Close();
            }
            return dt;
        }


        public static DataTable GetLocalDrivingLicenseApplicationsView()
        {
            DataTable dt = new DataTable();
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM LocalDrivingLicenseApplications_View";

            SqlCommand Command = new SqlCommand(query, Connection);

            try
            {
                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                SharedDVLD.RegisterLogInEventHandler(ex);

            }
            finally
            {
                Connection.Close();
            }
            return dt;
        }

        public static int GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            int PassedTestCount = 0;
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT COUNT(TestAppointments.TestTypeID) AS PassedTestCount
                             FROM Tests
                             INNER JOIN TestAppointments ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID
                             WHERE Tests.TestResult = 1 AND TestAppointments.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

            SqlCommand Command = new SqlCommand(query, Connection);
            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            try
            {
                Connection.Open();
                object result = Command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int _PassedTestCount))
                {
                    PassedTestCount = _PassedTestCount;
                }
            }
            catch (Exception ex)
            {
                SharedDVLD.RegisterLogInEventHandler(ex);

            }
            finally
            {
                Connection.Close();
            }

            return PassedTestCount;
        }


        public static bool IsPersonHasActiveLicenseClass(int PersonID, int LicenseClassID, ref int ApplicationID)
        {
            bool IsFound = false;
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT dbo.LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID, dbo.LicenseClasses.LicenseClassID, dbo.People.NationalNo, dbo.Applications.ApplicationStatus, dbo.Applications.ApplicationID
                             FROM  dbo.LocalDrivingLicenseApplications INNER JOIN
                         dbo.Applications ON dbo.LocalDrivingLicenseApplications.ApplicationID = dbo.Applications.ApplicationID INNER JOIN
                         dbo.LicenseClasses ON dbo.LocalDrivingLicenseApplications.LicenseClassID = dbo.LicenseClasses.LicenseClassID INNER JOIN
                         dbo.People ON dbo.Applications.ApplicantPersonID = dbo.People.PersonID 
						 WHERE dbo.LicenseClasses.LicenseClassID = @LicenseClassID AND People.PersonID = @PersonID AND (dbo.Applications.ApplicationStatus != 2 AND dbo.Applications.ApplicationStatus != 3)";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            Command.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();
                if (reader.Read())
                {
                    ApplicationID = (int)reader["ApplicationID"];
                    IsFound = true;
                }
                else
                {
                    IsFound = false;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                SharedDVLD.RegisterLogInEventHandler(ex);

            }
            finally
            {
                Connection.Close();
            }
            return IsFound;
        }

        public static bool FindLocalDrivingLicenseApplicationByID(int LocalDrivingLicenseApplicationID, ref int ApplicationID, ref int LicenseClassID)
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT ApplicationID, LicenseClassID
                                    FROM LocalDrivingLicenseApplications 
                                    WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";
            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            try
            {
                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read())
                {
                    ApplicationID = (int)reader["ApplicationID"];
                    LicenseClassID = (int)reader["LicenseClassID"];
                    IsFound = true;
                }
                else
                {
                    IsFound = false;
                }
            }
            catch (Exception ex)
            {
                SharedDVLD.RegisterLogInEventHandler(ex);

            }
            finally
            {
                Connection.Close();
            }

            return IsFound;
        }

        public static bool FindLocalDrivingLicenseByApplicationID(int ApplicationID, ref int LocalDrivingLicenseApplicationID, ref int LicenseClassID)
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT *
                                    FROM LocalDrivingLicenseApplications 
                                    WHERE ApplicationID = @ApplicationID";
            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read())
                {
                    LocalDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                    LicenseClassID = (int)reader["LicenseClassID"];
                    IsFound = true;
                }
                else
                {
                    IsFound = false;
                }
            }
            catch (Exception ex)
            {
                SharedDVLD.RegisterLogInEventHandler(ex);

            }
            finally
            {
                Connection.Close();
            }

            return IsFound;
        }

        public static int AddNewdLocalDrivingLicenseApplication(int ApplicationID, int LicenseClassID)
        {
            int LocalDrivingLicenseApplicationID = -1;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO LocalDrivingLicenseApplications (ApplicationID, LicenseClassID)
                             VALUES (@ApplicationID, @LicenseClassID)
                             SELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            Command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                Connection.Open();
                object result = Command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int ID))
                {
                    LocalDrivingLicenseApplicationID = ID;
                }
            }
            catch (Exception ex)
            {
                SharedDVLD.RegisterLogInEventHandler(ex);

            }
            finally
            {
                Connection.Close();
            }

            return LocalDrivingLicenseApplicationID;

        }

        public static bool UpdateLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID, int ApplicationID, int LicenseClassID)
        {
            int RowsAffected = 0;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update LocalDrivingLicenseApplications 
                             SET ApplicationID = @ApplicationID,
                                 LicenseClassID = @LicenseClassID
                                 WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";
            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            Command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                Connection.Open();

                RowsAffected = Command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                SharedDVLD.RegisterLogInEventHandler(ex);

            }
            finally
            {
                Connection.Close();
            }

            return (RowsAffected > 0);
        }

        public static bool DeleteLocalDrivingLicenseApplicationByID(int LocalDrivingLicenseApplicationID)
        {
            int RowsAffected = 0;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"DELETE LocalDrivingLicenseApplications 
                                 WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";
            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            try
            {
                Connection.Open();

                RowsAffected = Command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                SharedDVLD.RegisterLogInEventHandler(ex);

            }
            finally
            {
                Connection.Close();
            }

            return (RowsAffected > 0);
        }

        public static bool IsAnActiveAppointmentExist(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT TOP 1 IsLocked FROM TestAppointments WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID  AND TestTypeID = @TestTypeID AND IsLocked = 0
            ORDER BY TestAppointmentID DESC";

            SqlCommand Command = new SqlCommand(query, Connection);
            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                Connection.Open();
                object result = Command.ExecuteScalar();

                if (result != null)
                    IsFound = true;
            }


            catch (Exception ex)
            {
                SharedDVLD.RegisterLogInEventHandler(ex);

            }
            finally
            {
                Connection.Close();
            }
            return IsFound;
        }

        public static bool ItHasAppointmentTestBefore(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT TOP 1 1 FROM TestAppointments WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID  AND TestTypeID = @TestTypeID 
            ORDER BY TestAppointmentID DESC";

            SqlCommand Command = new SqlCommand(query, Connection);
            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                Connection.Open();
                object result = Command.ExecuteScalar();

                if (result != null)
                    IsFound = true;
            }


            catch (Exception ex)
            {
                SharedDVLD.RegisterLogInEventHandler(ex);

            }
            finally
            {
                Connection.Close();
            }
            return IsFound;
        }

        public static bool IsPassedAppointmentTestBefore(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            int IsFound = 0;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT COUNT(*) FROM TestAppointments
                            INNER JOIN Tests ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID
                            WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID AND TestTypeID = @TestTypeID AND TestResult = 1";

            SqlCommand Command = new SqlCommand(query, Connection);
            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                Connection.Open();
                object result = Command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int _IsFound))
                {
                    IsFound = _IsFound;
                }
            }


            catch (Exception ex)
            {
                SharedDVLD.RegisterLogInEventHandler(ex);

            }
            finally
            {
                Connection.Close();
            }
            return IsFound > 0;
        }

        public static int GetTotalSameExamTrialsForLicense(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            int TotalTrials = 0;
            string query = @"SELECT COUNT(*) FROM TestAppointments 
                            INNER JOIN TESTS ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                            WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID AND TestTypeID = @TestTypeID AND IsLocked = 1";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                Connection.Open();
                object result = Command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int _TotalTrails))
                {
                    TotalTrials = _TotalTrails;
                }
            }
            catch (SqlException ex)
            {

            }
            finally
            {
                Connection.Close();
            }
            return TotalTrials;
        }

        public static bool ItHasLocalDrivingLicenseClassBefore(int LicenseClassID, int PersonID, ref int outLicenseID)
        {
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            bool ItHas = false;
            outLicenseID = -1;

            string query = @"SELECT LicenseID FROM Licenses
                            INNER JOIN Drivers ON Drivers.DriverID = Licenses.DriverID
                            WHERE LicenseClass = @LicenseClassID AND Drivers.PersonID = @PersonID AND IsActive = 1";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            Command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                Connection.Open();
                object result = Command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int LicenseID))
                {
                    outLicenseID = LicenseID;
                }
            }
            catch (SqlException ex)
            {

            }
            finally
            {
                Connection.Close();
            }
            return (outLicenseID != -1);
        }
    }
}
