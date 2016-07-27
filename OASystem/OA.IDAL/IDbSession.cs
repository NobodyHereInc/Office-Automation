using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.IDAL
{
    public partial interface IDbSession
    {
        /// <summary>
        /// This funtion is used to save change.
        /// </summary>
        /// <returns></returns>
        bool SaveChanges();


        /// <summary>
        /// This function is used to Execute DDL. (DELETE INSERT UPDATE)
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        int ExecuteSql(String sql,params SqlParameter[] para);

        /// <summary>
        /// SELECT
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        List<T> ExecuteQuery<T>(String sql, params SqlParameter[] para);
    }
}
