using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Mittagessen.Web.Helpers
{
    public static class PasswordHelper
    {
        const int SALT_LENGTH = 5;

        public static byte[] CreateSalt()
        {
            //Generate a cryptographic random number.
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[SALT_LENGTH];
            rng.GetBytes(buff);
            return buff;
        }

        public static byte[] GeneratePassword(string utf8PlainText, byte[] salt)
        {
            return GeneratePassword(Encoding.UTF8.GetBytes(utf8PlainText), salt);
        }

        public static byte[] GeneratePassword(byte[] plainText, byte[] salt)
        {
            var algorithm = new SHA256Managed();

            return algorithm.ComputeHash(plainText.Concat(salt).ToArray());
        }

        public static bool PasswordsMatch(string specifiedUtf8Password, byte[] storedPassword)
        {
            return PasswordsMatch(Encoding.UTF8.GetBytes(specifiedUtf8Password), storedPassword);
        }

        public static bool PasswordsMatch(byte[] specifiedPassword, byte[] storedPassword)
        {
            return specifiedPassword.SequenceEqual(storedPassword);
        }
    }
}
