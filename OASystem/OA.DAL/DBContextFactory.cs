using OA.Model;
using System;
using System.Data.Entity;
using System.Runtime.Remoting.Messaging;

namespace OA.DAL
{
    /// <summary>
    /// This Class is used to create DbContext.
    /// </summary>
    public class DBContextFactory
    {
        /// <summary>
        /// This function is used to create Dbcontext(Singleton Pattern)
        /// </summary>
        /// <returns></returns>
        public static DbContext CreateDbContext()
        {
            // get dbContext from CallContext.
            DbContext dbContext = (DbContext)CallContext.GetData("dbContext");

            // check CallContext whether contains DbContext named "dbContext".
            if (dbContext == null)
            {
                // initiate an OA_DBEntities.
                dbContext = new OA_DBEntities();
                // save OA_DBEntities into CallContext.
                CallContext.SetData("dbContext", dbContext);
            }

            // return dbContext.
            return dbContext;
        }
    }
}
