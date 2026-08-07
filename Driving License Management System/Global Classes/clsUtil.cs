using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Driving_License_Management_System
{
    public class clsUtil
    {
        private static string LoggingSourceName = "DVLD";
        private const string SymmetricKey = "Almalki_DVLD_KEY"; // it's not safe to put it here
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
                Registry.SetValue(RegisteryPath, "Password", Encrypt(Password, SymmetricKey), RegistryValueKind.String);
                return true;
            }
            catch (Exception ex)
            {
                SharedDVLD.RegisterLogInEventHandler(ex);
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
                SharedDVLD.RegisterLogInEventHandler(ex);
                return false;
            }
        }

        public static bool GetSavedUserLoginInformation(ref string UserName, ref string Password)
        {
            try
            {

                string savedUserName = Registry.GetValue(RegisteryPath, "Username", null) as string;
                string savedPassword = Registry.GetValue(RegisteryPath, "Password", null) as string;

                if (!string.IsNullOrWhiteSpace(savedPassword) || !string.IsNullOrWhiteSpace(savedUserName))
                {
                    UserName = savedUserName;
                    Password = Decrypt(savedPassword, SymmetricKey);
                    return true;
                }
                else
                    return false;

            }
            catch (Exception ex)
            {
                SharedDVLD.RegisterLogInEventHandler(ex);
            }
            return false;
        }
        public static string ComputeHash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] HashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

                return BitConverter.ToString(HashBytes).Replace("-", "");
            }
        }

        static string Encrypt(string plainText, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = new byte[aesAlg.BlockSize / 8]; // -> this is not safey. we have to use GenerateIV(); method.

                ICryptoTransform encrypoter = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);


                using (var msEncrypt = new System.IO.MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encrypoter, CryptoStreamMode.Write))
                    using (var swEncrypt = new System.IO.StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        static string Decrypt(string cipherText, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = new byte[aesAlg.BlockSize / 8];

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (var msDecrypt = new System.IO.MemoryStream(Convert.FromBase64String(cipherText)))
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (var srDecrypt = new System.IO.StreamReader(csDecrypt))
                {
                    return srDecrypt.ReadToEnd();
                }
            }
        }
    }
}
