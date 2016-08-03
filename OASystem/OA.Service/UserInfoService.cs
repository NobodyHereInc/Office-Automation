using OA.Model;
using OA.IService;
using System.Collections.Generic;
using System.Linq;
using System;
using OA.Model.SearchParam;
using System.Net.Mail;
using System.Text;
using System.Net;

namespace OA.Service
{
    public partial class UserInfoService : BaseService<UserInfo>, IUserInfoService
    {
        #region Find User Pwd
        /// <summary>
        /// This function is used to find user Password.
        /// </summary>
        /// <param name="userInfo"></param>
        public void FindUserPwd(UserInfo userInfo)
        {
            string newPwd = Guid.NewGuid().ToString().Substring(0, 8);
            userInfo.UPwd = newPwd;
            this.DbSession.UserInfoDal.Edit(userInfo);
            this.DbSession.SaveChanges();
            MailMessage mailMsg = new MailMessage();
            mailMsg.From = new MailAddress("menglingchen3019@gmail.com", "Linchen Meng");
            mailMsg.To.Add(new MailAddress("menglingchen3019@gmail.com", "Lingchen Meng"));
            mailMsg.Subject = "New account Information:";
            StringBuilder sb = new StringBuilder();
            sb.Append("New User account:");
            sb.Append("User Name: " + userInfo.UName);
            sb.Append("PassWord: " + newPwd);
            mailMsg.Body = sb.ToString(); 
            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.Credentials = new NetworkCredential("menglingchen3019", "mlc883019");
            client.Send(mailMsg);
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
            // get user info by id.
            var userInfo = this.DbSession.UserInfoDal.GetList(u => u.ID == userId).FirstOrDefault();

            // if user is exist.
            if (userInfo != null)
            {
                // clear original role info in this user.
                userInfo.RoleInfoes.Clear();

                // add new role info for this user.
                foreach (int roleId in roleIds)
                {
                    // get role info.
                    var roleInfo = this.DbSession.RoleInfoDal.GetList(r => r.ID == roleId).FirstOrDefault();

                    // add to this user.
                    userInfo.RoleInfoes.Add(roleInfo);//根据RoleIdList集合中存储的角色编号，获取角色信息，然后给当前用户添加.
                }
            }

            // save change.
            return this.DbSession.SaveChanges();
        }
        #endregion

        #region Set User Action
        public bool SetUserAction(int userId, int actionId, bool value)
        {
            //
            var actionInfo = this.DbSession.R_UserInfo_ActionInfoDal.GetList(r => r.UserInfoID == userId && r.ActionInfoID == actionId).FirstOrDefault();
            // if user do not have
            if (actionInfo == null)
            {
                R_UserInfo_ActionInfo r_UserInfo_ActionInfo = new R_UserInfo_ActionInfo();
                r_UserInfo_ActionInfo.IsPass = value;
                r_UserInfo_ActionInfo.ActionInfoID = actionId;
                r_UserInfo_ActionInfo.UserInfoID = userId;

                this.DbSession.R_UserInfo_ActionInfoDal.Add(r_UserInfo_ActionInfo);
            }
            else// otherwise
            {
                if (actionInfo.IsPass != value)
                {
                    actionInfo.IsPass = value;
                }
            }
            return this.DbSession.SaveChanges();
        }
        #endregion
    }
}
