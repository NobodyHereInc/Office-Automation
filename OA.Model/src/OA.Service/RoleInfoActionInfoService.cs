using OA.Model;
using OA.IService;

namespace OA.Service
{
    public class RoleInfoActionInfoService : BaseService<RoleInfoActionInfo>, IRoleInfoActionInfoService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.RoleInfoActionInfo;
        }
    }
}
