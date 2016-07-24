using OA.Model;
using OA.IService;
using System.Collections.Generic;
using System.Linq;
using System;
using OA.Model.SearchParam;

namespace OA.Service
{
    public class UserInfoService : BaseService<UserInfo>, IUserInfoService
    {
        #region Find User Pwd
        /// <summary>
        /// This function is used to find user Password.
        /// </summary>
        /// <param name="userInfo"></param>
        public void FindUserPwd(UserInfo userInfo)
        {
        }
        #endregion

        #region Batch Remove
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool DeleteEntities(List<int> list)
        {
            // get all record that want to delete.
            var deleteList = this.DbSession.UserInfoDal.GetList(u => list.Contains(u.ID));

            // if deleteList is not null.
            if (deleteList != null)
            {
                // make deleteMark for each UserInfo.
                foreach (var UserInfo in deleteList)
                {
                    this.DbSession.UserInfoDal.Remove(UserInfo);
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
            var temp = this.DbSession.UserInfoDal.GetList(u => true);

            // if search condition UserName is set.
            if (!String.IsNullOrEmpty(filter.Uname))
            {
                temp = temp.Where<UserInfo>(U => U.UName.Contains(filter.Uname));
            }

            // if search condition UserRemark is set.
            if (!String.IsNullOrEmpty(filter.Uremark))
            {
                temp = temp.Where<UserInfo>(U => U.Remark.Contains(filter.Uremark));
            }

            // get total records after sarch condition.
            filter.TotalCount = temp.Count();

            // return resulte.
            return temp.OrderBy<UserInfo, String>(u => u.Sort).Skip<UserInfo>((filter.PageIndex - 1) * filter.PageSize).Take<UserInfo>(filter.PageSize);
        }
        #endregion

        #region Set User Role
        /// <summary>
        /// This function is used to set rols for this user (userId)
        /// </summary>
        /// <param name="userId"> user's id. </param>
        /// <param name="roleIds"> role'ids that need to set for this user.</param>
        /// <returns> true: Set Ok, false: Set Fail. </returns>
        public bool SetUserRole(int userId, List<int> roleIds)
        {
            return false;
        }
        #endregion

        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.UserInfoDal;
        }
    }
}
