using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OA.IDAL;
using System.Threading;

namespace OA.DalFactory
{
    public class DBSessionFactory
    {
        // CallContext can't find, so I use AsyncLocal
        private static AsyncLocal<IDBSeesion> _AsyncLocal = new AsyncLocal<IDBSeesion>();

        public static IDBSeesion GetDBSession()
        {
            IDBSeesion context = _AsyncLocal.Value;

            if (context == null)
            {
                context = new DBSession();
                _AsyncLocal.Value = context;
            }

            return _AsyncLocal.Value;
        }
    }
}
