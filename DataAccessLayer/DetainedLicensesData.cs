using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DataAccessLayer
{
    public class clsDetainedLicensesData
    {
        public static DataTable GetAllDetainedLicenses()
        {
            DataTable dt = new DataTable();
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM ListDetainedLicenses_View
                            ORDER BY DetainID ASC";

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

        public static bool IsLicenseDetained(int LicenseID)
        {
            bool IsDetained = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT IsFound = 1 FROM DetainedLicenses
                            WHERE LicenseID = @LicenseID AND IsReleased = 0";


            SqlCommand Command = new SqlCommand(query, Connection);
            Command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                Connection.Open();
                object result = Command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int IsFound))
                {
                    if (IsFound == 1)
                        IsDetained = true;
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

            return IsDetained;
        }

        public static int AddNewDetainLicense(int LicenseID,
               DateTime DetainDate, decimal FineFees, int CreatedByUserID)
        {

            int DetainedID = -1;
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO DetainedLicenses (LicenseID, 
                            DetainDate, FineFees, CreatedByUserID, IsReleased)
                             VALUES 
                            (@LicenseID, 
                            @DetainDate, @FineFees, @CreatedByUserID, 0)
                             SELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@LicenseID", LicenseID);
            Command.Parameters.AddWithValue("@DetainDate", DetainDate);
            Command.Parameters.AddWithValue("@FineFees", FineFees);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                Connection.Open();
                object result = Command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int ID))
                {
                    DetainedID = ID;
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

            return DetainedID;
        }

        public static bool UpdateDetainLicense(int DetianID, int LicenseID,
       DateTime DetainDate, decimal FineFees, int CreatedByUserID)
        {

            int RowsAffected = 0;
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE DetainedLicenses 
                            SET LicenseID = @LicenseID,
                            DetainDate = @DetainDate,
                            FineFees = @FineFees,
                             CreatedByUserID = @CreatedByUserID
                             WHERE DetainID = @DetainID";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@DetainID", DetianID);
            Command.Parameters.AddWithValue("@LicenseID", LicenseID);
            Command.Parameters.AddWithValue("@DetainDate", DetainDate);
            Command.Parameters.AddWithValue("@FineFees", FineFees);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

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

        public static bool ReleaseLicense(int DetainID, DateTime? ReleaseDate, int ReleasedByUserID, int ReleaseApplicationID)
        {

            int RowsAffected = 0;
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE DetainedLicenses 
                            SET ReleaseDate = @ReleaseDate,
                            ReleasedByUserID = @ReleasedByUserID,
                            ReleaseApplicationID = @ReleaseApplicationID,
                             IsReleased = 1
                             WHERE DetainID = @DetainID";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@DetainID", DetainID);
            Command.Parameters.AddWithValue("@ReleaseDate", ReleaseDate);
            Command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
            Command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
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

        public static bool FindDetainedLicenseByID(int DetainID, ref int LicenseID, ref DateTime DetainDate, ref decimal FineFees, ref int CreatedByUserID, ref bool IsReleased,
            ref DateTime? ReleaseDate, ref int ReleasedByUserID, ref int ReleaseApplicationID)
        {

            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM DetainedLicenses
                            WHERE DetainID = @DetainID";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@DetainID", DetainID);

            try
            {
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read())
                {
                    LicenseID = (int)reader["LicenseID"];

                    DetainDate = (DateTime)reader["DetainDate"];

                    FineFees = (decimal)reader["FineFees"];

                    CreatedByUserID = (int)reader["CreatedByUserID"];

                    IsReleased = (bool)reader["IsReleased"];

                    if (reader["ReleaseDate"] == DBNull.Value)
                    {
                        ReleaseDate = null;
                    }
                    else
                    {
                        ReleaseDate = (DateTime)reader["ReleaseDate"];
                    }


                    if (reader["ReleasedByUserID"] == DBNull.Value)
                    {
                        ReleasedByUserID = -1;
                    }
                    else
                    {
                        ReleasedByUserID = (int)reader["ReleasedByUserID"];
                    }


                    if (reader["ReleaseApplicationID"] == DBNull.Value)
                    {
                        ReleaseApplicationID = -1;
                    }
                    else
                    {
                        ReleaseApplicationID = (int)reader["ReleaseApplicationID"];
                    }
                }
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

        public static bool FindDetainedLicenseByLicenseID(int LicenseID, ref int DetainID, ref DateTime DetainDate, ref decimal FineFees, ref int CreatedByUserID, ref bool IsReleased,
          ref DateTime? ReleaseDate, ref int ReleasedByUserID, ref int ReleaseApplicationID)
        {

            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM DetainedLicenses
                            WHERE LicenseID = @LicenseID
                            AND IsReleased = 0";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read())
                {
                    DetainID = (int)reader["DetainID"];

                    DetainDate = (DateTime)reader["DetainDate"];

                    FineFees = (decimal)reader["FineFees"];

                    CreatedByUserID = (int)reader["CreatedByUserID"];

                    IsReleased = (bool)reader["IsReleased"];

                    if (reader["ReleaseDate"] == DBNull.Value)
                    {
                        ReleaseDate = null;
                    }
                    else
                    {
                        ReleaseDate = (DateTime)reader["ReleaseDate"];
                    }


                    if (reader["ReleasedByUserID"] == DBNull.Value)
                    {
                        ReleasedByUserID = -1;
                    }
                    else
                    {
                        ReleasedByUserID = (int)reader["ReleasedByUserID"];
                    }


                    if (reader["ReleaseApplicationID"] == DBNull.Value)
                    {
                        ReleaseApplicationID = -1;
                    }
                    else
                    {
                        ReleaseApplicationID = (int)reader["ReleaseApplicationID"];
                    }
                }
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


    }
}