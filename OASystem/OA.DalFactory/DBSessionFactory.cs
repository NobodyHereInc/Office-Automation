using OA.IDAL;
using OA.DAL;
using System.Runtime.Remoting.Messaging;

namespace OA.DalFactory
{
    public class DBSessionFactory
    {
        public static IDbSession CreateDbSession()
        {
            IDbSession dbSession = (IDbSession)CallContext.GetData("dbSession");
            if (dbSession == null)
            {
                dbSession = new DbSession();
                CallContext.SetData("dbSession", dbSession);
            }
            return dbSession;
        }
    }
}
