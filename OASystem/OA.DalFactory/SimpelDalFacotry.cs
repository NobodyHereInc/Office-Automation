 
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
		
	    public static IbookDal CreatebookDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["DalNameSpace"] + ".bookDal";

            var obj  = CreateInstance(classFulleName,ConfigurationManager.AppSettings["DalAssembly"]);

            return obj as IbookDal;
        }
		
	    public static IDepartmentDal CreateDepartmentDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["DalNameSpace"] + ".DepartmentDal";

            var obj  = CreateInstance(classFulleName,ConfigurationManager.AppSettings["DalAssembly"]);

            return obj as IDepartmentDal;
        }
		
	    public static IKeyWordsRankDal CreateKeyWordsRankDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["DalNameSpace"] + ".KeyWordsRankDal";

            var obj  = CreateInstance(classFulleName,ConfigurationManager.AppSettings["DalAssembly"]);

            return obj as IKeyWordsRankDal;
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
		
	    public static ISearchDetailDal CreateSearchDetailDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["DalNameSpace"] + ".SearchDetailDal";

            var obj  = CreateInstance(classFulleName,ConfigurationManager.AppSettings["DalAssembly"]);

            return obj as ISearchDetailDal;
        }
		
	    public static IUserInfoDal CreateUserInfoDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["DalNameSpace"] + ".UserInfoDal";

            var obj  = CreateInstance(classFulleName,ConfigurationManager.AppSettings["DalAssembly"]);

            return obj as IUserInfoDal;
        }
	}
	
}