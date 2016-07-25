 
using OA.IDAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OA.DalFactory
{
    public partial class DALAbstractFactory
    {
      
   
		
	    public static IActionInfoDal CreateActionInfoDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["DalNameSpace"] + ".ActionInfoDal";

            var obj  = CreateInstance(classFulleName,ConfigurationManager.AppSettings["DalAssembly"]);

            return obj as IActionInfoDal;
        }
		
	    public static IDepartmentDal CreateDepartmentDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["DalNameSpace"] + ".DepartmentDal";

            var obj  = CreateInstance(classFulleName,ConfigurationManager.AppSettings["DalAssembly"]);

            return obj as IDepartmentDal;
        }
		
	    public static IOrderInfoDal CreateOrderInfoDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["DalNameSpace"] + ".OrderInfoDal";

            var obj  = CreateInstance(classFulleName,ConfigurationManager.AppSettings["DalAssembly"]);

            return obj as IOrderInfoDal;
        }
		
	    public static IR_UserInfo_ActionInfoDal CreateR_UserInfo_ActionInfoDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["DalNameSpace"] + ".R_UserInfo_ActionInfoDal";

            var obj  = CreateInstance(classFulleName,ConfigurationManager.AppSettings["DalAssembly"]);

            return obj as IR_UserInfo_ActionInfoDal;
        }
		
	    public static IRoleInfoDal CreateRoleInfoDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["DalNameSpace"] + ".RoleInfoDal";

            var obj  = CreateInstance(classFulleName,ConfigurationManager.AppSettings["DalAssembly"]);

            return obj as IRoleInfoDal;
        }
		
	    public static IsysdiagramDal CreatesysdiagramDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["DalNameSpace"] + ".sysdiagramDal";

            var obj  = CreateInstance(classFulleName,ConfigurationManager.AppSettings["DalAssembly"]);

            return obj as IsysdiagramDal;
        }
		
	    public static IUserInfoDal CreateUserInfoDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["DalNameSpace"] + ".UserInfoDal";

            var obj  = CreateInstance(classFulleName,ConfigurationManager.AppSettings["DalAssembly"]);

            return obj as IUserInfoDal;
        }
	}
	
}