using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

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





        /// <summary>
        /// This function is used to split words by user enter in. (Lucene.net)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static String[] SplitWords(string str)
        {
            List<String> list = new List<string>();
            // use 
            Analyzer analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
            TokenStream tokenStream = analyzer.TokenStream("", new StringReader(str));
            Lucene.Net.Analysis.Token token = null;
            while ((token = tokenStream.Next()) != null)
            {
                list.Add(token.TermText());
            }

            return list.ToArray();
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="k"></param>
        /// <param name="keycontent"></param>
        /// <returns></returns>
        public static string CreateHightLight(String k, String keycontent)
        {
            string resultStr = keycontent;

            if (k.Trim().IndexOf(' ') > 0)
            {
                String[] myArray = k.Split(' ');

                for (int i = 0; i < myArray.Length; i++)
                {
                    resultStr = resultStr.Replace(myArray[i].ToString(), "<font color=\"red\">" + myArray[i].ToString() + "</font>");
                }

                return resultStr;
            }
            else
            {           
                return System.Text.RegularExpressions.Regex.Replace(resultStr, k, "<font color=\"red\">" + k.ToUpper() + "</font>", RegexOptions.IgnoreCase);
            }
        }
    }
}
