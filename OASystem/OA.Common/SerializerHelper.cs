using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Common
{
    public class SerializerHelper
    {
        /// <summary>
        /// This function is used to serilizer a object.
        /// </summary>
        /// <param name="obj">input object that need to serilizer.</param>
        /// <returns></returns>
        public static string SerializerToString(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }




        /// <summary>
        /// This function is used to Deserializer string to object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static T DeserializerToObject<T>(String str)
        {
            return JsonConvert.DeserializeObject<T>(str);
        }
    }
}
