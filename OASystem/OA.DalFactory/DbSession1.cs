 

using OA.DAL;
using OA.IDAL;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.DalFactory
{
	public partial class DbSession : IDbSession
    {
	
		private IActionInfoDal _ActionInfoDal;
        public IActionInfoDal ActionInfoDal
        {
            get
            {
                if(_ActionInfoDal == null)
                {
                    _ActionInfoDal = DALAbstractFactory.CreateActionInfoDal();
                }
                return _ActionInfoDal;
            }
            set { _ActionInfoDal = value; }
        }
	
		private IbookDal _bookDal;
        public IbookDal bookDal
        {
            get
            {
                if(_bookDal == null)
                {
                    _bookDal = DALAbstractFactory.CreatebookDal();
                }
                return _bookDal;
            }
            set { _bookDal = value; }
        }
	
		private IDepartmentDal _DepartmentDal;
        public IDepartmentDal DepartmentDal
        {
            get
            {
                if(_DepartmentDal == null)
                {
                    _DepartmentDal = DALAbstractFactory.CreateDepartmentDal();
                }
                return _DepartmentDal;
            }
            set { _DepartmentDal = value; }
        }
	
		private IKeyWordsRankDal _KeyWordsRankDal;
        public IKeyWordsRankDal KeyWordsRankDal
        {
            get
            {
                if(_KeyWordsRankDal == null)
                {
                    _KeyWordsRankDal = DALAbstractFactory.CreateKeyWordsRankDal();
                }
                return _KeyWordsRankDal;
            }
            set { _KeyWordsRankDal = value; }
        }
	
		private IR_UserInfo_ActionInfoDal _R_UserInfo_ActionInfoDal;
        public IR_UserInfo_ActionInfoDal R_UserInfo_ActionInfoDal
        {
            get
            {
                if(_R_UserInfo_ActionInfoDal == null)
                {
                    _R_UserInfo_ActionInfoDal = DALAbstractFactory.CreateR_UserInfo_ActionInfoDal();
                }
                return _R_UserInfo_ActionInfoDal;
            }
            set { _R_UserInfo_ActionInfoDal = value; }
        }
	
		private IRoleInfoDal _RoleInfoDal;
        public IRoleInfoDal RoleInfoDal
        {
            get
            {
                if(_RoleInfoDal == null)
                {
                    _RoleInfoDal = DALAbstractFactory.CreateRoleInfoDal();
                }
                return _RoleInfoDal;
            }
            set { _RoleInfoDal = value; }
        }
	
		private ISearchDetailDal _SearchDetailDal;
        public ISearchDetailDal SearchDetailDal
        {
            get
            {
                if(_SearchDetailDal == null)
                {
                    _SearchDetailDal = DALAbstractFactory.CreateSearchDetailDal();
                }
                return _SearchDetailDal;
            }
            set { _SearchDetailDal = value; }
        }
	
		private IUserInfoDal _UserInfoDal;
        public IUserInfoDal UserInfoDal
        {
            get
            {
                if(_UserInfoDal == null)
                {
                    _UserInfoDal = DALAbstractFactory.CreateUserInfoDal();
                }
                return _UserInfoDal;
            }
            set { _UserInfoDal = value; }
        }
	
		private IWF_InstanceDal _WF_InstanceDal;
        public IWF_InstanceDal WF_InstanceDal
        {
            get
            {
                if(_WF_InstanceDal == null)
                {
                    _WF_InstanceDal = DALAbstractFactory.CreateWF_InstanceDal();
                }
                return _WF_InstanceDal;
            }
            set { _WF_InstanceDal = value; }
        }
	
		private IWF_StepInfoDal _WF_StepInfoDal;
        public IWF_StepInfoDal WF_StepInfoDal
        {
            get
            {
                if(_WF_StepInfoDal == null)
                {
                    _WF_StepInfoDal = DALAbstractFactory.CreateWF_StepInfoDal();
                }
                return _WF_StepInfoDal;
            }
            set { _WF_StepInfoDal = value; }
        }
	
		private IWF_TempDal _WF_TempDal;
        public IWF_TempDal WF_TempDal
        {
            get
            {
                if(_WF_TempDal == null)
                {
                    _WF_TempDal = DALAbstractFactory.CreateWF_TempDal();
                }
                return _WF_TempDal;
            }
            set { _WF_TempDal = value; }
        }
	}	
}