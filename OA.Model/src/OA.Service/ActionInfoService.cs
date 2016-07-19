using OA.Model;
using OA.IService;
using System.Collections.Generic;
using System.Linq;

namespace OA.Service
{
    public class ActionInfoService : BaseService<ActionInfo>, IActionInfoService
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
            var deleteList = this.DbSession.ActionInfoDal.GetList(u => list.Contains(u.Id));

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
            // get actionInfo.
            var actionInfo = this.DbSession.ActionInfoDal.GetList(a => a.Id == actionId).FirstOrDefault();

            // if actionInfo is not null.
            if (actionInfo != null)
            {
                try
                {
                    // GET all original RoleInfo with this ationInfo.
                    var originRoles = this.DbSession.RoleInfoActionInfo.GetList(ra => ra.ActionInfoId == actionInfo.Id);
                    // delete orgin role info.
                    foreach (var item in originRoles)
                    {
                        this.DbSession.RoleInfoActionInfo.Remove(item);
                    }
                    this.DbSession.SaveChanges();

                }
                catch (System.Exception)
                {

                    return false;
                }

                // loop to add new role for this action.
                foreach (int roleId in roleIds)
                {
                    // get role info.
                    var roleInfo = this.DbSession.RoleInfoDal.GetList(r => r.Id == roleId).FirstOrDefault();
                    // create new RoleInfoActionInfo.
                    RoleInfoActionInfo newRoleAction = new RoleInfoActionInfo()
                    {
                        RoleInfoId = roleInfo.Id,
                        ActionInfoId = actionInfo.Id
                    };

                    // Add new RoleInfoActionInfo into database.
                    this.DbSession.RoleInfoActionInfo.Add(newRoleAction);
                }

                // save change.
                this.DbSession.SaveChanges();

                // return.
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.ActionInfoDal;
        }
    }
}
