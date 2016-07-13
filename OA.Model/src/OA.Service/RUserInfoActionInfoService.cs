using OA.Model;
using OA.IService;

namespace OA.Service
{
    public class RUserInfoActionInfoService : BaseService<RUserInfoActionInfo>
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.RUserInfoActionInfoDal;
        }
    }
}
