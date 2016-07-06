using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace OA.DAL
{
    public class BaseDal<T>
        where T: class
    {
        protected DbContext context = ContextFactory.GetContext();

        //add
        public int Add(T userInfo)
        {
            context.Set<T>().Add(userInfo);
            return context.SaveChanges();
        }

        // modify
        public int Edit(T userInfo)
        {
            context.Entry(userInfo).State = EntityState.Modified;
            return context.SaveChanges();
        }

        // delete
        //public int Remove(int id)
        //{
        //    T userInfo = context.Set<T>().Where(u => u.T.Equals(id)) as T;
        //    context.Set<T>().Remove(userInfo);
        //    return context.SaveChanges();
        //}
        //public int Remove(int[] ids)
        //{
        //    foreach (int id in ids)
        //    {
        //        T userInfo = context.Set<T>().Where(u => u.T.Equals(id)) as T;
        //        context.Set<T>().Remove(userInfo);
        //    }

        //    return context.SaveChanges();
        //}
        public int Remove(T userInfo)
        {
            context.Set<T>().Remove(userInfo);

            return context.SaveChanges();
        }

        // seach
        //public T GetById(int id)
        //{
        //    context.Set<T>().s
        //    return context.Set<T>().Where(u => u..Equals(id)) as T;
        //}

        public IQueryable<T> GetList(Expression<Func<T, bool>> whereLambda)
        {
            return context.Set<T>().Where(whereLambda);
        }

        public IQueryable<T> GetPageList<Tkey>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, Tkey>> orderLambda, int pageIndex, int pageSize)
        {
            return context.Set<T>().Where(whereLambda)
                .OrderByDescending(orderLambda)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize);
        }
    }
}
