using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using Encryption;

namespace Core
{
    /// <summary>
    ///
    /// </summary>
    public class Encryption
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Encryption"/> class.
        /// </summary>
        public Encryption()
        {
        }

        /// <summary>
        /// Encrypts the specified plain text.
        /// </summary>
        /// <param name="plainText">The plain text.</param>
        /// <param name="keyValue">The key value.</param>
        /// <returns>
        /// Encrypt the supplied plain text.
        /// </returns>
        public static string Encrypt(string plainText)
        {
            string intVector = "gotabayaking@tc.";

            EncryptionAlgorithm algorithm = EncryptionAlgorithm.Rijndael;
            //
            //  Init variables.
            //
            byte[] IV = null;
            byte[] cipherText = null;
            byte[] key = null;


            //  Try to encrypt.
            //  Create the encryptor.
            //
            Encryptor enc = new Encryptor(algorithm);
            byte[] plainTextByteArray = Encoding.ASCII.GetBytes(plainText);

            //  3Des only work with a 16 or 24 byte key.
            //
            key = Encoding.ASCII.GetBytes("sDnf0i7I7LFmCDSrqx8hLA==");
            //
            //  Must be 16 bytes for Rijndael.
            //
            IV = Encoding.ASCII.GetBytes(intVector);
            //
            //  Perform the encryption.
            //
            cipherText = enc.Encrypt(plainTextByteArray, key, IV);
            //
            //  Look at your cipher text and initialization vector.
            //
            return Convert.ToBase64String(cipherText);

        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="encryptedText"></param>
        /// <param name="keyValue"></param>
        /// <param name="intVector"></param>
        /// <param name="algoType"></param>
        /// <returns>
        /// Returns the decrypted value to the parameterized encrypt text.
        /// </returns>
        public static string Decrypt(string encryptedText, string keyValue, string intVector, EncryptionAlgorithm algoType)
        {
            EncryptionAlgorithm algorithm = algoType;

            //  Init variables.
            //
            byte[] IV = null;
            byte[] cipherText = null;
            byte[] key = null;

            try
            {
                //  3Des only work with a 16 or 24 byte key.
                //
                key = Encoding.ASCII.GetBytes(keyValue);
                //
                //  Must be 16 bytes for Rijndael.
                //
                IV = Encoding.ASCII.GetBytes(intVector);

                //  Try to decrypt.
                //  Set up your decryption, give it the algorithm and initialization vector.
                //
                Decryptor dec = new Decryptor(algorithm);

                cipherText = Convert.FromBase64String(encryptedText);
                //
                //  Go ahead and decrypt.
                //
                byte[] plainText = dec.Decrypt(cipherText, key, IV);
                //
                //  Look at your plain text.
                //
                return Encoding.ASCII.GetString(plainText);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static SecureString convertToSecureString(string strPassword)
        {
            var secureStr = new SecureString();
            if (strPassword.Length > 0)
            {
                foreach (var c in strPassword.ToCharArray()) secureStr.AppendChar(c);
            }
            return secureStr;
        }

        /// <summary>
        /// Converts to un secure string.
        /// </summary>
        /// <param name="secstrPassword">The secstr password.</param>
        /// <returns></returns>
        public static string convertToUNSecureString(SecureString secstrPassword)
        {
            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secstrPassword);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
    }
}