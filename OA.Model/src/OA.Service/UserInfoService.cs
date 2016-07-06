using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OA.Model;
using System.Linq.Expressions;
using OA.IDAL;
using OA.DalFactory;

namespace OA.Service
{
    public partial class UserInfoService
    {
        // UserInfoDal dal = new UserInfoDal();
        private IUserInfoDal Idal = DalFactory1.GetUserInfo();
        //rivate IUserInfoDal Ida2 = DalFactory1.GetUserInfo2();

        // add
        public bool Add(UserInfo userInfo)
        {
            return Idal.Add(userInfo) > 0; 
        }

        // modify
        public bool Edit(UserInfo userInfo)
        {
            return Idal.Edit(userInfo) > 0; 
        }

        // delete

        // search

        public UserInfo GetById(int id)
        {
            return Idal.GetById(id);
        }

        public IQueryable<UserInfo> GetList(Expression<Func<UserInfo, bool>> whereLambda)
        {
            return Idal.GetList(whereLambda);
        }
    }
}
