using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OA.Model;
using System.Linq.Expressions;
using OA.IService;

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


        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.UserInforDal;
        }
    }
}
