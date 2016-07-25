using OA.Model;
using OA.IService;
using System.Collections.Generic;
using System.Linq;
using System;

namespace OA.Service
{
    public partial class ActionInfoService : BaseService<ActionInfo>, IActionInfoService
    {
        
        #region Batch Remove
        /// <summary>
        /// This function is used to delete batch of actionInfo.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool DeleteEntities(List<int> list)
        {
            // get all record that want to delete.
            var deleteList = this.DbSession.ActionInfoDal.GetList(u => list.Contains(u.ID));

            // if deleteList is not null.
            if (deleteList != null)
            {
                // make deleteMark for each UserInfo.
                foreach (var actionInfo in deleteList)
                {
                    this.DbSession.ActionInfoDal.Remove(actionInfo);
                }
            }

            // calling DbSession.SaveChanges method. (change to update "deleteFlag")
            return this.DbSession.SaveChanges();
        }
        #endregion

        #region Set Action Role
        /// <summary>
        /// This function is used to set actionInfo for role.
        /// </summary>
        /// <param name="actionId"></param>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        public bool SetActionRole(int actionId, List<int> roleIds)
        {
            // get action info by id.
            var actionInfo = this.DbSession.ActionInfoDal.GetList(a => a.ID == actionId).FirstOrDefault();
            // check this action whether is exist.
            if (actionInfo != null)
            {
                // clear all role info  in this action.
                actionInfo.RoleInfoes.Clear();
                // set new role info.
                foreach (int roleId in roleIds)
                {
                    // get role info.
                    var roleInfo = this.DbSession.RoleInfoDal.GetList(r => r.ID == roleId).FirstOrDefault();
                    actionInfo.RoleInfoes.Add(roleInfo);
                }
            }
            // save change.
            return this.DbSession.SaveChanges();
        }
        #endregion
    }
}