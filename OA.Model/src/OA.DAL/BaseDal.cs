using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace OA.DAL
{
    /*
     * Class Description: 
    */
    public class BaseDal<T>
        where T: class
    {
        // instance a DbContext from context Factory.
        protected DbContext context = ContextFactory.GetContext();

        /// <summary>
        /// This Function is used to add new Entity into database.
        /// </summary>
        /// <param name="Entity"> new entity. </param>
        /// <returns> affect row. </returns>
        public bool Add(T Entity)
        {
            context.Set<T>().Add(Entity);
            return true;
        }

        /// <summary>
        /// This function is used to modify info modify data in database.
        /// </summary>
        /// <param name="Entity"> Entity need to be updated from database. </param>
        /// <returns> affect row. </returns>
        public bool Edit(T Entity)
        {
            context.Entry(Entity).State = EntityState.Modified;
            return true;
        }



        /// <summary>
        /// This function is used to delete data from database.
        /// </summary>
        /// <param name="Entity"> Entity need to be deleted from database. </param>
        /// <returns> affect row. </returns>
        public bool Remove(T Entity)
        {
            context.Entry<T>(Entity).State = EntityState.Deleted;
            //context.Set<T>().Remove(Entity);

            //return context.SaveChanges();
            return true;
        }

        /// <summary>
        /// This function is used to get total data from database.
        /// </summary>
        /// <param name="whereLambda"> where Lambda. </param>
        /// <returns>IQueryable List</returns>
        public IQueryable<T> GetList(Expression<Func<T, bool>> whereLambda)
        {
            return context.Set<T>().Where(whereLambda);
        }

        /// <summary>
        /// This function is used to make page list.
        /// </summary>
        /// <typeparam name="Tkey"></typeparam>
        /// <param name="whereLambda"> where Lambda. </param>
        /// <param name="orderLambda"> order Lambda. </param>
        /// <param name="pageIndex"> which page. </param>
        /// <param name="pageSize"> how many record in one page. </param>
        /// <param name="totalCount"> total record. </param>
        /// <param name="isAsc"> Asc or Dsc </param>
        /// <returns>IQueryable List</returns>
        public IQueryable<T> GetPageList<Tkey>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, Tkey>> orderLambda, int pageIndex, int pageSize, out int totalCount, bool isAsc)
        {
            // get total data that match the where Lambda.
            var temp = context.Set<T>().Where(whereLambda);

            // get total count of data.
            totalCount = temp.Count();

            // if orderBy Asc or Des
            if (isAsc)
            {
                temp = temp.OrderBy<T, Tkey>(orderLambda).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
            }
            else
            {
                temp = temp.OrderByDescending<T, Tkey>(orderLambda).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
            }

            // return final result.
            return temp;
        }
    }
}
