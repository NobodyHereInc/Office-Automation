using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OA.Model;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.CSharp;
using System.Data;

namespace OA.DAL
{
    public partial class UserInfoDal
    {
        DbContext context = new OAContext();

        //add
        public int Add(UserInfo userInfo)
        {
            context.Set<UserInfo>().Add(userInfo);
            return context.SaveChanges();
        }

        // modify
        public int Edit(UserInfo userInfo)
        {
            context.Entry(userInfo).State = EntityState.Modified;
            return context.SaveChanges();
        }

        // delete
        public int Remove(int id)
        {
            UserInfo userInfo = context.Set<UserInfo>().Where(u => u.UserId.Equals(id)) as UserInfo;
            context.Set<UserInfo>().Remove(userInfo);
            return context.SaveChanges();
        }
        public int Remove(int[] ids)
        {
            foreach (int id in ids)
            {
                UserInfo userInfo = context.Set<UserInfo>().Where(u => u.UserId.Equals(id)) as UserInfo;
                context.Set<UserInfo>().Remove(userInfo);
            }

            return context.SaveChanges();
        }
        public int Remove(UserInfo userInfo)
        {
            context.Set<UserInfo>().Remove(userInfo);

            return context.SaveChanges();
        }

        // seach
        public UserInfo GetById(int id)
        {
            return context.Set<UserInfo>().Where(u => u.UserId.Equals(id)) as UserInfo;
        }

        public IQueryable<UserInfo> GetList(Expression<Func<UserInfo, bool>> whereLambda)
        {
            return context.Set<UserInfo>().Where(whereLambda);
        }

        public IQueryable<UserInfo> GetPageList<Tkey>(Expression<Func<UserInfo, bool>> whereLambda, Expression<Func<UserInfo, Tkey>> orderLambda, int pageIndex, int pageSize)
        {
            return context.Set<UserInfo>().Where(whereLambda)
                .OrderByDescending(orderLambda)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize);
        }
    }
}
