using OA.Model;
using OA.IService;

namespace OA.Service
{
    public class RoleInfoService : BaseService<RoleInfo>
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.RoleInfoDal;
        }
    }
}
