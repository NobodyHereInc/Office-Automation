using OA.IDAL;
using System.Configuration;
using System.Reflection;

namespace OA.DalFactory
{
    /// <summary>
    /// Class Description: this class is used to reflection to create each dal.
    /// </summary>
    public partial class DALAbstractFactory
    {
        private static readonly string DalNameSpace = ConfigurationManager.AppSettings["DalNameSpace"];
        private static readonly string DalAssembly = ConfigurationManager.AppSettings["DalAssembly"];

        /// <summary>
        /// This function is used to create IUserInfoDal by reflection.
        /// </summary>
        /// <returns></returns>
        public static IUserInfoDal CreateUserInfoDal()
        {
            string classFulleName = DalNameSpace + ".UserInfoDal";
            
            var obj = CreateInstance(classFulleName, DalAssembly);

            return obj as IUserInfoDal;
        }





        /// <summary>
        /// 
        /// </summary>
        /// <param name="fullClassName"></param>
        /// <param name="assemblyPath"></param>
        /// <returns></returns>
        private static object CreateInstance(string fullClassName, string assemblyPath)
        {
            var assembly = Assembly.Load(assemblyPath);// load assembly.
            return assembly.CreateInstance(fullClassName);
        }
    }
}
