using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;

namespace DataAccessLayer
{
    public class clsUserData
    {
        public static int AddNewUser(int PersonID, string UserName, string Password, bool IsActive)
        {
            int UserID = -1;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO Users(PersonID, UserName, Password, IsActive) Values 
                            (@PersonID , @UserName, @Password, @IsActive)
                            SELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@PersonID", PersonID);
            Command.Parameters.AddWithValue("@UserName", UserName);
            Command.Parameters.AddWithValue("@Password", Password);
            Command.Parameters.AddWithValue("@IsActive", IsActive);


            try
            {
                Connection.Open();

                object result = Command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                {
                    UserID = InsertedID;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }
            return UserID;
        }

        public static bool UpdateUser(int UserID, int PersonID, string UserName, string Password, bool IsActive)
        {
            int RowAffected = 0;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update Users SET 
                            PersonID = @PersonID,
                            UserName = @UserName, 
                            Password = @Password, 
                            IsActive = @IsActive
                            WHERE UserID = @UserID";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@PersonID", PersonID);
            Command.Parameters.AddWithValue("@UserName", UserName);
            Command.Parameters.AddWithValue("@Password", Password);
            Command.Parameters.AddWithValue("@IsActive", IsActive);
            Command.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                Connection.Open();

               RowAffected = Command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }
            return (RowAffected > 0);
        }

        public static bool DeleteUser(int UserID)
        {
            int RowAffected = 0;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"DELETE Users 
                            WHERE UserID = @UserID";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                Connection.Open();

                RowAffected = Command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }
            return (RowAffected > 0);
        }

        public static bool GetUserInfoByUserID(int UserID, ref int PersonID, ref string UserName, ref string Password, ref bool IsActive)
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM Users 
                            WHERE UserID = @UserID";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read())
                {
                    PersonID = (int)reader["PersonID"];
                    UserName = (string)reader["UserName"];
                    Password = (string)reader["Password"];
                    IsActive = (bool)reader["IsActive"];

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

        public static bool GetUserInfoByUserName(string UserName, ref int PersonID, ref int UserID, ref string Password, ref bool IsActive)
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM Users 
                            WHERE UserName = @UserName";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@UserName", UserName);

            try
            {
                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read())
                {
                    PersonID = (int)reader["PersonID"];
                    UserID = (int)reader["UserID"];
                    Password = (string)reader["Password"];
                    IsActive = (bool)reader["IsActive"];

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

        public static bool GetUserInfoByPersonID(int PersonID, ref int UserID, ref string UserName, ref string Password, ref bool IsActive)
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM Users 
                            WHERE PersonID = @PersonID";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read())
                {
                    UserID = (int)reader["UserID"];
                    UserName = (string)reader["UserName"];
                    Password = (string)reader["Password"];
                    IsActive = (bool)reader["IsActive"];

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

        public static bool GetUserByUserNameAndPassword(string UserName, string Password, ref int PersonID, ref int UserID, ref bool IsActive)
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM Users 
                            WHERE UserName = @UserName AND Password = @Password";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@UserName", UserName);
            Command.Parameters.AddWithValue("@Password", Password);

            try
            {
                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read())
                {
                    PersonID = (int)reader["PersonID"];
                    UserID = (int)reader["UserID"];
                    IsActive = (bool)reader["IsActive"];

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

        public static bool IsUserExistForPersonID(int PersonID)
        {
            bool IsHasUser = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Select IsFound = 1 
                        FROM Users
            INNER JOIN People ON People.PersonID = Users.PersonID
            WHERE People.PersonID = @PersonID";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                Connection.Open();
                object result = Command.ExecuteScalar();

                if (result != null)
                    IsHasUser = true;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }
            return IsHasUser;
        }

        public static bool IsUserExist(string UserName)
        {
            bool IsFound = false;


            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "Select IsFound = 1 From Users WHERE UserName = @UserName";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@UserName", UserName);
            try
            {
                Connection.Open();
                object result = Command.ExecuteScalar();

                if (result != null)
                    IsFound = true;
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

        public static bool IsUserExist(int UserID)
        {
            bool IsFound = false;


            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "Select IsFound = 1 From Users WHERE UserID = @UserID";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                Connection.Open();
                object result = Command.ExecuteScalar();

                if (result != null)
                    IsFound = true;
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

        public static bool ChangePassword(int UserID, string NewPassword)
        {
            int rowsAffected = 0;
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update Users
                             SET Password = @Password
                             WHERE UserID = @UserID";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue(@"UserID", UserID);

            try
            {
                Connection.Open();
                rowsAffected = Command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }
            return rowsAffected > 0;
        } 

        public static DataTable GetAllUsers()
        {
            DataTable dt = new DataTable();
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT Users.UserID, Users.PersonID, CONCAT_WS(' ', People.FirstName, People.SecondName, People.ThirdName, People.LastName) AS FullName,
                           Users.UserName, Users.IsActive FROM Users
                           INNER JOIN People ON People.PersonID = Users.PersonID";

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
            catch (Exception e)
            {

            }
            finally
            {
                Connection.Close();
            }
            return dt;
        }

        public static int GetTotalUsers()
        {
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            int TotalUsers = 0;
            string query = "SELECT COUNT(*) From Users";

            SqlCommand Command = new SqlCommand(query, Connection);

            try
            {
                Connection.Open();
                object result = Command.ExecuteScalar();
                if (result != null)
                    int.TryParse(result.ToString(), out TotalUsers);
            }
            catch (Exception e)
            {

            }
            finally
            {
                Connection.Close();
            }

            return TotalUsers;
        }

    }
}
