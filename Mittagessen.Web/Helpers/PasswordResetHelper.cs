using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Mittagessen.Web.Helpers
{
    public static class PasswordResetHelper
    {
        public static string EncryptString(string clearText)
        {

            byte[] clearTextBytes = Encoding.UTF8.GetBytes(clearText);

            SymmetricAlgorithm rijn = SymmetricAlgorithm.Create();

            var ms = new MemoryStream();
            byte[] rgbIV = Encoding.ASCII.GetBytes("ryojvlzmdalyglrj");
            byte[] key = Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings["PasswordResetKey"]);
            var cs = new CryptoStream(ms, rijn.CreateEncryptor(key, rgbIV),CryptoStreamMode.Write);

            cs.Write(clearTextBytes, 0, clearTextBytes.Length);

            cs.Close();

            return Convert.ToBase64String(ms.ToArray());
        }

        public static string DecryptString(string encryptedText)
        {
            byte[] encryptedTextBytes = Convert.FromBase64String(encryptedText);

            var ms = new MemoryStream();

            SymmetricAlgorithm rijn = SymmetricAlgorithm.Create();

            byte[] rgbIV = Encoding.ASCII.GetBytes("ryojvlzmdalyglrj");
            byte[] key = Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings["PasswordResetKey"]);

            var cs = new CryptoStream(ms, rijn.CreateDecryptor(key, rgbIV),CryptoStreamMode.Write);

            cs.Write(encryptedTextBytes, 0, encryptedTextBytes.Length);

            cs.Close();

            return Encoding.UTF8.GetString(ms.ToArray());

        }
    }
}