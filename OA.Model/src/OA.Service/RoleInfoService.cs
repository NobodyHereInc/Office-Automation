using OA.Model;
using OA.IService;
using System.Collections.Generic;

namespace OA.Service
{
    public class RoleInfoService : BaseService<RoleInfo>, IRoleInfoService
    {

        #region Batch Remove
        /// <summary>
        /// This function is used to delete batch of roleInfo.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool DeleteEntities(List<int> list)
        {
            // get all record that want to delete.
            var deleteList = this.DbSession.RoleInfoDal.GetList(u => list.Contains(u.Id));

            // if deleteList is not null.
            if (deleteList != null)
            {
                // make deleteMark for each UserInfo.
                foreach (var RoleInfo in deleteList)
                {
                    this.DbSession.RoleInfoDal.Remove(RoleInfo);
                }
            }

            // calling DbSession.SaveChanges method. (change to update "deleteFlag")
            return this.DbSession.SaveChanges();
        }
        #endregion

        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.RoleInfoDal;
        }
    }
}
