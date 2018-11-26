using System.IO;

namespace WpfApp.Helper
{
    /// <summary>
    /// Defines the file related operations.
    /// </summary>
    public class FileHelper
    {
        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <param name="path">The path.</param>
        public static void DeleteFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
