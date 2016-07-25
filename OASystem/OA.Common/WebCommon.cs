using System;
using System.Security.Cryptography;
using System.Text;

namespace OA.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class WebCommon
    {
        /// <summary>
        /// This function is used to get Md5 string.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Md5Stirng(string str)
        {
            MD5 md5 = MD5.Create();
            byte[] buffer = Encoding.UTF8.GetBytes(str);
            byte[] md5buffer = md5.ComputeHash(buffer);

            StringBuilder sb = new StringBuilder();
            foreach (var item in md5buffer)
            {
                sb.Append(item);
            }

            return sb.ToString();
        }
    }
}
