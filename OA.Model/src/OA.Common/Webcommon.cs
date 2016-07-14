using System;
using System.Security.Cryptography;
using System.Text;

namespace OA.Common
{
    public class Webcommon
    {
        #region MD5 Encode
        /// <summary>
        /// This function is used to create MD5.
        /// </summary>
        /// <param name="str"> input string. </param>
        /// <returns> MD5 string. </returns>
        public static String Md5String(String str)
        {
            MD5 md5 = MD5.Create();
            byte[] buffer = Encoding.UTF8.GetBytes(str);
            byte[] md5Buffer = md5.ComputeHash(buffer);
            StringBuilder sb = new StringBuilder();
            foreach (byte item in md5Buffer)
            {
                sb.Append(item.ToString("x2"));
            }

            return sb.ToString();
        }
        #endregion

    }
}
