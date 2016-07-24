using Memcached.ClientLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Common
{
    /// <summary>
    /// Class Description: MemcacheHelper.
    /// </summary>
    public class MemcacheHelper
    {
        private static readonly MemcachedClient mc = null;

        /// <summary>
        /// static constructor.
        /// </summary>
        static MemcacheHelper()
        {
            string[] serverlist = { "127.0.0.1:11211", "10.0.0.132:11211" };

            
            SockIOPool pool = SockIOPool.GetInstance();
            pool.SetServers(serverlist);

            pool.InitConnections = 3;
            pool.MinConnections = 3;
            pool.MaxConnections = 5;

            pool.SocketConnectTimeout = 1000;
            pool.SocketTimeout = 3000;

            pool.MaintenanceSleep = 30;
            pool.Failover = true;

            pool.Nagle = false;
            pool.Initialize();

            // get instance of MemcachedClient
            mc = new MemcachedClient();
            mc.EnableCompression = false;
        }





        /// <summary>
        /// this function is used to save data to memcache.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Set(string key, object value)
        {
            mc.Set(key, value);
        }





        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="time"></param>
        public static void Set(string key, object value, DateTime time)
        {
            mc.Set(key, value, time);
        }





        /// <summary>
        /// get value from memcache.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object Get(string key)
        {
            return mc.Get(key);
        }





        /// <summary>
        /// delete value from memcache.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Delete(string key)
        {
            if (mc.KeyExists(key))
            {
                return mc.Delete(key);
            }

            return false;
        }
    }
}
