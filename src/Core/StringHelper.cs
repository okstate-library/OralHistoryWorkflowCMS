using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
namespace Core
{
    /// <summary>
    /// 
    /// </summary>
    public class StringHelper
    {
        /// <summary>
        /// Shorts the strings.
        /// </summary>
        /// <param name="phrase">The phrase.</param>
        /// <param name="requiredLength">Length of the required.</param>
        /// <returns></returns>
        public static string ShortString(string phrase, int requiredLength)
        {
            string edited = string.Empty;

            if (!string.IsNullOrEmpty(phrase))
            {
                if (phrase.Length > requiredLength)
                {
                    edited = phrase.Substring(0, requiredLength) + "...";
                }
                else
                {
                    edited = phrase;
                }
            }

            return edited;
        }

        /// <summary>
        /// Creates the salt.
        /// </summary>
        /// <returns>
        /// Returns the created salt value.
        /// </returns>
        public static string CreateSalt()
        {
            //Create and populate random byte array
            byte[] randomArray = new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };

            string randomString;

            //Create random salt and convert to string
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

            rng.GetBytes(randomArray);

            randomString = Convert.ToBase64String(randomArray);

            return randomString;
        }

        /// <summary>
        /// Generates the password.
        /// </summary>
        /// <returns>
        /// Returns the randomly generated password.
        /// </returns>
        public static string GeneratePassword()
        {
            string strPwdchar = "abcdefghijklmnopqrstuvwxyz0123456789#+@&$ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            string strPwd = "";

            Random rnd = new Random();

            for (int i = 0; i <= 7; i++)
            {
                int iRandom = rnd.Next(0, strPwdchar.Length - 1);
                strPwd += strPwdchar.Substring(iRandom, 1);
            }

            return strPwd;
        }

        /// <summary>
        /// create a new guid.
        /// </summary>
        /// <returns>
        /// string with new guid id.
        /// </returns>
        public static string CreateNewGuid()
        {
            String id = Guid.NewGuid().ToString();

            return id;
        }

        /// <summary>
        /// Gets the random string.
        /// </summary>
        /// <returns>
        /// 
        /// </returns>
        public static string RandomString()
        {
            int size = 6;

            Random _rng = new Random();

            string _chars = "0987654321";

            char[] buffer = new char[size];

            for (int i = 0; i < size; i++)
            {
                buffer[i] = _chars[_rng.Next(_chars.Length)];
            }

            return new string(buffer);
        }

        /// <summary>
        /// Gets the mobile no without mask.
        /// </summary>
        /// <param name="phoneNumberWithMask">The phone number with mask.</param>
        /// <returns></returns>
        public static string GetMobileNoWithoutMask(string phoneNumberWithMask)
        {
            return string.Format("{0}{1}{2}", phoneNumberWithMask.Substring(1, 3), phoneNumberWithMask.Substring(6, 3), phoneNumberWithMask.Substring(10, 4));
        }
    }
}