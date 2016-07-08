using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace OA.DAL
{
    public class ContextFactory
    {
        // CallContext can't find, so I use AsyncLocal
        private static AsyncLocal<OAContext> _AsyncLocal = new AsyncLocal<OAContext>();

        public static DbContext GetContext()
        {
            OAContext context = _AsyncLocal.Value;

            if (context == null)
            {
                context = new OAContext();
                _AsyncLocal.Value = context;
            }

            return _AsyncLocal.Value;
        }
    }
}
