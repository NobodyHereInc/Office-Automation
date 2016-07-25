 
using OA.Model;
using OA.IService;
using System.Collections.Generic;
using System.Linq;
using System;
using OA.Model.SearchParam;
using System.Net.Mail;
using System.Text;
using System.Net;

namespace OA.Service
{
	
	public partial class ActionInfoService :BaseService<ActionInfo>,IActionInfoService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.ActionInfoDal;
        }
    }   
	
	public partial class DepartmentService :BaseService<Department>,IDepartmentService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.DepartmentDal;
        }
    }   
	
	public partial class OrderInfoService :BaseService<OrderInfo>,IOrderInfoService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.OrderInfoDal;
        }
    }   
	
	public partial class R_UserInfo_ActionInfoService :BaseService<R_UserInfo_ActionInfo>,IR_UserInfo_ActionInfoService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.R_UserInfo_ActionInfoDal;
        }
    }   
	
	public partial class RoleInfoService :BaseService<RoleInfo>,IRoleInfoService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.RoleInfoDal;
        }
    }   
	
	public partial class sysdiagramService :BaseService<sysdiagram>,IsysdiagramService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.sysdiagramDal;
        }
    }   
	
	public partial class UserInfoService :BaseService<UserInfo>,IUserInfoService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.UserInfoDal;
        }
    }   
	
}