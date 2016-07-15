using System;
using System.Collections.Generic;
using System.Linq;
using OA.Model;
using OA.IService;
using OA.Model.SearchParams;
using MailKit.Net.Smtp;
using MimeKit;

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

            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("walter", "walter1214@126.com"));
            message.To.Add(new MailboxAddress("EVA No1 真治", "527768601@qq.com"));
            message.Subject = "How you doin'?";
            message.Body = new TextPart("plain") { Text = @"Hey" };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.126.com");

                ////Note: only needed if the SMTP server requires authentication
                client.Authenticate("walter1214@126.com", "mlc883019");

                client.Send(message);
                client.Disconnect(true);
            }
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
            var deleteList = this.DbSession.UserInfoDal.GetList(u => list.Contains(u.Id));

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
                temp = temp.Where<UserInfo>(U => U.Uname.Contains(filter.Uname));
            }

            // if search condition UserRemark is set.
            if (!String.IsNullOrEmpty(filter.Uremark))
            {
                temp = temp.Where<UserInfo>(U => U.Uname.Contains(filter.Uremark));
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
            // get this user info by id.
            var user = this.DbSession.UserInfoDal.GetList(u => u.Id == userId).FirstOrDefault();

            // whether this user is exist.
            if (user != null)
            {
                try
                {   
                    // clear all origin roles.
                    var originRoles = this.DbSession.UserInfoRoleInfoDal.GetList(ur => ur.UserInfoId == user.Id);
                    foreach (var item in originRoles)
                    {
                        this.DbSession.UserInfoRoleInfoDal.Remove(item);
                    }

                    this.DbSession.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }


                foreach (int roleId in roleIds)
                {
                    var roleInfo = this.DbSession.RoleInfoDal.GetList(r => r.Id == roleId).FirstOrDefault();
                    UserInfoRoleInfo newRoles = new UserInfoRoleInfo
                    {
                        UserInfoId = user.Id,
                        RoleInfoId = roleId,
                    };
                    user.UserInfoRoleInfo.Add(newRoles);
                }

                this.DbSession.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion


        /// <summary>
        /// Constructor.
        /// </summary>
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.UserInfoDal;
        }


    }
}
