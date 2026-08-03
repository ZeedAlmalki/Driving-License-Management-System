using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Driving_License_Management_System
{
    public class clsUtil
    {
        public static string FilePath = @"C:\Course 19 DVLD\SavedUserInformationByRememberMe.txt";
        public static string RegisteryPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLD";


        public static bool IsRememberMe = false;
        public static string GenerateGUID()
        {
            return Guid.NewGuid().ToString();
        }

        public static bool CreateFolderIfDoesNotExist(string FolderPath)
        {
            if (Directory.Exists(FolderPath))
                return true;

            try
            {
                Directory.CreateDirectory(FolderPath);
                return true;
            }
            catch (IOException e)
            {
                return false;
            }
        }

        public static string ReplaceFileNameWithGUID(string sourceFile)
        {
            string fileName = sourceFile;
            FileInfo fi = new FileInfo(fileName);
            string extn = fi.Extension;
            return GenerateGUID() + extn;
        }

        public static bool CopyImageProjectImageFolder(ref string sourceFile)
        {
            string DestinationFolder = @"C:\Course 19 DVLD\Images\";
            if (!CreateFolderIfDoesNotExist(DestinationFolder))
            {
                return false;
            }

            string DestinationFile = DestinationFolder + ReplaceFileNameWithGUID(sourceFile);

            try
            {
                File.Copy(sourceFile, DestinationFile, true);
            }
            catch (IOException iox)
            {
                MessageBox.Show(iox.Message, "Error", MessageBoxButtons.OK);
                return false;
            }
            sourceFile = DestinationFile;
            return true;
        }

        public static bool SaveUserLoginInformation(string UserName, string Password, bool RememberMe)
        {
            try
            {
                Registry.SetValue(RegisteryPath, "Username", UserName, RegistryValueKind.String);
                Registry.SetValue(RegisteryPath, "Password", Password, RegistryValueKind.String);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public static bool RemoveUserLoginInformation()
        {
            try
            {
                Registry.SetValue(RegisteryPath, "Username", "", RegistryValueKind.String);
                Registry.SetValue(RegisteryPath, "Password", "", RegistryValueKind.String);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public static bool GetSavedUserLoginInformation(ref string UserName, ref string Password)
        {
            try
            {
                string savedUserName = Registry.GetValue(RegisteryPath, "Username", null) as string;
                string savedPassword = Registry.GetValue(RegisteryPath, "Password", null) as string;

                if (savedUserName != null && savedPassword != null)
                {
                    UserName = savedUserName;
                    Password = savedPassword;
                    return true;
                }
                else
                    return false;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
    }
}
