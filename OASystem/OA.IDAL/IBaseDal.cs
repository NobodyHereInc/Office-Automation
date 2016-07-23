using System;
using System.Linq;
using System.Linq.Expressions;

namespace OA.IDAL
{
    public interface IBaseDal<T> where T : class, new()
    {
        bool Add(T t);
        bool Edit(T t);
        bool Remove(T t);

        IQueryable<T> GetList(Expression<Func<T, bool>> whereLambda);
        IQueryable<T> GetPageList<TKey>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderLambda, int pageSize, int pageIndex, out int totalCount, bool isAsc);
    }
}
