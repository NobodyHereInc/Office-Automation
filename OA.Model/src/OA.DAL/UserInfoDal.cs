using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OA.Model;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.CSharp;
using System.Data;
using OA.IDAL;

namespace OA.DAL
{
    public partial class UserInfoDal : BaseDal<UserInfo>, IUserInfoDal
    {
        // delete

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Remove(int id)
        {
            UserInfo userInfo = context.Set<UserInfo>().Where(u => u.UserId.Equals(id)) as UserInfo;
            context.Set<UserInfo>().Remove(userInfo);
            return context.SaveChanges();
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int Remove(int[] ids)
        {
            foreach (int id in ids)
            {
                UserInfo userInfo = context.Set<UserInfo>().Where(u => u.UserId.Equals(id)) as UserInfo;
                context.Set<UserInfo>().Remove(userInfo);
            }

            return context.SaveChanges();
        }

        //// seach


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserInfo GetById(int id)
        {
            return context.Set<UserInfo>().Where(u => u.UserId.Equals(id)) as UserInfo;
        }
    }
}
