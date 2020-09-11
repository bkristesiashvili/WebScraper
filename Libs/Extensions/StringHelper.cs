using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace CGCCrawler.Libs.Extensions
{
    public static class StringHelper
    {
        /// <summary>
        /// Regular expression matches check method
        /// </summary>
        /// <param name="text"></param>
        /// <param name="RegexPattern"></param>
        /// <returns></returns>
        public static bool IsMatch(this string text, string RegexPattern)
        {
            return Regex.IsMatch(text, RegexPattern);
        }

        /// <summary>
        /// Get String from text helper method
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static byte[] GetBytes(this string text)
        {
            return Encoding.ASCII.GetBytes(text);
        }

        /// <summary>
        /// Gets Base64 string helper method
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ToBase64(this string text)
        {
            return Convert.ToBase64String(GetBytes(text));
        }

        /// <summary>
        /// get strin from bast64 cipher text
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string FromBase64(this string text)
        {
            return Encoding.ASCII.GetString(Convert.FromBase64String(text));
        }

        /// <summary>
        /// Gets md5 encrypted text
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ToMD5String(this string text)
        {
            byte[] bits = text.GetBytes();

            using (MD5 _md5 = MD5.Create())
            {
                bits = _md5.ComputeHash(bits);
                _md5.Clear();
            }

            text = string.Empty;

            foreach (byte bit in bits)
                text += bit.ToString("X2");

            return text;
        }
    }
}
