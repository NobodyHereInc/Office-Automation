using OA.IService;
using OA.Model;

namespace OA.Service
{
    public class UserInfoRoleInfoService : BaseService<UserInfoRoleInfo>, IUserInfoRoleInfoService
    {


        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.UserInfoRoleInfoDal;
        }
    }
}
