using OA.IDAL;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace OA.IService
{
    public interface IBaseService<T> where T : class, new()
    {
        IDbSession DbSession { get; }
        IBaseDal<T> CurrentDal { get; set; }

        bool Add(T Entity);
        bool Edit(T Entity);
        bool Remove(T Entity);
        IQueryable<T> GetList(Expression<Func<T, bool>> whereLambda);
        IQueryable<T> GetPageList<Tkey>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, Tkey>> orderLambda, int pageIndex, int pageSize, out int totalCount, bool isAsc);
    }
}
