using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OA.Model;

namespace OA.IDAL
{
    /// <summary>
    /// This Interface for UserInfoDal, and it is used to reuse code and standard UserInfoDal.
    /// </summary>
    public partial interface IUserInfoDal : IBaseDal<UserInfo>
    {
    }
}
