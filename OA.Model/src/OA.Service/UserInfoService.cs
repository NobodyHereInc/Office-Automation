using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OA.DAL;
using OA.Model;
using System.Linq.Expressions;

namespace OA.Service
{
    public partial class UserInfoService
    {
        UserInfoDal dal = new UserInfoDal();

        // add
        public bool Add(UserInfo userInfo)
        {
            return dal.Add(userInfo) > 0; 
        }

        // modify
        public bool Edit(UserInfo userInfo)
        {
            return dal.Edit(userInfo) > 0; 
        }

        // delete

        // search

        public UserInfo GetById(int id)
        {
            return dal.GetById(id);
        }

        public IQueryable<UserInfo> GetList(Expression<Func<UserInfo, bool>> whereLambda)
        {
            return dal.GetList(whereLambda);
        }
    }
}
