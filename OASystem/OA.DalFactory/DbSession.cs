using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OA.DAL;
using OA.IDAL;
using System.Data.Entity;
using System.Data.SqlClient;

namespace OA.DalFactory
{
    /// <summary>
    /// Class Description: This class is used to as middle layout for dal and service.
    /// </summary>
    public partial class DbSession : IDbSession
    {

        public DbContext Db
        {
            get { return DBContextFactory.CreateDbContext(); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool SaveChanges()
        {
            try
            {
                return Db.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
           
        }

        /// <summary>
        /// This function is used to EXECUTE insert delete and update
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        public int ExecuteSql(String sql,params SqlParameter[] para)
        {
            return Db.Database.ExecuteSqlCommand(sql, para);
        }

        /// <summary>
        /// This function is used to EXECUTE SELECT
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        public List<T> ExecuteQuery<T>(String sql, params SqlParameter[] para)
        {
            return Db.Database.SqlQuery<T>(sql, para).ToList();
        }
    }
}
