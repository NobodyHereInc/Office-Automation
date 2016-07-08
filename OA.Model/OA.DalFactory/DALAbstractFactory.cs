using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OA.IDAL;
using OA.DAL;
using System.Reflection;
using OA.Common;
using Microsoft.Extensions.Configuration;

namespace OA.DalFactory
{
    /// <summary>
    /// Class Descrption: abstract factory
    /// </summary>
    public class DALAbstractFactory
    {
        private static readonly String DalNameSpace = ""; // from AppSetting.json
        private static readonly String DalAssembly = ""; // from AppSetting.json

        // abstract factory
        public static IUserInfoDal GetUserInfoDal()
        {
            String fullClassName = DalNameSpace + ".UserInforDal"; // get full class name.
            return CreateInstance(fullClassName, DalAssembly) as IUserInfoDal;
        }

        
        private static object CreateInstance(String fullClassName, String assemblyPath)
        {

            //// get Assembly.
            //var a1 = Assembly.Load(assemblyPath);

            //// return object instance.
            //return a1.CreateInstance(fullClassName) as IUserInfoDal;

            return null;
        }
    }
}
