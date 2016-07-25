using OA.Model;
using System.Collections.Generic;
using System.Linq;
using OA.Model.SearchParam;

namespace OA.IService
{
    public partial interface IUserInfoService : IBaseService<UserInfo>
    {
        /// <summary>
        /// batch remove user info.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        bool DeleteEntities(List<int> list);

        /// <summary>
        /// multiple search user info.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IQueryable<UserInfo> LoadSearchUserInfo(UserInfoFilter filter);

        /// <summary>
        /// find user passWord
        /// </summary>
        /// <param name="userInfo"></param>
        void FindUserPwd(UserInfo userInfo);

        /// <summary>
        /// use to set roles for user by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        bool SetUserRole(int userId, List<int> roleIds);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="actionId"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool SetUserAction(int userId, int actionId, bool value);
    }
}
