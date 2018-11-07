using System;
using System.IO;
using System.Linq;
using System.Numerics;

namespace Core
{
    /// <summary>
    ///
    /// </summary>
    public class FileManipulationHelper
    {
        /// <summary>
        /// Gets the random name of the file.
        /// </summary>
        /// <returns>
        ///
        /// </returns>
        public static string GetRandomFileName()
        {
            Random random = new Random();

            byte[] data = new byte[4];

            random.NextBytes(data);

            int bigInt = (int)new BigInteger(data);

            if (bigInt < 0)
            {
                bigInt = bigInt * (-1);
            }

            return bigInt.ToString();
        }

        /// <summary>
        /// Gets the file extension.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public static string GetFileExtension(string fileName)
        {
            //string[] fileNameWithExtension = fileName.Split('.');

            string extension = fileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).Last().Trim();

            return extension;
            //return fileNameWithExtension[1];
        }



        //Read Json File
        public static string ReadTextFromJsonFile(string fileName)
        {
            string fileContents = string.Empty;

            using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                using (TextReader tr = new StreamReader(fileStream))
                {
                    fileContents = tr.ReadToEnd();
                }
            }

            return fileContents;
        }


        //Write Json File
        private static void WriteTextToJsonFile(string fileName, string content)
        {
            using (TextWriter tw = new StreamWriter(fileName))
            {
                tw.Write(content);
                tw.Close();
            }
        }


    }
}