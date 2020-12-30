using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LSS.BE.Core.Common.Utiles
{
    public class Utiles
    {
        /// <summary>
        /// Check Valid Path
        /// </summary>
        /// <returns>
        ///  True if the caller has the required permissions and path contains the name of an existing file; otherwise, false.
        /// </returns>
        public static bool IsValidPath(string path)
        {
            bool exists = File.Exists(path);
            return exists;
        }
    }
}
