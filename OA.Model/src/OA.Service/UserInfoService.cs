using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OA.Model;
using System.Linq.Expressions;
using OA.IService;
using OA.Model.SearchParams;

namespace OA.Service
{
    public class UserInfoService : BaseService<UserInfo>, IUserInfoService
    {
        #region Batch Remove
        public bool DeleteEntities(List<int> list)
        {
            // get all record that want to delete.
            var deleteList = this.DbSession.UserInforDal.GetList(u => list.Contains(u.UserId));

            // if deleteList is not null.
            if (deleteList != null)
            {
                // make deleteMark for each UserInfo.
                foreach (var UserInfo in deleteList)
                {
                    this.DbSession.UserInforDal.Remove(UserInfo);
                }
            }

            // calling DbSession.SaveChanges method. (change to update "deleteFlag")
            return this.DbSession.SaveChanges();
        }
        #endregion

        #region Batch Search
        /// <summary>
        /// This function is used to get result of searching by Filter.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IQueryable<UserInfo> LoadSearchUserInfo(UserInfoFilter filter)
        {
            // get all records from database.
            var temp = this.DbSession.UserInforDal.GetList(u => true);

            // if search condition UserName is set.
            if (!String.IsNullOrEmpty(filter.Uname))
            {
                temp = temp.Where<UserInfo>(U => U.UserName.Contains(filter.Uname));
            }

            // if search condition UserRemark is set.
            if (!String.IsNullOrEmpty(filter.Uremark))
            {
                temp = temp.Where<UserInfo>(U => U.UserName.Contains(filter.Uremark));
            }

            // get total records after sarch condition.
            filter.TotalCount = temp.Count();

            // return resulte.
            return temp.OrderBy<UserInfo, String>(u => u.Sort).Skip<UserInfo>((filter.PageIndex - 1) * filter.PageSize).Take<UserInfo>(filter.PageSize);
        }
        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.UserInforDal;
        }
    }
}
