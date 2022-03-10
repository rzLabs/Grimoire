using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Security.Cryptography;

namespace Grimoire.Utilities
{
    /// <summary>
    /// Provides access to various cryptography methods
    /// </summary>
    public static class CryptoUtility
    {
        /// <summary>
        /// Generate a MD5 hash string (for Rapopelz password comparison)
        /// </summary>
        /// <param name="input">Salt+Password string</param>
        /// <returns>Salted and Hashed password string</returns>
        public static string GenerateMD5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inBuffer = System.Text.Encoding.Default.GetBytes(input);
                byte[] hashBuffer = md5.ComputeHash(inBuffer);

                string outStr = string.Empty;

                for (int i = 0; i < hashBuffer.Length; i++)
                    outStr += hashBuffer[i].ToString("X2");

                return outStr;
            }
        }
    }
}
