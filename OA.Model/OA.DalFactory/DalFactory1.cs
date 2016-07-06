using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OA.DAL;
using OA.IDAL;
using System.Reflection;
using Microsoft.Framework;
using Microsoft.Extensions.Options;

namespace OA.DalFactory
{
    public class DalFactory1
    {
        // simple factory
        public static IUserInfoDal GetUserInfo()
        {
            return new UserInfoDal();
        }

        // abstract factory
        public static IUserInfoDal GetUserInfo2()
        {
            // get Assembly.
            Assembly a1 = Assembly.Load("");

            // return object instance.
            return a1.CreateInstance("") as IUserInfoDal;
        }
    }
}
