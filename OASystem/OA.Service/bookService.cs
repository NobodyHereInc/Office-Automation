using OA.Model;
using OA.IService;
using System.Collections.Generic;

namespace OA.Service
{
    public partial class bookService : BaseService<book>, IbookService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DeleteList"></param>
        /// <returns></returns>
        public bool DeleteEntities(List<int> DeleteList)
        {
            // get all record that want to delete.
            var deleteList = this.DbSession.bookDal.GetList(u => DeleteList.Contains(u.Id));

            // if deleteList is not null.
            if (deleteList != null)
            {
                // make deleteMark for each UserInfo.
                foreach (var bookInfo in deleteList)
                {
                    this.DbSession.bookDal.Remove(bookInfo);
                }
            }

            // calling DbSession.SaveChanges method. (change to update "deleteFlag")
            return this.DbSession.SaveChanges();
        }
    }
}
