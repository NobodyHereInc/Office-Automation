 
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
	
	public partial class bookService :BaseService<book>,IbookService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.bookDal;
        }
    }   
	
	public partial class DepartmentService :BaseService<Department>,IDepartmentService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.DepartmentDal;
        }
    }   
	
	public partial class KeyWordsRankService :BaseService<KeyWordsRank>,IKeyWordsRankService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.KeyWordsRankDal;
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
	
	public partial class SearchDetailService :BaseService<SearchDetail>,ISearchDetailService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.SearchDetailDal;
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