using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class InternationalLicensesData
    {

        public class clsInternationalLicensesData
        {
            public static DataTable GetAllInternationalLicenses()
            {
                DataTable dt = new DataTable();
                SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

                string query = @"SELECT * FROM InternationalLicenses";

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

                }
                finally
                {
                    Connection.Close();
                }
                return dt;
            }

            public static DataTable GetAllInternationalLicensesByPersonID(int PersonID)
            {
                DataTable dt = new DataTable();
                SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

                string query = @"SELECT InternationalLicenses.* FROM InternationalLicenses
                            INNER JOIN Drivers ON InternationalLicenses.DriverID = Drivers.DriverID
                            WHERE Drivers.PersonID = @PersonID;";

                SqlCommand Command = new SqlCommand(query, Connection);
                Command.Parameters.AddWithValue("@PersonID", PersonID);

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

                }
                finally
                {
                    Connection.Close();
                }
                return dt;
            }


            public static bool FindInternationalLicenseByID(int InternationalLicenseID, ref int ApplicationID,
                    ref int DriverID, ref int IssuedUsingLocalLicenseID, ref DateTime IssueDate,
                    ref DateTime ExpirationDate, ref bool IsActive, ref int CreatedByUserID)
            {
                bool IsFound = false;

                SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

                string query = @"SELECT * FROM InternationalLicenses 
                             WHERE InternationalLicenseID = @InternationalLicenseID";

                SqlCommand Command = new SqlCommand(query, Connection);
                Command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

                try
                {
                    Connection.Open();
                    SqlDataReader reader = Command.ExecuteReader();

                    if (reader.Read())
                    {
                        ApplicationID = (int)reader["ApplicationID"];
                        DriverID = (int)reader["DriverID"];
                        IssuedUsingLocalLicenseID = (int)reader["IssuedUsingLocalLicenseID"];
                        IssueDate = (DateTime)reader["IssueDate"];
                        ExpirationDate = (DateTime)reader["ExpirationDate"];
                        IsActive = (bool)reader["IsActive"];
                        CreatedByUserID = (int)reader["CreatedByUserID"];

                        IsFound = true;
                    }
                    else
                    {
                        IsFound = false;
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    Connection.Close();
                }
                return IsFound;
            }

            public static bool ItHasInternationalDrivingLicense(int IssuedUsingLocalDrivingLicenseID)
            {
                SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
                bool ItHas = false;

                string query = @"
							SELECT COUNT(*) FROM InternationalLicenses
                            WHERE IssuedUsingLocalLicenseID = @IssuedUsingLocalDrivingLicenseID AND IsActive = 1";

                SqlCommand Command = new SqlCommand(query, Connection);

                Command.Parameters.AddWithValue("@IssuedUsingLocalDrivingLicenseID", IssuedUsingLocalDrivingLicenseID);

                try
                {
                    Connection.Open();
                    object result = Command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int LicenseID))
                    {
                        if (LicenseID == 1)
                            ItHas = true;
                    }
                }
                catch (SqlException ex)
                {

                }
                finally
                {
                    Connection.Close();
                }
                return ItHas;
            }

            public static bool FindInternationalLicenseByLocalLicenseID(int IssuedUsingLocalLicenseID, ref int InternationalLicenseID, ref int ApplicationID,
                 ref int DriverID, ref DateTime IssueDate,
                 ref DateTime ExpirationDate, ref bool IsActive, ref int CreatedByUserID)
            {
                bool IsFound = false;

                SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

                string query = @"SELECT * FROM InternationalLicenses 
                             WHERE IssuedUsingLocalLicenseID = @IssuedUsingLocalLicenseID AND IsActive = 1";

                SqlCommand Command = new SqlCommand(query, Connection);
                Command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);

                try
                {
                    Connection.Open();
                    SqlDataReader reader = Command.ExecuteReader();

                    if (reader.Read())
                    {
                        ApplicationID = (int)reader["ApplicationID"];
                        DriverID = (int)reader["DriverID"];
                        InternationalLicenseID = (int)reader["InternationalLicenseID"];
                        IssueDate = (DateTime)reader["IssueDate"];
                        ExpirationDate = (DateTime)reader["ExpirationDate"];
                        IsActive = (bool)reader["IsActive"];
                        CreatedByUserID = (int)reader["CreatedByUserID"];

                        IsFound = true;
                    }
                    else
                    {
                        IsFound = false;
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    Connection.Close();
                }
                return IsFound;
            }

            public static bool FindInternationalLicenseByApplicationID(int ApplicationID, ref int InternationalLicenseID,
                    ref int DriverID, ref int IssuedUsingLocalLicenseID, ref DateTime IssueDate,
                    ref DateTime ExpirationDate, ref bool IsActive, ref int CreatedByUserID)
            {
                bool IsFound = false;

                SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

                string query = @"SELECT * FROM InternationalLicenses 
                             WHERE ApplicationID = @ApplicationID";

                SqlCommand Command = new SqlCommand(query, Connection);
                Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

                try
                {
                    Connection.Open();
                    SqlDataReader reader = Command.ExecuteReader();

                    if (reader.Read())
                    {
                        InternationalLicenseID = (int)reader["InternationalLicenseID"];
                        DriverID = (int)reader["DriverID"];
                        IssuedUsingLocalLicenseID = (int)reader["IssuedUsingLocalLicenseID"];
                        IssueDate = (DateTime)reader["IssueDate"];
                        ExpirationDate = (DateTime)reader["ExpirationDate"];
                        IsActive = (bool)reader["IsActive"];
                        CreatedByUserID = (int)reader["CreatedByUserID"];

                        IsFound = true;
                    }
                    else
                    {
                        IsFound = false;
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    Connection.Close();
                }
                return IsFound;
            }

            public static int AddNewInternationalLicense(int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID,
                    DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
            {
                int InternationalLicenseID = -1;

                SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

                string query = @"
                                UPDATE InternationalLicenses
                                SET IsActive = 0
                                WHERE DriverID = @DriverID;

                            INSERT INTO InternationalLicenses 
                            (ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, 
                             ExpirationDate, IsActive, CreatedByUserID)
                             VALUES 
                            (@ApplicationID, @DriverID, @IssuedUsingLocalLicenseID, @IssueDate, 
                             @ExpirationDate, @IsActive, @CreatedByUserID);
                             SELECT SCOPE_IDENTITY();";

                SqlCommand Command = new SqlCommand(query, Connection);

                Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                Command.Parameters.AddWithValue("@DriverID", DriverID);
                Command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
                Command.Parameters.AddWithValue("@IssueDate", IssueDate);
                Command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
                Command.Parameters.AddWithValue("@IsActive", IsActive);
                Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

                try
                {
                    Connection.Open();
                    object result = Command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int ID))
                    {
                        InternationalLicenseID = ID;
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    Connection.Close();
                }

                return InternationalLicenseID;
            }

            public static bool UpdateInternationalLicense(int InternationalLicenseID, int ApplicationID, int DriverID,
                    int IssuedUsingLocalLicenseID, DateTime IssueDate, DateTime ExpirationDate,
                    bool IsActive, int CreatedByUserID)
            {
                int RowsAffected = 0;

                SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

                string query = @"UPDATE InternationalLicenses 
                             SET ApplicationID = @ApplicationID,
                                 DriverID = @DriverID,
                                 IssuedUsingLocalLicenseID = @IssuedUsingLocalLicenseID,
                                 IssueDate = @IssueDate,
                                 ExpirationDate = @ExpirationDate,
                                 IsActive = @IsActive,
                                 CreatedByUserID = @CreatedByUserID
                             WHERE InternationalLicenseID = @InternationalLicenseID";

                SqlCommand Command = new SqlCommand(query, Connection);

                Command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);
                Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                Command.Parameters.AddWithValue("@DriverID", DriverID);
                Command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
                Command.Parameters.AddWithValue("@IssueDate", IssueDate);
                Command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
                Command.Parameters.AddWithValue("@IsActive", IsActive);
                Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

                try
                {
                    Connection.Open();
                    RowsAffected = Command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    Connection.Close();
                }

                return (RowsAffected > 0);
            }
        }
    }
}
