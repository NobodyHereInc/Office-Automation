 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OA.Model;
using OA.IDAL;


namespace OA.DAL
{
   
	
	public partial class ActionInfoDal :BaseDal<ActionInfo>,IActionInfoDal
    {
      
    }
	
	public partial class DepartmentDal :BaseDal<Department>,IDepartmentDal
    {
      
    }
	
	public partial class OrderInfoDal :BaseDal<OrderInfo>,IOrderInfoDal
    {
      
    }
	
	public partial class R_UserInfo_ActionInfoDal :BaseDal<R_UserInfo_ActionInfo>,IR_UserInfo_ActionInfoDal
    {
      
    }
	
	public partial class RoleInfoDal :BaseDal<RoleInfo>,IRoleInfoDal
    {
      
    }
	
	public partial class sysdiagramDal :BaseDal<sysdiagram>,IsysdiagramDal
    {
      
    }
	
	public partial class UserInfoDal :BaseDal<UserInfo>,IUserInfoDal
    {
      
    }
	
}