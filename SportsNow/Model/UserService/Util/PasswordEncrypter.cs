using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Es.Udc.DotNet.SportsNow.Model.UserService.Util
{
    /// <summary>
    /// Static Class with cryptografic utilities
    /// </summary>
    public static class PasswordEncrypter
    {
        /// <summary>
        /// Method to encrypt with a SHA 256 Algorithm
        /// </summary>
        /// <param name="password">String to encrypt</param>
        /// <returns>Returns a String with the <paramref name="password"/> encrypted
        /// </returns>                       
        public static String Crypt(String password)
        {
            HashAlgorithm hashAlg = new SHA256Managed();

            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            byte[] encryptedPasswordBytes = hashAlg.ComputeHash(passwordBytes);

            String encryptedPassword = Convert.ToBase64String(encryptedPasswordBytes);

            return encryptedPassword;
        }


        public static Boolean IsClearPasswordCorrect(String clearPassword,
            String encryptedPassword)
        {
            string encryptedClearPassword = Crypt(clearPassword);

            return encryptedClearPassword.Equals(encryptedPassword);
        }
    }
}
