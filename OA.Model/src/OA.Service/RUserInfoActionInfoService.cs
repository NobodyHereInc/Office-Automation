using OA.Model;
using OA.IService;

namespace OA.Service
{
    public class RUserInfoActionInfoService : BaseService<RUserInfoActionInfo>, IRUserInfoActionInfoService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.RUserInfoActionInfoDal;
        }
    }
}
