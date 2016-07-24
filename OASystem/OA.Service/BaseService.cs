using OA.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OA.DalFactory;
using System.Linq.Expressions;

namespace OA.Service
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseService<T> where T : class, new()
    {
        public IDbSession DbSession
        {
            get
            {
                return DBSessionFactory.CreateDbSession(); // 
            }
        }

        public IBaseDal<T> CurrentDal { get; set; } // Get Current Dal class
        public abstract void SetCurrentDal(); // abstract method.
        public BaseService()
        {
            SetCurrentDal(); // child class must be create this mathod
        }

        /// <summary>
        /// This function is used to complete Service level Add Entiry.
        /// </summary>
        /// <param name="Entity"></param>
        /// <returns></returns>
        public bool Add(T Entity)
        {
            this.CurrentDal.Add(Entity);
            return this.DbSession.SaveChanges();
        }

        /// <summary>
        /// This function is used to complete Service Edit AddEntiry.
        /// </summary>
        /// <param name="Entity"></param>
        /// <returns></returns>
        public bool Edit(T Entity)
        {
            this.CurrentDal.Edit(Entity);
            return this.DbSession.SaveChanges();
        }

        /// <summary>
        /// This function is used to complete Service Level Remove Entiry.
        /// </summary>
        /// <param name="Entity"></param>
        /// <returns></returns>
        public bool Remove(T Entity)
        {
            this.CurrentDal.Remove(Entity);
            return this.DbSession.SaveChanges();
        }


        /// <summary>
        /// This function is used to complete Service level Get total Entiry.
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public IQueryable<T> GetList(Expression<Func<T, bool>> whereLambda)
        {
            return this.CurrentDal.GetList(whereLambda);
        }

        /// <summary>
        /// This function is used to complete Service level Page of Entiry.
        /// </summary>
        /// <typeparam name="Tkey"></typeparam>
        /// <param name="whereLambda"></param>
        /// <param name="orderLambda"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public IQueryable<T> GetPageList<Tkey>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, Tkey>> orderLambda, int pageIndex, int pageSize, out int totalCount, bool isAsc)
        {
            return this.CurrentDal.GetPageList<Tkey>(whereLambda, orderLambda, pageIndex, pageSize, out totalCount, isAsc);
        }
    }
}
