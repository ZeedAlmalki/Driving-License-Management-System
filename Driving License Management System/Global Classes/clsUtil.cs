using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driving_License_Management_System
{
    public class clsUtil
    {
        public static string FilePath = @"C:\Course 19 DVLD\SavedUserInformationByRememberMe.txt";

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
            string Information = UserName + "#//#" + Password + "#//#" + RememberMe;

            if (File.Exists(FilePath))
            {
                File.WriteAllText(FilePath, Information);
                return true;
            }
            return false;
        }

        public static bool RemoveUserLoginInformation()
        {
            if (File.Exists(FilePath))
            {
                File.WriteAllText(FilePath, string.Empty);
                return true;
            }
           else
            {
                return false;
            }
        }

        public static bool IsFileExistAndHasData(string FilePath)
        {
            if (File.Exists(FilePath) && File.ReadAllText(FilePath).Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool GetSavedUserLoginInformation(ref string UserName, ref string Password, ref bool RememberMe)
        {
            if (IsFileExistAndHasData(FilePath))
            {
                string Data = File.ReadAllText(FilePath);

                string[] Parts = Data.Split(new string[] { "#//#" }, StringSplitOptions.None);

                UserName = Parts[0];
                Password = Parts[1];
                RememberMe = Convert.ToBoolean(Parts[2]);
                return true;
            }
            return false;
        }
    }
}
